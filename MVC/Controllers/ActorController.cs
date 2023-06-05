using Microsoft.AspNetCore.Mvc;
using MVC.Models.Actor;
using MVC.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using MVC.Models.Actor;

namespace MVC.Controllers
{

    public class ActorController : Controller
    {
        private readonly Uri url = new Uri("http://localhost:5263");
        public async Task<ActionResult> Index(Models.Actor.IndexVM model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/actor/Get");
                string jsonString = await response.Content.ReadAsStringAsync();
                List<ActorVM> responseData = JsonConvert.DeserializeObject<List<ActorVM>>(jsonString);

                model.Pager = model.Pager ?? new PagerVM();

                model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;
                model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 12 : model.Pager.ItemsPerPage;

                model.Filter = model.Filter ?? new Models.Actor.FilterVM();

                var filteredData = responseData.Where(u =>
                    string.IsNullOrEmpty(model.Filter.First_Name) || u.First_Name.Contains(model.Filter.First_Name)).ToList();

                model.Pager.PagesCount = (int)Math.Ceiling(filteredData.Count / (double)model.Pager.ItemsPerPage);

                model.Items = filteredData
                    .OrderBy(i => i.ActorId)
                    .Skip(model.Pager.ItemsPerPage * (model.Pager.Page - 1))
                    .Take(model.Pager.ItemsPerPage)
                    .ToList();

                return View(model);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ActorVM model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var content = JsonConvert.SerializeObject(model);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var bytecontent = new ByteArrayContent(buffer);
                    bytecontent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await client.PostAsync("api/actor/Save", bytecontent);

                    string jsonString = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<ActorVM>(jsonString);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make the request
                HttpResponseMessage response = await client.GetAsync("api/actor/GetById/" + id);

                // parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ActorVM>(jsonString);
                return View(responseData);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(ActorVM model)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(model);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // make the request // Save or Update?
                    HttpResponseMessage response = await client.PostAsync("api/actor/Edit", byteContent);

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync("api/actor/Delete/" + id);

                return RedirectToAction("Index");
            }
        }
    }
}

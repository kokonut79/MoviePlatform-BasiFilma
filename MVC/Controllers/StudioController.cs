using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.Studio;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace MVC.Controllers
{
    public class StudioController : Controller
    {
        private readonly Uri url = new Uri("http://localhost:5263/api/Studio");
        public async Task<ActionResult> Index(IndexVM model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("");
                string jsonString = await response.Content.ReadAsStringAsync();
                List<StudioVM> responseData = JsonConvert.DeserializeObject<List<StudioVM>>(jsonString);

                model.Pager = model.Pager ?? new PagerVM();

                model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;
                model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 12 : model.Pager.ItemsPerPage;

                model.Filter = model.Filter ?? new FilterVM();

                var filteredData = responseData.Where(u =>
                    string.IsNullOrEmpty(model.Filter.Name) || u.Name.Contains(model.Filter.Name)).ToList();

                model.Pager.PagesCount = (int)Math.Ceiling(filteredData.Count / (double)model.Pager.ItemsPerPage);

                model.Items = filteredData
                    .OrderBy(i => i.StudioID)
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
        public async Task<ActionResult> Create(StudioVM model)
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

                    HttpResponseMessage response = await client.PostAsync("", bytecontent);

                    string jsonString = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<StudioVM>(jsonString);
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
                HttpResponseMessage response = await client.GetAsync("Studio/Edit/" + id);

                // parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<StudioVM>(jsonString);
                return View(responseData);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Edit(StudioVM model)
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
                    HttpResponseMessage response = await client.PutAsync("", byteContent);

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync("" + id);

                return RedirectToAction("Index");
            }
        }
    }
}

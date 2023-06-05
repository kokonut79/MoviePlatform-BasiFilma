using Microsoft.AspNetCore.Mvc;
using MVC.Models.Actor;
using MVC.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using MVC.Models.Movie;

namespace MVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly Uri url = new Uri("http://localhost:5263");
        public async Task<ActionResult> Index(Models.Movie.IndexVM model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/movie/Get");
                string jsonString = await response.Content.ReadAsStringAsync();
                List<MovieVM> responseData = JsonConvert.DeserializeObject<List<MovieVM>>(jsonString);

                model.Pager = model.Pager ?? new PagerVM();

                model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;
                model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 12 : model.Pager.ItemsPerPage;

                model.Filter = model.Filter ?? new Models.Movie.FilterVM();

                var filteredData = responseData.Where(u =>
                    string.IsNullOrEmpty(model.Filter.Title) || u.Title.Contains(model.Filter.Title)).ToList();

                model.Pager.PagesCount = (int)Math.Ceiling(filteredData.Count / (double)model.Pager.ItemsPerPage);

                model.Items = filteredData
                    .OrderBy(i => i.MovieId)
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
        public async Task<ActionResult> Create(MovieVM model)
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

                    HttpResponseMessage response = await client.PostAsync("api/movie/Save", bytecontent);

                    string jsonString = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<MovieVM>(jsonString);
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
                HttpResponseMessage response = await client.GetAsync("api/movie/GetById/" + id);

                // parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<MovieVM>(jsonString);
                return View(responseData);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(MovieVM model)
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
                    HttpResponseMessage response = await client.PostAsync("api/movie/Edit", byteContent);

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

                HttpResponseMessage response = await client.DeleteAsync("api/movie/Delete/" + id);

                return RedirectToAction("Index");
            }
        }
    }
}

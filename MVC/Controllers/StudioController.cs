using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.Studio;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MVC.Controllers
{
    public class StudioController : Controller
    {
        private readonly Uri url = new Uri("https://localhost:44362/api/studio");
        public async Task<ActionResult> Index(IndexVM model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("");
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<StudioVM>>(jsonString);

                model.Pager = model.Pager ?? new PagerVM();

                model.Pager.Page = model.Pager.Page <= 0
                    ? 1
                    : model.Pager.Page;

                model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                    ? 12
                    : model.Pager.ItemsPerPage;


                model.Filter = model.Filter ?? new FilterVM();


                model.Pager.PagesCount = (int)Math.Ceiling(responseData.Where(u =>
                    string.IsNullOrEmpty(model.Filter.Name) || u.Name.Contains(model.Filter.Name)).Count() / (double)model.Pager.ItemsPerPage);


                model.Items = responseData
                    .OrderBy(i => i.StudioID)
                    .Where(u =>
                        string.IsNullOrEmpty(model.Filter.Name) || u.Name.Contains(model.Filter.Name))
                    .Skip(model.Pager.ItemsPerPage * (model.Pager.Page - 1))
                    .Take(model.Pager.ItemsPerPage)
                    .ToList();
                return View(model);
            }
        }
    }
}

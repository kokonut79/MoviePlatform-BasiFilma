using AppService.DTOs;
using AppService.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Messages;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MovieController : Controller
    {
        private readonly MovieManagementService _movieManagementService;

        public MovieController()
        {
            _movieManagementService = new MovieManagementService();
        }

        [HttpGet]
        [Route("api/movie")]
        public IActionResult Get()
        {
            return Json(_movieManagementService.Get());
        }

        [HttpGet]
        [Route("api/movie/{id}")]
        public IActionResult Get(int id)
        {
            return Json(_movieManagementService.GetById(id));
        }

        [HttpPost]
        [Route("api/movie/Save")]
        public IActionResult Save([FromBody] MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResponseMessage response = new ResponseMessage();

            if (_movieManagementService.Save(movieDTO))
            {
                response.Code = 200;
                response.Body = "Movie is saved.";
            }
            else
            {
                response.Code = 500;
                response.Error = "Movie was not saved.";
            }

            return Json(response);
        }

        [HttpPut]
        [Route("api/movie/Edit")]
        public IActionResult Edit([FromBody] MovieDTO movieDTO)
        {
            ResponseMessage response = new ResponseMessage();


            if (!ModelState.IsValid)
            {
                return Json(new ResponseMessage
                {
                    Code = 500,
                    Error = "Data is not valid !"
                });
            }

            if (_movieManagementService.Edit(movieDTO))
            {
                response.Code = 200;
                response.Body = "Movie was edited.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Movie was not edited.";
            }

            return Json(response);
        }

        [HttpDelete]
        [Route("api/movie/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            ResponseMessage response = new ResponseMessage();

            if (_movieManagementService.Delete(id))
            {
                response.Code = 200;
                response.Body = "Movie is deleted.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Movie is not deleted.";
            }

            return Json(response);
        }
    }
}

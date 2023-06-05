using AppService.DTOs;
using AppService.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Messages;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly MovieManagementService _movieManagementService;

        public MovieController()
        {
            _movieManagementService = new MovieManagementService();
        }

        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok(_movieManagementService.Get());
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [AllowAnonymous]
        public IActionResult GetById(int id)
        {
            return Ok(_movieManagementService.GetById(id));
        }

        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
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

            return Ok(response);
        }

        [HttpPost]
        [Route("Edit")]
        [AllowAnonymous]
        public IActionResult Edit([FromBody] MovieDTO movieDTO)
        {
            ResponseMessage response = new ResponseMessage();


            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
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

            return Ok(response);
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        [AllowAnonymous]
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

            return Ok(response);
        }
    }
}

using AppService.DTOs;
using AppService.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Messages;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/")]
    public class StudioController : ControllerBase
    {
        private readonly StudioManagementService _studioManagementService;
        public StudioController()
        {
            _studioManagementService = new StudioManagementService();
        }

        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok(_studioManagementService.Get());
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [AllowAnonymous]
        public IActionResult GetById(int id)
        {
            Console.WriteLine(id);
            if (id >=1)
            {
                return Ok(_studioManagementService.GetById(id));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public IActionResult Save([FromBody] StudioDTO studioDTO)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            ResponseMessage response = new ResponseMessage();
            if (_studioManagementService.Save(studioDTO))
            {
                response.Code = 200;
                response.Body = "Studio is saved.";
            }
            else
            {
                response.Code = 500;
                response.Error = "Studio was not saved.";
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("Edit")]
        [AllowAnonymous]
        public IActionResult Edit([FromBody] StudioDTO studioDTO)
        {
            Console.WriteLine("basimamata");
            Console.WriteLine(studioDTO.StudioID);
            ResponseMessage response = new ResponseMessage();
            
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                if (_studioManagementService.Edit(studioDTO))
                {
                    response.Code = 200;
                    response.Body = "Studio was edited.";
                }
                else
                {
                    response.Code = 500;
                    response.Body = "Studio was not edited.";
                }
            return Ok(response);
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        [AllowAnonymous]
        public IActionResult Delete(int id)
        { 
            ResponseMessage response = new ResponseMessage();

            if (_studioManagementService.Delete(id))
            {
                response.Code = 200;
                response.Body = "Studio is deleted.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Studio is not deleted.";
            }

            return Ok(response);
        }
    }
}

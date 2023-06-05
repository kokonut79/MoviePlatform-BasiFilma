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
    [Authorize]
    public class ActorController : ControllerBase
    {
        private readonly ActorManagementService _actorManagementService;

        public ActorController()
        {
            _actorManagementService = new ActorManagementService();
        }

        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public IActionResult Get()
        {

            return Ok(_actorManagementService.Get());
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [AllowAnonymous]
        public IActionResult GetById(int id)
        {
            return Ok(_actorManagementService.GetById(id));
        }

        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public IActionResult Save(ActorDTO actorDTO)
        {
            if (!actorDTO.Validate())
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            ResponseMessage response = new ResponseMessage();
            if (_actorManagementService.Save(actorDTO))
            {
                response.Code = 200;
                response.Body = "Actor is saved.";
            }
            else
            {
                response.Code = 500;
                response.Error = "Actor was not saved.";
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("Edit")]
        [AllowAnonymous]
        public IActionResult Edit([FromBody] ActorDTO actorDTO)
        {
            ResponseMessage response = new ResponseMessage();


            if (!actorDTO.Validate())
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (_actorManagementService.Edit(actorDTO))
            {
                response.Code = 200;
                response.Body = "Actor was edited.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Actor was not edited.";
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        [AllowAnonymous]
        public IActionResult Delete(int id)
        {
            ResponseMessage response = new ResponseMessage();

            if (_actorManagementService.Delete(id))
            {
                response.Code = 200;
                response.Body = "Actor is deleted.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Actor is not deleted.";
            }

            return Ok(response);
        }
    }
}

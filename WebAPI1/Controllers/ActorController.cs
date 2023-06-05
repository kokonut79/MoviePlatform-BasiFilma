using AppService.DTOs;
using AppService.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Messages;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : Controller
    {
        private readonly ActorManagementService _actorManagementService;

        public ActorController()
        {
            _actorManagementService = new ActorManagementService();
        }

        [HttpGet]
        [Route("api/actor")]
        public IActionResult Get()
        {

            return Json(_actorManagementService.Get());
        }

        [HttpGet]
        [Route("api/actor/{id}")]
        public IActionResult GetById(int id)
        {
            return Json(_actorManagementService.GetById(id));
        }

        [HttpPost]
        [Route("api/actor/Save")]
        public IActionResult Save(ActorDTO actorDTO)
        {
            if (!actorDTO.Validate())
            {
                return Json(new ResponseMessage
                {
                    Code = 500,
                    Error = "Data is not valid !  "
                });
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

            return Json(response);
        }

        [HttpPut]
        [Route("api/actor/Edit")]
        public IActionResult Edit([FromBody] ActorDTO actorDTO)
        {
            ResponseMessage response = new ResponseMessage();


            if (!actorDTO.Validate())
            {
                return Json(new ResponseMessage
                {
                    Code = 500,
                    Error = "Data is not valid !"
                });
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

            return Json(response);
        }

        [HttpDelete]
        [Route("api/actor/{id}")]
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

            return Json(response);
        }
    }
}

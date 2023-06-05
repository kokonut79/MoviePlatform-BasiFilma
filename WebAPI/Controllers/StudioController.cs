using AppService.DTOs;
using AppService.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Messages;

namespace WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudioController : Controller
    {
        private readonly StudioManagementService _studioManagementService;
        public StudioController()
        {
            _studioManagementService = new StudioManagementService();
        }

        [HttpGet]
        [Route("api/studio/getAll")]
        public IActionResult Get()
        {
            return Json(_studioManagementService.Get());
        }

        [HttpGet]
        [Route("api/studio/{id}")]
        public IActionResult GetById(int id)
        {
            return Json(_studioManagementService.GetById(id));
        }

        [HttpPost]
        [Route("api/studio/Save")]
        public IActionResult Save([FromBody] StudioDTO studioDTO)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResponseMessage
                {
                    Code = 500,
                    Error = "Data is not valid !  "
                });
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

            return Json(response);
        }

        [HttpPut]
        [Route("api/studio/Edit/{id}")]
        public IActionResult Edit(int id, [FromBody] StudioDTO studioDTO)
        {
            ResponseMessage response = new ResponseMessage();
            if (studioDTO.StudioID == id)
            {
                if (!ModelState.IsValid)
                {
                    return Json(new ResponseMessage
                    {
                        Code = 500,
                        Error = "Data is not valid !"
                    });
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
            }
            return Json(response);
        }

        [HttpDelete]
        [Route("api/studio/{id}")]
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

            return Json(response);
        }
    }
}

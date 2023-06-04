using AppService.DTOs;
using AppService.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    public class ActorController : Controller
    {
        private readonly ActorManagementService service;
        public ActorController(ActorManagementService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("api/actor")]
        public ActionResult GetAll()
        {
            var actors = service.Get();

            return Ok(actors);

        }

        [HttpGet]
        [Route("api/actor/{id}")]
        public  ActionResult GetById(int id)
        {
            var actors = service.GetById(id);

            if (actors == null)
            {
                return NotFound();
            }

            return Ok(actors);
        }

        [HttpPost]
        [Route("api/actor/create")]
        public ActionResult Create([FromBody] ActorDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created =  service.Save(model);

            if (!created)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpGet]
        [Route("api/actor/edit/{id}")]

        public ActionResult GetEditDetails([FromRoute] int id , ActorDTO actorDTO)
        {
            var actor =  this.service.Edit(actorDTO);

            if (actor == null)
            {
                return NotFound();
            }

            return Ok(actor);
        }


    }
}

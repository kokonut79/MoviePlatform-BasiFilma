using AppService.DTOs;
using Data.Entities;
using Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Implementation
{
    public class ActorManagementService
    {
        public List<ActorDTO> Get()
        {
            List<ActorDTO> actorDTOs = new List<ActorDTO>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.ActorsRepository.Get())
                {
                    actorDTOs.Add(new ActorDTO
                    {
                        ActorId = item.Id,
                        First_Name = item.First_Name,
                        Last_Name = item.Last_Name,
                        Email = item.Email,
                        MovieId = item.MovieId
                    });
                }
            }

            return actorDTOs;
        }
        public ActorDTO GetById(int id)
        {
            ActorDTO actorDTO = new ActorDTO();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Actor actor = unitOfWork.ActorsRepository.GetByID(id);
                if (actor != null)
                {
                    actorDTO = new ActorDTO()
                    {
                        ActorId = actor.Id,
                        First_Name = actor.First_Name,
                        Last_Name = actor.Last_Name,
                        Email = actor.Email,
                        MovieId = actor.MovieId
                    };
                }
            }

            return actorDTO;
        }

        public bool Save(ActorDTO actorDTO)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (actorDTO == null)
                    {
                        return false;
                    }
                    var actor = new Actor
                    {
                        First_Name = actorDTO.First_Name,
                        Last_Name = actorDTO.Last_Name,
                        Email = actorDTO.Email,
                        MovieId = actorDTO.MovieId
                    };
                    unitOfWork.ActorsRepository.Insert(actor);
                    unitOfWork.Save();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(ActorDTO actorDTO)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Actor actor = unitOfWork.ActorsRepository.GetByID(actorDTO.ActorId);
                    if (actor != null)
                    {
                        actor.First_Name = actorDTO.First_Name;
                        actor.Last_Name = actorDTO.Last_Name;
                        actor.Email = actorDTO.Email;
                        actor.MovieId = actorDTO.MovieId;

                        unitOfWork.ActorsRepository.Update(actor);
                        unitOfWork.Save();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Actor actor = unitOfWork.ActorsRepository.GetByID(id);
                    unitOfWork.ActorsRepository.Delete(actor);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

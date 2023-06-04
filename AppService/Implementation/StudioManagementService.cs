﻿using AppService.DTOs;
using Data.Entities;
using Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Implementation
{
    public class StudioManagementService
    {
        public List<StudioDTO> Get()
        {
            List<StudioDTO> studioDTOs = new List<StudioDTO>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.StudioRepository.Get())
                {
                    studioDTOs.Add(new StudioDTO
                    {
                        StudioID = item.Id,
                        Name = item.Name,
                        Description = item.Description,

                    });
                }
            }
            return studioDTOs;
        }
        public StudioDTO GetById(int id)
        {
            StudioDTO studioDTO = new StudioDTO();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Studio studio = unitOfWork.StudioRepository.GetByID(id);
                List<Movie> movies = unitOfWork.MovieRepository.Get().ToList();
                List<Movie> createdMovies = new List<Movie>();
                if (studio != null)
                {
                    foreach (var item in movies)
                    {
                        if (item.StudioId == studio.Id)
                        {
                            createdMovies.Add(item);
                        }


                    }
                    studioDTO = new StudioDTO()
                    {
                        StudioID = studio.Id,
                        Name = studio.Name,
                        Description = studio.Description,
                        Movies = createdMovies
                    };
                }
            }

            return studioDTO;
        }
        public bool Edit(StudioDTO studioDTO)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Studio studio = unitOfWork.StudioRepository.GetByID(studioDTO.StudioID);
                    if (studio != null)
                    {
                        studio.Name = studioDTO.Name;
                        studio.Description = studioDTO.Description;


                        unitOfWork.StudioRepository.Update(studio);
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
        public bool Save(StudioDTO studioDTO)
        {
            Studio studio = new Studio()
            {
                Name = studioDTO.Name,
                Description = studioDTO.Description
            };

            try
            {

                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    unitOfWork.StudioRepository.Insert(studio);
                    unitOfWork.Save();
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
                    Studio studio = unitOfWork.StudioRepository.GetByID(id);
                    unitOfWork.StudioRepository.Delete(studio);
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

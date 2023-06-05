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
    public class MovieManagementService
    {
        public List<MovieDTO> Get()
        {
            List<MovieDTO> movieDTOs = new List<MovieDTO>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.MovieRepository.Get())
                {
                    movieDTOs.Add(new MovieDTO
                    {
                        MovieId = item.Id,
                        Title = item.Title,
                        Description = item.Description,
                        Budget = item.Budget,
                        Genre = item.Genre,
                        StudioId = item.StudioId,

                    });
                }
            }

            return movieDTOs;
        }
        public MovieDTO GetById(int id)
        {
            MovieDTO movieDTO = new MovieDTO();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Movie movie = unitOfWork.MovieRepository.GetByID(id);
                if (movie != null)
                {
                 
                    movieDTO = new MovieDTO()
                    {
                        MovieId = movie.Id,
                        Title = movie.Title,
                        Description = movie.Description,
                        Budget = movie.Budget,
                        StudioId = movie.StudioId,

                    };
                }
            }

            return movieDTO;
        }

        public bool Save(MovieDTO movieDTO)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (movieDTO == null)
                    {
                        return false;
                    }
                    var movie = new Movie
                    {
                        Title = movieDTO.Title,
                        Description = movieDTO.Description,
                        Budget = movieDTO.Budget,
                        StudioId = movieDTO.StudioId
                    };
                    unitOfWork.MovieRepository.Insert(movie);
                    unitOfWork.Save();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(MovieDTO movieDTO)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Movie movie = unitOfWork.MovieRepository.GetByID(movieDTO.MovieId);
                    if (movie != null)
                    {
                        movie.Title = movieDTO.Title;
                        movie.Description = movieDTO.Description;
                        movie.Budget = movieDTO.Budget;
                        movie.StudioId = movieDTO.StudioId;

                        unitOfWork.MovieRepository.Update(movie);
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
                    Movie movie = unitOfWork.MovieRepository.GetByID(id);
                    unitOfWork.MovieRepository.Delete(movie);
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

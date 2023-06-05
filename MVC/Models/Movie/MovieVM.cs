using AppService.DTOs;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Movie
{
    public class MovieVM
    {
        [Key]
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Budget { get; set; }
        public string Genre { get; set; }
        public int? StudioId { get; set; }

        public MovieVM() { }

        public MovieVM(MovieDTO movieDTO)
        {
            MovieId = movieDTO.MovieId;
            Title = movieDTO.Title;
            Description = movieDTO.Description;
            Budget = movieDTO.Budget;
            Genre = movieDTO.Genre;
            StudioId = movieDTO.StudioId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Movie : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        public DateTime TimeOfRelease { get; set; }
        public decimal Budget { get; set; }
        public string Genre { get; set; }
        public int ActorId { get; set; }
        public ICollection<Actor> Actors { get; set; }
        [InverseProperty("Movies")]
        public int StudioId { get; set; }
        public Studio Studio { get; set; }


    }
}

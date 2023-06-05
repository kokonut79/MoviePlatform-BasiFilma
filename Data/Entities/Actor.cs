using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Actor : BaseEntity
    {
        [Required]
        [StringLength(25)]
        public string First_Name { get; set; }
        public string  Last_Name {get; set; }
        public int Age { get; set; }
        [DataType(DataType.Currency)] 
        public decimal Salary { get; set; }
        public DateTime DOB  { get; set; }
        public string Email { get; set; }
        public int? MovieId { get; set; }
        public Movie? Movie { get; set; }


    }
}

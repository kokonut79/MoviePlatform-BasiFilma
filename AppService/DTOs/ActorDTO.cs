using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AppService.DTOs
{
    public class ActorDTO
    {
        public int ActorId { get; set; }
        public string First_Name { get; set; }
        public string Last_Name  { get; set; }
        public string Email { get; set; }
        public int ? MovieId { get; set; }

        public bool Validate()
        {
            return !String.IsNullOrEmpty(First_Name);
        }
    }
}

﻿using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.DTOs
{
    public class StudioDTO
    {
        public int StudioID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubbish.Models
{
    public class Vacation
    {
        [Key]
        public int ID { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
    }
}

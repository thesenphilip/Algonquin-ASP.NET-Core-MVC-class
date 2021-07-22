﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6.Models
{
    public class StudentBase
    {
        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string FirstName
        {
            get; set;

        }

        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string LastName
        {
            get; set;
        }

        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string Program
        {
            get; set;
        }

    }
}

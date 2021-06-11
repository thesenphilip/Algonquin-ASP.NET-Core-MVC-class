using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lab3.Models
{
    public class Person
    {


       [Required]
       [StringLength(100)]
        public String FirstName
        {
            get;
            set;
        }


        [Required]
        [StringLength(100)]
        public string LastName
        {
            get;
            set;
        }
        public string Age
        {
            get;
            set;
        }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string EmailAddress
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Date)]
        public string DateofBirth
        {
            get;
            set;
        }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        }

        [Required]
        [StringLength(100)]
        public string Description
        {
            get;
            set;
        }


    }
}

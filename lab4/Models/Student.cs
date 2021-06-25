using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lab4.Models
{
    public class Student
    {

        public int Id
        {
            get;
            set;
        }

        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string LastName
        {
            get;
            set;
        }

        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string FirstName
        {
            get;
            set;
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate
        {
            get;
            set;
        }

        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
            
        }

        public List<CommunityMembership> Memberships 
        {
            get;
            set; 
        }
    }
}

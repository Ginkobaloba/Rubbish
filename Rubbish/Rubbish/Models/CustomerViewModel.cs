using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rubbish.Models
{

        public class RegisterCustomerViewModel
        {
            [Required]
            [StringLength(30)]
            [Display(Name = "Street Address")]
            public string StreetNumber { get; set; }


            [Required]
            [StringLength(30)]
            [Display(Name = "Street Name")]
            public string StreetName { get; set; }

            [Required]
            [StringLength(30)]
            [Display(Name = "City")]
            public string City { get; set; }

            [Required]
            [StringLength(30)]
            [Display(Name = "State")]
            public string State { get; set; }

            [Required]
            [StringLength(30)]
            [Display(Name = "Zip")]
            public string ZipCode { get; set; }

        [Required]

        [Display(Name = "Day of Week")]
        public string DayOfWeek { get; set; }

    }
    }

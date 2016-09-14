using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rubbish.Models
{
    public class EmployeeIndexViewModel 
    {
        

            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "RouteNumber")]
            public string RouteNumber { get; set; }

            [Display(Name = "ID")]
            public string ID { get; set; }

        
    }
    }

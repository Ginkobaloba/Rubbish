using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubbish.Models
{
    public class Address
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Street Number")]
        public string StreetNumber { get; set; }

        [Display(Name = "Street Name")]
        public string StreetName { get; set; }

        
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Display(Name = "Route Number")]
        public int RouteNumber { get; set; }

        public string City { get; set; }

        public float? Lat { get; set; }

        public float? Lng { get; set; }
    }
}

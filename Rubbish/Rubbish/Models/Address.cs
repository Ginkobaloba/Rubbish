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

        public string StreetNumber { get; set; }

        public string StreetName { get; set; }
        
        public string State { get; set; }

        public string ZipCode { get; set; }


        public string City { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubbish.Models
{
    public class PickupSite
    {

        [Key]

        public int ID { get; set; }

        public int DayOfWeekID { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        public Customer Customer { get; set; }

        public int RouteNumber { get; set; }

        [ForeignKey("Address")]
        public int AddressID { get; set; }

        public Address Address { get; set;}


    }
}

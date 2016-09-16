using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubbish.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public decimal MoneyOwed { get; set; }

        public int? DayOfWeek { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("Address")]
        public int? AddressID { get; set; }

        public Address Address { get; set; }

        [ForeignKey("Vacation")]
        public int? VacationID { get; set; }

        public Vacation Vacation { get; set; }





    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubbish.Models
{
    public class Customer_Vacation
    {

            [Key]
            public int ID { get; set; }

            [ForeignKey("Vacation")]
            public int VacationID { get; set; }

            public Vacation Vacation { get; set; }

            [ForeignKey("Customer")]
            public int CustomerID { get; set; }

            public Customer Customer { get; set; }

        }
    }


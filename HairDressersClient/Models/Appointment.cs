using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairDressersClient.Models
{
   public class Appointment 
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}

using HairDressersClient.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairDressersClient.Models
{
   public  class Customer
    {
        public int Id { get; set; }

        public WorkType? Type { get; set; }
        public string MasterSurname { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalSupport.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }

        int SexId { get; set; }
        Sex Sex { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }
        public string Phome { get; set; }
        public string Email { get; set; }
        
    }
}

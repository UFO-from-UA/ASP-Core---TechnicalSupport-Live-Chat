using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalSupport.Models
{
    public class User
    {
        public int Id { get; set; }
        public Guid Guid { get; set; } //current connection identifier

        public Sex Sex { get; set; }
        public int SexId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string CurrentIp { get; set; }

    }
}

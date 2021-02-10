using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalSupport.Models
{
    public class Client
    {
        public int Id { get; set; }
        public Guid Guid { get; set; } //current connection identifier

        public User User;
        public int UserId;

        public Sex Sex { get; set; }
        public int ClientSexId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        public string CurrentIp { get; set; }

    }
}

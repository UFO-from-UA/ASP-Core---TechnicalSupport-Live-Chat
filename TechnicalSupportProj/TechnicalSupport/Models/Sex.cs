using System;
using System.Collections.Generic;

#nullable disable

namespace TechnicalSupport.Models
{
    public partial class Sex
    {
        public Sex()
        {
            Employees = new HashSet<Employee>();
            Users = new HashSet<User>();
        }

        public int SexId { get; set; }
        public string Sex1 { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}

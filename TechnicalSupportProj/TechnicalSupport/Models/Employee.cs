﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TechnicalSupport.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Tasks = new HashSet<Task>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondName { get; set; }
        public int? Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Sex { get; set; }
        public int WorkTime { get; set; }
        [Display(Name = "Password")]
        public byte[] PasswordHash { get; set; }
        public Guid EmployeeGuid { get; set; }

        public virtual Sex SexNavigation { get; set; }
        public virtual WorkTime WorkTimeNavigation { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}

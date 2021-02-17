﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TechnicalSupport.Models
{
    public partial class WorkTime
    {
        public WorkTime()
        {
            Employees = new HashSet<Employee>();
        }

        public int WorkTimeId { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}

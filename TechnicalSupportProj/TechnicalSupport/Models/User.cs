﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalSupport.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int RoleName { get; set; }
        // 0 user
        // 1 client
        // 2 admin =
        public int RoleId { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
    }
}

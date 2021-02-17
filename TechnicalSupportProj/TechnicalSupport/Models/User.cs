﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TechnicalSupport.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public Guid UserGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondName { get; set; }
        public int? Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserIp { get; set; }
        public int? Sex { get; set; }
        public byte[] PasswordHash { get; set; }

        public virtual Sex SexNavigation { get; set; }
    }
}

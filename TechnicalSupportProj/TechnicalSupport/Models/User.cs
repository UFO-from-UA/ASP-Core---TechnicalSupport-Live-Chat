using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalSupport.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        /// <summary>
        /// Types :
        /// 0 : undefined user
        /// 1 : client
        /// 2 : employee
        /// 3 : admin
        /// </summary>
        public int RoleId { get; set; }


        public ICollection<Employee> Employees { get; set; }
        public ICollection <Client> Clients { get; set; }


        public string GetRoleName()
        {
            switch (RoleId)
            {
                case 1:
                    return "client";
                case 2:
                    return "employee";
                case 3:
                    return "admin";
                default:
                    return "undefined";
            }
        }
    }
}

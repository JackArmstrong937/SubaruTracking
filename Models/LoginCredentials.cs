using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubaruEfficiencyTracking.Models
{
    public class LoginCredentials
    {
        public Guid LoginGuid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        
    }
}

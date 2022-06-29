using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubaruEfficiencyTracking.Models
{
    public class TechModel
    {
        public Guid TechGuid { get; set; }
        public Guid StoreGuid { get; set; }
        public string TechFirstName { get; set; }
        public string TechLastName { get; set; }
        public string TechNumber { get; set; }
        public bool TechActive { get; set; }
       
    }

}

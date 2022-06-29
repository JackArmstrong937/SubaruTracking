using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubaruEfficiencyTracking.Models
{
    public class StoreLocationModel
    { 
        public Guid StoreGuid { get; set; }
        
        public string StoreName { get; set; }

        public bool StoreActive { get; set; }


    }
}

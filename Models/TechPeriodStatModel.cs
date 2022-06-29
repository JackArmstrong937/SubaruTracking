using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubaruEfficiencyTracking.Models
{
    public class TechPeriodStatModel
    {
        public Guid StoreGuid { get; set; }
        public Guid TechGuid { get; set; }
        public string TechFirstName { get; set; }
        public string TechLastName { get; set; }
        public int CarsCompleted { get; set; }
        public float HoursTurned { get; set; }
        public float ExpectedHours { get; set; }
        public float OverallEfficiency { get; set; }
    }
}

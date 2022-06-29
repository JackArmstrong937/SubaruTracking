using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubaruEfficiencyTracking.Models
{
    public class TimeModel
    {
        public Guid TimeGuid { get; set; }
        public Guid TechGuid { get; set; }
        public Guid PayPeriodGuid { get; set; }
        public Guid StoreGuid { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public float ExptHours { get; set; }

        public float TotalTime
        {
            get
            {
                return (float)((CloseTime - OpenTime).TotalHours);
            }
            set { }
        }

        public string overallEfficiency
        {
            get
            {
                return Math.Abs(((ExptHours / TotalTime)*100)).ToString() + "%";
            }
            set { }
        }
    }
}

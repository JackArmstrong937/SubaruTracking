using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubaruEfficiencyTracking.Models
{
    //for individual stats
    public class TimeModel
    {
        public Guid TimeGuid { get; set; }
        public Guid TechGuid { get; set; }
        public Guid PayPeriodGuid { get; set; }
        public Guid StoreGuid { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public float ExptHours { get; set; }
        public float totalEfficiency { get; set; }
        
        
        public float TotalTime
        {
            get
            {
                return (float)((CloseTime - OpenTime).TotalHours);
            }
            set { }
        }

        public float overallEfficiency
        {
            get
            {
                if (ExptHours <= 0 || TotalTime <= 0) return 0;
                return (ExptHours / TotalTime)*100;
            }
            set { }
        }

        public string RoNumber { get; set; }

        public DateTime RoClockOpenTime { get; set; }
        public DateTime RoClockCloseTime { get; set; }

        public float RoClockTotalTime
        {
            get
            {
                return (float)((RoClockCloseTime - RoClockOpenTime).TotalHours);
            }
            set { }
        }

        public float RoClockoverallEfficiency
        {
            get
            {
                if (ExptHours <= 0 || RoClockTotalTime <= 0) return 0;
                return (ExptHours / RoClockTotalTime) * 100;
            }
            set { }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubaruEfficiencyTracking.Models;

namespace SubaruEfficiencyTracking.Logic
{
    public interface ITechStatService
    {
        public List<TechPeriodStatModel> CalcStats(List<TimeModel> entries, List<TechModel> techs);
    }
}

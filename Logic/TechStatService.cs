using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubaruEfficiencyTracking.Models;

namespace SubaruEfficiencyTracking.Logic
{
    public class TechStatService : ITechStatService
    {
        public List<TechPeriodStatModel> CalcStats(List<TimeModel> entries, List<TechModel> techs)
        {
            List<TechPeriodStatModel> Output = new List<TechPeriodStatModel>();

            //Get the list of distinct Tech GUIDs
            List<Guid> distinctTechs = entries.Select(t => t.TechGuid).Distinct().ToList();
            foreach(Guid techGuid in distinctTechs)
            {
                TechPeriodStatModel temp = new TechPeriodStatModel();
                temp.TechGuid = techGuid;
                temp.TechFirstName = techs.Find(m => m.TechGuid == techGuid).TechFirstName;
                temp.TechLastName = techs.Find(m => m.TechGuid == techGuid).TechLastName;
                temp.CarsCompleted = entries.FindAll(m => m.TechGuid == techGuid && m.ExptHours != 0).Count;
                temp.HoursTurned = entries.FindAll(m => m.TechGuid == techGuid).Select(h => h.TotalTime).Sum();
                temp.ExpectedHours = entries.FindAll(m => m.TechGuid == techGuid).Select(h => h.ExptHours).Sum();
                temp.OverallEfficiency = (temp.ExpectedHours / temp.HoursTurned);
                temp.ROCarsCompleted = entries.FindAll(m => m.TechGuid == techGuid && m.RoClockTotalTime > 0 && m.ExptHours > 0).Count;
                temp.ROHoursTurned = entries.FindAll(m => m.TechGuid == techGuid && m.RoClockTotalTime > 0 && m.ExptHours > 0).Select(h => h.RoClockTotalTime).Sum();
                temp.ROExpectedHours = entries.FindAll(m => m.TechGuid == techGuid && m.RoClockTotalTime > 0 && m.ExptHours > 0).Select(h => h.ExptHours).Sum();
                temp.ROOverrallEfficiency = (temp.ROExpectedHours / temp.ROHoursTurned);
                Output.Add(temp);
            }

            return Output;
        }
    }
}

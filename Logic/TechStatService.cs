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
                temp.CarsCompleted = entries.FindAll(m => m.TechGuid == techGuid).Count;
                temp.HoursTurned = entries.FindAll(m => m.TechGuid == techGuid).Select(h => h.TotalTime).Sum();
                temp.ExpectedHours = entries.FindAll(m => m.TechGuid == techGuid).Select(h => h.ExptHours).Sum();
                temp.OverallEfficiency = Math.Abs(temp.ExpectedHours / temp.HoursTurned);
                Output.Add(temp);
            }

            return Output;
        }
    }
}

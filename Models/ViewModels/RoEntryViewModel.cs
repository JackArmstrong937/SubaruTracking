using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubaruEfficiencyTracking.Models
{
    public class RoEntryViewModel
    {
        public List<TechModel> Techs;
        public Guid? SelectedTechGuid;

        public List<PayPeriodModel> PayPeriods;
        public Guid? SelectedPayPeriodGuid;

        public List<StoreLocationModel> Stores;
        public Guid? SelectedStoreGuid;

        public List<TechPeriodStatModel> TechStats;
        public List<TimeModel> TechTimeDetails;

        public bool RoEntry;
    }
}

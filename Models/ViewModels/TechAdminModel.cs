using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubaruEfficiencyTracking.Models
{
    public class TechAdminModel
    {
        public List<TechModel> Techs;

        public List<StoreLocationModel> Stores;
        public Guid? SelectedStoreGuid;

        public TechModel EditingTech { get; set; }

        public bool AddedTech;
        public bool UpdatedTech;
    }
}

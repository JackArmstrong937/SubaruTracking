using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubaruEfficiencyTracking.Models;
using SubaruEfficiencyTracking.Logic;

namespace SubaruEfficiencyTracking.Controllers
{
    [Route("Tracking")]
    public class TrackingController : Controller
    {
        private IDBConnector _DB;
        private ITechStatService _TechStatService;

        public TrackingController(IDBConnector db, ITechStatService techStat)
        {
            _DB = db;
            _TechStatService = techStat;
        }

        [Route("Listing")]
        [HttpGet]
        public IActionResult Listing(Guid? p, Guid? s)
        {
            new PayPeriodService(_DB).VerifyPayPeriods();

            RoEntryViewModel ModelData = new RoEntryViewModel();
            ModelData.Techs = _DB.SelectRows<TechModel>().OrderBy(m => m.TechFirstName).ToList<TechModel>();
            ModelData.PayPeriods = _DB.SelectRows<PayPeriodModel>().OrderByDescending(m => m.StartDate).ToList<PayPeriodModel>();
            ModelData.Stores = _DB.SelectRows<StoreLocationModel>().OrderBy(m => m.StoreName).ToList<StoreLocationModel>();

            ModelData.SelectedPayPeriodGuid = p;
            ModelData.SelectedStoreGuid = s;
            if (ModelData.SelectedPayPeriodGuid == null && ModelData.PayPeriods.Count > 0) { ModelData.SelectedPayPeriodGuid = ModelData.PayPeriods.First().PayPeriodGuid; }
            if (ModelData.SelectedStoreGuid == null && ModelData.Stores.Count > 0) { ModelData.SelectedStoreGuid = ModelData.Stores.First().StoreGuid; }

            if (ModelData.SelectedPayPeriodGuid != null && ModelData.SelectedStoreGuid != null)
            {
                List<TimeModel> timeEntries = _DB.SelectRows<TimeModel>().FindAll(m => m.PayPeriodGuid == ModelData.SelectedPayPeriodGuid && m.StoreGuid == ModelData.SelectedStoreGuid).ToList<TimeModel>();
                ModelData.TechStats = _TechStatService.CalcStats(timeEntries, ModelData.Techs).OrderBy(m=>m.TechFirstName).ToList<TechPeriodStatModel>();
            }

            return View(ModelData);
        }

        private TechAdminModel GetTechAdminModel()
        {
            TechAdminModel ModelData = new TechAdminModel();
            ModelData.Techs = _DB.SelectRows<TechModel>().OrderBy(m => m.TechFirstName).ToList<TechModel>();
            ModelData.Stores = _DB.SelectRows<StoreLocationModel>().OrderBy(m => m.StoreName).ToList<StoreLocationModel>();
            ModelData.EditingTech = new TechModel() { TechGuid = Guid.Empty, TechActive = true };
            ModelData.AddedTech = false;

            return ModelData;
        }
        
        [Route("Techs")]
        [HttpGet]
        public IActionResult Techs(Guid? t)
        {
            TechAdminModel ModelData = GetTechAdminModel();
            if (t != null) { ModelData.EditingTech = _DB.SelectRows<TechModel>().Find(m => m.TechGuid == t);  }

            return View(ModelData);
        }

        [Route("newTech")]
        [HttpPost]
        public IActionResult newTech(TechModel model)
        {
            bool Added = false;
            bool Updated = false;
            if (model.TechGuid == Guid.Empty)
            {
                model.TechGuid = Guid.NewGuid();
                model.TechActive = true;
                _DB.CreateRow<TechModel>(model);
                Added = true;
            }
            else
            {
                _DB.UpdateRow<TechModel>(model);
                Updated = true;
            }

            TechAdminModel ModelData = GetTechAdminModel();
            ModelData.AddedTech = Added;
            ModelData.UpdatedTech = Updated;
            return View("Techs", ModelData);
        }

        [Route("Stores")]
        [HttpGet]
        public IActionResult Stores()
        {
            return View();
        }

        [Route("ROClock")]
        [HttpGet]
        public IActionResult ROClock()
        {
            RoEntryViewModel ModelData = new RoEntryViewModel();
            ModelData.Techs = _DB.SelectRows<TechModel>().OrderBy(m => m.TechFirstName).ToList<TechModel>();
            ModelData.PayPeriods = _DB.SelectRows<PayPeriodModel>().OrderByDescending(m => m.StartDate).ToList<PayPeriodModel>();
            ModelData.Stores = _DB.SelectRows<StoreLocationModel>().OrderBy(m => m.StoreName).ToList<StoreLocationModel>();

            return View(ModelData);
        }

        [Route("newEntry")]
        [HttpPost]
        public IActionResult newEntry(TimeModel model)
        {
            model.TimeGuid = Guid.NewGuid();
            _DB.CreateRow<TimeModel>(model);

            return Redirect("/Tracking/Listing/");
        }

        [Route("TechTimes")]
        [HttpGet]
        public IActionResult TechTimes(Guid? t, Guid? p, Guid? s)
        {
            RoEntryViewModel ModelData = new RoEntryViewModel();
            ModelData.Techs = _DB.SelectRows<TechModel>().OrderBy(m => m.TechFirstName).ToList<TechModel>();
            ModelData.PayPeriods = _DB.SelectRows<PayPeriodModel>().OrderByDescending(m => m.StartDate).ToList<PayPeriodModel>();
            ModelData.Stores = _DB.SelectRows<StoreLocationModel>().OrderBy(m => m.StoreName).ToList<StoreLocationModel>();

            ModelData.SelectedTechGuid = t;
            ModelData.SelectedPayPeriodGuid = p;
            ModelData.SelectedStoreGuid = s;
            if (ModelData.SelectedTechGuid == null && ModelData.Techs.Count > 0) { ModelData.SelectedTechGuid = ModelData.Techs.First().TechGuid; }
            if (ModelData.SelectedPayPeriodGuid == null && ModelData.PayPeriods.Count > 0) { ModelData.SelectedPayPeriodGuid = ModelData.PayPeriods.First().PayPeriodGuid; }
            if (ModelData.SelectedStoreGuid == null && ModelData.Stores.Count > 0) { ModelData.SelectedStoreGuid = ModelData.Stores.First().StoreGuid; }

            if (ModelData.SelectedTechGuid != null && ModelData.SelectedPayPeriodGuid != null && ModelData.SelectedStoreGuid != null)
            {
                ModelData.TechTimeDetails = _DB.SelectRows<TimeModel>().FindAll(m => m.TechGuid == ModelData.SelectedTechGuid && m.PayPeriodGuid == ModelData.SelectedPayPeriodGuid && m.StoreGuid == ModelData.SelectedStoreGuid).ToList<TimeModel>();
            }

            return View(ModelData);
        }
    }
}

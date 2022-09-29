using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubaruEfficiencyTracking.Models;
using SubaruEfficiencyTracking.Logic;
using SubaruEfficiencyTracking.Attributes;

namespace SubaruEfficiencyTracking.Controllers
{
    [Route("Tracking")]
    [AuthorizeUsers("Admin,normalUser")]
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
            ModelData.PayPeriods = _DB.SelectRows<PayPeriodModel>().OrderByDescending(m => m.StartDate).Take(15).ToList<PayPeriodModel>();
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

        [Route("ROSearch")]
        [HttpGet]
        public JsonResult ROSearch(/*Guid p, */Guid s, Guid t, string r)
        {
            TimeModel timeEntry = _DB.SelectRows<TimeModel>().Find(m => /*m.PayPeriodGuid == p &&*/ m.StoreGuid == s && m.RoNumber == r);
            return Json(timeEntry);
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
        public IActionResult ROClock(bool Saved = false)
        {
            RoEntryViewModel ModelData = new RoEntryViewModel();
            ModelData.Techs = _DB.SelectRows<TechModel>().OrderBy(m => m.TechFirstName).ToList<TechModel>();
            ModelData.SelectedPayPeriodGuid = _DB.SelectRows<PayPeriodModel>().OrderByDescending(m => m.StartDate).ToList<PayPeriodModel>().First().PayPeriodGuid;
            ModelData.SelectedStoreGuid = _DB.SelectRows<StoreLocationModel>().OrderBy(m => m.StoreName).ToList<StoreLocationModel>().First().StoreGuid;
            ModelData.RoEntry = Saved;
            return View("RoClock", ModelData);
        }

        [Route("ListingEntrySave")]
        [HttpPost]
        public IActionResult ListingEntrySave(TimeModel model)
        {
            //If we are updating a record
            if (model.TimeGuid != Guid.Empty)
            {
                TimeModel updateRecord = _DB.SelectRows<TimeModel>().Find(m => m.TimeGuid == model.TimeGuid);
                updateRecord.OpenTime = model.OpenTime;
                updateRecord.CloseTime = model.CloseTime;
                updateRecord.ExptHours = model.ExptHours;
                _DB.UpdateRow<TimeModel>(updateRecord);
            }
            else //Make a new one
            {
                model.TimeGuid = Guid.NewGuid();
                _DB.CreateRow<TimeModel>(model);
            }

            return Redirect("/Tracking/Listing/");
        }

        [Route("RoClockSave")]
        [HttpPost]
        public IActionResult RoClockSave(TimeModel model)
        {
            TimeModel timeEntry = _DB.SelectRows<TimeModel>().Find(m => m.PayPeriodGuid == model.PayPeriodGuid && m.StoreGuid == model.StoreGuid && m.RoNumber == model.RoNumber && m.TechGuid == model.TechGuid);
            if(timeEntry != null)
            {
                timeEntry.RoClockOpenTime = model.RoClockOpenTime;
                timeEntry.RoClockCloseTime = model.RoClockCloseTime;
                _DB.UpdateRow<TimeModel>(timeEntry);
            }
            else
            {
                model.TimeGuid = Guid.NewGuid();
                _DB.CreateRow<TimeModel>(model);
            }
            return ROClock(true);
        }

        [Route("TechTimes")]
        [HttpGet]
        public IActionResult TechTimes(Guid? t, Guid? p, Guid? s, string ro)
        {
            RoEntryViewModel ModelData = new RoEntryViewModel();
            ModelData.Techs = _DB.SelectRows<TechModel>().OrderBy(m => m.TechFirstName).ToList<TechModel>();
            ModelData.PayPeriods = _DB.SelectRows<PayPeriodModel>().OrderByDescending(m => m.StartDate).ToList<PayPeriodModel>();
            ModelData.Stores = _DB.SelectRows<StoreLocationModel>().OrderBy(m => m.StoreName).ToList<StoreLocationModel>();

            if (!string.IsNullOrEmpty(ro))
            {
                TimeModel timeEntries = _DB.SelectRows<TimeModel>().Find(m => m.RoNumber == ro);
                if (timeEntries != null)
                {
                    ModelData.SelectedTechGuid = timeEntries.TechGuid;
                    ModelData.SelectedPayPeriodGuid = timeEntries.PayPeriodGuid;
                    ModelData.SelectedStoreGuid = timeEntries.StoreGuid;
                    ModelData.TechTimeDetails = _DB.SelectRows<TimeModel>().FindAll(m => m.TimeGuid == timeEntries.TimeGuid).ToList<TimeModel>();
                }
            }
            else
            {
                ModelData.SelectedTechGuid = t;
                ModelData.SelectedPayPeriodGuid = p;
                ModelData.SelectedStoreGuid = s;
                //if (ModelData.SelectedTechGuid == null && ModelData.Techs.Count > 0) { ModelData.SelectedTechGuid = ModelData.Techs.First().TechGuid; }
                if (ModelData.SelectedPayPeriodGuid == null && ModelData.PayPeriods.Count > 0) { ModelData.SelectedPayPeriodGuid = ModelData.PayPeriods.First().PayPeriodGuid; }
                if (ModelData.SelectedStoreGuid == null && ModelData.Stores.Count > 0) { ModelData.SelectedStoreGuid = ModelData.Stores.First().StoreGuid; }
                
                if (ModelData.SelectedTechGuid != null && ModelData.SelectedPayPeriodGuid != null && ModelData.SelectedStoreGuid != null)
                {
                    ModelData.TechTimeDetails = _DB.SelectRows<TimeModel>().FindAll(m => m.TechGuid == ModelData.SelectedTechGuid 
                                                                                        && m.PayPeriodGuid == ModelData.SelectedPayPeriodGuid 
                                                                                        && m.StoreGuid == ModelData.SelectedStoreGuid
                                                                                        ).ToList<TimeModel>();
                }
            }
            if(ModelData.TechTimeDetails != null)
            {
                ModelData.ROCarsIncomplete = ModelData.TechTimeDetails.FindAll(m => m.ExptHours <= 0).Count;
            }

            return View(ModelData);
        }

        [Route("DeleteROEntry")]
        [HttpPost]
        public IActionResult DeleteROEntry(Guid ID)
        {
            TimeModel deleteVar = _DB.SelectRows<TimeModel>().Find(m => m.TimeGuid == ID);
            if (deleteVar != null){
                _DB.DeleteRow<TimeModel>(deleteVar);
                return Ok();
            }
            return BadRequest();
        }

        [Route("DownloadList")]
        [HttpGet]
        public IActionResult DownloadPayPeriodData()
        {
            List<TimeModel> AllRows = _DB.SelectRows<TimeModel>();
            List<TechModel> AllTechs = _DB.SelectRows<TechModel>();

            StringBuilder Output = new StringBuilder();

            Output.AppendLine("Tech Name, Tech Number, Cars Completed, Actual Hours, Expected Hours, Overall Efficiency, RO-CLock Efficiency");
            foreach(TimeModel tm in AllRows)
            {
                TechModel curTech = AllTechs.Find(m => m.TechGuid == tm.TechGuid);
                Output.Append("\"" + curTech.TechFirstName + " " + curTech .TechLastName + "\",");
                Output.Append("\"" + "\","); //need tech number from techModel
                Output.Append("\"" + tm.RoNumber + "\",");
                Output.AppendLine("");
            }

            byte[] outputBytes = Encoding.ASCII.GetBytes(Output.ToString());
            Response.Headers.Add("Content-Disposition", "attachment;filename=download.csv");
            Response.BodyWriter.WriteAsync(outputBytes);
            Response.BodyWriter.FlushAsync();

            return Ok();
        }

        [Route("SessionActive")]
        [HttpGet]
        //this will be called every 3 minutes to keep browser from timing out after idle
        public IActionResult SessionActive() { return Ok(); }
    }
}

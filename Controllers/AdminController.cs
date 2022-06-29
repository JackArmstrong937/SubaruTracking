using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubaruEfficiencyTracking.Models;
using SubaruEfficiencyTracking.Logic;

namespace SubaruEfficiencyTracking.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private IDBConnector _DB;

        public AdminController(IDBConnector db)
        {
            _DB = db;
        }

        [Route("ClearCache")]
        public IActionResult ClearCache()
        {
            _DB.ClearCache();
            return null;
        }

        [Route("InitialSetup")]
        public IActionResult InitialSetup(string dt )
        {
            if (dt == "Stores")
            {
                StoreLocationModel newStore = new StoreLocationModel();
                newStore.StoreGuid = Guid.NewGuid();
                newStore.StoreName = "Beechmont Subaru";
                newStore.StoreActive = true;
                _DB.CreateRow<StoreLocationModel>(newStore);
            }
            

            //TechModel newTech = new TechModel()
            //{
            //    TechGuid = Guid.NewGuid(),
            //    TechName = "Jack Armstrong",
            //    TechNumber = "32515",
            //    TechActive = true
            //};
            //_DB.CreateRow<TechModel>(newTech);

            //newTech.TechGuid = Guid.NewGuid();
            //newTech.TechName = "Joe Scott";
            //newTech.TechNumber = "37875";
            //newTech.TechActive = true;
            //_DB.CreateRow<TechModel>(newTech);

            //newTech.TechGuid = Guid.NewGuid();
            //newTech.TechName = "John Harold";
            //newTech.TechNumber = "35579";
            //newTech.TechActive = true;
            //_DB.CreateRow<TechModel>(newTech);

            return Ok();
        }
    }
}

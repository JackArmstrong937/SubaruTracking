using Microsoft.AspNetCore.Mvc;
using System;
using SubaruEfficiencyTracking.Models;
using SubaruEfficiencyTracking.Logic;
using SubaruEfficiencyTracking.Attributes;

namespace SubaruEfficiencyTracking.Controllers
{
    [Route("Admin")]
    [AuthorizeUsers("Admin")]
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
            return Ok();
        }

        [Route("InitialSetup")]
        public IActionResult InitialSetup(string dt, string pw )
        {
            if (dt == "Stores")
            {
                StoreLocationModel newStore = new StoreLocationModel();
                newStore.StoreGuid = Guid.NewGuid();
                newStore.StoreName = "Beechmont Subaru";
                newStore.StoreActive = true;
                _DB.CreateRow<StoreLocationModel>(newStore);
            }

            if (dt == "Admin")
            {
                LoginCredentials newLogin = new LoginCredentials();
                newLogin.LoginGuid = Guid.NewGuid();
                newLogin.UserName = "subaruadmin";
                newLogin.Password = pw;
                newLogin.UserType = "Admin";
                _DB.CreateRow<LoginCredentials>(newLogin);
            }
               
            if (dt == "Tech")
            {
                LoginCredentials newLogin = new LoginCredentials();
                newLogin.LoginGuid = Guid.NewGuid();
                newLogin.UserName = "subarutech";
                newLogin.Password = pw;
                newLogin.UserType = "normalUser";
                _DB.CreateRow<LoginCredentials>(newLogin);
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

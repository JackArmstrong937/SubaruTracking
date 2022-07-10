using Microsoft.AspNetCore.Mvc;
using SubaruEfficiencyTracking.Models;
using SubaruEfficiencyTracking.Logic;
using Microsoft.AspNetCore.Http;


namespace SubaruEfficiencyTracking.Controllers
{
    [Route("Login")]
    
    public class LoginController:Controller
    {
        private IDBConnector _DB;

        public LoginController(IDBConnector db)
        {
            _DB = db;
        }

        [Route("Index")]
        [HttpGet]
        public IActionResult Index()
        {
            LoginCredentials ModelData = new LoginCredentials();
            return View(ModelData);
        }
        [Route("Index")]
        [HttpPost]
        public IActionResult Index(LoginCredentials Model)
        {
            LoginCredentials checkUser = _DB.SelectRows<LoginCredentials>().Find(m => m.UserName == Model.UserName && m.Password == Model.Password);
            if (checkUser != null)
            {
                HttpContext.Session.SetString("UserType", checkUser.UserType);
                return Redirect("/Tracking/Listing");
            }
            else
            {
                ViewBag.failureMessage = "Invalid Username or Password.";
                return View(new LoginCredentials());
            }
        }

        [Route("Logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("UserType","");
            return Redirect("/Login/Index");
        }
    }
}

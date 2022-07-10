using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace SubaruEfficiencyTracking.Attributes
{
    public class AuthorizeUsers : ActionFilterAttribute
    {
        private List<string> allowedTypes;
        public AuthorizeUsers(string userTypes)
        {
            allowedTypes =  new List<string>(userTypes.Split(','));
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string userType = filterContext.HttpContext.Session.GetString("UserType");
            if (string.IsNullOrEmpty(userType))
            {
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }

            if (allowedTypes.Find(m=>m == userType)==null)
            {
                filterContext.Result =  new RedirectResult("/Tracking/Listing");
                return;
            }
        }
    }
}

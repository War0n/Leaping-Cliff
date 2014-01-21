using BarometerDataAccesLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Barometer_ASP_NET.Filters
{
    public class AuthFilter : AuthorizeAttribute
    {

        public String GivenRole { get; set; }

        public AuthFilter(string givenRole)
        {
            this.GivenRole = givenRole;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            CurrentUser curUser = CurrentUser.getInstance();
            if(curUser.Role == null) {
                filterContext.Result = new RedirectResult("/Account/login");
            }
            else if (!curUser.Role.ToLower().Equals(GivenRole.ToLower()) && !GivenRole.ToLower().Equals("all") && !GivenRole.ToLower().Equals("admod"))
                {                    
                        filterContext.Result = new RedirectResult("/Account/login");                                     
                }
            if (GivenRole.ToLower().Equals("admod") && curUser.Role.ToLower().Equals("user"))
            {
                filterContext.Result = new RedirectResult("/Account/login");
            }
  
             
        }

    }
}
using Article_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Article_Web.Filters
{
    public class AdminFilter : FilterAttribute, IAuthorizationFilter
    {
     
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            User usr =(User)filterContext.HttpContext.Session["login"];
            if(usr!=null && usr.Admin ==false)
            {
               filterContext.Result =new RedirectResult("/Home/Index");
            }
        }
    }
}

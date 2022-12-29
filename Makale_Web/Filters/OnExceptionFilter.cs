using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Article_Web.Filters
{
    public class OnExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Controller.TempData["error"] = filterContext.Exception;
            filterContext.ExceptionHandled=true;
            filterContext.Result= new RedirectResult("/Home/PageNotFound");
        }
    }
}

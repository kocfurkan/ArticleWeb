using Article_BLL;
using Article_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Article_Web.Models
{
    public class CacheHelper
    {
        public static List<Category> Categories()
        {
            //For Frequently Accessed "Categories" , Cache Is Used. For Perfromance Reasons
            var result = WebCache.Get("category-cache");
            if (result == null)
            {
                CategoryBL ctgBL = new CategoryBL();
                result = ctgBL.ReadCategories();
                WebCache.Set("category-cache", result,20,true);
            }
            return result;
        }
    }
}
﻿using Makale_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class Test
    {
      
        public Test()
        {
         DatabaseContext db = new DatabaseContext();
         db.Categories.ToList();
        }
    }
}

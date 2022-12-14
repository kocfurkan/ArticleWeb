using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_DAL
{
    public class DbSingleton
    {
        public static DatabaseContext db;
        public DbSingleton()
        {
            if (db == null)
            {
                db = new DatabaseContext();
            }
        }
    }
}

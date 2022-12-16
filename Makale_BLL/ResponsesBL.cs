using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    //T Could only be a class(Preventing other types of arguments)
    public class ResponsesBL<T> where T : class
    {
       public List<String> errors
        {
            get; set;
        }
        public T Obj { get; set; }
        public ResponsesBL()
        {
            errors = new List<String>();
        }
    }
}

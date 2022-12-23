using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article_Entities
{
    public class Like
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Note Note { get; set; }
    }
}

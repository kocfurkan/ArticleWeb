using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    public class Comment:BaseEntity
    {
        [Required,StringLength(250)]
        public string Text { get; set; }
        public virtual User User { get; set;}
        public virtual Note Note { get; set; }  
    }
}

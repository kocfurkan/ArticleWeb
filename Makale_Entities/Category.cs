using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article_Entities
{
    public class Category : BaseEntity
    {
        [DisplayName("Category"),Required, StringLength(50)]
        public string Title { get; set; }
        [StringLength(150)]
        public string Description { get; set; }
        public virtual List<Note> Notes { get; set; }
        public Category()
        {
            Notes = new List<Note>();   
        }
    }
}

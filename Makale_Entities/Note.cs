using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    public class Note : BaseEntity
    {
        [DisplayName("Category"),Required, StringLength(250)]
        public string Title { get; set; }
        [Required, StringLength(250)]
        public string Text { get; set; }
        public bool Draft { get; set; }
        public int LikeNumber { get; set; }
		[DisplayName("Category")]
        public int CategoryId { get; set; } 
        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Like> Likes { get; set; }
        public Note()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }
    }
}

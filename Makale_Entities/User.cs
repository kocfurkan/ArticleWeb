using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    public class User : BaseEntity
    {
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Surname { get; set; }
        [StringLength(20), Required]
        public string Username { get; set; }
        [Required,StringLength(50)]
        public string Email { get; set; }
        [Required,StringLength(20)]
        public string Password { get; set; }
        public bool Active { get; set; }
        public bool Admin { get; set; }
        [Required]
        public Guid ActivationGuid { get; set; }
        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Like> Likes { get; set; }

        public User()
        {
            Notes = new List<Note>();   
            Comments = new List<Comment>();
            Likes = new List<Like>();
        }
    }
}

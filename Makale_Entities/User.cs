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
        [StringLength(20), Required(ErrorMessage = "Please Enter a Valid Username")]
        public string Username { get; set; }
        [Required, StringLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter a Valid Password"), StringLength(20)]
        public string Password { get; set; }
        //For not including prop in scaffolding, use ScaffoldColumn(false)
        [StringLength(20),ScaffoldColumn(false)]
        public string Avatar { get; set; }
        public bool Active { get; set; }
        public bool Admin { get; set; }
        [Required,ScaffoldColumn(false)]
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

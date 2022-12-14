using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    public class BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        [Required]
        public DateTime RegisterationDate { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
        [Required, StringLength(20)]
        public string UpdatedBy { get; set; }
    }
}

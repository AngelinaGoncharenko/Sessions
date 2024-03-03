using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Models
{
    public class BannedUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public User User { get; set; } = new User();

        [Required, Column(TypeName = "datetime2")]
        public DateTime Time { get; set; } = DateTime.MinValue;
    }
}

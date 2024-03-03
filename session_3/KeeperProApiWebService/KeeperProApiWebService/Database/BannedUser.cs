using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperProCommonDivision.Database
{
    public class BannedUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public User User { get; set; } = new User();

        [Required]
        public string Note { get; set; } = string.Empty;
    }
}

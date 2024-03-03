using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperProCommonDivision.Database
{
    public class User
    {
        [Key]
        public string Email { get; set; } = string.Empty;

        public string? Login { get; set; }
        public string? Password { get; set; }
    }
}

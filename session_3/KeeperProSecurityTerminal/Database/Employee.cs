using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace KeeperProCommonDivision.Database
{
    public class Employee
    {
        [Key]
        public string Code { get; set; } = string.Empty;

        [Required]
        public string SecondName { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }

        public Department? Department { get; set; } 
        public string? Division { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;

        public List<Bid> Bids { get; set; } = new List<Bid>();

        [NotMapped]
        public string FullName => GetFullName();

        public string GetFullName()
        {
            var name = SecondName;
            if (FirstName.Length >= 1)
            {
                name += " " + FirstName[0] + ".";
            }

            if (LastName != null && LastName.Length >= 1)
            {
                name += " " + LastName[0] + ".";
            }

            return name;
        }
    }
}

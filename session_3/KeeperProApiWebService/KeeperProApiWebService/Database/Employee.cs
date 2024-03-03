using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

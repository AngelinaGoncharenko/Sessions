using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperProCommonDivision.Database
{
    public class Visitor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SecondName { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }

        [StringLength(18)]
        public string? PhoneNumber { get; set; }

        [Required]
        public User User { get; set; } = new User();

        [Required]
        public DateTime BirthDay { get; set; } = DateTime.MinValue;

        [Required, StringLength(4)]
        public string PassportSeries { get; set; } = string.Empty;

        [Required, StringLength(6)]
        public string PassportNumber { get; set; } = string.Empty;

        [Required]
        public Bid Bid { get; set; } = new Bid();

        public string? Organization { get; set; }
        public string? Note { get; set; }
        public string? Photo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SecondName { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; }

        [Required]
        public Gender Gender { get; set; } = new Gender();

        [Required]
        public string Job { get; set; } = string.Empty;

        public UserType? Type { get; set; } 

        public string? SecretWord { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }

        [Required]
        public bool HasDataReadMandate { get; set; } = false;
        [Required]
        public bool HasDataWriteMandate { get; set; } = false;
        [Required]
        public bool HasFormateGraphMandate { get; set; } = false;

        [Required]
        public bool IsAccepted { get; set; } = false;        [MaxLength]        public string? Photo { get; set; }        [NotMapped]
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
                name += LastName[0] + ".";
            }

            return name;
        }    }
}

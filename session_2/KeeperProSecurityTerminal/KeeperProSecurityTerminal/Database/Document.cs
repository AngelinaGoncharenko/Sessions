using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperProCommonDivision.Database
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Source { get; set; } = string.Empty;

        [Required]
        public Bid Bid { get; set; } = new Bid();
    }
}

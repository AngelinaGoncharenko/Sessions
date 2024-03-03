using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperProCommonDivision.Database
{
    public class Bid
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Employee Employee { get; set; } = new Employee();

        public DateTime? VisitDate { get; set; }
        public TimeSpan? VisitTime { get; set; }

        [Required]
        public DateTime StartDate { get; set; } = DateTime.MaxValue;

        [Required]
        public DateTime EndDate { get; set; } = DateTime.MaxValue;

        [Required]
        public BidStatus Status { get; set; } = new BidStatus();

        public string? StatusNote { get; set; }

        public BidVisitTarget VisitTarget { get; set; } = new BidVisitTarget();

        [Required]
        public BidType Type { get; set; } = new BidType();

        public TimeSpan? EntrySecurityTime { get; set; }
        public TimeSpan? ExitSecurityTime { get; set; }
        public TimeSpan? EntryDepartmentTime { get; set; }
        public TimeSpan? ExitDepartmentTime { get; set; }

        public List<Visitor> Visitors { get; set; } = new List<Visitor>();
        public List<Document> Documents { get; set; } = new List<Document>();
    }
}

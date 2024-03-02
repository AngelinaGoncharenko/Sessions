using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbClasses.Models
{
    public class PatientEntities
    {
        public int id { get; set; }
        public string name { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string pasport { get; set; }
        public DateTime birthday { get; set; }
        public virtual GenderEntities gender { get; set; }
        public string address { get; set; }
        public string number { get; set; }
        public string email { get; set; }
        public virtual MedCardEntities medcard { get; set; }
        public int policy { get; set; }
        public DateTime policyDate { get; set; }
        public string workPlace { get; set; }
        public virtual PaimentEntities paiment { get; set; }
        public virtual TypeInsuranceEntities typeInsuranceCompany { get; set; }
        public string insuranceCompany { get; set; }
    }
}

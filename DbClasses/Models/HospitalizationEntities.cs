using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbClasses.Models
{
    public class HospitalizationEntities
    {
        public int id {  get; set; }
        public virtual PatientEntities patient { get; set; }
        public DateTime dateHospitalozation { get; set; }
        public virtual MedCardEntities medCard { get; set; }
        public string target {  get; set; }
        public virtual DepartmentEntities department { get; set; }
        public virtual ConditionEntities condition { get; set; }
        public string informations { get; set; }
        public DateTime dateEndHospitalization { get; set; }
        public virtual StatusEntities status { get; set; }
    }
}

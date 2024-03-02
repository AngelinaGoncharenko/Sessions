using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbClasses.Models
{
    public class MedHistoryEntities
    {
        public int id { get; set; }
        public DateTime date { get; set; } // Дата установки диагноза
        public DateTime dateOfRecovery { get; set; } // Дата выздоровления
        public string anamnesis { get; set; }
        public string symptoms { get; set; }
        public string recomendations { get; set; }
        public virtual ReceptEntities recept { get; set; }
        public virtual DiagnosisEntities diagnosis { get; set; }
        
    }
}

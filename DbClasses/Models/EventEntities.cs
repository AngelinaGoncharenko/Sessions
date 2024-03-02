using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbClasses.Models
{
    public class EventEntities
    {
        public int id { get; set; }
        public string name { get; set;}
        public virtual TypeEntities typeEvent {  get; set; }
    }
}

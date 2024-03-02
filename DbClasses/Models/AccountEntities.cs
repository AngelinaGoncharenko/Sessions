using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbClasses.Models
{
    public class AccountEntities
    {
        public int id { get; set; }
        public string name { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string login {  get; set; }
        public string password { get; set; }
    }
}

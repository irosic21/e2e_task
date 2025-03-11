using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e2e_DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Picture { get; set; }
        public string Gender { get; set; }
        public int YearOfBirth { get; set; }
    }
}

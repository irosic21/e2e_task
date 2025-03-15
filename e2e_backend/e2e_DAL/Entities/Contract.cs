using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e2e_DAL.Entities
{
    public class Contract
    {
        private DateTime _dateOfEmployment;
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DateOfEmployment
        {
            get => _dateOfEmployment.Date;
            set => _dateOfEmployment = value.Date;
        }
        public string ContractType { get; set; }
        public int DurationOfContract { get; set; }
        public string Department { get; set; }

        public Employee Employee { get; set; }
    }
}

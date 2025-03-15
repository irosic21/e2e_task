using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e2e_DAL.Entities
{
    public class LeaveRecords
    {
        public int Id {  get; set; }
        public int EmployeeId { get; set; }
        public int VacationDays { get; set; }
        public int FreeDays { get; set; }
        public int PaidLeaveDays { get; set; }

        public Employee Employee { get; set; }
    }
}

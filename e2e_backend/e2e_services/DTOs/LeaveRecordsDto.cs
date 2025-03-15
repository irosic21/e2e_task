using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e2e_services.DTOs
{
    public class CreateLeaveRecordsDto
    {
        public int VacationDays { get; set; }
        public int FreeDays { get; set; }
        public int PaidLeaveDays { get; set; }
    }
    public class LeaveRecordsDto : CreateLeaveRecordsDto
    {
        public int Id { get; set; }
    }
    public class UpdateLeaveRecordsDto
    {
        public int? VacationDays { get; set; }
        public int? FreeDays { get; set; }
        public int? PaidLeaveDays { get; set; }
    }
}

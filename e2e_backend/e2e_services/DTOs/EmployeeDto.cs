using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e2e_services.DTOs
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public int YearOfBirth { get; set; }
        public CreateContractDto Contract { get; set; }
        public CreateLeaveRecordsDto LeaveRecords { get; set; }
    }
    public class EmployeeDto : CreateEmployeeDto
    {
        public int ID { get; set; }
        public string Picture { get; set; }

        public new ContractDto Contract { get; set; }
        public new LeaveRecordsDto LeaveRecords { get; set; }
    }
    public class UpdateEmployeeDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Gender { get; set; }
        public int? YearOfBirth { get; set; }
        public UpdateContractDto? Contract { get; set; }
        public UpdateLeaveRecordsDto? LeaveRecords { get; set; }
    }
}

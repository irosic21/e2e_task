using e2e_DAL.Entities;
using e2e_services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e2e_services.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeesByIdAsync(int id);
        Task<Employee> AddEmployeeAsync(EmployeeDto employee);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<Employee> UpdateEmployeeAsync(EmployeeDto employee, int id);
    }
}

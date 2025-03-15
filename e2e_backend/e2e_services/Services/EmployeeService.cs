using AutoMapper;
using e2e_DAL.Entities;
using e2e_DAL.Repositories.Interfaces;
using e2e_services.DTOs;
using e2e_services.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e2e_services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IContractRepository _contractRepository;
        private readonly ILeaveRecordsRepository _leaveRecordsRepository;
        private readonly ILogger<EmployeeService> _logger;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IFileService fileService, IContractRepository contractRepository, ILeaveRecordsRepository leaveRecordsRepository, ILogger<EmployeeService> logger, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _fileService = fileService;
            _contractRepository = contractRepository;
            _leaveRecordsRepository = leaveRecordsRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Employee> AddEmployeeAsync(EmployeeDto employeeDto)
        {
            try
            {
                if (string.IsNullOrEmpty(employeeDto.Name) || string.IsNullOrEmpty(employeeDto.Surname))
                {
                    _logger.LogWarning("Validation failed: Employee name and surname are required.");
                    return null;
                }

                var employee = _mapper.Map<Employee>(employeeDto);
                await _employeeRepository.AddAsync(employee);

                return await _employeeRepository.GetByIdAsync(employee.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an employee.");
                return null;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                var existingEmployee = await _employeeRepository.GetByIdAsync(id);
                if (existingEmployee == null)
                {
                    _logger.LogWarning("Delete failed: Employee with ID {Id} not found.", id);
                    return false;
                }

                await _employeeRepository.DeleteAsync(id);
                _logger.LogInformation("Employee with ID {Id} deleted successfully.", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting employee ID {Id}.", id);
                return false;
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _employeeRepository.GetAllAsync();
                var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

                foreach (var employeeDto in employeeDtos)
                {
                    if (!string.IsNullOrEmpty(employeeDto.Picture))
                    {
                        employeeDto.Picture = _fileService.GetFilePath(employeeDto.Picture);
                    }
                }

                _logger.LogInformation("Successfully retrieved {Count} employees.", employeeDtos.Count());
                return employeeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving employees.");
                return Enumerable.Empty<EmployeeDto>();
            }
        }

        public async Task<EmployeeDto> GetEmployeesByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null)
                {
                    _logger.LogWarning("Employee with ID {Id} not found.", id);
                    return null;
                }

                var employeeDto = _mapper.Map<EmployeeDto>(employee);

                if (!string.IsNullOrEmpty(employeeDto.Picture))
                {
                    employeeDto.Picture = _fileService.GetFilePath(employeeDto.Picture);
                }

                _logger.LogInformation("Successfully retrieved employee with ID {Id}.", id);
                return employeeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving employee ID {Id}.", id);
                return null;
            }
        }

        public async Task<Employee> UpdateEmployeeAsync(EmployeeDto employee, int id)
        {
            try
            {
                var existingEmployee = await _employeeRepository.GetByIdAsync(id);
                if (existingEmployee == null)
                {
                    _logger.LogWarning("Update failed: Employee ID {Id} not found.", id);
                    return null;
                }

                if (!string.IsNullOrEmpty(employee.Name))
                    existingEmployee.Name = employee.Name;
                if (!string.IsNullOrEmpty(employee.Surname))
                    existingEmployee.Surname = employee.Surname;
                if (!string.IsNullOrEmpty(employee.Picture))
                    existingEmployee.Picture = employee.Picture;
                if (!string.IsNullOrEmpty(employee.Gender))
                    existingEmployee.Gender = employee.Gender;
                if (employee.YearOfBirth > 0)
                    existingEmployee.YearOfBirth = employee.YearOfBirth;

                await _employeeRepository.UpdateAsync(existingEmployee);

                if (existingEmployee.Contract != null && employee.Contract != null)
                {
                    if (!string.IsNullOrEmpty(employee.Contract.ContractType))
                        existingEmployee.Contract.ContractType = employee.Contract.ContractType;
                    if (employee.Contract.DurationOfContract >= 0)
                        existingEmployee.Contract.DurationOfContract = employee.Contract.DurationOfContract;
                    if (!string.IsNullOrEmpty(employee.Contract.Department))
                        existingEmployee.Contract.Department = employee.Contract.Department;

                    await _contractRepository.UpdateAsync(existingEmployee.Contract);
                }

                if (existingEmployee.LeaveRecords != null && employee.LeaveRecords != null)
                {
                    if (employee.LeaveRecords.VacationDays >= 0)
                        existingEmployee.LeaveRecords.VacationDays = employee.LeaveRecords.VacationDays;
                    if (employee.LeaveRecords.FreeDays >= 0)
                        existingEmployee.LeaveRecords.FreeDays = employee.LeaveRecords.FreeDays;
                    if (employee.LeaveRecords.PaidLeaveDays >= 0)
                        existingEmployee.LeaveRecords.PaidLeaveDays = employee.LeaveRecords.PaidLeaveDays;

                    await _leaveRecordsRepository.UpdateAsync(existingEmployee.LeaveRecords);
                }

                _logger.LogInformation("Employee ID {Id} updated successfully.", id);
                return await _employeeRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating employee ID {Id}.", id);
                return null;
            }
        }
    }
}

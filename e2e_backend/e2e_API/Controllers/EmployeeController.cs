using AutoMapper;
using e2e_DAL.Entities;
using e2e_services.DTOs;
using e2e_services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace e2e_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService, IFileService fileService, IMapper mapper)
        {
            _logger = logger;
            _employeeService = employeeService;
            _fileService = fileService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllAsync()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                return Ok(employees);
            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeesByIdAsync(id);
                if (employee == null)
                    return NotFound($"Employee with ID {id} not found.");

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("add")]
        public async Task<ActionResult> AddEmployee([FromForm] CreateEmployeeDto createEmployee, IFormFile imageFile)
        {
            try
            {
                if (createEmployee == null)
                    return BadRequest("Employee data is null");

                string imagePath;
                try
                {
                    imagePath = await _fileService.SaveFileInDirectoryAsync(imageFile);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "File upload failed.");
                    return BadRequest("File upload failed: " + ex.Message);
                }

                var employee = _mapper.Map<EmployeeDto>(createEmployee);
                employee.Picture = imagePath;

                var addedEmployee = await _employeeService.AddEmployeeAsync(employee);
                if (addedEmployee == null)
                    return BadRequest("Invalid employee data");

                return Ok("Employee added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, [FromForm] UpdateEmployeeDto updateEmployee, IFormFile? imageFile)
        {
            try
            {
                var existingEmployee = await _employeeService.GetEmployeesByIdAsync(id);
                if (existingEmployee == null)
                    return NotFound($"Employee with ID {id} not found.");

                string? imagePath = existingEmployee.Picture;
                if (imageFile != null && imageFile.Length > 0)
                {
                    _fileService.DeleteFile(existingEmployee.Picture);
                    imagePath = await _fileService.SaveFileInDirectoryAsync(imageFile);
                }

                var employeeToUpdate = _mapper.Map<EmployeeDto>(updateEmployee);
                employeeToUpdate.Picture = imagePath;

                var updatedEmployee = await _employeeService.UpdateEmployeeAsync(employeeToUpdate, id);
                if (updatedEmployee == null)
                    return BadRequest("Failed to update employee.");

                return Ok("Employee updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                var isDeleted = await _employeeService.DeleteEmployeeAsync(id);
                if (!isDeleted)
                    return NotFound($"Employee with ID {id} not found or already deleted.");

                return Ok("Employee deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

using e2e_DAL.Entities;
using e2e_DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e2e_DAL.Repositories
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task AddAsync(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}

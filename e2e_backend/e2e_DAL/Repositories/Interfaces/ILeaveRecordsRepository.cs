using e2e_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e2e_DAL.Repositories.Interfaces
{
    public interface ILeaveRecordsRepository
    {
        Task<LeaveRecords> GetByIdAsync(int id);
        Task AddAsync(LeaveRecords leaveRecords);
        Task UpdateAsync(LeaveRecords leaveRecords);
        Task DeleteAsync(int id);
    }
}

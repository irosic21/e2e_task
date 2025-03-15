using e2e_DAL.Entities;
using e2e_DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e2e_DAL.Repositories
{
    public class LeaveRecordsRepository : ILeaveRecordsRepository
    {
        private readonly ApplicationDbContext _context;
        public LeaveRecordsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(LeaveRecords leaveRecords)
        {
            await _context.LeaveRecords.AddAsync(leaveRecords);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var leaveRecords = await _context.LeaveRecords.FindAsync(id);
            if (leaveRecords != null)
            {
                _context.LeaveRecords.Remove(leaveRecords);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<LeaveRecords> GetByIdAsync(int id)
        {
            return await _context.LeaveRecords.FindAsync(id);
        }

        public async Task UpdateAsync(LeaveRecords leaveRecords)
        {
            _context.LeaveRecords.Update(leaveRecords);
            await _context.SaveChangesAsync();
        }
    }
}

using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _context.AddRangeAsync(allocations);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeid, int period)
        {
            return await _context.LeaveAllocations.AnyAsync(s => s.EmployeeId == userId && s.LeaveTypeId == leaveTypeid && s.Period == period);
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var leaveAllocation = await _context.LeaveAllocations
                .Include(s => s.LeaveType)
                .FirstOrDefaultAsync(s => s.Id == id);

            return leaveAllocation;
        }

        public async Task<IEnumerable<LeaveAllocation>> GetLeaveAllocationWithDetails()
        {
            var leaveAllocations = await _context.LeaveAllocations
                .Include(s => s.LeaveType)
                .ToListAsync();

            return leaveAllocations;
        }

        public async Task<IEnumerable<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId)
        {
            var leaveAllocations = await _context.LeaveAllocations
                .Where(s => s.EmployeeId == userId)
               .Include(s => s.LeaveType)
               .ToListAsync();

            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        {
            return await _context.LeaveAllocations.FirstOrDefaultAsync(s => s.EmployeeId == userId && s.LeaveTypeId == leaveTypeId);
        }
    }
}

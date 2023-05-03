using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var leaveRequests = await _context.LeaveRequests
                .Include(q => q.LeaveType)
                .ToListAsync();

            return leaveRequests;
        }

        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
        {
            var leaveRequests = await _context.LeaveRequests
                .Where(s => s.RequestingEmployeeId == userId)
               .Include(q => q.LeaveType)
               .ToListAsync();

            return leaveRequests;
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            var leaveRequests = await _context.LeaveRequests
              .Include(q => q.LeaveType)
              .FirstOrDefaultAsync(s => s.Id == id);

            return leaveRequests;
        }
    }
}

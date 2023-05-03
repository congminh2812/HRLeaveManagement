using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation> {
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
        Task<IEnumerable<LeaveAllocation>> GetLeaveAllocationWithDetails();
        Task<IEnumerable<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId);
        Task<bool> AllocationExists(string userId, int leaveTypeid, int period);
        Task AddAllocations(List<LeaveAllocation> allocations);
        Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId);

    }
}

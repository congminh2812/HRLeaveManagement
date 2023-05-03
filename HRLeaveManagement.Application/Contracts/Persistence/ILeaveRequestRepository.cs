using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest> {
        Task<LeaveRequest> GetLeaveRequestWithDetails(int id);
        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsWithDetails();
        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsWithDetails(string userId);
    }
}

using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests
{
    public record GetLeaveRequestDetailsQuery(int Id) : IRequest<LeaveRequestDetailsDto>;
}

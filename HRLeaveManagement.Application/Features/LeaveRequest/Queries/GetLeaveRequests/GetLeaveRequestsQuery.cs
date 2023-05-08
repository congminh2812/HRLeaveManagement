using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests
{
    public record GetLeaveRequestsQuery : IRequest<List<LeaveRequestDto>>
    {
    }
}

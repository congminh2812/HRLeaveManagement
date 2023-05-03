using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public record GetLeaveTypeDetailsQuery(int Id) : IRequest<LeaveTypeDetailDto>;
}

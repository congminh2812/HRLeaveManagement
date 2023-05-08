using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{
    public record GetLeaveAllocationDetailsQuery(int Id) : IRequest<LeaveAllocationDetailsDto>;
}

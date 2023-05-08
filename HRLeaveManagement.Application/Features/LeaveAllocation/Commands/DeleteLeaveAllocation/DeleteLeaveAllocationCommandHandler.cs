using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            // convert to domain entity object
            var leaveAllocationToDelete = await _leaveAllocationRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Domain.LeaveAllocation), request);

            // delete to database
            await _leaveAllocationRepository.DeleteAsync(leaveAllocationToDelete);

            // return record id
            return Unit.Value;
        }
    }
}

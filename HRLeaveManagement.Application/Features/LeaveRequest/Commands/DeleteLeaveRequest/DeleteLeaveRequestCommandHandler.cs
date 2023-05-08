using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            // convert to domain entity object
            var leaveRequestToDelete = await _leaveRequestRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Domain.LeaveRequest), request);

            // delete to database
            await _leaveRequestRepository.DeleteAsync(leaveRequestToDelete);

            // return record id
            return Unit.Value;
        }
    }
}

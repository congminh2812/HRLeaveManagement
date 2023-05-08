using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository=leaveTypeRepository;
        }

        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            // Validate incoming data
            var validator = new UpdateLeaveAllocationCommandValidator(_leaveTypeRepository, _leaveAllocationRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Update Leave Allocation", validationResult);

            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(LeaveAllocation), request.Id);

            // convert to domain entity object
            var leaveAllocationToUpdate = _mapper.Map<Domain.LeaveAllocation>(request);

            // update to database
            await _leaveAllocationRepository.UpdateAsync(leaveAllocationToUpdate);

            // return unit value
            return Unit.Value;
        }
    }
}

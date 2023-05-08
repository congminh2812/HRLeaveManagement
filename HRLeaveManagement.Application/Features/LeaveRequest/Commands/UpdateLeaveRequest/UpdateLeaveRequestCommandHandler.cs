using AutoMapper;
using FluentValidation.Internal;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveRequest.Shared;
using HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HRLeaveManagement.Application.Models.Email;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _logger;

        public UpdateLeaveRequestCommandHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IEmailSender emailSender, IAppLogger<UpdateLeaveRequestCommandHandler> logger)
        {
            _mapper = mapper;
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository=leaveTypeRepository;
            _emailSender=emailSender;
            _logger=logger;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            // Validate incoming data
            var validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository, _leaveRequestRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Update Leave Request", validationResult);

            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(LeaveRequest), request.Id);

            // convert to domain entity object
            var leaveRequestToUpdate = _mapper.Map<Domain.LeaveRequest>(request);

            // update to database
            await _leaveRequestRepository.UpdateAsync(leaveRequestToUpdate);

            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty, /* Get email from employee record */
                    Body = @$"Your leave request for {request.StartDate:D} to {request.EndDate:D} has been updated successfully",
                    Subject = "Leave Request Submitted"
                };

                await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }

            return Unit.Value;
        }
    }
}

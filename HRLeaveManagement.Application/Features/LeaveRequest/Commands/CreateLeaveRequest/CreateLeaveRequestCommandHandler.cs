using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Models.Email;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;

        public CreateLeaveRequestCommandHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IEmailSender emailSender, IAppLogger<CreateLeaveRequestCommandHandler> logger)
        {
            _mapper = mapper;
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository=leaveTypeRepository;
            _emailSender=emailSender;
            _logger=logger;
        }

        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            // Validate incoming data
            var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Leave Request", validationResult);


            // Get requesting Employee's id

            // check on employee's allocation

            // if allocations aren't enough, return validatoion error with messgae

            // create leave request
            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
            await _leaveRequestRepository.CreateAsync(leaveRequest);

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

using FluentValidation;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommandValidator : AbstractValidator<ChangeLeaveRequestApprovalCommand>
    {
        public ChangeLeaveRequestApprovalCommandValidator() {
            RuleFor(s => s.Approved)
                .NotNull()
                .WithMessage("Approval status cannot be null");
        }
    }
}

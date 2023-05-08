using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Shared
{
    public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository=leaveTypeRepository;

            RuleFor(s => s.StartDate)
                .LessThan(s => s.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}");

            RuleFor(s => s.EndDate)
                .GreaterThan(s => s.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}");

            RuleFor(s => s.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist)
                .WithMessage("{PropertyName} does not exist.");

        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken arg2)
        {
            var leaveRequest = await _leaveTypeRepository.GetByIdAsync(id);
            return leaveRequest != null;
        }

    }
}

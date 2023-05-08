﻿using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(p => p.NumberOfDays)
                .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");

            RuleFor(p => p.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}");

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist)
                .WithMessage("{PropertyName} does not exist.");

            RuleFor(s => s.Id)
                .NotNull()
                .MustAsync(LeaveAllocationMustExist)
                .WithMessage("{PropertyName} must be present");
            _leaveAllocationRepository=leaveAllocationRepository;
        }

        private async Task<bool> LeaveAllocationMustExist(int id, CancellationToken arg2)
        {
            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(id);

            return leaveAllocation != null;
        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken arg2)
        {
            var leaveAllocation = await _leaveTypeRepository.GetByIdAsync(id);
            return leaveAllocation != null;
        }

    }
}

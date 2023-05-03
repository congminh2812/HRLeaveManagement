﻿using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // convert to domain entity object
            var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);

            // verify that record exists
            if (leaveTypeToDelete is null)
                throw new NotFoundException(nameof(Domain.LeaveType), request);

            // delete to database
            await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

            // return record id
            return Unit.Value;
        }
    }
}

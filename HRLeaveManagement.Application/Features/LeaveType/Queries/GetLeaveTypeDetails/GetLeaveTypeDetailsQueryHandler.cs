using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{

    public class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public GetLeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper=mapper;
            _leaveTypeRepository=leaveTypeRepository;
        }

        public async Task<LeaveTypeDetailDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
        {
            // Query to database
            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.Id);

            // verify that record exists
            if (leaveType is null)
                throw new NotFoundException(nameof(Domain.LeaveType), request);

            // convert dto to object
            var data = _mapper.Map<LeaveTypeDetailDto>(leaveType);

           return data;
        }
    }
}

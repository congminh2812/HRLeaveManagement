using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{

    public class GetLeaveAllocationDetailsQueryHandler : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public GetLeaveAllocationDetailsQueryHandler(IMapper mapper, ILeaveAllocationRepository LeaveAllocationRepository)
        {
            _mapper=mapper;
            _leaveAllocationRepository=LeaveAllocationRepository;
        }

        public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);

            return _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation); ;
        }
    }
}

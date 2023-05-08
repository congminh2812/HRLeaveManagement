using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{

    public class GetLeaveAllocationsQueryHandler : IRequestHandler<GetLeaveAllocationsQuery, List<LeaveAllocationDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IAppLogger<GetLeaveAllocationsQueryHandler> _logger;

        public GetLeaveAllocationsQueryHandler(IMapper mapper, ILeaveAllocationRepository LeaveAllocationRepository, IAppLogger<GetLeaveAllocationsQueryHandler> logger)
        {
            _mapper=mapper;
            _leaveAllocationRepository=LeaveAllocationRepository;
            _logger = logger;
        }

        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsQuery request, CancellationToken cancellationToken)
        {
            // To add later
            // Get records for specific user
            // Get allocations per employee

            var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationWithDetails();
            var allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);


           return allocations;
        }
    }
}

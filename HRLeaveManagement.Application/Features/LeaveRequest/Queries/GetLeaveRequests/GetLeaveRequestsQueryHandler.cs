using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests
{

    public class GetLeaveRequestsQueryHandler : IRequestHandler<GetLeaveRequestsQuery, List<LeaveRequestDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IAppLogger<GetLeaveRequestsQueryHandler> _logger;

        public GetLeaveRequestsQueryHandler(IMapper mapper, ILeaveRequestRepository LeaveRequestRepository, IAppLogger<GetLeaveRequestsQueryHandler> logger)
        {
            _mapper=mapper;
            _leaveRequestRepository=LeaveRequestRepository;
            _logger = logger;
        }

        public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
        {
            // Check if it is logged in employee

            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
            var requests = _mapper.Map<List<LeaveRequestDto>>(leaveRequests);

            // Fill requests with employee informartion

           return requests;
        }
    }
}

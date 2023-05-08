using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests
{

    public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public GetLeaveRequestDetailsQueryHandler(IMapper mapper, ILeaveRequestRepository LeaveRequestRepository)
        {
            _mapper=mapper;
            _leaveRequestRepository=LeaveRequestRepository;
        }

        public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);

            // Add employee details as needed

            return _mapper.Map<LeaveRequestDetailsDto>(leaveRequest); ;
        }
    }
}

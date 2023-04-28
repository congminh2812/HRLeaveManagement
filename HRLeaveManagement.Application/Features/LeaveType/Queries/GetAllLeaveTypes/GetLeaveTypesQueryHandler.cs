using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{

    public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper=mapper;
            _leaveTypeRepository=leaveTypeRepository;
        }

        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
        {
            // Query to database
            var leaveTypes = await _leaveTypeRepository.GetAllAsync();

            // convert dto to object
            var data = _mapper.Map<IEnumerable<LeaveTypeDto>>(leaveTypes);

           return data.ToList();
        }
    }
}

using HRLeaveManagement.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRLeaveManagement.Domain
{
    public class LeaveAllocation : BaseEntity
    {
        public int NumberOfDays { get; set; }

        [ForeignKey("LeaveTypeId")]
        public LeaveType? LeaveType { get; set; }
        public int LeaveTypeId { get; set; }    
        public int Period { get; set; } 
    }
}
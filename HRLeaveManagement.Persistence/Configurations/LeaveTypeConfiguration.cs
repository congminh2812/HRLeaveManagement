using HRLeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Persistence.Configurations
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            //builder.HasData(
            //   new List<LeaveType>
            //   {
            //        new LeaveType
            //        {
            //            Id = 1,
            //            Name = "Vacation",
            //            DefaultDays = 10,
            //            DateCreated = DateTime.Now,
            //            DateModified = DateTime.Now,
            //        },
            //        new LeaveType
            //        {
            //            Id = 2,
            //            Name = "Holidate",
            //            DefaultDays = 8,
            //            DateCreated = DateTime.Now,
            //            DateModified = DateTime.Now,
            //        },
            //   }
            //);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}

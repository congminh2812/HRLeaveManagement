using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                    new IdentityRole
                    {
                        Id = "45dbe5f7-4c55-4fa8-b1a8-11c8e1651554",
                        Name = "Employee",
                        NormalizedName = "EMPLOYEE"
                    },
                   new IdentityRole
                   {
                       Id = "faab7c9a-8705-4d46-81b2-e9ee485a426b",
                       Name = "Administrator",
                       NormalizedName = "ADMINISTRATOR"
                   }
                );
        }
    }
}

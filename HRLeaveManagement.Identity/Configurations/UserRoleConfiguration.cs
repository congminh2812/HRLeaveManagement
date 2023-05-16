using HRLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                    new IdentityUserRole<string>
                    {
                       RoleId = "faab7c9a-8705-4d46-81b2-e9ee485a426b",
                       UserId = "45dbe5f7-4c55-4fa8-b1a8-11c8e1651123",
                    },
                   new IdentityUserRole<string>
                   {
                       RoleId = "45dbe5f7-4c55-4fa8-b1a8-11c8e1651554",
                       UserId = "faab7c9a-8705-4d46-81b2-e9ee485a4123",
                   }
                );
        }
    }
}

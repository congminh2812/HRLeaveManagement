using HRLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                    new ApplicationUser
                    {
                        Id = "45dbe5f7-4c55-4fa8-b1a8-11c8e1651123",
                        Email = "admin@localhost.com",
                        NormalizedEmail = "ADMIN@LOCALHOST.COM",
                        FirstName = "System",
                        LastName = "Admin",
                        UserName = "admin@localhost.com",
                        NormalizedUserName = "ADMIN@LOCALHOST.COM",
                        PasswordHash = hasher.HashPassword(null, "Admin@123"),
                        EmailConfirmed = true,
                    },
                   new ApplicationUser
                   {
                       Id = "faab7c9a-8705-4d46-81b2-e9ee485a4123",
                       Email = "user@localhost.com",
                       NormalizedEmail = "USER@LOCALHOST.COM",
                       FirstName = "System",
                       LastName = "User",
                       UserName = "user@localhost.com",
                       NormalizedUserName = "USER@LOCALHOST.COM",
                       PasswordHash = hasher.HashPassword(null, "User@123"),
                       EmailConfirmed = true,
                   }
                );
        }
    }
}

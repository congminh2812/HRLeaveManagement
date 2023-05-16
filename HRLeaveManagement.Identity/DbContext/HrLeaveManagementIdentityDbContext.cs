using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Identity.DbContext
{
    public class HrLeaveManagementIdentityDbContext : IdentityDbContext
    {
        public HrLeaveManagementIdentityDbContext(DbContextOptions<HrLeaveManagementIdentityDbContext> otions) : base(otions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(HrLeaveManagementIdentityDbContext).Assembly);
        }
    }
}

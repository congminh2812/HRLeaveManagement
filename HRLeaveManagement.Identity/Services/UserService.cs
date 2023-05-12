using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Models.Identity;
using HRLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace HRLeaveManagement.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager=userManager;
        }

        public async Task<Employee> GetEmployee(string userId)
        {
            var employee = await _userManager.FindByIdAsync(userId);

            return new Employee
            {
                Email = employee.Email,
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
            };
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            return employees.Select(s => new Employee
            {
                Email = s.Email,
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
            }).ToList();
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace LoanApplictationApp.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
    }

}

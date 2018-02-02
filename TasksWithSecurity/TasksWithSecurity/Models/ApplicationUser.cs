using AspNetCore.Identity.DocumentDb;

namespace TasksWithSecurity.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : DocumentDbIdentityUser
    {
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoanApplictationApp.Data
{
    public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
    }
}

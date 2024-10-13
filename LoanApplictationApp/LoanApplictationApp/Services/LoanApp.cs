using LoanApplictationApp.Data;
using Microsoft.EntityFrameworkCore;

namespace LoanApplictationApp.Services
{
    public class LoanApp
    {
        private readonly ApplicationDbContext _context;
        public LoanApp(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetAllRolesAsync()
        {
            return await _context.Roles.Select(x => x.Name).Distinct().ToListAsync();
        }

        public async Task<List<string>> GetAllEmailsAsync()
        {
            return await _context.Users.Select(x => x.UserName).ToListAsync();
        }
    }
}

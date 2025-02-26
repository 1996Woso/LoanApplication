using LoanApplicationApp.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoanApplicationApp.API.Data
{
    public class LoanApplicationDbContext: DbContext
    {
        public LoanApplicationDbContext(DbContextOptions<LoanApplicationDbContext> options)
            :base(options)
        {
        }
        public DbSet<Application> Applications { get; set; }
        public DbSet<LoanProcessor> LoanProcessors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Configure 1-many relation between LoanProcessors and Applicans
            modelBuilder.Entity<LoanProcessor>()
                .HasMany(x => x.Applications)
                .WithOne(x => x.LoanProcessor)
                .HasForeignKey(x => x.LoanProcessorNo)
                .IsRequired();
        }

    }
}

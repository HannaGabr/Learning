using Microsoft.EntityFrameworkCore;
using Jobby.Domain.Models;

namespace Jobby.Infra.Persistence.EF
{
    public class JobTrackingContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobInstance> JobInstances { get; set; }

        public JobTrackingContext(DbContextOptions<JobTrackingContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                .HasKey(job => job.Id);

            modelBuilder.Entity<JobInstance>()
                .HasKey(j => j.Id);

            modelBuilder.Entity<JobInstance>()
                .HasOne<Job>()
                .WithMany()
                .HasForeignKey(j => j.JobId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

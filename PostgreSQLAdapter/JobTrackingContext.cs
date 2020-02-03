using Microsoft.EntityFrameworkCore;
using Jobby.Domain.Models;

namespace Jobby.Infra.Persistence.EF
{
    public class JobTrackingContext : DbContext
    {
        public DbSet<Job> Jobs;
        public DbSet<JobInstance> JobInstances;

        public JobTrackingContext()
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
                .WithOne()
                .HasForeignKey<JobInstance>(j => j.JobId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

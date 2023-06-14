using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Resume_Portfolio.DatabaseDesign;
using Resume_Portfolio.Interfaces;
using Resume_Portfolio.Models;

namespace Resume_Portfolio.Data;

public class JobContext : DbContext
{
    public JobContext(DbContextOptions<JobContext> options) : base(options)
    {
    }

    public DbSet<Job> Jobs { get; set; }
    public DbSet<RequiredSkill> RequiredSkills { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships and other configurations

        modelBuilder.Entity<JobApplication>()
            .HasOne(ja => ja.Job)
            .WithMany()
            .HasForeignKey(ja => ja.JobID);

        modelBuilder.Entity<Job>()
            .HasOne(j => j.Company)
            .WithMany(c => c.Jobs)
            .HasForeignKey(j => j.CompanyID);

        modelBuilder.Entity<Job>()
            .HasMany(j => j.RequiredSkills)
            .WithOne(rs => rs.Job)
            .HasForeignKey(rs => rs.JobID);

        base.OnModelCreating(modelBuilder);
    }


    public class JobContextDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IPluralizer, CustomPluralizer>();
        }
    }
}

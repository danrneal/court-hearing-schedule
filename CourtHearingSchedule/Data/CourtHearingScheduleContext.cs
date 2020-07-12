using CourtHearingSchedule.Models;
using Microsoft.EntityFrameworkCore;


namespace CourtHearingSchedule.Data
{
    public class CourtHearingScheduleContext : DbContext
    {
        public CourtHearingScheduleContext(DbContextOptions<CourtHearingScheduleContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Hearing> Hearings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Case>().ToTable("Case");
            modelBuilder.Entity<Hearing>().ToTable("Hearing");
        }
    }
}
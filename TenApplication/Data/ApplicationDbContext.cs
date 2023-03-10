using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TenApplication.Models;
using TenApplication.Models.CatModels;
using TenApplication.Models.RaportModels;

namespace TenApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, ApplicationRole, Guid>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Inbox> Inboxs { get; set; }
        public DbSet<InboxItem> InboxItems { get; set; }
        public DbSet<Raport> Raports { get; set; }
        public DbSet<RaportRecord> RaportRecords { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<CatRecord> CatRecords { get; set; }
        public DbSet<CatRecordCell> CatRecordCells { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });

            modelBuilder.Entity<User>()
                .Property(g => g.Level)
                .HasConversion(
                    v => v.ToString(),
                    v => (Level)Enum.Parse(typeof(Level), v));

            modelBuilder.Entity<Job>()
            .Property(b => b.Received).ValueGeneratedOnAdd();

            modelBuilder.Entity<Job>()
                .Property(g => g.TaskType)
                .HasConversion(
                    v => v.ToString(),
                    v => (TaskType)Enum.Parse(typeof(TaskType), v));

            modelBuilder.Entity<Job>()
                .Property(g => g.Region)
                .HasConversion(
                    v => v.ToString(),
                    v => (Region)Enum.Parse(typeof(Region), v!));

            modelBuilder.Entity<Job>()
                .Property(g => g.Software)
                .HasConversion(
                    v => v.ToString(),
                    v => (Software)Enum.Parse(typeof(Software), v));

            new DbInitializer(modelBuilder).Seed();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.EnableDetailedErrors();
        }
    }
}

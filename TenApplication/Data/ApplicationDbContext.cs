
using Microsoft.EntityFrameworkCore;
using TenApplication.Models;

namespace TenApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Designer> Designers { get; set; }
        public DbSet<Engineer> Engineers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Inbox> Inboxs { get; set; }
        public DbSet<InboxItem> InboxItems { get; set; }
        public DbSet<Raport> Raports { get; set; }
        public DbSet<RaportRecord> RaportRecords { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<CatRecord> CatRecords { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Raport>()
            .Property(b => b.RaportCreateDate).ValueGeneratedOnAdd();

            modelBuilder.Entity<Cat>()
            .Property(b => b.CatCreateDate).ValueGeneratedOnAdd();

            modelBuilder.Entity<Job>()
            .Property(b => b.Received).ValueGeneratedOnAdd();

            modelBuilder.Entity<Designer>()
                .Property(g => g.Level)
                .HasConversion(
                    v => v.ToString(),
                    v => (Level)Enum.Parse(typeof(Level), v));

            modelBuilder.Entity<Designer>()
                .Property(g => g.UserRole)
                .HasConversion(
                    v => v.ToString(),
                    v => (UserRole)Enum.Parse(typeof(UserRole), v));

            modelBuilder.Entity<Engineer>()
                .Property(g => g.Client)
                .HasConversion(
                    v => v.ToString(),
                    v => (Client)Enum.Parse(typeof(Client), v));

            modelBuilder.Entity<Job>()
                .Property(g => g.TaskType)
                .HasConversion(
                    v => v.ToString(),
                    v => (TaskType)Enum.Parse(typeof(TaskType), v));

            modelBuilder.Entity<Job>()
                .Property(g => g.Region)
                .HasConversion(
                    v => v.ToString(),
                    v => (Region)Enum.Parse(typeof(Region), v));

            modelBuilder.Entity<Job>()
                .Property(g => g.Software)
                .HasConversion(
                    v => v.ToString(),
                    v => (Software)Enum.Parse(typeof(Software), v));

            //new DbInitializer(modelBuilder).Seed();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.EnableDetailedErrors();
        }
    }
}

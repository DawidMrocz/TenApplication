using Microsoft.EntityFrameworkCore;
using TenApplication.Models;

namespace TenApplication.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;
        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public void Seed()
        {
            modelBuilder.Entity<Job>().HasData(
                new Job
                {
                    JobId = 1,
                    JobDescription = "Create muffler",
                    TaskType = TaskType.Models,
                    Software = Software.Catia,
                    Ecm = 32016495,
                    Region = Region.NA,
                    Status = 0,
                },
                new Job
                {
                    JobId = 2,
                    JobDescription = "Create pipe",
                    TaskType = TaskType.Models,
                    Software = Software.NX,
                    Ecm = 32016408,
                    Region = Region.NA,
                    Status = 10
                },
                new Job
                {
                    JobId = 3,
                    JobDescription = "Create drawing",
                    TaskType = TaskType.Drawings,
                    Software = Software.Catia,
                    Ecm = 32016497,
                    Region = Region.NA,
                    Status = 20
                },
                new Job
                {
                    JobId = 4,
                    JobDescription = "Update drawing",
                    TaskType = TaskType.Drawings,
                    Software = Software.Catia,
                    Ecm = 32016485,
                    Region = Region.NA,
                    Status = 0
                },
                new Job
                {
                    JobId = 5,
                    JobDescription = "Partition holes",
                    TaskType = TaskType.Models,
                    Software = Software.Catia,
                    Ecm = 32016464,
                    Region = Region.CN,
                    Status = 100
                },
                new Job
                {
                    JobId = 6,
                    JobDescription = "Hot end proposal",
                    TaskType = TaskType.Both,
                    Software = Software.Catia,
                    Ecm = 32016435,
                    Region = Region.NA,
                    Status = 45
                }
             );;
        }
    }
}

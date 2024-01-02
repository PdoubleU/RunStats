using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RunStats.Models;

namespace RunStats.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RunningSession>()
                .HasOne(r => r.ExerciseType)
                .WithMany()
                .HasForeignKey(r => r.ExerciseTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<RunningSession>()
                .HasOne(r => r.Weather)
                .WithMany()
                .HasForeignKey(r => r.WeatherId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<RunningSession>()
                .HasOne(r => r.Shoes)
                .WithMany()
                .HasForeignKey(r => r.ShoesId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Shoes>()
                .HasOne(r => r.ShoesType)
                .WithMany()
                .HasForeignKey(r => r.ShoesTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<RunStats.Models.RunningSession>? RunningSession { get; set; }
        public DbSet<RunStats.Models.Shoes>? Shoes { get; set; }
        public DbSet<RunStats.Models.ExerciseType>? ExerciseType { get; set; }
        public DbSet<RunStats.Models.ShoesType>? ShoesType { get; set; }
    }
}
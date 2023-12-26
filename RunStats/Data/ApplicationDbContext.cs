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
        public DbSet<RunStats.Models.RunningSession>? RunningSession { get; set; }
        public DbSet<RunStats.Models.Shoes>? Shoes { get; set; }
        public DbSet<RunStats.Models.ExcerciseType>? ExcerciseType { get; set; }
        public DbSet<RunStats.Models.ShoesType>? ShoesType { get; set; }
    }
}
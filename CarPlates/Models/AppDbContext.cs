using Microsoft.EntityFrameworkCore;

namespace CarPlates.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

		public DbSet<CarType> CarTypes { get; set; }
        public DbSet<CarState> CarStates { get; set; }
        public DbSet<CarPlate> CarPlates { get; set; }

        public DbSet<ExecutedCarState> ExecutedCarStates { get; set; }
        public DbSet<ExecutedPlate> ExecutedPlates { get; set; }
	}
}

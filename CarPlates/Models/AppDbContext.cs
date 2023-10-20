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


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<CarType>().HasData(
				new CarType { Id = 1, Type = "نقل" },
				new CarType { Id = 2, Type = "ملاكي" },
				new CarType { Id = 3, Type = "أجرة" },
				new CarType { Id = 4, Type = "مقطورة" },
				new CarType { Id = 5, Type = "حكومة" },
				new CarType { Id = 6, Type = "قطاع عام" },
				new CarType { Id = 7, Type = "معدة ثقيلة" },
				new CarType { Id = 8, Type = "محافظة" },
				new CarType { Id = 9, Type = "دراجة محافظة" },
				new CarType { Id = 10, Type = "تحت الطلب" },
				new CarType { Id = 11, Type = "مؤقت" },
				new CarType { Id = 12, Type = "جرار زراعي" },
				new CarType { Id = 13, Type = "مقطورة قطاع عام" },
				new CarType { Id = 14, Type = "أتوبيس عام" },
				new CarType { Id = 15, Type = "أتوبيس خاص" },
				new CarType { Id = 16, Type = "أتوبيس مدارس" },
				new CarType { Id = 17, Type = "أتوبيس رحلات" },
				new CarType { Id = 18, Type = "تجاري" },
				new CarType { Id = 19, Type = "دراجة" },
				new CarType { Id = 20, Type = "دراجة أجرة" },
				new CarType { Id = 21, Type = "أتوبيس سياحة" },
				new CarType { Id = 22, Type = "سياحة" },
				new CarType { Id = 23, Type = "مقطورة محافظة" },
				new CarType { Id = 24, Type = "دراجة حكومة" },
				new CarType { Id = 25, Type = "دراجة قطاع عام" },
				new CarType { Id = 26, Type = "مقطورة حكومة" },
				new CarType { Id = 27, Type = "ملحقة" }
			);

			modelBuilder.Entity<CarState>().HasData(
				new CarState { Id = 1, State = "بالحركة" },
				new CarState { Id = 2, State = "مرتجع سليم" },
				new CarState { Id = 3, State = "سليم" }
			);

			modelBuilder.Entity<ExecutedCarState>().HasData(
				new ExecutedCarState { Id = 1, State = "مرتجع تالف" },
				new ExecutedCarState { Id = 2, State = "مرتجع فاقد بالزوج" },
				new ExecutedCarState { Id = 3, State = "مرتجع فاقد بالفرد" }
			);

		}
	}
}

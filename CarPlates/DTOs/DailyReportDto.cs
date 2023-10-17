using CarPlates.Models;

namespace CarPlates.DTOs
{
	public class DailyReportDto
	{
		public string CarType { get; set; }
		public List<int> Counts { get; set; }
	}
}

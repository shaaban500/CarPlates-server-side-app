using CarPlates.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlates.DTOs
{
	public class ExecutedPlateDto
	{
		public long Id { get; set; } = 0;
		public string? Letters { get; set; }
		public string? Numbers { get; set; }
		public int ExecutionYear { get; set; }
		public int ExecutionNumber { get; set; }
		public DateTime Date { get; set; }
		public long CarTypeId { get; set; }
		public long ExecutedCarStateId { get; set; }
	}
}

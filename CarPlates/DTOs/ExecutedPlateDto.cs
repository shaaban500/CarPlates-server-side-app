using CarPlates.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlates.DTOs
{
	public class ExecutedPlateDto
	{
		public int Id { get; set; } = 0;
		public string? Letters { get; set; }
		public string? Numbers { get; set; }
		public int ExecutionYear { get; set; }
		public int ExecutionNumber { get; set; }
		public DateTime Date { get; set; }
		public int CarTypeId { get; set; }
		public int ExecutedCarStateId { get; set; }
	}
}

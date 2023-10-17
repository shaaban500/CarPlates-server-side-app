using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlates.Models
{
	public class ExecutedPlate : BaseEntity, IPlateBaseEntity
	{
		public string? Letters { get; set ; }
		public string? Numbers { get; set; }
		public int ExecutionYear { get; set; }
		public int ExecutionNumber { get; set; }
		public DateTime? Date { get; set; }

		public long CarTypeId { get; set ; }
		[ForeignKey("CarTypeId")]
		public CarType CarType { get; set; }

		public long ExecutedCarStateId { get; set; }
		[ForeignKey("ExecutedCarStateId")]
		public ExecutedCarState ExecutedCarState { get; set; }
	}
}

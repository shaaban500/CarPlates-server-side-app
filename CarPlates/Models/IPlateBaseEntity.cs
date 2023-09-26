using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlates.Models
{
	public interface IPlateBaseEntity
	{
		public string? Letters { get; set; }
		public string? Numbers { get; set; }
		public DateTime? Date { get; set; }
		public long CarTypeId { get; set; }
		[ForeignKey("CarTypeId")]
		public CarType CarType { get; set; }
	}
}

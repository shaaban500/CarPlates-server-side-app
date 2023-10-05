namespace CarPlates.DTOs
{
	public class CarFilterModel
	{
		// pagination
		public int PageIndex { get; set; } = 1;
		public int PageSize { get; set; } = 10;

		// filter
		public long? CarTypeId { get; set; }
		public long? CarStateId { get; set; }
        
		public string? Letters { get; set; }
		public string? Numbers { get; set; }
		public string? OwnerName { get; set; }
		public string? OwnerPhone { get; set; }
		public string? OwnerNationalId { get; set; }

		// only for executedPlates
		public int? ExecutionYear { get; set; }
		public int? ExecutionNumber { get; set; }
		public DateTime? Date { get; set; }
	}
}

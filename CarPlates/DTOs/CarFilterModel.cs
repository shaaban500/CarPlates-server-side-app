namespace CarPlates.DTOs
{
	public class CarFilterModel
	{
		// pagination
		public int PageIndex { get; set; }
		public int PageSize { get; set; }

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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlates.Models
{
    public class CarPlate : BaseEntity, IPlateBaseEntity
    {
        public string? Letters { get; set; }
        public string? Numbers { get; set; }
        public string? OwnerName { get; set; }
        public string? OwnerAdress { get; set; }
        public string? OwnerPhone { get; set; }
        public string? OwnerNationalId { get; set; }
        public DateTime? Date { get; set; }

		[Required]
		public long CarTypeId { get; set; }
        [ForeignKey("CarTypeId")]
        public CarType CarType { get; set; }

        public long CarStateId { get; set; }

        [Required]
        [ForeignKey("CarStateId")]
        public CarState CarState { get; set; }
    }
}

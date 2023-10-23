using CarPlates.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlates.DTOs
{
    public class CarPlateDto
    {
        public long Id { get; set; }
        public string? Letters { get; set; }
        public string? Numbers { get; set; }
        public string? OwnerName { get; set; }
        public string? OwnerAdress { get; set; }
        public string? OwnerPhone { get; set; }
        public string? OwnerNationalId { get; set; }
        public DateTime? Date { get; set; }

        public long CarTypeId { get; set; }
        public long CarStateId { get; set; }
    }
}

using CarPlates.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlates.DTOs
{
    public class CarPlateDto
    {
        public int Id { get; set; }
        public string? Letters { get; set; }
        public string? Numbers { get; set; }
        public string? OwnerName { get; set; }
        public string? OwnerAdress { get; set; }
        public string? OwnerPhone { get; set; }
        public string? OwnerNationalId { get; set; }
        public DateTime? Date { get; set; }

        public int CarTypeId { get; set; }
        public int CarStateId { get; set; }
    }
}

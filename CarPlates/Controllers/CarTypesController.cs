using CarPlates.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CarPlates.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarTypesController : Controller
    {
        private readonly AppDbContext _context;
        public CarTypesController(AppDbContext context)
        {
            _context = context;
        }


	[EnableCors("AllowSpecificOrigin")]
        [HttpGet("GetAll")]
		public async Task<IActionResult> GetAll()
        {
            var carTypes = _context.CarTypes.Where(st => st.IsDeleted != true);
            return Ok(carTypes);
        }
    }
}

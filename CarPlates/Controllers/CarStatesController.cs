using CarPlates.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarPlates.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarStatesController : Controller
    {
        private readonly AppDbContext _context;
        public CarStatesController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var carStates = _context.CarStates.Where(st => st.IsDeleted != true);
            return Ok(carStates);
        }
    }
}

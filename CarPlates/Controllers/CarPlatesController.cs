using CarPlates.DTOs;
using CarPlates.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPlates.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarPlatesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CarPlatesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(long id)
        {
            var carPlate = await _context.CarPlates.FindAsync(id);
            return Ok(carPlate);
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] CarFilterModel model)
        {
            model.CarTypeId = model.CarTypeId == 0 ? 1 : model.CarTypeId;
            model.CarStateId = model.CarStateId == 0 ? 1 : model.CarStateId;

            var carPlates = _context.CarPlates
                            .Where(x => x.IsDeleted != true &&
                                        x.CarStateId == model.CarStateId &&
                                        x.CarTypeId == model.CarTypeId)
                            .Include(x => x.CarType)
                            .Include(x => x.CarState)
                            .ToList();
            return Ok(carPlates);
        }


        [HttpPost]
        public async Task<IActionResult> AddOrEdit(CarPlateDto model)
        {
            if (model.Id == 0)
            {
                var carPlate = new CarPlate();

                carPlate.Letters = model.Letters;
                carPlate.Numbers = model.Numbers;
                carPlate.OwnerName = model.OwnerName;
                carPlate.OwnerPhone = model.OwnerPhone;
                carPlate.OwnerAdress = model.OwnerAdress;
                carPlate.OwnerNationalId = model.OwnerNationalId;
                carPlate.CarStateId = model.CarStateId;
                carPlate.CarTypeId = model.CarTypeId;
                carPlate.Date = model.Date;

                var addedCarPlate = await _context.CarPlates.AddAsync(carPlate);
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                var carPlate = await _context.CarPlates.FindAsync(model.Id);

                if (carPlate is not null)
                {
                    carPlate.Id = model.Id;
                    carPlate.Letters = model.Letters;
                    carPlate.Numbers = model.Numbers;
                    carPlate.OwnerName = model.OwnerName;
                    carPlate.OwnerPhone = model.OwnerPhone;
                    carPlate.OwnerAdress = model.OwnerAdress;
                    carPlate.OwnerNationalId = model.OwnerNationalId;
                    carPlate.CarStateId = model.CarStateId;
                    carPlate.CarTypeId = model.CarTypeId;
                    carPlate.Date = model.Date;

                    var updatedCarPlate = _context.CarPlates.Update(carPlate);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var carPlate = await _context.CarPlates.FindAsync(id);

            if (carPlate is not null)
            {
                var removedPlate = _context.CarPlates.Remove(carPlate);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }


		[HttpGet("GetDailyReport")]
		public async Task<IActionResult> GetDailyReport()
		{
			var carStates = _context.CarStates.ToList();
			var carTypes = _context.CarTypes.ToList();

            var dailyReport = new List<int>();

            foreach(var type in carTypes)
            {
                foreach(var state in carStates)
                {
                    var count = await _context.CarPlates
                                .Where(x => x.CarTypeId == type.Id && x.CarStateId == state.Id)
                                .CountAsync();
                    
                    dailyReport.Add(count);
                }
            }

            var stateCountList = new List<int>();

            foreach(var state in carStates)
            {
                var stateCount = _context.CarPlates.Where(st => st.CarStateId == state.Id).Count();
                stateCountList.Add(stateCount);
            }

            return Ok(new 
                    {   
                        carStates = carStates, 
                        carTypes = carTypes,
                        dailyReport = dailyReport,
                        stateCountList = stateCountList
                    });
		}


	}
}
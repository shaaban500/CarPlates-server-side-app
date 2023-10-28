using CarPlates.DTOs;
using CarPlates.Models;
using Microsoft.AspNetCore.Cors;
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

	[EnableCors("AllowSpecificOrigin")]
		[HttpGet("getById")]
		public async Task<IActionResult> GetById(long id)
		{
			var carPlate = await _context.CarPlates.FindAsync(id);
			return Ok(carPlate);
		}


	[EnableCors("AllowSpecificOrigin")]
		[HttpPost("GetAll")]
		public async Task<IActionResult> GetAll([FromBody] CarFilterModel model)
		{
			var carPlates = _context.CarPlates.Where(c => c.IsDeleted != true).AsQueryable();

			carPlates = !string.IsNullOrWhiteSpace(model.OwnerPhone) && !string.IsNullOrEmpty(model.OwnerPhone) ? carPlates.Where(c => c.OwnerPhone == model.OwnerPhone) : carPlates;
			carPlates = !string.IsNullOrWhiteSpace(model.OwnerName) && !string.IsNullOrEmpty(model.OwnerName) ? carPlates.Where(c => c.OwnerName == model.OwnerName) : carPlates;
			carPlates = !string.IsNullOrWhiteSpace(model.OwnerNationalId) && !string.IsNullOrEmpty(model.OwnerNationalId) ? carPlates.Where(c => c.OwnerNationalId == model.OwnerNationalId) : carPlates;
			carPlates = !string.IsNullOrWhiteSpace(model.Letters) && !string.IsNullOrEmpty(model.Letters) ? carPlates.Where(c => c.Letters == model.Letters) : carPlates;
			carPlates = !string.IsNullOrWhiteSpace(model.Numbers) && !string.IsNullOrEmpty(model.Numbers) ? carPlates.Where(c => c.Numbers == model.Numbers) : carPlates;
			carPlates = model.CarTypeId is not null ? carPlates.Where(c => c.CarTypeId == model.CarTypeId) : carPlates;
			carPlates = model.CarStateId is not null ? carPlates.Where(c => c.CarStateId == model.CarStateId) : carPlates;
			carPlates = carPlates.Include(c => c.CarType).Include(c => c.CarState);

			carPlates = carPlates.Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize);

			return Ok(await carPlates.ToListAsync());
		}


	[EnableCors("AllowSpecificOrigin")]
		[HttpPost]
		public async Task<IActionResult> AddOrEdit(CarPlateDto model)
		{
			var isRepeatedPlate = _context.CarPlates.Where(x => x.Letters == model.Letters && x.Numbers == model.Numbers && x.IsDeleted != true);

			if(isRepeatedPlate != null)
			{
				return BadRequest();
			}

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


		[EnableCors("AllowSpecificOrigin")]
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


	}
}
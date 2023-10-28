using CarPlates.DTOs;
using CarPlates.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPlates.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ExecutedPlatesController : Controller
	{
		private readonly AppDbContext _context;
		public ExecutedPlatesController(AppDbContext context)
		{
			_context = context;
		}

	[EnableCors("AllowSpecificOrigin")]
		[HttpGet("getById")]
		public async Task<IActionResult> GetById(long id)
		{
			var carPlate = await _context.ExecutedPlates.FindAsync(id);
			return Ok(carPlate);
		}

	[EnableCors("AllowSpecificOrigin")]
		[HttpPost("GetAll")]
		public async Task<IActionResult> GetAll([FromBody] CarFilterModel model)
		{
			var executedPlates = _context.ExecutedPlates.Where(c => c.IsDeleted != true).AsQueryable();

			executedPlates = !string.IsNullOrWhiteSpace(model.Letters) && !string.IsNullOrEmpty(model.Letters) ? executedPlates.Where(c => c.Letters == model.Letters) : executedPlates;
			executedPlates = !string.IsNullOrWhiteSpace(model.Numbers) && !string.IsNullOrEmpty(model.Numbers) ? executedPlates.Where(c => c.Numbers == model.Numbers) : executedPlates;
			executedPlates = model.CarTypeId is not null ? executedPlates.Where(c => c.CarTypeId == model.CarTypeId) : executedPlates;
			executedPlates = model.CarStateId is not null ? executedPlates.Where(c => c.ExecutedCarStateId == model.CarStateId) : executedPlates;
			executedPlates = model.ExecutionYear is not null ? executedPlates.Where(c => c.ExecutionYear == model.ExecutionYear) : executedPlates;
			executedPlates = model.ExecutionNumber is not null ? executedPlates.Where(c => c.ExecutionNumber == model.ExecutionNumber) : executedPlates;
			
			
			executedPlates = executedPlates.Include(c => c.CarType).Include(c => c.ExecutedCarState);

			executedPlates = executedPlates.Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize);

			return Ok(await executedPlates.ToListAsync());
		}


		[EnableCors("AllowSpecificOrigin")]
		[HttpPost]
		public async Task<IActionResult> AddOrEdit(ExecutedPlateDto model)
		{
			var isRepeatedPlate = _context.ExecutedPlates.Where(x => x.Letters == model.Letters && x.Numbers == model.Numbers && x.IsDeleted != true);

			if (isRepeatedPlate != null)
			{
				return BadRequest();
			}

			if (model.Id == 0)
			{
				var executedPlate = new ExecutedPlate();

				executedPlate.Date = model.Date;
				executedPlate.Letters = model.Letters;
				executedPlate.Numbers = model.Numbers;
				executedPlate.CarTypeId = model.CarTypeId;
				executedPlate.ExecutionYear = model.ExecutionYear;
				executedPlate.ExecutionNumber = model.ExecutionNumber;
				executedPlate.ExecutedCarStateId = model.ExecutedCarStateId;

				var addedPlate = await _context.ExecutedPlates.AddAsync(executedPlate);
				await _context.SaveChangesAsync();

				return Ok();
			}
			else
			{
				var plate = await _context.ExecutedPlates.FindAsync(model.Id);

				if (plate is not null)
				{
					plate.Id = model.Id;
					plate.Date = model.Date;
					plate.Letters = model.Letters;
					plate.Numbers = model.Numbers;
					plate.CarTypeId = model.CarTypeId;
					plate.ExecutionYear = model.ExecutionYear;
					plate.ExecutionNumber = model.ExecutionNumber;
					plate.ExecutedCarStateId = model.ExecutedCarStateId;

					var updatedPlate = _context.ExecutedPlates.Update(plate);
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
			var executedPlate = await _context.ExecutedPlates.FindAsync(id);

			if (executedPlate is not null)
			{
				var removedPlate = _context.ExecutedPlates.Remove(executedPlate);
				await _context.SaveChangesAsync();
				return Ok();
			}
			
			return BadRequest();
		}

	}
}

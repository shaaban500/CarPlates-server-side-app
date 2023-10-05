using CarPlates.DTOs;
using CarPlates.Models;
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

		[HttpGet("getById")]
		public async Task<IActionResult> GetById(long id)
		{
			var carPlate = await _context.ExecutedPlates.FindAsync(id);
			return Ok(carPlate);
		}

		[HttpPost("GetAll")]
		public async Task<IActionResult> GetAll([FromQuery] CarFilterModel model)
		{
			var executedPlates = _context.ExecutedPlates.Where(c => c.IsDeleted != true).AsQueryable();

			executedPlates = model.Letters is not null ? executedPlates.Where(c => c.Letters == model.Letters) : executedPlates;
			executedPlates = model.Numbers is not null ? executedPlates.Where(c => c.Numbers == model.Numbers) : executedPlates;
			executedPlates = model.CarTypeId is not null ? executedPlates.Where(c => c.CarTypeId == model.CarTypeId) : executedPlates;
			executedPlates = model.Date is not null ? executedPlates.Where(c => c.Date == model.Date) : executedPlates;
			executedPlates = model.ExecutionYear is not null ? executedPlates.Where(c => c.ExecutionYear == model.ExecutionYear) : executedPlates;
			executedPlates = model.ExecutionNumber is not null ? executedPlates.Where(c => c.ExecutionNumber == model.ExecutionNumber) : executedPlates;

			executedPlates = executedPlates.Include(c => c.CarType);

			return Ok(await executedPlates.ToListAsync());
		}


		[HttpPost]
		public async Task<IActionResult> AddOrEdit(ExecutedPlateDto model)
		{
			if (model.Id == 0)
			{
				var executedPlate = new ExecutedPlate();

				executedPlate.Date = model.Date;
				executedPlate.Letters = model.Letters;
				executedPlate.Numbers = model.Numbers;
				executedPlate.CarTypeId = model.CarTypeId;
				executedPlate.ExecutionYear = model.ExecutionYear;
				executedPlate.ExecutionNumber = model.ExecutionNumber;

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
					plate.Letters = model.Letters;
					plate.Numbers = model.Numbers;
					plate.CarTypeId = model.CarTypeId;
					plate.Date = model.Date;

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

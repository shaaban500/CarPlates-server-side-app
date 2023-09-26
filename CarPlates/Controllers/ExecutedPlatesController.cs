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
		public async Task<IActionResult> GetAll([FromQuery] ExecutedPlateDto model)
		{
			model.CarTypeId = model.CarTypeId == 0 ? 1 : model.CarTypeId;
			model.ExecutionNumber = model.ExecutionNumber == 0 ? 1 : model.ExecutionNumber;
			model.ExecutionYear = model.ExecutionYear == 0 ? 1 : model.ExecutionYear;

			var executedPlates = _context.ExecutedPlates
							.Where(x => x.IsDeleted != true &&
										x.CarTypeId == model.CarTypeId &&
										x.ExecutionYear == model.ExecutionYear &&
										x.ExecutionNumber == model.ExecutionNumber)
							.Include(x => x.CarType)
							.ToList();

			return Ok(executedPlates);
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
		public async Task Delete(long id)
		{
			var executedPlate = await _context.ExecutedPlates.FindAsync(id);

			if (executedPlate is not null)
			{
				var removedPlate = _context.ExecutedPlates.Remove(executedPlate);
				await _context.SaveChangesAsync();
			}
		}

	}
}

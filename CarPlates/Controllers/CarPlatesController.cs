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
			var carTypes = _context.CarTypes.ToList();
			var carStates = _context.CarStates.ToList();
			var executedCarStates = _context.ExecutedCarStates.ToList();

			var allStates = new List<string>();
			var dailyReportDto = new List<DailyReportDto>();

			int sum = 0;

			foreach (var type in carTypes)
			{
				sum = 0;
				var report = new DailyReportDto
				{
					CarType = type.Type,
					Counts = new List<int>()
				};

				foreach (var state in carStates)
				{
					var count = await _context.CarPlates
								.Where(x => x.CarTypeId == type.Id && x.CarStateId == state.Id && x.IsDeleted != true)
								.CountAsync();

					report.Counts.Add(count);
					sum += count;
				}
				foreach (var state in executedCarStates)
				{
					var count = await _context.ExecutedPlates
								.Where(x => x.CarTypeId == type.Id && x.ExecutedCarStateId == state.Id && x.IsDeleted != true)
								.CountAsync();

					report.Counts.Add(count);
					sum += count;
				}

				report.Counts.Add(sum);

				dailyReportDto.Add(report);
			}




			sum = 0;
			var totalCountsReport = new DailyReportDto
			{
				CarType = "الإجمالي",
				Counts = new List<int>()
			};

			foreach (var state in carStates)
			{
				var count = await _context.CarPlates
							.Where(x => x.CarStateId == state.Id && x.IsDeleted != true)
							.CountAsync();

				totalCountsReport.Counts.Add(count);
				sum += count;

				allStates.Add(state.State);
			}
			foreach (var state in executedCarStates)
			{
				var count = await _context.ExecutedPlates
							.Where(x => x.ExecutedCarStateId == state.Id && x.IsDeleted != true)
							.CountAsync();

				totalCountsReport.Counts.Add(count);
				sum += count;

				allStates.Add(state.State);
			}

			totalCountsReport.Counts.Add(sum);

			allStates.Add("الإجمالي");

			dailyReportDto.Add(totalCountsReport);

			return Ok(new
			{
				carStates = allStates,
				dailyReport = dailyReportDto,
			});
		}


	}
}
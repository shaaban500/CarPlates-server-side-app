using CarPlates.DTOs;
using CarPlates.Models;
using CarPlates.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPlates.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DailyReportController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IDailyReportServices _dailyReportServices;
		public DailyReportController(AppDbContext context, IDailyReportServices dailyReportServices)
		{
			_context = context;
			_dailyReportServices = dailyReportServices;
		}


		[EnableCors("AllowSpecificOrigin")]
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

					int previousCount = await _dailyReportServices.GetDefaultValues(type.Id, state.Id);
					count += previousCount;

					report.Counts.Add(count);

					sum += count;
				}
				foreach (var state in executedCarStates)
				{
					var count = await _context.ExecutedPlates
								.Where(x => x.CarTypeId == type.Id && x.ExecutedCarStateId == state.Id && x.IsDeleted != true)
								.CountAsync();

					int previousCount = await _dailyReportServices.GetDefaultValues(type.Id, carStates.Count() + state.Id);
					count += previousCount;

					report.Counts.Add(count);

					sum += count;
				}

				report.Counts.Add(sum);

				dailyReportDto.Add(report);
			}



			// last displayed row at the report showing the total for each state
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

				int previousCount = await _dailyReportServices.GetDefaultValues(state.Id);
				count += previousCount;

				totalCountsReport.Counts.Add(count);
				sum += count;

				allStates.Add(state.State);
			}
			foreach (var state in executedCarStates)
			{
				var count = await _context.ExecutedPlates
							.Where(x => x.ExecutedCarStateId == state.Id && x.IsDeleted != true)
							.CountAsync();

				int previousCount = await _dailyReportServices.GetDefaultValues(carStates.Count() + state.Id);
				count += previousCount;

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

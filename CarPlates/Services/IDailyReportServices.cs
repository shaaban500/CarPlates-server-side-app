using System.Numerics;

namespace CarPlates.Services
{
	public interface IDailyReportServices
	{
		Task<int> GetDefaultValues(long carTypeId, long carStateId);
		Task<int> GetDefaultValues(long carStateId);
	}
}

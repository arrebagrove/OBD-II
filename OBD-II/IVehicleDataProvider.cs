using System.Collections.Generic;
using System.Threading.Tasks;

namespace TTGStudios.OBDII
{
	public interface IVehicleDataProvider
	{
		Task ConnectAsync();
		Task<IEnumerable<string>> GetDtcsAsync();
		Task ClearDtcsAsync();
	}
}

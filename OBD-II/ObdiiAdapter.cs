using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTGStudios.OBDII;

namespace TTGStudios.OBDII
{
	// This is the base type of any class that will communicate directly to a physical OBD-II adapter.
	public abstract class ObdiiAdapter : IVehicleDataProvider
	{
		public async virtual Task ConnectAsync()
		{
			await Initialize();
		}

		async Task Initialize()
		{
			await SendCommand("atz");
		}

		public async Task<string> GetVinAsync()
		{
			return await SendCommand("atz");
		}

		public async Task<IEnumerable<string>> GetDtcsAsync()
		{
			string response = await SendCommand("03");
			return new string[] { };
		}

		public async Task ClearDtcsAsync()
		{
			await SendCommand("04");
		}

		protected abstract Task<string> SendCommand(string command);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTGStudios.OBDII;

namespace OBDII
{
	public abstract class AdapterDataProvider : IVehicleDataProvider
	{
		public abstract Task ConnectAsync();

		public async Task<string> GetVinAsync()
		{
			await WriteAsync("ATZ");
			return await WriteAsync("");
		}

		public Task<IEnumerable<object>> GetDtcsAsync()
		{
			throw new NotImplementedException();
		}

		public Task ClearDtcsAsync()
		{
			throw new NotImplementedException();
		}

		protected abstract Task<string> WriteAsync(string command);
	}
}

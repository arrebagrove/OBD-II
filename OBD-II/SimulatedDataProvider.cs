using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TTGStudios.OBDII
{
	public class SimulatedDataProvider : IVehicleDataProvider
	{
		public Task ConnectAsync()
		{
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			tcs.SetResult(null);
			return tcs.Task;
		}

		public Task<string> GetVinAsync()
		{
			TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
			tcs.SetResult("1G2WP1212WF340276");
			return tcs.Task;
		}

		public Task<IEnumerable<object>> GetDtcsAsync()
		{
			TaskCompletionSource<IEnumerable<object>> tcs = new TaskCompletionSource<IEnumerable<object>>();
			tcs.SetResult(null);
			return tcs.Task;
		}

		public Task ClearDtcsAsync()
		{
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			tcs.SetResult(null);
			return tcs.Task;
		}
	}
}

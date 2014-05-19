using System.Collections.Generic;
using System.Threading.Tasks;

namespace TTGStudios.OBDII
{
	public class SimulatedDataProvider : IVehicleDataProvider
	{
		public SimulatedDataProvider()
		{
			_codes = new List<string>(new string[] { "P0440" });
		}

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

		public Task<IEnumerable<string>> GetDtcsAsync()
		{
			TaskCompletionSource<IEnumerable<string>> tcs = new TaskCompletionSource<IEnumerable<string>>();
			tcs.SetResult(_codes);
			return tcs.Task;
		}

		List<string> _codes;

		public Task ClearDtcsAsync()
		{
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			_codes.Clear();
			tcs.SetResult(null);
			return tcs.Task;
		}
	}
}

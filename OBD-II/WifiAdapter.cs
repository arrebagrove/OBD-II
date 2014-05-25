using System;
using System.Threading.Tasks;

namespace TTGStudios.OBDII
{
	public class WifiAdapter : ObdiiAdapter
	{
		public override Task ConnectAsync()
		{
			throw new NotImplementedException();
		}

		protected async override Task WriteAsync(string command)
		{
			throw new NotImplementedException();
		}

		protected async override Task<string> ReadAsync(TimeSpan delayTimeSpan)
		{
			throw new NotImplementedException();
		}
	}
}

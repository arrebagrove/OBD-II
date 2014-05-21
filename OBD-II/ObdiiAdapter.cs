using System.Collections.Generic;
using System.Threading.Tasks;
using TTGStudios.OBDII.Protocols;

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
			// Reset adapter.
			await SendCommand("atz");

			// Automatically detect protocol.
			await SendCommand("atsp0");

			// Get detected protocol.
			string response = await SendCommand("atdpn");
#error Set _protocol based on atdp or atdpn response.
		}

		IObdiiProtocol _protocol;

		public async Task<IEnumerable<string>> GetDtcsAsync()
		{
			string response = await SendCommand("03");
			return new string[] { };
		}

		public async Task ClearDtcsAsync()
		{
			await SendCommand("04");
		}

#error Implement SendCommand here and create abstract methods Write(string) and Read(Func<string, bool> isDone).
		protected abstract Task<string> SendCommand(string command);
	}
}

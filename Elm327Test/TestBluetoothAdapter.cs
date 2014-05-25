using System.Threading.Tasks;
using TTGStudios.OBDII.WP8;
using Windows.Networking;

namespace Elm327Test
{
	public class TestBluetoothAdapter : BluetoothAdapter
	{
		public TestBluetoothAdapter(HostName hostName) : base(hostName)
		{
		}

		public Task<string> SendCommandPassthroughAsync(string command)
		{
			return base.SendCommandAsync(command);
		}
	}
}

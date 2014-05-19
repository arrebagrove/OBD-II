using System;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace TTGStudios.OBDII.WP8
{
	public class BluetoothAdapter : ObdiiAdapter
	{
		public BluetoothAdapter(HostName hostName)
		{
			_hostName = hostName;
		}

		HostName _hostName;

		public async override Task ConnectAsync()
		{
			DisposeSocket();
			_socket = new StreamSocket();
			await _socket.ConnectAsync(_hostName, "1");
		}

		StreamSocket _socket;

		protected async override Task<string> SendCommand(string command)
		{
			// Terminate with 0x0D (LF).
			if (!command.EndsWith("\r"))
			{
				command += "\r";
			}

			using (DataWriter writer = new DataWriter(_socket.OutputStream))
			{
				writer.WriteString(command);
				await writer.StoreAsync();
				await writer.FlushAsync();
				writer.DetachStream();
			}

			using (DataReader reader = new DataReader(_socket.InputStream))
			{
				reader.InputStreamOptions = InputStreamOptions.Partial;

				await Task.Delay(1000);

				await reader.LoadAsync(1024);
				string response = reader.ReadString(reader.UnconsumedBufferLength);
				reader.DetachStream();

				return response;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				DisposeSocket();
			}
		}

		private void DisposeSocket()
		{
			if (_socket != null)
			{
				_socket.Dispose();
				_socket = null;
			}
		}
	}
}

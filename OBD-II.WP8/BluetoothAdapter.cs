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

			await base.ConnectAsync();
		}

		StreamSocket _socket;

		protected async override Task WriteAsync(string command)
		{
			using (DataWriter writer = new DataWriter(_socket.OutputStream))
			{
				writer.WriteString(command);
				await writer.StoreAsync();
				await writer.FlushAsync();
				writer.DetachStream();
			}
		}

		protected async override Task<string> ReadAsync(TimeSpan delayTimeSpan)
		{
			await Task.Delay(delayTimeSpan);

			using (DataReader reader = new DataReader(_socket.InputStream))
			{
				reader.InputStreamOptions = InputStreamOptions.Partial;
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

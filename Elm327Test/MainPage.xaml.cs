using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Elm327Test
{
	public partial class MainPage : PhoneApplicationPage
	{
		// Constructor
		public MainPage()
		{
			InitializeComponent();

			Loaded += MainPage_Loaded;
		}

		async void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			PeerFinder.AlternateIdentities["Bluetooth:Paired"] = "";
			IReadOnlyList<PeerInformation> pairedDevices = null;

			try
			{
				pairedDevices = await PeerFinder.FindAllPeersAsync();
			}
			catch (Exception exception)
			{
				if (exception.HResult == -2147023729)
				{
					pairedDevices = new List<PeerInformation>().AsReadOnly();
				}
				else
				{
					throw;
				}
			}

			try
			{
				var obdlink = pairedDevices.SingleOrDefault(device => device.DisplayName == "OBDLink MX");

				if (obdlink != null)
				{
					_socket = new StreamSocket();
					await _socket.ConnectAsync(obdlink.HostName, "1");
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButton.OK);
			}
		}

		StreamSocket _socket;

		private async void SendButton_Click(object sender, RoutedEventArgs e)
		{
				string command = commandTextBox.Text + "\n";
				commandTextBox.Text = string.Empty;
				resultsTextBox.Text += command;

			try
			{
				using (DataWriter writer = new DataWriter(_socket.OutputStream))
				{
					writer.WriteString(command);
					await writer.StoreAsync();
					await writer.FlushAsync();
					writer.DetachStream();
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message + "\n\n" + exception.StackTrace, exception.GetType().ToString(), MessageBoxButton.OK);
			}

			try
			{
				using (DataReader reader = new DataReader(_socket.InputStream))
				{
					await reader.LoadAsync(1024);
					string response = reader.ReadString(1);
					resultsTextBox.Text += response;
					response = reader.ReadString(1);
					resultsTextBox.Text += response;
					response = reader.ReadString(1);
					resultsTextBox.Text += response;
					response = reader.ReadString(1);
					resultsTextBox.Text += response;
					response = reader.ReadString(1);
					resultsTextBox.Text += response;
					response = reader.ReadString(1);
					resultsTextBox.Text += response;
					reader.DetachStream();
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message + "\n\n" + exception.StackTrace, exception.GetType().ToString(), MessageBoxButton.OK);
			}
		}
	}
}
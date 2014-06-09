using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Windows.Networking.Proximity;
using Windows.Storage.Streams;

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
					_adapter = new TestBluetoothAdapter(obdlink.HostName);
					await _adapter.ConnectAsync();
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButton.OK);
			}
		}

		TestBluetoothAdapter _adapter;

		private async void SendButton_Click(object sender, RoutedEventArgs e)
		{
			string command = commandTextBox.Text;
			commandTextBox.Text = string.Empty;
			resultsTextBox.Text += command + "\n";

			try
			{
				if (command == "03")
				{
					var codes = await _adapter.GetDtcsAsync();
					foreach (string code in codes)
					{
						resultsTextBox.Text += code + "\n";
					}
				}
				else
				{
					resultsTextBox.Text += await _adapter.SendCommandPassthroughAsync(command) + "\n";
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButton.OK);
			}
			finally
			{
				resultsTextBox.Text += ">";
			}
		}
	}
}
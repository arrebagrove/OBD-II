﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TTGStudios.OBDII.Exceptions;
using TTGStudios.OBDII.Protocols;

namespace TTGStudios.OBDII
{
	// This is the base type of any class that will communicate directly to a physical OBD-II adapter.
	public abstract class ObdiiAdapter : IVehicleDataProvider
	{
		public async virtual Task ConnectAsync()
		{
			await InitializeAsync();
		}

		async Task InitializeAsync()
		{
			// Reset adapter.
			await SendCommandAsync("atz");

			// Echo command for verification.
			await SendCommandAsync("ate1");

			// Automatically detect protocol.
			await SendCommandAsync("atsp0");

			// Get detected protocol.
			string response = await SendCommandAsync("atdpn");
		}

		IObdiiProtocol _protocol;

		public async Task<IEnumerable<string>> GetDtcsAsync()
		{
			string response = await SendCommandAsync("03");
			return new string[] { };
		}

		public async Task ClearDtcsAsync()
		{
			await SendCommandAsync("04");
		}

		protected async virtual Task<string> SendCommandAsync(string command)
		{
			// Terminate with 0x0D (LF).
			if (!command.EndsWith("\r"))
			{
				command += "\r";
			}

			await WriteAsync(command);

			// Read the response until it ends with >.
			string response = await ReadAsync(TimeSpan.Zero);
			while (!response.EndsWith(">"))
			{
				response += await ReadAsync(TimeSpan.FromMilliseconds(50));
			}

			// Parse the response. Look for 1. the command echoed back correctly,
			// 2. the effective response and 3. the prompt character >.
			string rawCommand = command.Trim(new char[] { '\r', '\n' });
			string responseFormat = string.Format(
				CultureInfo.InvariantCulture,
				"^{0}[\\r\\n]+(.*)[\\r\\n]+>$",
				rawCommand);
			Regex regex = new Regex(responseFormat, RegexOptions.CultureInvariant | RegexOptions.Multiline);
			Match match = regex.Match(response);

			if (match.Success)
			{
				// Regex matched which means command was echoed back correctly.
				response = match.Groups[1].Captures[0].Value.Trim(new char[] { '\r', '\n' });

				if (response.StartsWith("?"))
				{
					// The command was not understood.
					string message = string.Format(
						CultureInfo.InvariantCulture,
						"Sent command: {0}; response: {1}",
						rawCommand,
						response);
					throw new CommandNotUnderstoodException(message);
				}
			}
			else
			{
				// Command echoed back is not what was sent.
				string message = string.Format(
					CultureInfo.InvariantCulture,
					"Sent command: {0}; response: {1}",
					rawCommand,
					response);
				throw new CommunicationsException(message);
			}

			return response;
		}

		protected abstract Task WriteAsync(string command);
		protected abstract Task<string> ReadAsync(TimeSpan delayTimeSpan);
	}
}

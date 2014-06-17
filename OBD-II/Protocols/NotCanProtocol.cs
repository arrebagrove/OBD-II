using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TTGStudios.OBDII.Protocols
{
	public class NotCanProtocol : ProtocolCommon
	{
		protected override IEnumerable<ushort> GetTroubleCodeValues(string response)
		{
			response = StripOffCommand("43", response);

			// Pull out pairs of bytes.
			Regex regex = new Regex("(?:([0-9a-f]{2})\\s*){2}", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
			MatchCollection matches = regex.Matches(response);

			foreach (Match m in matches)
			{
				if (m.Success)
				{
					string hexWordString = m.Groups[1].Captures[0].Value + m.Groups[1].Captures[1].Value;
					ushort value = 0;
					for (int n = 0; n < 4; n++)
					{
						value = (ushort)((value << 4) + (ushort)HexCharToNibble(hexWordString[n]));
					}

					if (value != 0)
					{
						yield return value;
					}
				}
			}
		}

		private static string StripOffCommand(string command, string response)
		{
			// Strip off the echoed command and take the rest.
			string pattern = string.Format(
				CultureInfo.InvariantCulture,
				"^\\s*{0}\\s+(.*)",
				command);
			Regex regex = new Regex(pattern);
			Match match = regex.Match(response);

			if (match.Success && (match.Groups.Count >= 2))
			{
				return match.Groups[1].Value;
			}
			else
			{
				string message = string.Format(
					"input = \"{0}\", match.success = {1}, match.Groups.Count = {2}",
					response,
					match.Success,
					match.Groups.Count);
				for (int group = 0; group < match.Groups.Count; group++)
				{
					message += string.Format(
						"\nmatch.Groups[{0}].Value = {1}",
						group,
						match.Groups[group].Value);
				}
				throw new Exception(message);
			}
		}

		private byte HexCharToNibble(char hex)
		{
			switch (hex)
			{
				case '0':
					return 0;
				case '1':
					return 1;
				case '2':
					return 2;
				case '3':
					return 3;
				case '4':
					return 4;
				case '5':
					return 5;
				case '6':
					return 6;
				case '7':
					return 7;
				case '8':
					return 8;
				case '9':
					return 9;
				case 'a':
				case 'A':
					return 10;
				case 'b':
				case 'B':
					return 11;
				case 'c':
				case 'C':
					return 12;
				case 'd':
				case 'D':
					return 13;
				case 'e':
				case 'E':
					return 14;
				case 'f':
				case 'F':
					return 15;
				default:
					throw new InvalidOperationException("HexCharToNibble encountered char '" + hex + "'");
			}
		}
	}
}

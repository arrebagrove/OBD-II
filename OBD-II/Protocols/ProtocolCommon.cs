using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TTGStudios.OBDII.Protocols
{
	public abstract class ProtocolCommon : IObdiiProtocol
	{
		public virtual IEnumerable<string> InterpretTroubleCodesResponse(string response)
		{
			IEnumerable<ushort> validValues = GetTroubleCodeValues(response).Where(value => value != 0);
			return validValues.Select(TroubleCodeValueToName);
		}

		private static string TroubleCodeValueToName(ushort value)
		{
			ushort categoryMask = 0xC000;
			ushort category = (ushort)((value & categoryMask) >> 14);
			value &= (ushort)(~categoryMask);

			return string.Format(
				CultureInfo.InvariantCulture,
				"{0}{1:X4}",
				GetCategoryCode(category),
				value);
		}

		private static string GetCategoryCode(ushort category)
		{
			string code = "";
			switch (category)
			{
				case 0:
					code = "P";
					break;

				case 1:
					code = "C";
					break;

				case 2:
					code = "B";
					break;

				case 3:
					code = "U";
					break;
			}

			return code;
		}

		protected abstract IEnumerable<ushort> GetTroubleCodeValues(string response);
	}
}

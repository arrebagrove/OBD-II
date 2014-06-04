using System.Collections.Generic;

namespace TTGStudios.OBDII.Protocols
{
	public class CanProtocol : ProtocolCommon
	{
		protected override IEnumerable<ushort> GetTroubleCodeValues(string response)
		{
			return new ushort[] { };
		}
	}
}

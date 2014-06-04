using System.Collections.Generic;

namespace TTGStudios.OBDII.Protocols
{
	public class NotCanProtocol : ProtocolCommon
	{
		protected override IEnumerable<ushort> GetTroubleCodeValues(string response)
		{
			return new ushort[] { };
		}
	}
}

using System.Collections.Generic;

namespace TTGStudios.OBDII.Protocols
{
	public interface IObdiiProtocol
	{
		IEnumerable<string> InterpretTroubleCodesResponse(string response);
	}
}

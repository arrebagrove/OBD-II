namespace TTGStudios.OBDII.Protocols
{
	public interface IObdiiProtocol
	{
		string[] IntrepretTroubleCodesResponse(string response, bool echo, string command);
	}
}

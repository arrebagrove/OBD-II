﻿using System;
using System.Threading.Tasks;

namespace TTGStudios.OBDII
{
	public class SerialAdapter : ObdiiAdapter
	{
		public override Task ConnectAsync()
		{
			throw new NotImplementedException();
		}

		protected override Task<string> SendCommand(string command)
		{
			throw new NotImplementedException();
		}
	}
}

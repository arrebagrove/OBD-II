﻿using System;
using System.Threading.Tasks;

namespace TTGStudios.OBDII
{
	public class BluetoothAdapter : AdapterDataProvider
	{
		public override Task ConnectAsync()
		{
			throw new NotImplementedException();
		}

		protected override Task<string> WriteAsync(string command)
		{
			throw new NotImplementedException();
		}
	}
}
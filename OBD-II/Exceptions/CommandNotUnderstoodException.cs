﻿using System;

namespace TTGStudios.OBDII.Exceptions
{
	public class CommandNotUnderstoodException : Exception
	{
		// Summary:
		//     Initializes a new instance of the CommandNotUnderstoodException class.
		public CommandNotUnderstoodException()
			: base()
		{
		}

		//
		// Summary:
		//     Initializes a new instance of the CommandNotUnderstoodException class with a specified
		//     error message.
		//
		// Parameters:
		//   message:
		//     The message that describes the error.
		public CommandNotUnderstoodException(string message)
			: base(message)
		{
		}

		//
		// Summary:
		//     Initializes a new instance of the CommandNotUnderstoodException class with a specified
		//     error message and a reference to the inner exception that is the cause of
		//     this exception.
		//
		// Parameters:
		//   message:
		//     The error message that explains the reason for the exception.
		//
		//   innerException:
		//     The exception that is the cause of the current exception, or a null reference
		//     (Nothing in Visual Basic) if no inner exception is specified.
		public CommandNotUnderstoodException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}

using System;
using SmallScript.Shared.Exceptions;

namespace SmallScript.PolishWriteback.Executor.Exceptions
{
	public class RuntimeException : SmallScriptException
	{
		public RuntimeException()
		{
		}

		public RuntimeException(string message) : base(message)
		{
		}

		public RuntimeException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
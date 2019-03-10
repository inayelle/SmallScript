using System;

namespace SmallScript.Shared.Exceptions
{
	public abstract class SmallScriptException : Exception
	{
		protected SmallScriptException()
		{
		}

		protected SmallScriptException(string message) : base(message)
		{
		}

		protected SmallScriptException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
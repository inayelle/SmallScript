using System;
using SmallScript.Shared.Exceptions;

namespace SmallScript.Grammars.BackusNaur.Parser.Exceptions
{
	public class InvalidGrammarSyntaxException : SmallScriptException
	{
		public InvalidGrammarSyntaxException()
		{
		}

		public InvalidGrammarSyntaxException(string message) : base(message)
		{
		}

		public InvalidGrammarSyntaxException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
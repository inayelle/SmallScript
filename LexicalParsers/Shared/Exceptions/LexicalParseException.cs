using System;
using SmallScript.Shared.Details.Navigation;
using SmallScript.Shared.Exceptions;

namespace SmallScript.LexicalParsers.Shared.Exceptions
{
	public class LexicalParseException : SmallScriptException
	{
		public FilePosition OccuredAt { get; }

		public LexicalParseException(FilePosition occuredAt)
		{
			OccuredAt = occuredAt;
		}

		public LexicalParseException(string message, FilePosition occuredAt) : base(message)
		{
			OccuredAt = occuredAt;
		}

		public LexicalParseException(string message, Exception innerException, FilePosition occuredAt)
				: base(message, innerException)
		{
			OccuredAt = occuredAt;
		}
	}
}
using System;
using SmallScript.Shared.Details.Navigation;
using SmallScript.Shared.Exceptions;

namespace SmallScript.Grammars.Shared.Exceptions
{
	public class GrammarParseException : SmallScriptException
	{
		public GrammarParseException(FilePosition occuredAt)
		{
			OccuredAt = occuredAt;
		}

		public GrammarParseException(FilePosition occuredAt, string message) : base(message)
		{
			OccuredAt = occuredAt;
		}

		public GrammarParseException(FilePosition occuredAt, Exception inner) : base(inner.Message, inner)
		{
			OccuredAt = occuredAt;
		}

		public GrammarParseException(string message, FilePosition occuredAt, Exception inner) : base(message, inner)
		{
			OccuredAt = occuredAt;
		}

		public FilePosition OccuredAt { get; }
	}
}
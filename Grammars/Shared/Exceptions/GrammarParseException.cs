using System;
using SmallScript.Shared;
using SmallScript.Shared.Exceptions;

namespace SmallScript.Grammars.Shared.Exceptions
{
	public class GrammarParseException : SmallScriptException
	{
		public Position OccuredAt { get; }

		public GrammarParseException(Position occuredAt)
		{
			OccuredAt = occuredAt;
		}

		public GrammarParseException(Position occuredAt, string message) : base(message)
		{
			OccuredAt = occuredAt;
		}

		public GrammarParseException(Position occuredAt, Exception inner) : base(inner.Message, inner)
		{
			OccuredAt = occuredAt;
		}

		public GrammarParseException(string message, Position occuredAt, Exception inner) : base(message, inner)
		{
			OccuredAt = occuredAt;
		}
	}
}
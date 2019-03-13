using SmallScript.Shared.Details.Navigation;
using SmallScript.Shared.Exceptions;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Parser.Exceptions
{
	public class PrecedenceParseException : SmallScriptException
	{
		public FilePosition OccuredAt { get; }

		public PrecedenceParseException(string message, FilePosition occuredAt) : base(message)
		{
			OccuredAt = occuredAt;
		}
	}
}
using SmallScript.Shared.Exceptions;

namespace SmallScript.LexicalParsers.RegexParser.Parser.Exceptions
{
	public class InvalidTokenException : SmallScriptException
	{
		public InvalidTokenException(string token) : base($"Invalid token [{token}] occured")
		{
			Token = token;
		}

		public string Token { get; }
	}
}
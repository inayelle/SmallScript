using SmallScript.Shared.Exceptions;

namespace SmallScript.LexicalParsers.RegexParser.Parser.Exceptions
{
	public class InvalidTokenException : SmallScriptException
	{
		public string Token { get; }

		public InvalidTokenException(string token) : base($"Invalid token [{token}] occured")
		{
			Token = token;
		}
	}
}
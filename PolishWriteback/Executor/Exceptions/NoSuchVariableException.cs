using SmallScript.LexicalParsers.Shared.Details.Tokens;

namespace SmallScript.PolishWriteback.Executor.Exceptions
{
	public class NoSuchVariableException : RuntimeException
	{
		public VariableToken Token { get; }

		public NoSuchVariableException(VariableToken token)
				: base($"Usage of undefined variable \"{token.Value}\" at {token.Position}")
		{
			Token = token;
		}
	}
}
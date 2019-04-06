using SmallScript.LexicalParsers.Shared.Details.Tokens;

namespace SmallScript.PolishWriteback.Executor.Exceptions
{
	public class VariableRedeclarationException : RuntimeException
	{
		public VariableToken Variable { get; }
		
		public VariableRedeclarationException(VariableToken token)
				: base($"Duplicate declaration of variable \"{token.Value}\" at {token.Position}")
		{
			Variable = token;
		}
	}
}
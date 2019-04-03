using SmallScript.LexicalParsers.Shared.Details.Tokens;

namespace SmallScript.PolishWriteback.Executor.Exceptions
{
	public class VariableRedeclarationException : RuntimeException
	{
		public VariableToken Variable { get; }
		
		public VariableRedeclarationException(VariableToken token)
		{
			Variable = token;
		}
	}
}
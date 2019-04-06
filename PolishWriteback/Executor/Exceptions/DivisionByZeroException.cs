using SmallScript.LexicalParsers.Shared.Interfaces;

namespace SmallScript.PolishWriteback.Executor.Exceptions
{
	public sealed class DivisionByZeroException : RuntimeException
	{
		public IToken Token { get; }

		public DivisionByZeroException(IToken token) : base($"Division by zero at {token.Position}")
		{
			Token = token;
		}
	}
}
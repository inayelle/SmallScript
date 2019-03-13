using SmallScript.Grammars.Shared.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Interfaces
{
	public interface IResolveVisitor
	{
		void Visit(IRule rule);
	}
}
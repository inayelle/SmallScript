using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details.Resolvers
{
	public class FirstLastResolver : IResolveVisitor
	{
		public void Visit(IRule rule)
		{
			var root = rule.Root as DetailedNonTerminal;

			foreach (var alternative in rule.Alternatives)
			{
				root.SequenceRelations.AddFirst(alternative.Entries.First());
				root.SequenceRelations.AddLast(alternative.Entries.Last());
			}
		}
	}
}
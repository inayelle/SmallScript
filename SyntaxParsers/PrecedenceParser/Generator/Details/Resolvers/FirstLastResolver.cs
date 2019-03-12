using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details.Resolvers
{
	public class FirstLastResolver : IResolveVisitor
	{
		public void Visit(IRule rule)
		{
			var root = Require.OfType<DetailedNonTerminal>(rule.Root);

			foreach (var alternative in rule.Alternatives)
			{
				root.SequenceRelations.AddFirst(alternative.Entries.First());
				root.SequenceRelations.AddLast(alternative.Entries.Last());
			}

			;
		}
	}
}
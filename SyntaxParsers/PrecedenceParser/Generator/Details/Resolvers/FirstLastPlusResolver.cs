using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details.Resolvers
{
	public class FirstLastPlusResolver : IResolveVisitor
	{
		public void Visit(IRule rule)
		{
			Visit(rule.Root as DetailedNonTerminal);
		}

		private static void Visit(DetailedNonTerminal nonTerminal)
		{
			if (nonTerminal.PlusVisited)
			{
				return;
			}

			nonTerminal.PlusVisited = true;

			nonTerminal.SequenceRelations.AddFirstPlus(nonTerminal.SequenceRelations.First.ToArray());
			nonTerminal.SequenceRelations.AddLastPlus(nonTerminal.SequenceRelations.Last.ToArray());

			foreach (var entry in nonTerminal.SequenceRelations.First.OfType<DetailedNonTerminal>())
			{
				Visit(entry);

				nonTerminal.SequenceRelations.AddFirstPlus(entry.SequenceRelations.FirstPlus.ToArray());
			}

			foreach (var entry in nonTerminal.SequenceRelations.Last.OfType<DetailedNonTerminal>())
			{
				Visit(entry);

				nonTerminal.SequenceRelations.AddLastPlus(entry.SequenceRelations.Last.ToArray());
			}
		}
	}
}
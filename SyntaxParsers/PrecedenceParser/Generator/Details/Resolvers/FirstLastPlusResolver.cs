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
			ResolveFirstPlus(nonTerminal);
			ResolveLastPlus(nonTerminal);
		}

		private static void ResolveFirstPlus(DetailedNonTerminal nonTerminal)
		{
//			if (nonTerminal.FirstPlusVisited)
//			{
//				return;
//			}

			nonTerminal.FirstPlusVisited = true;

			var relations = nonTerminal.SequenceRelations;

			relations.AddFirstPlus(relations.First);

			foreach (var entry in relations.First.OfType<DetailedNonTerminal>().ToList())
			{
				if (!entry.FirstPlusVisited)
					ResolveFirstPlus(entry);

				relations.AddFirstPlus(entry.SequenceRelations.FirstPlus);
			}
		}

		private static void ResolveLastPlus(DetailedNonTerminal nonTerminal)
		{
//			if (nonTerminal.LastPlusVisited)
//			{
//				return;
//			}

			nonTerminal.LastPlusVisited = true;

			var relations = nonTerminal.SequenceRelations;
			relations.AddLastPlus(relations.Last);

			foreach (var entry in relations.Last.OfType<DetailedNonTerminal>().ToList())
			{
				if (!entry.LastPlusVisited)
					ResolveLastPlus(entry);

				relations.AddLastPlus(entry.SequenceRelations.LastPlus);
			}
		}
	}
}
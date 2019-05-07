using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details.Collections;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details.Resolvers;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Enums;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Extensions;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details
{
	public class PrecedenceGenerator
	{
		private readonly FirstLastResolver     _firstLastResolver;
		private readonly FirstLastPlusResolver _firstLastPlusResolver;

		public PrecedenceGenerator()
		{
			_firstLastResolver     = new FirstLastResolver();
			_firstLastPlusResolver = new FirstLastPlusResolver();
		}

		public PrecedenceGeneratorResult Generate(IGrammar grammar)
		{
			Require.NotNull(grammar, nameof(grammar));

			var duplicateAlternatives = GetDuplicateAlternatives(grammar);

			SetFirstLast(grammar);
			SetFirstLastPlus(grammar);

			var pairs = ResolveEqualPairs(grammar);

			ResolveGreaterRelationLeftNonTerminal(pairs);
			ResolveLessRelationRightNonTerminal(pairs);
			ResolveRelationsBetweenNonTerminals(pairs);

			
			
			return new PrecedenceGeneratorResult(pairs, duplicateAlternatives);
		}

		private void SetFirstLast(IGrammar grammar)
		{
			foreach (var rule in grammar.Rules)
			{
				rule.Accept(_firstLastResolver);
			}
		}

		private void SetFirstLastPlus(IGrammar grammar)
		{
			foreach (var rule in grammar.Rules)
			{
				rule.Accept(_firstLastPlusResolver);
			}
		}

		private static PairCollection ResolveEqualPairs(IGrammar grammar)
		{
			var pairs = new PairCollection();

			foreach (var alternative in grammar.GetAllAlternatives())
			{
				foreach (var pair in alternative.GetPairs())
				{
					pairs[pair].AddRelation(RelationType.Equal);
				}
			}

			return pairs;
		}

		private static void ResolveRelationsBetweenNonTerminals(PairCollection collection)
		{
			var pairs = collection.Where(x => x.HasRelation(RelationType.Equal))
			                      .Where(x => x.Left is DetailedNonTerminal)
			                      .Where(x => x.Right is DetailedNonTerminal)
			                      .ToArray();

			foreach (var pair in pairs)
			{
				var left  = pair.Left as DetailedNonTerminal;
				var right = pair.Right as DetailedNonTerminal;

				foreach (var leftEntry in left.SequenceRelations.LastPlus)
				{
					foreach (var rightEntry in right.SequenceRelations.FirstPlus)
					{
						collection[leftEntry, rightEntry].AddRelation(RelationType.Greater);
					}
				}
			}
		}

		private static void ResolveGreaterRelationLeftNonTerminal(PairCollection collection)
		{
			var pairs = collection.Where(x => x.HasRelation(RelationType.Equal))
			                      .Where(x => x.Left is DetailedNonTerminal)
			                      .ToArray();

			foreach (var pair in pairs)
			{
				var left  = pair.Left as DetailedNonTerminal;
				var right = pair.Right;

				foreach (var leftEntry in left.SequenceRelations.LastPlus)
				{
					collection[leftEntry, right].AddRelation(RelationType.Greater);
				}
			}
		}

		private static void ResolveLessRelationRightNonTerminal(PairCollection collection)
		{
			var pairs = collection.Where(x => x.HasRelation(RelationType.Equal))
			                      .Where(x => x.Right is DetailedNonTerminal)
			                      .ToArray();

			foreach (var pair in pairs)
			{
				var left  = pair.Left;
				var right = pair.Right as DetailedNonTerminal;

				foreach (var rightEntry in right.SequenceRelations.FirstPlus)
				{
					collection[left, rightEntry].AddRelation(RelationType.Less);
				}
			}
		}

		private static IList<IAlternative> GetDuplicateAlternatives(IGrammar grammar)
		{
			return grammar.GetAllAlternatives()
			              .GroupBy(x => x)
			              .Where(g => g.Count() > 1)
			              .Select(g => g.Key)
			              .ToList();
		}
	}
}
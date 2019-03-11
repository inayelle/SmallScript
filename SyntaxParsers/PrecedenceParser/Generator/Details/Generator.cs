using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details.Resolvers;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Enums;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Extensions;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details
{
	public class Generator
	{
		public GeneratorResult Generate(IGrammar grammar)
		{
			Require.NotNull(grammar, nameof(grammar));

			SetFirstLast(grammar);
			SetFirstLastPlus(grammar);
			ResolveEqualPairs(grammar);
			
			var pairs = ResolveEqualPairs(grammar);
			ResolveRelationsBetweenNonTerminals(pairs);
			ResolveGreaterRelationLeftNonTerminal(pairs);
			ResolveLessRelationRightNonTerminal(pairs);
			
			return new GeneratorResult(pairs);
		}

		private static void SetFirstLast(IGrammar grammar)
		{
			var resolver = new FirstLastResolver();
			
			foreach (var rule in grammar.Rules)
			{
				rule.Accept(resolver);
			}
		}
		
		private static void SetFirstLastPlus(IGrammar grammar)
		{
			var resolver = new FirstLastPlusResolver();
			
			foreach (var rule in grammar.Rules)
			{
				rule.Accept(resolver);
			}
		}

		private static PairCollection ResolveEqualPairs(IGrammar grammar)
		{
			var pairs = new PairCollection();
			
			foreach (var alternative in grammar.Rules.SelectMany(r => r.Alternatives))
			{
				var entries = alternative.Entries.ToArray();

				for (var i = 0; i < entries.Length - 1; ++i)
				{
					var left = entries[i];
					var right = entries[i + 1];
					
					pairs.GetOrAdd(left, right).AddRelation(RelationType.Equal);
				}
			}

			return pairs;
		}

		private static void ResolveRelationsBetweenNonTerminals(PairCollection collection)
		{
			var pairs = collection.WhereBothAreNonTerminals();

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
			var pairs = collection.WhereLeftIsNonTerminal();

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
			var pairs = collection.WhereRightIsNonTerminal();

			foreach (var pair in pairs)
			{
				var left = pair.Left;
				var right  = pair.Right as DetailedNonTerminal;

				foreach (var rightEntry in right.SequenceRelations.FirstPlus)
				{
					collection[left, rightEntry].AddRelation(RelationType.Less);
				}
			}
		}
	}
}
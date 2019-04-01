using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Details;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.Shared.Details.Errors;
using SmallScript.Shared.Exceptions;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details.Collections;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Enums;
using SmallScript.SyntaxParsers.PrecedenceParser.Parser.Exceptions;
using SmallScript.SyntaxParsers.Shared.Details;
using SmallScript.SyntaxParsers.Shared.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Parser.Details
{
	public class PrecedenceParser : ISyntaxParser
	{
		private readonly Generator.Details.Generator _generator;

		private IGrammar _grammar;

		public IGrammar Grammar
		{
			get => _grammar;
			set => _grammar = Require.NotNull(value);
		}

		public PrecedenceParser(IGrammar grammar)
		{
			Grammar    = grammar;
			_generator = new Generator.Details.Generator();
		}

		public event Action<string> OnSequenceReplacement;

		public SyntaxParseResult Parse(LexicalParseResult result)
		{
			Require.NotNull(result, nameof(result));

			var pairs  = _generator.Generate(Grammar).Pairs;
			var tokens = PrepareTokensAndPairs(pairs, result.Tokens);

			var stateMachine = new AscendingStateMachine(Grammar, pairs);
			stateMachine.OnSequenceReplacement += OnSequenceReplacement;

			try
			{
				stateMachine.Run(tokens);
			}
			catch (PrecedenceParseException exception)
			{
				var error = new ParseError(exception.Message, exception.OccuredAt);
				return new SyntaxParseResult(error);
			}

			return SyntaxParseResult.Successful;
		}

		private IEnumerable<IToken> PrepareTokensAndPairs(PairCollection pairs, IEnumerable<IToken> tokens)
		{
			var leftBoundEntry  = new BoundEntry("$left$");
			var rightBoundEntry = new BoundEntry("$right$");

			var leftBoundToken  = new BoundToken(leftBoundEntry);
			var rightBoundToken = new BoundToken(rightBoundEntry);

			foreach (var entry in Grammar.Entries)
			{
				pairs[leftBoundEntry, entry].AddRelation(RelationType.Less);
				pairs[entry, rightBoundEntry].AddRelation(RelationType.Greater);
			}

			return tokens.Prepend(leftBoundToken).Append(rightBoundToken).ToList();
		}
	}
}
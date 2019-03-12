using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.BackusNaur.Grammar.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.Shared.Exceptions;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details.Collections;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Enums;
using SmallScript.SyntaxParsers.PrecedenceParser.Parser.Exceptions;
using SmallScript.SyntaxParsers.PrecedenceParser.Parser.Extensions;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Parser.Details
{
	internal sealed class AscendingStateMachine
	{
		private static readonly BoundEntry LeftBoundEntry  = new BoundEntry("$left$");
		private static readonly BoundEntry RightBoundEntry = new BoundEntry("$right$");

		private readonly IGrammar       _grammar;
		private readonly PairCollection _pairs;
		private readonly IGrammarEntry  _syntax;
		private          EntryManager   _entryManager;

		public event Action<string> OnSequenceReplacement;

		public AscendingStateMachine(IGrammar grammar, PairCollection pairs)
		{
			_grammar = Require.NotNull(grammar, nameof(grammar));
			_pairs   = Require.NotNull(pairs, nameof(pairs));

			_syntax = _grammar.Rules.First().Root;
		}

		public void Run(IEnumerable<IToken> tokens)
		{
			_entryManager = new EntryManager(tokens);

			State1();
		}

		private void State1()
		{
			
		}
		
		private void State10()
		{
			_entryManager?.Dispose();
		}

		private void RaiseError(string message)
		{
			throw new PrecedenceParseException(message, _entryManager.GetCurrentToken().Position);
		}
	}
}
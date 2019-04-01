using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.BackusNaur.Grammar.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.Shared.Exceptions;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details;
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
		private readonly IGrammarEntry  _syntax;
		private readonly PairCollection _pairs;

		private EntryManager _entryManager;

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
			var tokenEntry = _entryManager.GetNextTokenEntry();
			_entryManager.Push(tokenEntry);
			_entryManager.MoveNextTokenEntry();

			State2();
		}

		private void State2()
		{
			var stackEntry = _entryManager.Peek();
			var tokenEntry = _entryManager.GetCurrentTokenEntry();

			if (_syntax.Equals(stackEntry))
			{
				State7();
				return;
			}

			var pair = _pairs[stackEntry, tokenEntry];

			if (pair.HasAnyRelation(RelationType.Less, RelationType.Equal))
			{
				State3();
			}
			else if (pair.HasRelation(RelationType.Greater))
			{
				State4();
			}
			else
			{
				RaiseError();
			}
		}

		private void State3()
		{
			var tokenEntry = _entryManager.GetCurrentTokenEntry();
			_entryManager.Push(tokenEntry);
			_entryManager.MoveNextTokenEntry();

			State2();
		}

		private void State4()
		{
			var sequence = new List<IGrammarEntry> { _entryManager.Pop() };

			State5(sequence);
		}

		private void State5(List<IGrammarEntry> sequence)
		{
			var pair = _pairs[_entryManager.Peek(), sequence.Last()];

			while (!pair.HasRelation(RelationType.Less))
			{
				sequence.Add(_entryManager.Pop());
				pair = _pairs[_entryManager.Peek(), sequence.Last()];
			}

			State6(sequence);
		}

		private void State6(List<IGrammarEntry> sequence)
		{
			sequence.Reverse();

			var grammarEntry = _grammar.With(new Alternative(sequence));

			if (grammarEntry == null)
			{
				RaiseError();
			}
			else
			{
				var message = $"{grammarEntry.Value} ::= {String.Join(' ', sequence)}";
				OnSequenceReplacement?.Invoke(message);
				_entryManager.Push(grammarEntry);
				State2();
			}
		}

		private void State7()
		{
			var syntaxEntry    = _entryManager.Pop();
			var leftBoundEntry = _entryManager.Pop();

			if (LeftBoundEntry.Equals(leftBoundEntry))
			{
				State8();
			}
			else
			{
				RaiseError();
			}
		}

		private void State8()
		{
			_entryManager?.Dispose();
		}

		private void RaiseError(string message = "Unexpected token")
		{
			var debug = $"??? ::= {String.Join(' ', _entryManager.StackContent.Reverse())}";
			OnSequenceReplacement?.Invoke(debug);

			var token = _entryManager.GetCurrentToken();
			_entryManager?.Dispose();
			throw new PrecedenceParseException($"{message} {token.Value}", token.Position);
		}
	}
}
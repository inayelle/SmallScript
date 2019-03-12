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
			var currentEntry = _entryManager.GetNextEntry();
			_entryManager.Push(currentEntry);

			State2();
		}

		private void State2()
		{
			var stackEntry   = _entryManager.Peek();
			var currentEntry = _entryManager.GetNextEntry();

			if (currentEntry == null)
			{
				State9();
				return;
			}

			var pair = _pairs[stackEntry, currentEntry];

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
				RaiseError($"Invalid sequence {pair.Left.Value} {pair.Right.Value}");
			}
		}

		private void State3()
		{
			var currentEntry = _entryManager.GetCurrentEntry();
			_entryManager.Push(currentEntry);

			State2();
		}

		private void State4()
		{
			var stackEntry = _entryManager.Pop();
			var sequence   = new List<IGrammarEntry> { stackEntry };

			State5(sequence);
		}

		private void State5(List<IGrammarEntry> sequence)
		{
			var stackEntry = _entryManager.Peek();

			var pair = _pairs[stackEntry, sequence.Last()];

			if (pair.HasAnyRelation(RelationType.Greater, RelationType.Equal))
			{
				sequence.Add(_entryManager.Pop());

				State5(sequence);
			}
			else if (pair.HasRelation(RelationType.Less))
			{
				State7(sequence);
			}
			else
			{
				RaiseError($"Invalid sequence {pair.Left.Value} {pair.Right.Value}");
			}
		}

		private void State7(List<IGrammarEntry> sequence)
		{
			sequence.Reverse();

			var replacement = _grammar.With(new Alternative(sequence));

			if (replacement == null)
			{
				RaiseError("Invalid sequence " + String.Join(' ', sequence));
			}
			else
			{
				OnSequenceReplacement?.Invoke(replacement.Value + " ::= " + String.Join(' ', sequence));
				_entryManager.Push(replacement);
				_entryManager.Push(_entryManager.GetCurrentEntry());
				State2();
			}
		}

		private void State9()
		{
			var right  = _entryManager.Pop();
			var syntax = _entryManager.Pop();

			if (_syntax.Equals(syntax) && right.Equals(RightBoundEntry))
			{
				State10();
			}
			else
			{
				RaiseError($"Invalid sequence {syntax?.Value}");
			}
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
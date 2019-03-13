using System;
using System.Collections.Generic;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Parser.Details
{
	internal sealed class EntryManager : IDisposable
	{
		private readonly Stack<IGrammarEntry> _stack;
		private readonly IEnumerator<IToken>  _tokenIterator;

		public IEnumerable<IGrammarEntry> StackContent => _stack;

		public EntryManager(IEnumerable<IToken> tokens)
		{
			_tokenIterator = tokens.GetEnumerator();
			_stack         = new Stack<IGrammarEntry>();
		}

		public IGrammarEntry Pop()
		{
			return _stack.Count > 0 ? _stack.Pop() : null;
		}

		public IGrammarEntry Peek()
		{
			return _stack.Count > 0 ? _stack.Peek() : null;
		}

		public void Push(IGrammarEntry entry)
		{
			_stack.Push(entry);
		}

		public IGrammarEntry GetNextTokenEntry()
		{
			if (_tokenIterator.MoveNext())
			{
				return _tokenIterator.Current.GrammarEntry;
			}

			return null;
		}

		public IGrammarEntry GetCurrentTokenEntry()
		{
			return _tokenIterator.Current.GrammarEntry;
		}

		public IToken GetCurrentToken()
		{
			return _tokenIterator.Current;
		}

		public void MoveNextTokenEntry()
		{
			_tokenIterator.MoveNext();
		}

		public void Dispose()
		{
			_tokenIterator?.Dispose();
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.LexicalParsers.Shared.Interfaces;

namespace SmallScript.PolishWriteback.Generator.Details
{
	public class TokenIterator
	{
		private readonly IList<IToken> _tokens;

		private int _position;

		public virtual IToken Current => At(_position);

		public IToken Next     => At(_position + 1);
		public IToken Previous => At(_position - 1);

		public bool IsValid  => _position < _tokens.Count;
		public int  Position => _position;

		public TokenIterator(IEnumerable<IToken> tokens)
		{
			_tokens = tokens.ToArray();
		}

		public IToken At(int index)
		{
			if (index < _tokens.Count)
			{
				return _tokens[index];
			}

			throw new IndexOutOfRangeException();
		}

		public void MoveNext()
		{
			++_position;
		}

		public void MoveTo(int index)
		{
			_position = index - 1;
		}
	}
}
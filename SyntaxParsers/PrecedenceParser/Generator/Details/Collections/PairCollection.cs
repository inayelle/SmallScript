using System.Collections;
using System.Collections.Generic;
using SmallScript.Grammars.Shared.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details.Collections
{
	public class PairCollection : ICollection<Pair>
	{
		private readonly HashSet<Pair> _pairs;

		public PairCollection()
		{
			_pairs = new HashSet<Pair>();
		}

		public IEnumerator<Pair> GetEnumerator()
		{
			return _pairs.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public Pair GetOrAdd(Pair pair)
		{
			Add(pair);

			_pairs.TryGetValue(pair, out var actual);
			return actual;
		}

		public Pair GetOrAdd(IGrammarEntry left, IGrammarEntry right)
		{
			var pair = new Pair(left, right);

			return GetOrAdd(pair);
		}

		public Pair this[IGrammarEntry left, IGrammarEntry right] => GetOrAdd(left, right);

		public Pair this[Pair pair] => GetOrAdd(pair);

		public void Add(Pair pair)
		{
			_pairs.Add(pair);
		}

		public void Clear()
		{
			_pairs.Clear();
		}

		public bool Contains(Pair pair)
		{
			return _pairs.Contains(pair);
		}

		public void CopyTo(Pair[] array, int arrayIndex)
		{
			_pairs.CopyTo(array, arrayIndex);
		}

		public bool Remove(Pair pair)
		{
			return _pairs.Remove(pair);
		}

		public int  Count      => _pairs.Count;
		public bool IsReadOnly => false;
	}
}
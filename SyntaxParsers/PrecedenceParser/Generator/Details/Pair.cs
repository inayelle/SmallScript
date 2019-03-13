using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Enums;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Extensions;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details
{
	public sealed class Pair : IEquatable<Pair>
	{
		private readonly HashSet<RelationType> _relations;

		public IGrammarEntry Left  { get; }
		public IGrammarEntry Right { get; }

		public IEnumerable<RelationType> Relations => _relations;

		public Pair(IGrammarEntry left, IGrammarEntry right)
		{
			Left  = Require.NotNull(left, nameof(left));
			Right = Require.NotNull(right, nameof(right));

			_relations = new HashSet<RelationType>();
		}

		public void AddRelation(RelationType relation)
		{
			_relations.Add(relation);
		}

		public bool HasRelation(RelationType relation)
		{
			return _relations.Contains(relation);
		}

		public bool Equals(Pair other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return Left.Equals(other.Left) && Right.Equals(other.Right);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Pair);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Left, Right);
		}

		public override string ToString()
		{
			return $"{Left, 20} | {String.Join(' ', Relations.Select(r => r.AsString())), 7} | {Right}";
		}
	}
}
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.Shared.Details.Navigation;
using SmallScript.Shared.Extensions;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Parser.Details
{
	public class BoundToken : IToken
	{
		public FilePosition Position => null;

		public string        Value        { get; }
		public IGrammarEntry GrammarEntry { get; }

		public BoundToken(IGrammarEntry grammarEntry)
		{
			GrammarEntry = Require.NotNull(grammarEntry, nameof(grammarEntry));
			Value        = grammarEntry.Value;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as IToken);
		}

		public override int GetHashCode()
		{
			return Value.InvariantHashCode();
		}

		public bool Equals(IToken other)
		{
			if (ReferenceEquals(other, null))
			{
				return false;
			}

			if (ReferenceEquals(other, this))
			{
				return true;
			}

			return other is BoundToken && Value.InvariantEquals(other.Value);
		}

		public override string ToString()
		{
			return Value;
		}
	}
}
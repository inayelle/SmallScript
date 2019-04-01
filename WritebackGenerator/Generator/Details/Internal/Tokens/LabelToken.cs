using System;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.WritebackGenerator.Generator.Details.Internal.Tokens
{
	public class LabelToken : IToken
	{
		public int Id               { get; }
		public int TargetTokenOrder { get; set; }

		public string       Value    => "@LBL";
		public FilePosition Position => null;

		public IGrammarEntry GrammarEntry { get; } = new Terminal("@LBL");

		public LabelToken(int id, int targetTokenOrder)
		{
			Id = id;

			TargetTokenOrder = targetTokenOrder;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(obj, this))
			{
				return true;
			}

			return Equals(obj as IToken);
		}

		public bool Equals(IToken other)
		{
			if (!(other is LabelToken label))
			{
				return false;
			}

			return Id == label.Id;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(GrammarEntry, Id);
		}

		public override string ToString()
		{
			return $"[{nameof(LabelToken)}, {Id}] -> {TargetTokenOrder}";
		}
	}
}
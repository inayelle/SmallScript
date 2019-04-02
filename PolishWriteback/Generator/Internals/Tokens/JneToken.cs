using System;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.PolishWriteback.Generator.Internals.Tokens
{
	public class JneToken : IToken
	{
		public LabelDeclarationToken TargetLabel { get; }

		public string       Value    => Symbol.JumpByNotEquality;
		public FilePosition Position => null;

		public IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.JumpByNotEquality);

		public JneToken(LabelDeclarationToken targetLabel)
		{
			TargetLabel = Require.NotNull(targetLabel, nameof(targetLabel));
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
			if (!(other is JneToken jne))
			{
				return false;
			}

			return TargetLabel.Equals(jne.TargetLabel);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(GrammarEntry, Value, TargetLabel);
		}

		public override string ToString()
		{
			return $"[{nameof(JneToken)} -> {TargetLabel.Id}] {Value} : {Position}";
		}
	}
}
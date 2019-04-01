using System;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.PolishWriteback.Generator.Details.Internal.Tokens
{
	public class JmpToken : IToken
	{
		public LabelDeclarationToken TargetLabel { get; }

		public string       Value    => "@JMP";
		public FilePosition Position => null;

		public IGrammarEntry GrammarEntry { get; } = new Terminal("@JMP");

		public JmpToken(LabelDeclarationToken targetLabel)
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
			if (!(other is JmpToken jne))
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
			return $"[{nameof(JmpToken)} -> {TargetLabel.Id}] {Value} : {Position}";
		}
	}
}
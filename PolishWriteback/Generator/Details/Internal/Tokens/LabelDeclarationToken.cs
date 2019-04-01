using System;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.PolishWriteback.Generator.Details.Internal.Tokens
{
	public class LabelDeclarationToken : IToken
	{
		private LabelToken _targetLabel;
		
		public int Id               { get; }
		public int TargetTokenOrder => _targetLabel?.TargetTokenOrder ?? -1;

		public string       Value    => "@LBL_DECL";
		public FilePosition Position => null;

		public IGrammarEntry GrammarEntry { get; } = new Terminal("@LBL_DECL");

		public LabelDeclarationToken(int id)
		{
			Id = id;
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
			if (!(other is LabelDeclarationToken labelDecl))
			{
				return false;
			}

			return Id == labelDecl.Id;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(GrammarEntry, Id);
		}

		public override string ToString()
		{
			return $"[{nameof(LabelDeclarationToken)}, {Id}] -> {TargetTokenOrder}";
		}

		public LabelToken CreateLabel(int targetTokenOrder)
		{
			var label = new LabelToken(Id, targetTokenOrder);

			_targetLabel = label;

			return label;
		}
	}
}
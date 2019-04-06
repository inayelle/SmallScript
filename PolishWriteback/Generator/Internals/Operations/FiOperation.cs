using System.Collections.Generic;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Generator.Base;
using SmallScript.PolishWriteback.Generator.Details;
using SmallScript.PolishWriteback.Generator.Internals.Tokens;

namespace SmallScript.PolishWriteback.Generator.Internals.Operations
{
	internal class FiOperation : OperationBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.CloseCondition);
		
		public override IEnumerable<IToken> Consume(TokenIterator iterator, Stack<IToken> stack)
		{
			iterator.MoveNext(); //skip fi
			iterator.MoveNext(); //skip eol
			
			var labelDecl = stack.Pop() as LabelDeclarationToken;

			return new[] { labelDecl.CreateLabel(-322) };
		}
	}
}
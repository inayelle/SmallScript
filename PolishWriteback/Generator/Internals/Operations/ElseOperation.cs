using System.Collections.Generic;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Generator.Base;
using SmallScript.PolishWriteback.Generator.Details;
using SmallScript.PolishWriteback.Generator.Interfaces;
using SmallScript.PolishWriteback.Generator.Internals.Tokens;

namespace SmallScript.PolishWriteback.Generator.Internals.Operations
{
	internal class ElseOperation : OperationBase
	{
		private readonly ILabelIdentitySource _labelIdentitySource = LabelIdentitySource.Instance;

		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.Else);

		public override IEnumerable<IToken> Consume(TokenIterator iterator, Stack<IToken> stack)
		{
			iterator.MoveNext(); //skip else
			
			var targetLabelDecl = stack.Pop() as LabelDeclarationToken;

			var outLabelDecl = new LabelDeclarationToken(_labelIdentitySource.NextLabelId);

			stack.Push(outLabelDecl);

			var outputTokens = new List<IToken>
			{
				outLabelDecl,
				new JmpToken(outLabelDecl),
				targetLabelDecl.CreateLabel(-322)
			};
			
			outputTokens.AddRange(ProcessDefaultOperationUntilEntry(iterator, new Terminal(Symbol.CloseCondition)));

			return outputTokens;
		}
	}
}
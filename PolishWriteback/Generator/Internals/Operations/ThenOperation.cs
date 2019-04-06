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
	internal class ThenOperation : OperationBase
	{
		private readonly ILabelIdentitySource _labelIdentitySource = LabelIdentitySource.Instance;

		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.Then);

		public override IEnumerable<IToken> Consume(TokenIterator iterator, Stack<IToken> stack)
		{
			iterator.MoveNext(); //skip then

			var labelDecl = new LabelDeclarationToken(_labelIdentitySource.NextLabelId);

			stack.Push(labelDecl);

			var outputTokens = new List<IToken>
			{
				labelDecl,
				new JneToken(labelDecl)
			};
			
			outputTokens.AddRange(ProcessDefaultOperationUntilEntry(iterator, new Terminal(Symbol.Else)));

			return outputTokens;
		}
	}
}
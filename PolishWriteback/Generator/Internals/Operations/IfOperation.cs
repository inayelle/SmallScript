using System.Collections.Generic;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Generator.Base;
using SmallScript.PolishWriteback.Generator.Details;
using SmallScript.PolishWriteback.Generator.Interfaces;
using SmallScript.PolishWriteback.Generator.Internals.Tokens;
using SmallScript.Shared.Extensions;

namespace SmallScript.PolishWriteback.Generator.Internals.Operations
{
	internal class IfOperation : OperationBase
	{
		private readonly ILabelIdentitySource _labelIdentitySource = LabelIdentitySource.Instance;

		public override IGrammarEntry GrammarEntry { get; } = new Terminal("if");

		public override IEnumerable<IToken> Consume(TokenIterator iterator, Stack<IToken> stack)
		{
			var outputTokens = new List<IToken>();

			iterator.MoveNext(); //skip if

			outputTokens.AddRange(ProcessDijkstraUntilEntry(iterator, new Terminal("then")));
			iterator.MoveNext(); //skip then

			var jneLabelDecl = new LabelDeclarationToken(_labelIdentitySource.NextLabelId);
			var jneToken     = new JneToken(jneLabelDecl);
			outputTokens.Add(jneLabelDecl, jneToken);

			outputTokens.AddRange(ProcessDefaultOperationUntilEntry(iterator, new Terminal("else")));
			iterator.MoveNext(); //skip else
			
			var jmpLabelDecl = new LabelDeclarationToken(_labelIdentitySource.NextLabelId);
			var jmpToken = new JmpToken(jmpLabelDecl);
			
			outputTokens.Add(jmpLabelDecl, jmpToken, jneLabelDecl.CreateLabel(-322));
			
			outputTokens.AddRange(ProcessDefaultOperationUntilEntry(iterator, new Terminal("fi")));
			iterator.MoveNext(); //skip fi
			iterator.MoveNext(); //skip EOL
			
			outputTokens.Add(jmpLabelDecl.CreateLabel(-322));

			return outputTokens;
		}
	}
}
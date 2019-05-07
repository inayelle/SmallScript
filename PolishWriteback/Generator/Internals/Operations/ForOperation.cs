using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Generator.Base;
using SmallScript.PolishWriteback.Generator.Details;
using SmallScript.PolishWriteback.Generator.Interfaces;
using SmallScript.PolishWriteback.Generator.Internals.Tokens;
using SmallScript.Shared.Extensions;

namespace SmallScript.PolishWriteback.Generator.Internals.Operations
{
	internal class ForOperation : OperationBase
	{
		private readonly ILabelIdentitySource _labelIdentitySource = LabelIdentitySource.Instance;

		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.OpenLoop);

		//for i = 10 by 2 to 100 do ... rof
		public override IEnumerable<IToken> Consume(TokenIterator iterator, Stack<IToken> stack)
		{
			var outputTokens = new List<IToken>();

			iterator.MoveNext();                 //skip for
			var loopVariable = iterator.Current; //i

			outputTokens.AddRange(ProcessDijkstraUntilEntry(iterator, new Terminal(Symbol.By))); // i 10 =
			outputTokens.Add(new KeywordToken(Symbol.Let, null, new Terminal(Symbol.Let)));
			iterator.MoveNext(); //skip by

			var stepTokens = ProcessDijkstraUntilEntry(iterator, new Terminal(Symbol.To)).ToList(); // 2
			iterator.MoveNext(); //skip to
			
			var boundTokens = ProcessDijkstraUntilEntry(iterator, new Terminal(Symbol.Do)); // 100
			iterator.MoveNext(); //skip do

			var bodyTokens = ProcessDefaultOperationUntilEntry(iterator, new Terminal(Symbol.CloseLoop));
			iterator.MoveNext(); //skip rof
			iterator.MoveNext(); //skip EOL
			
			var loopLabelDecl = new LabelDeclarationToken(_labelIdentitySource.NextLabelId);
			var loopExitDecl  = new LabelDeclarationToken(_labelIdentitySource.NextLabelId);
			
			outputTokens.Add(loopLabelDecl.CreateLabel(-322), loopVariable);
			outputTokens.AddRange(boundTokens);
			outputTokens.Add(new DelimiterToken(Symbol.LessEqual, null, new Terminal("<=")));
			
			outputTokens.Add(loopExitDecl, new JneToken(loopExitDecl));
			
			outputTokens.AddRange(bodyTokens);
			
			outputTokens.Add(loopVariable, loopVariable);
			outputTokens.AddRange(stepTokens);
			outputTokens.Add(new DelimiterToken(Symbol.Plus, null, new Terminal(Symbol.Plus)));
			outputTokens.Add(new DelimiterToken(Symbol.Let, null, new Terminal(Symbol.Let)));
			
			outputTokens.Add(loopLabelDecl, new JmpToken(loopLabelDecl), loopExitDecl.CreateLabel(-322));

			return outputTokens;
		}
	}
}
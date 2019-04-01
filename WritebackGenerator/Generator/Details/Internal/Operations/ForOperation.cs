using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Extensions;
using SmallScript.WritebackGenerator.Generator.Base;
using SmallScript.WritebackGenerator.Generator.Details.Internal.Tokens;
using SmallScript.WritebackGenerator.Generator.Interfaces;

namespace SmallScript.WritebackGenerator.Generator.Details.Internal.Operations
{
	internal class ForOperation : OperationBase
	{
		private readonly ILabelIdentitySource _labelIdentitySource = LabelIdentitySource.Instance;

		public override IGrammarEntry GrammarEntry { get; } = new Terminal("for");

		public override IEnumerable<IToken> Consume(TokenIterator iterator, Stack<IToken> stack)
		{
			var outputTokens = new List<IToken>();

			iterator.MoveNext();                 //skip for
			var loopVariable = iterator.Current; //i

			outputTokens.AddRange(ProcessDijkstraUntilEntry(iterator, new Terminal("by"))); // i 10 =
			iterator.MoveNext(); //skip by

			var stepTokens = ProcessDijkstraUntilEntry(iterator, new Terminal("to")).ToList(); // 2
			iterator.MoveNext(); //skip to
			
			var boundTokens = ProcessDijkstraUntilEntry(iterator, new Terminal("do")); // 10
			iterator.MoveNext(); //skip do

			var bodyTokens = ProcessDefaultOperationUntilEntry(iterator, new Terminal("rof"));
			iterator.MoveNext(); //skip rof
			iterator.MoveNext(); //skip EOL
			
			var loopLabelDecl = new LabelDeclarationToken(_labelIdentitySource.NextLabelId);
			var loopExitDecl  = new LabelDeclarationToken(_labelIdentitySource.NextLabelId);
			
			outputTokens.Add(loopLabelDecl.CreateLabel(-322), loopVariable);
			outputTokens.AddRange(stepTokens);
			outputTokens.Add(new DelimiterToken("+", null, new Terminal("+")));
			outputTokens.AddRange(boundTokens);
			outputTokens.Add(new DelimiterToken("<=", null, new Terminal("<=")));
			
			outputTokens.Add(loopExitDecl, new JneToken(loopExitDecl));
			
			outputTokens.AddRange(bodyTokens);
			
			outputTokens.Add(loopVariable, loopVariable);
			outputTokens.AddRange(stepTokens);
			outputTokens.Add(new DelimiterToken("+", null, new Terminal("+")));
			outputTokens.Add(new DelimiterToken("let", null, new Terminal("let")));
			
			outputTokens.Add(loopLabelDecl, new JmpToken(loopLabelDecl), loopExitDecl.CreateLabel(-322));

			return outputTokens;
		}
	}
}
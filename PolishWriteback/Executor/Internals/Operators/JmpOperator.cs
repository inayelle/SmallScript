using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Base;
using SmallScript.PolishWriteback.Generator.Internals.Tokens;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class JmpOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.Jump);
		
		public override void Execute(RuntimeData runtimeData)
		{
			var label = runtimeData.Stack.Pop() as LabelDeclarationToken;
			
			runtimeData.Iterator.MoveTo(label.TargetTokenOrder);
		}
	}
}
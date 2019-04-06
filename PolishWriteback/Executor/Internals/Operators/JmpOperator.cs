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
		
		protected override void ExecuteImpl(RuntimeData runtime)
		{
			var label = runtime.Stack.Pop() as LabelDeclarationToken;
			
			runtime.Iterator.MoveTo(label.TargetTokenOrder);
		}
	}
}
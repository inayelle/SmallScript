using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Base;
using SmallScript.PolishWriteback.Generator.Internals.Tokens;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class JneOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal("@JNE");
		
		public override void Execute(RuntimeData runtimeData)
		{
			var label = runtimeData.Stack.Pop() as LabelDeclarationToken;

			var boolValue = PopBoolValue(runtimeData);

			if (!boolValue)
			{
				runtimeData.Iterator.MoveTo(label.TargetTokenOrder);
			}
		}
	}
}
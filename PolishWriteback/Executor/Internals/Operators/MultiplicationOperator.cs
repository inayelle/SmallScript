using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Base;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class MultiplicationOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal("*");

		public override void Execute(RuntimeData runtimeData)
		{
			var first = PopIntValue(runtimeData);
			var last  = PopIntValue(runtimeData);

			var result = first * last;
			
			PushValue(runtimeData, result);
		}
	}
}
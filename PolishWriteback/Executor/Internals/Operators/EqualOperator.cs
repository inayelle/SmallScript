using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Base;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class EqualOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.Equal);
		
		public override void Execute(RuntimeData runtimeData)
		{
			var last  = PopIntValue(runtimeData);
			var first = PopIntValue(runtimeData);

			bool result = first == last;
			
			PushValue(runtimeData, result);
		}
	}
}
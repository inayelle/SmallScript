using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Base;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class StdoutOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.StandartOutput);

		public override void Execute(RuntimeData runtimeData)
		{
			var value = PopIntValue(runtimeData);

			runtimeData.InputOutput.Write(value);
		}
	}
}
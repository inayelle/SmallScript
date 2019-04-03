using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Base;
using SmallScript.PolishWriteback.Executor.Extensions;
using SmallScript.PolishWriteback.Executor.Interfaces;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class StdoutOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.StandartOutput);

		public override void Execute(RuntimeData runtime)
		{
			var value = runtime.Pop();

			runtime.InputOutput.Write(value);
		}
	}
}
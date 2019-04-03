using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Base;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class LetOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.Let);

		public override void Execute(RuntimeData runtimeData)
		{
			var stack = runtimeData.Stack;

			var value = PopIntValue(runtimeData);
			var token = stack.Pop() as VariableToken;

			runtimeData.Variables.Alter(token, value);
		}
	}
}
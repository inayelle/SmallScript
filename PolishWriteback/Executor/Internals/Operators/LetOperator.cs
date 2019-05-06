using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Base;
using SmallScript.PolishWriteback.Executor.Extensions;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class LetOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.Let);

		protected override void ExecuteImpl(Runtime runtime)
		{
			var stack = runtime.Stack;

			var value = runtime.PopInt();
			var token = stack.Pop() as VariableToken;

			runtime.Variables.Alter(token, value);
		}
	}
}
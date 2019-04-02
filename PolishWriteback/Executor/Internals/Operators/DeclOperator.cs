using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.PolishWriteback.Executor.Base;
using SmallScript.PolishWriteback.Executor.Interfaces;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class DeclOperator : OperatorBase
	{
		public override IGrammarEntry GrammarEntry { get; } = new Terminal("decl");

		public override void Execute(RuntimeData runtimeData)
		{
			var value = PopIntValue(runtimeData);
			var token = runtimeData.Stack.Pop() as VariableToken;

			runtimeData.Variables.Create(token, value);
		}
	}
}
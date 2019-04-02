using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Interfaces;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class StdinOperator : IOperator
	{
		public IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.StandartInput);
		
		public void Execute(RuntimeData runtimeData)
		{
			var token = runtimeData.Stack.Pop() as VariableToken;

			var value = runtimeData.InputOutput.Read();

			runtimeData.Variables.Alter(token, value);
		}
	}
}
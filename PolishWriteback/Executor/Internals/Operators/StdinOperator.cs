using System;
using SmallScript.Grammars.Shared.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.PolishWriteback.Executor.Details;
using SmallScript.PolishWriteback.Executor.Interfaces;

namespace SmallScript.PolishWriteback.Executor.Internals.Operators
{
	internal class StdinOperator : IOperator
	{
		public IGrammarEntry GrammarEntry { get; } = new Terminal(Symbol.StandartInput);

		public event EventHandler<HistoryPoint> OnExecution;
		
		public void Execute(Runtime runtime)
		{
			var token = runtime.Stack.Pop() as VariableToken;

			var value = runtime.Input.Read();

			runtime.Variables.Alter(token, value);
		}
	}
}
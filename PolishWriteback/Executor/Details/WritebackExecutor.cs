using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Interfaces;
using SmallScript.PolishWriteback.Executor.Internals;
using SmallScript.PolishWriteback.Generator.Details;
using SmallScript.Shared.Details.Auxiliary;

namespace SmallScript.PolishWriteback.Executor.Details
{
	public class WritebackExecutor
	{
		private readonly IDictionary<IGrammarEntry, IOperator> _operators;

		private IInputOutput _inputOutput;

		public IInputOutput InputOutput
		{
			get => _inputOutput;
			set => _inputOutput = Require.NotNull(value);
		}

		public WritebackExecutor(IInputOutput inputOutput)
		{
			InputOutput = inputOutput;

			_operators = new OperatorProvider().Operators
			                                   .ToDictionary(o => o.GrammarEntry);
		}

		public void Execute(IToken[] tokens)
		{
			var iterator = new TokenIterator(tokens);
			var stack    = new Stack<IToken>();

			var runtimeData = RuntimeData.Builder
			                             .UseIterator(iterator)
			                             .UseIo(_inputOutput)
			                             .UseStack(stack)
			                             .UseVariableTable(new VariablesData())
			                             .Build();

			while (iterator.IsValid)
			{
				var token = iterator.Current;

				if (_operators.TryGetValue(token.GrammarEntry, out var @operator))
				{
					@operator.Execute(runtimeData);
				}
				else
				{
					stack.Push(token);
				}

				iterator.MoveNext();
			}
		}
	}
}
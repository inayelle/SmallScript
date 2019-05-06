using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Exceptions;
using SmallScript.PolishWriteback.Executor.Interfaces;
using SmallScript.PolishWriteback.Executor.Internals;
using SmallScript.PolishWriteback.Generator.Details;
using SmallScript.Shared.Details.Auxiliary;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: InternalsVisibleTo("SmallScript.PolishWriteback.Tests")]

namespace SmallScript.PolishWriteback.Executor.Details
{
	public class WritebackExecutor
	{
		private readonly IDictionary<IGrammarEntry, IOperator> _operators;

		private IInput  _input;
		private IOutput _output;

		public IInput Input
		{
			get => _input;
			set => _input = Require.NotNull(value, nameof(value));
		}

		public IOutput Output
		{
			get => _output;
			set => _output = Require.NotNull(value, nameof(value));
		}

		private readonly IList<HistoryPoint> _history;

		public WritebackExecutor(IInput input, IOutput output)
		{
			_history = new List<HistoryPoint>();

			_operators = new OperatorProvider().Operators
			                                   .ToDictionary(o =>
			                                   {
				                                   o.OnExecution += (sender, e) => _history.Add(e);
				                                   return o.GrammarEntry;
			                                   });

			Input  = input;
			Output = output;
		}

		public WritebackExecutionResult Execute(IEnumerable<IToken> tokens)
		{
			_history.Clear();

			var runtime = BuildRuntime(tokens);

			try
			{
				Execute(runtime);
				return WritebackExecutionResult.FromHistory(_history);
			}
			catch (RuntimeException exception)
			{
				return WritebackExecutionResult.FromException(exception, _history);
			}
		}

		private Runtime BuildRuntime(IEnumerable<IToken> tokens)
		{
			return Runtime.Builder
			                  .UseIterator(new TokenIterator(tokens))
			                  .UseInput(_input)
			                  .UseOutput(_output)
			                  .UseStack(new Stack<IToken>())
			                  .UseVariableTable(new VariablesData())
			                  .Build();
		}

		private void Execute(Runtime runtime)
		{
			var iterator = runtime.Iterator;
			var stack    = runtime.Stack;

			while (iterator.IsValid)
			{
				var token = iterator.Current;

				if (IsOperator(token, out var @operator))
				{
					@operator.Execute(runtime);
				}
				else
				{
					stack.Push(token);
				}

				iterator.MoveNext();
			}
		}

		private bool IsOperator(IToken token, out IOperator @operator)
		{
			return _operators.TryGetValue(token.GrammarEntry, out @operator);
		}
	}
}
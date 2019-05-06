using System;
using System.Collections.Generic;
using System.Linq;
using SmallScript.Grammars.Shared.Details;
using SmallScript.LexicalParsers.Shared.Details.Tokens;
using SmallScript.LexicalParsers.Shared.Enums;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Interfaces;
using SmallScript.PolishWriteback.Executor.Internals;
using SmallScript.PolishWriteback.Executor.Internals.Tokens;
using SmallScript.PolishWriteback.Generator.Details;
using SmallScript.Shared.Details.Navigation;
using Xunit;

[assembly: CollectionBehavior(MaxParallelThreads = 1, DisableTestParallelization = true)]

namespace SmallScript.PolishWriteback.Tests.Base
{
	public abstract class MathOperatorTestBase
	{
		protected abstract Type OperatorType { get; }

		protected abstract Stack<IToken> Arguments { get; }

		protected abstract int ExpectedResult { get; }

		[Fact]
		public void TestOperator()
		{
			var instance = Activator.CreateInstance(OperatorType);

			var @operator = instance as IOperator;

			Assert.NotNull(@operator);

			var runtime = Runtime.Builder
			                         .UseStack(Arguments)
			                         .UseIterator(new TokenIterator(ArraySegment<IToken>.Empty))
			                         .Build();
			
			Assert.NotNull(runtime);
			Assert.IsType<Runtime>(runtime);

			@operator.Execute(runtime);

			Assert.Single(runtime.Stack);
			var result = runtime.Stack.Pop() as IntValueToken;
			Assert.NotNull(result);

			Assert.Equal(ExpectedResult, result.IntValue);
		}

		protected static Stack<IToken> GetStack(params int[] args)
		{
			return new Stack<IToken>(
					args.Select
					(
							arg => new ConstantToken(0,
									arg.ToString(),
									new FilePosition(0, 0),
									new Terminal(Symbol.Const)
							)
					)
			);
		}
	}
}
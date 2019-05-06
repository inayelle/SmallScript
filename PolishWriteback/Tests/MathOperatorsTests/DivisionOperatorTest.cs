using System;
using System.Collections.Generic;
using Moq;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Exceptions;
using SmallScript.PolishWriteback.Executor.Internals;
using SmallScript.PolishWriteback.Executor.Internals.Operators;
using SmallScript.PolishWriteback.Executor.Internals.Tokens;
using SmallScript.PolishWriteback.Generator.Details;
using SmallScript.PolishWriteback.Tests.Base;
using Xunit;

namespace SmallScript.PolishWriteback.Tests.MathOperatorsTests
{
	public class DivisionOperatorTest : MathOperatorTestBase
	{
		protected override Type OperatorType => typeof(DivisionOperator);

		protected override Stack<IToken> Arguments => GetStack(4, 2);

		protected override int ExpectedResult => 2;

		[Fact]
		public void TestDivisionByZero()
		{
			var @operator = new DivisionOperator();

			var iteratorMock = new Mock<TokenIterator>(ArraySegment<IToken>.Empty);
			iteratorMock.Setup(i => i.Current).Returns(new IntValueToken(0));

			var runtime = Runtime.Builder
			                         .UseStack(GetStack(4, 0))
			                         .UseIterator(iteratorMock.Object)
			                         .Build();

			Assert.Throws<DivisionByZeroException>(() => @operator.Execute(runtime));
		}
	}
}
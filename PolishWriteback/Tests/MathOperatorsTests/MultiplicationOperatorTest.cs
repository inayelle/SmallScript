using System;
using System.Collections.Generic;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Internals.Operators;
using SmallScript.PolishWriteback.Tests.Base;

namespace SmallScript.PolishWriteback.Tests.MathOperatorsTests
{
	public class MultiplicationOperatorTest : MathOperatorTestBase
	{
		protected override Type OperatorType => typeof(MultiplicationOperator);

		protected override Stack<IToken> Arguments => GetStack(3, 2);

		protected override int ExpectedResult => 6;
	}
}
using System;
using System.Collections.Generic;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Internals.Operators;
using SmallScript.PolishWriteback.Tests.Base;

namespace SmallScript.PolishWriteback.Tests.MathOperatorsTests
{
	public class MinusOperatorTest : MathOperatorTestBase
	{
		protected override Type OperatorType => typeof(MinusOperator);

		protected override Stack<IToken> Arguments => GetStack(1, 2);

		protected override int ExpectedResult => -1;
	}
}
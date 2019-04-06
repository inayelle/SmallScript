using System;
using System.Collections.Generic;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Internals.Operators;
using SmallScript.PolishWriteback.Tests.Base;

namespace SmallScript.PolishWriteback.Tests.MathOperatorsTests
{
	public class PowerOperatorTest : MathOperatorTestBase
	{
		protected override Type OperatorType => typeof(PowerOperator);

		protected override Stack<IToken> Arguments => GetStack(4, 2);

		protected override int ExpectedResult => 16;
	}
}
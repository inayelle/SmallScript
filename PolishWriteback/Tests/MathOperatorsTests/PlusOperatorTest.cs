using System;
using System.Collections.Generic;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Internals.Operators;
using SmallScript.PolishWriteback.Tests.Base;

namespace SmallScript.PolishWriteback.Tests.MathOperatorsTests
{
	public sealed class PlusOperatorTest : MathOperatorTestBase
	{
		protected override Type OperatorType => typeof(PlusOperator);

		protected override Stack<IToken> Arguments => GetStack(1, 2);

		protected override int ExpectedResult => 3;
	}
}
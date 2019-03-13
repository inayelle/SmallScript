using System;
using System.Collections.Generic;
using SmallScript.SyntaxParsers.PrecedenceParser.Calculator.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Calculator.Details.Operations
{
	internal class Power : IOperation
	{
		public int Priority => 3;
		
		public void Perform(Stack<double> tokens)
		{
			var right = tokens.Pop();
			var left = tokens.Pop();
			
			var result = Math.Pow(left, right);
			
			tokens.Push(result);
		}
	}
}
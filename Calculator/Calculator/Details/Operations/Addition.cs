using System.Collections.Generic;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.SyntaxParsers.PrecedenceParser.Calculator.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Calculator.Details.Operations
{
	internal class Addition : IOperation
	{
		public int Priority => 1;

		public void Perform(Stack<double> tokens)
		{
			var result = tokens.Pop() + tokens.Pop();
			
			tokens.Push(result);
		}
	}
}
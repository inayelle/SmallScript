using System.Collections.Generic;
using SmallScript.Calculator.Interfaces;

namespace SmallScript.Calculator.Internals.Operations
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
using System.Collections.Generic;

namespace SmallScript.Calculator.Interfaces
{
	public interface IOperation
	{
		int Priority { get; }

		void Perform(Stack<double> tokens);
	}
}
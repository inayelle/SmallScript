using System.Collections.Generic;
using SmallScript.LexicalParsers.Shared.Interfaces;

namespace SmallScript.SyntaxParsers.PrecedenceParser.Calculator.Interfaces
{
	public interface IOperation
	{
		int Priority { get; }

		void Perform(Stack<double> tokens);
	}
}
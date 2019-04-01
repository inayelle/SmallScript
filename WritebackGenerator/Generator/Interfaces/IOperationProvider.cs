using System.Collections.Generic;

namespace SmallScript.WritebackGenerator.Generator.Interfaces
{
	public interface IOperationProvider
	{
		IEnumerable<IOperation> Operations { get; }
	}
}
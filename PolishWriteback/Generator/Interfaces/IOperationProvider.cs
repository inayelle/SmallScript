using System.Collections.Generic;

namespace SmallScript.PolishWriteback.Generator.Interfaces
{
	public interface IOperationProvider
	{
		IEnumerable<IOperation> Operations { get; }
	}
}
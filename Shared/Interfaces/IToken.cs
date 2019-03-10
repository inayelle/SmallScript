using SmallScript.Shared.Details.Navigation;

namespace SmallScript.Shared.Interfaces
{
	public interface IToken
	{
		string Code  { get; }
		string Value { get; }

		FilePosition Position { get; }
	}
}
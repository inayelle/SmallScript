namespace SmallScript.LexicalParsers.Shared.Interfaces
{
	public interface IIdentitySource
	{
		int NextVariableId { get; }
		int NextConstantId { get; }

		void Reset();
	}
}
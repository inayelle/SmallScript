using SmallScript.Grammars.Shared.Interfaces;

namespace SmallScript.Grammars.BackusNaur.Parser.Interfaces
{
	public interface IEntryFactory
	{
		IGrammarEntry CreateEntry(string value);
	}
}
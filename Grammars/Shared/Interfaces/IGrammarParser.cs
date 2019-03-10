using System.IO;

namespace SmallScript.Grammars.Shared.Interfaces
{
	public interface IGrammarParser<out TGrammar>
			where TGrammar : IGrammar
	{
		TGrammar Parse(string input);
		TGrammar Parse(Stream stream);
	}
}
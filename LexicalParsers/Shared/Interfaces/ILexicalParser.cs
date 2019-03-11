using System.IO;
using SmallScript.LexicalParsers.Shared.Details;

namespace SmallScript.LexicalParsers.Shared.Interfaces
{
	public interface ILexicalParser
	{
		LexicalParseResult Parse(string input);
		LexicalParseResult Parse(Stream stream);
	}
}
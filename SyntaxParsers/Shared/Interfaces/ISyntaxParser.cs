using SmallScript.LexicalParsers.Shared.Details;
using SmallScript.SyntaxParsers.Shared.Details;

namespace SmallScript.SyntaxParsers.Shared.Interfaces
{
	public interface ISyntaxParser
	{
		SyntaxParserResult Parse(LexicalParseResult result);
	}
}
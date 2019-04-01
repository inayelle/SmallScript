using SmallScript.LexicalParsers.Shared.Details;
using SmallScript.SyntaxParsers.Shared.Details;

namespace SmallScript.SyntaxParsers.Shared.Interfaces
{
	public interface ISyntaxParser
	{
		SyntaxParseResult Parse(LexicalParseResult result);
	}
}
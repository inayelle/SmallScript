using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Navigation;

namespace SmallScript.LexicalParsers.RegexParser.Parser.Interfaces
{
	public interface ITokenFactory
	{
		IToken Create(string value, FilePosition position);
	}
}
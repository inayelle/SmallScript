using System.Collections.Generic;

namespace SmallScript.LexicalParsers.RegexParser.Parser.Interfaces
{
	public interface ISourceCodeSplitter
	{
		ICollection<string> SplitByLines(string  sourceCodeText);
		ICollection<string> SplitByTokens(string line);
	}
}
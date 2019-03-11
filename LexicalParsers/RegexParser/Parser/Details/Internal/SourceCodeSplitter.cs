using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SmallScript.LexicalParsers.RegexParser.Parser.Interfaces;

[assembly: InternalsVisibleTo("SmallScript.LexicalParsers.RegexParser.Tests")]

namespace SmallScript.LexicalParsers.RegexParser.Parser.Details.Internal
{
	internal class SourceCodeSplitter : ISourceCodeSplitter
	{
		public ICollection<string> SplitByLines(string sourceCodeText)
		{
			var result = new List<string>();

			using (var reader = new StringReader(sourceCodeText))
			{
				string line = null;

				while ((line = reader.ReadLine()) != null) result.Add(line + '\n');
			}

			return result;
		}

		public ICollection<string> SplitByTokens(string line)
		{
			return Regex.Split(line, @"([\$@A-z_]+|[0-9]+|\-|\+|\*|\/|\*\*|,|\.|\?|\:|\n|\(|\)|\[|\])| |\t")
			            .Where(l => !string.IsNullOrEmpty(l))
			            .ToList();
		}
	}
}
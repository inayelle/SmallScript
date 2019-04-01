using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using SmallScript.LexicalParsers.RegexParser.Parser.Interfaces;

[assembly: InternalsVisibleTo("SmallScript.LexicalParsers.RegexParser.Tests")]

namespace SmallScript.LexicalParsers.RegexParser.Parser.Details.Internal
{
	internal class SourceCodeSplitter : ISourceCodeSplitter
	{
		private const string TokenPattern = 
				@"([A-z_][A-z0-9_]+|->|>>|<<|>|<|==|!=|>=|<=|[0-9]+|\-|\+|\*\*|\/|\*|\?|\:|\n|\(|\)|\[|\]|=)| |\t";
		
		public ICollection<string> SplitByLines(string sourceCodeText)
		{
			var result = new List<string>();

			var sb = new StringBuilder();
			
			foreach (var ch in sourceCodeText)
			{
				sb.Append(ch);

				if (ch == '\n')
				{
					result.Add(sb.ToString());
					sb.Clear();
				}
			}

			
			if (sb.Length > 0)
			{
				result.Add(sb.ToString());
			}
			
			result.Add("\n");
			
			return result;
		}

		public ICollection<string> SplitByTokens(string line)
		{
			return Regex.Split(line, TokenPattern)
			            .Where(l => !string.IsNullOrEmpty(l))
			            .ToList();
		}
	}
}
using System.Collections.Generic;
using SmallScript.LexicalParsers.Shared.Extensions;
using SmallScript.LexicalParsers.Shared.Interfaces;

namespace SmallScript.LexicalParsers.Shared.Details
{
	public class PostParseFormattingHandler
	{
		public static void Handle(IList<IToken> tokens)
		{
			var indiciesToRemove = new List<int>();

			for (var i = 0; i < tokens.Count - 1; ++i)
			{
				if (NeedsFormat(tokens[i], tokens[i + 1]))
				{
					indiciesToRemove.Add(i + 1);
				}
			}

			int offset = 0;

			foreach (var i in indiciesToRemove)
			{
				tokens.RemoveAt(i + offset);
				--offset;
			}
		}

		private static bool NeedsFormat(IToken first, IToken last)
		{
			return first.IsKeyword("do") && last.IsDelimiter("<EOL>") ||
			       first.IsDelimiter("<EOL>") && last.IsDelimiter("<EOL>") ||
			       first.IsDelimiter("{") && last.IsDelimiter("<EOL>") ||
			       first.IsDelimiter("}") && last.IsDelimiter("<EOL>") ||
			       first.IsKeyword("else") && last.IsDelimiter("<EOL>") ||
			       first.IsKeyword("then") && last.IsDelimiter("<EOL>");
		}
	}
}
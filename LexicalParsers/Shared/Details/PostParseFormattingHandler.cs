using System.Collections.Generic;
using SmallScript.LexicalParsers.Shared.Enums;
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
			return first.IsKeyword(Symbol.Do) && last.IsDelimiter(Symbol.OperationDelimiter) ||
			       first.IsDelimiter(Symbol.OperationDelimiter) && last.IsDelimiter(Symbol.OperationDelimiter) ||
			       first.IsDelimiter(Symbol.OpenCurlyBrace) && last.IsDelimiter(Symbol.OperationDelimiter) ||
			       first.IsDelimiter(Symbol.CloseCurlyBrace) && last.IsDelimiter(Symbol.OperationDelimiter) ||
			       first.IsKeyword("else") && last.IsDelimiter(Symbol.OperationDelimiter) ||
			       first.IsKeyword("then") && last.IsDelimiter(Symbol.OperationDelimiter);
		}
	}
}
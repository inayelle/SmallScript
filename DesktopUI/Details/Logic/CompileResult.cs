using System.Collections.Generic;
using SmallScript.LexicalParsers.Shared.Details;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.Shared.Details.Errors;
using SmallScript.SyntaxParsers.Shared.Details;

namespace SmallScript.DesktopUI.Details.Logic
{
	internal sealed class CompileResult
	{
		public bool                Ok     { get; }
		public ParseError          Error  { get; }
		public IEnumerable<IToken> Tokens { get; }

		public LexicalParseResult LexicalParseResult { get; set; }
		public SyntaxParseResult  SyntaxParseResult  { get; set; }

		public CompileResult(LexicalParseResult lexicalParseResult) : this(lexicalParseResult, null)
		{
		}

		public CompileResult(LexicalParseResult lexicalParseResult, SyntaxParseResult syntaxParseResult)
		{
			LexicalParseResult = lexicalParseResult;
			SyntaxParseResult  = syntaxParseResult;
			Tokens             = lexicalParseResult.Tokens;

			Ok    = lexicalParseResult.Ok && syntaxParseResult.Ok;
			Error = lexicalParseResult.Error ?? syntaxParseResult.Error;
		}
	}
}
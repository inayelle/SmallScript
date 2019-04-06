using System.Threading.Tasks;
using SmallScript.LexicalParsers.Shared.Details;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.SyntaxParsers.Shared.Details;
using SmallScript.SyntaxParsers.Shared.Interfaces;

namespace SmallScript.DesktopUI.Details.Logic
{
	internal sealed class Compiler
	{
		public ILexicalParser LexicalParser { get; set; }
		public ISyntaxParser  SyntaxParser  { get; set; }

		public async Task<CompileResult> CompileAsync(string code)
		{
			var lexicalParseResult = await LexicalParseAsync(code);

			if (!lexicalParseResult.Ok)
			{
				return new CompileResult(lexicalParseResult);
			}

			var syntaxParseResult = await SyntaxParseAsync(lexicalParseResult);
			
			return new CompileResult(lexicalParseResult, syntaxParseResult);
		}

		private Task<LexicalParseResult> LexicalParseAsync(string code)
		{
			return Task.Run(() => LexicalParser.Parse(code));
		}
		
		private Task<SyntaxParseResult> SyntaxParseAsync(LexicalParseResult lexicalParseResult)
		{
			return Task.Run(() => SyntaxParser.Parse(lexicalParseResult));
		}
	}
}
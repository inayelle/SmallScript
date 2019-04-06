using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallScript.DesktopUI.Details.Enums;
using SmallScript.DesktopUI.Interfaces;
using SmallScript.Grammars.BackusNaur.Parser.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.RegexParser.Parser.Details;
using SmallScript.LexicalParsers.Shared.Details;
using SmallScript.PolishWriteback.Executor.Details;
using SmallScript.PolishWriteback.Generator.Details;
using SmallScript.Shared.Details.Auxiliary;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details;
using SmallScript.SyntaxParsers.PrecedenceParser.Parser.Details;

namespace SmallScript.DesktopUI.Details.Logic
{
	internal sealed class Model
	{
		private readonly IView _view;

		private readonly IGrammarParser<IGrammar> _grammarParser;

		private readonly Compiler _compiler;

		private readonly WritebackGenerator _writebackGenerator;
		private readonly WritebackExecutor  _writebackExecutor;

		public Model(IView view)
		{
			_view = Require.NotNull(view, nameof(view));

			_grammarParser = new BackusNaurGrammarParser(new PrecedenceEntryFactory());

			var grammar = LoadGrammar(Configuration.GrammarFile);

			var regexParser = new RegexParser(grammar);

			regexParser.OnSuccessfulParse += PostParseFormattingHandler.Handle;

			_compiler = new Compiler
			{
					LexicalParser = regexParser,
					SyntaxParser  = new PrecedenceParser(grammar)
			};

			_writebackGenerator = new WritebackGenerator();
			_writebackExecutor  = new WritebackExecutor(_view.StandartInput, _view.StandartOutput);
		}

		public async Task CompileAsync()
		{
			await CompileAsync(_view.CodeField);

			_view.ActiveTab = DisplayTab.Tokens;
		}

		public async Task ExecuteAsync()
		{
			_view.StatusField = "Execution...";

			var compileResult = await CompileAsync(_view.CodeField);

			if (!compileResult.Ok)
			{
				_view.ActiveTab = DisplayTab.Tokens;
				return;
			}

			var writebackTokens = _writebackGenerator.Generate(compileResult.Tokens).ToList();
			_view.WritebackList = writebackTokens.Select(t => t.ToString());

			_view.ActiveTab = DisplayTab.Execution;
			_view.StandartOutput.Clear();

			var executionResult = _writebackExecutor.Execute(writebackTokens);

			_view.HistoryList = executionResult.History.Select(h => h.ToString());

			_view.StatusField = executionResult.Ok ? "Execution ended" : executionResult.Message;
		}

		private IGrammar LoadGrammar(string file)
		{
			using (var reader = new StreamReader(file, Encoding.UTF8))
			{
				var grammarConfig = reader.ReadToEnd();

				return _grammarParser.Parse(grammarConfig);
			}
		}

		private async Task<CompileResult> CompileAsync(string code)
		{
			var compileResult = await _compiler.CompileAsync(code);

			_view.TokenList = compileResult.LexicalParseResult
			                               .Tokens
			                               .Select(t => t.ToString());

			_view.StatusField = compileResult.Ok
					? "Compilation succeeded"
					: compileResult.Error.ToString();

			return compileResult;
		}
	}
}
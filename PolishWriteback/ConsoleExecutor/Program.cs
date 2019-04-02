using System;
using System.IO;
using System.Text;
using SmallScript.Grammars.BackusNaur.Parser.Details;
using SmallScript.Grammars.Shared.Interfaces;
using SmallScript.LexicalParsers.RegexParser.Parser.Details;
using SmallScript.LexicalParsers.Shared.Details;
using SmallScript.LexicalParsers.Shared.Interfaces;
using SmallScript.PolishWriteback.Executor.Details;
using SmallScript.PolishWriteback.Executor.Interfaces;
using SmallScript.PolishWriteback.Generator.Details;
using SmallScript.SyntaxParsers.PrecedenceParser.Generator.Details;
using SmallScript.SyntaxParsers.PrecedenceParser.Parser.Details;
using SmallScript.SyntaxParsers.Shared.Interfaces;

namespace SmallScript.PolishWriteback.ConsoleExecutor
{
	class Program
	{
		private static readonly string WorkingDirectory = Directory.GetCurrentDirectory();

		private static readonly string Grammar = "Docs/grammar";
		private static readonly string Input   = "input";

		static void Main(string[] args)
		{
			string input = GetInput();

			Console.WriteLine(new string('-', 20));
			Console.WriteLine(input);
			Console.WriteLine(new string('-', 20));

			var lexicalParser = GetLexicalParser();
			var syntaxParser  = GetSyntaxParser();
			var generator     = GetGenerator();
			var executor      = GetExecutor();

			var lexicalParseResult = lexicalParser.Parse(input);
			if (!lexicalParseResult.Ok)
			{
				Console.WriteLine("Lexical parse failure");
				Console.WriteLine(lexicalParseResult.Error);
				return;
			}

			Console.WriteLine("Tokens");
			foreach (var token in lexicalParseResult.Tokens)
			{
				Console.WriteLine(token);
			}

			Console.WriteLine(new string('-', 20));

			var syntaxParseResult = syntaxParser.Parse(lexicalParseResult);
			if (!syntaxParseResult.Ok)
			{
				Console.WriteLine("Syntax parse failure");
				Console.WriteLine(lexicalParseResult.Error);
				return;
			}

			var writebackTokens = generator.Generate(lexicalParseResult.Tokens);

			Console.WriteLine("Writeback tokens");
			for (var i = 0; i < writebackTokens.Length; i++)
			{
				var token = writebackTokens[i];
				Console.WriteLine($"{i,-3} {token}");
			}

			Console.WriteLine(new string('-', 20));

			Console.WriteLine("Execution...");
			Console.WriteLine(new string('-', 20));
			executor.Execute(writebackTokens);
		}

		private static string GetInput()
		{
			using (var reader = new StreamReader(GetFileStream(Input), Encoding.UTF8))
			{
				return reader.ReadToEnd();
			}
		}

		private static IGrammar GetGrammar()
		{
			using (var stream = GetFileStream(Grammar))
			{
				return new BackusNaurGrammarParser(new PrecedenceEntryFactory()).Parse(stream);
			}
		}

		private static ILexicalParser GetLexicalParser()
		{
			var parser = new RegexParser(GetGrammar());
			parser.OnSuccessfulParse += PostParseFormattingHandler.Handle;

			return parser;
		}

		private static ISyntaxParser GetSyntaxParser()
		{
			return new PrecedenceParser(GetGrammar());
		}

		private static WritebackGenerator GetGenerator()
		{
			return new WritebackGenerator();
		}

		private static WritebackExecutor GetExecutor()
		{
			return new WritebackExecutor(GetIo());
		}

		private static IInputOutput GetIo()
		{
			return new InputOutput();
		}

		private static Stream GetFileStream(string filename)
		{
			return new FileStream(Path.Combine(WorkingDirectory, filename), FileMode.Open, FileAccess.Read);
		}
	}

	internal class InputOutput : IInputOutput
	{
		public int Read()
		{
			return Int32.Parse(Console.ReadLine()?.Trim());
		}

		public void Write(int arg)
		{
			Console.WriteLine(arg);
		}

		public void Write(string arg)
		{
			Console.WriteLine(arg);
		}
	}
}
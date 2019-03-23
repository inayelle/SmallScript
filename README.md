# SmallScript
TTD developed compiler

### Avaliable modules:
 - Lexical parsers:
    - Shared - declares common lexical parsers interfaces
    - Regex parser - splits input code into tokens by grammar-based regular expression
 - Syntax parsers:
    - Shared - declares common syntax parsers interfaces
    - Precedence-bases parser - ascending analyzer, based on grammar's precedence table
 - Calculator - polish writeback expression simple executor, uses any of defined lexical/syntax parsers
 - Grammars:
    - Shared - declares common interfaces for grammar implementations
    - BackusNaur - Backus-Naur grammar+parser implementation
 - Shared - solution-wide auxiliary things
 - DesktopUI - [AvaloniaUI](https://github.com/AvaloniaUI/Avalonia) based graphics interface, features editing files, switching between parsers and compiling
 - Docs - language-specific grammar, examples, etc

##### Also, each implementation (except UI) includes 'Tests' project for unit-testing.

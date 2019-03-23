# SmallScript
TTD developed compiler

### Avaliable modules:
 - __Lexical parsers__:
    - __Shared__ - declares common lexical parsers interfaces
    - __Regex parser__ - splits input code into tokens by grammar-based regular expression
 - __Syntax parsers__:
    - __Shared__ - declares common syntax parsers interfaces
    - __Precedence-based parser__ - ascending analyzer, based on grammar's precedence table
 - __Calculator__ - polish writeback expression simple executor, uses any of defined lexical/syntax parsers
 - __Grammars__:
    - __Shared__ - declares common interfaces for grammar implementations
    - __BackusNaur__ - Backus-Naur grammar+parser implementation
 - __Shared__ - solution-wide auxiliary things
 - __DesktopUI__ - [AvaloniaUI](https://github.com/AvaloniaUI/Avalonia) based graphics interface, features editing files, switching between parsers and compiling
 - __Docs__ - language-specific grammar, examples, etc

##### Also, each implementation (except UI) includes 'Tests' project for unit-testing.

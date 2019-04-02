namespace SmallScript.LexicalParsers.Shared.Enums
{
	public static class Symbol
	{
		public const string Var                = "@VAR";
		public const string Const              = "@CONST";
		public const string BoolValue          = "@BOOL";
		public const string IntValue           = "@INT";
		public const string OperationDelimiter = "<EOL>";
		public const string Label              = "@LBL";
		public const string LabelDeclaration   = "@LBL_DECL";
		public const string JumpByNotEquality  = "@JNE";
		public const string Jump               = "@JMP";
		public const string Declare            = "decl";
		public const string Let                = "let";
		public const string OpenLoop           = "for";
		public const string To                 = "to";
		public const string By                 = "by";
		public const string Do                 = "do";
		public const string CloseLoop          = "rof";
		public const string OpenCondition      = "if";
		public const string Then               = "then";
		public const string Else               = "else";
		public const string CloseCondition     = "fi";
		public const string StandartInput      = "stdin";
		public const string StandartOutput     = "stdout";
		public const string Assign             = "=";
		public const string OpenCurlyBrace     = "{";
		public const string CloseCurlyBrace    = "}";
		public const string OpenParenthesis    = "(";
		public const string CloseParenthesis   = ")";
		public const string Less               = "<";
		public const string Greater            = ">";
		public const string GreaterEqual       = ">=";
		public const string LessEqual          = "<=";
		public const string Equal              = "==";
		public const string NotEqual           = "!=";
		public const string Plus               = "+";
		public const string Minus              = "-";
		public const string Multiplication     = "*";
		public const string Division           = "/";
		public const string Power              = "**";
		public const string StreamReading      = ">>";
		public const string StreamWriting      = "<<";
		public const string QuestionMark       = "?";
		public const string Colon              = ":";
		public const string Tilde              = "~";
	}
}
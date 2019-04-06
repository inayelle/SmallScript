using System;
using System.Collections.Generic;
using Avalonia.Interactivity;
using SmallScript.DesktopUI.Details.Enums;
using SmallScript.PolishWriteback.Executor.Interfaces;

namespace SmallScript.DesktopUI.Interfaces
{
	internal interface IView
	{
		int CodeFontSize { get; set; }

		string StatusField { get; set; }
		string CodeField   { get; set; }

		string Title { get; set; }

		IEnumerable<string> TokenList     { get; set; }
		IEnumerable<string> WritebackList { get; set; }
		IEnumerable<string> HistoryList   { get; set; }

		IInput  StandartInput  { get; }
		IOutput StandartOutput { get; }

		DisplayTab ActiveTab { get; set; }

		event EventHandler<string> OnOpenFileButtonClick;
		event EventHandler<string> OnSaveFileButtonClick;

		event EventHandler<RoutedEventArgs> OnCloseFileButtonClick;
		event EventHandler<RoutedEventArgs> OnExitFileButtonClick;

		event EventHandler<RoutedEventArgs> OnCompileButtonClick;
		event EventHandler<RoutedEventArgs> OnExecuteButtonClick;
	}
}
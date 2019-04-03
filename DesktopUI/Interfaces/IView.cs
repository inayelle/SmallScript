using System;
using System.Collections.Generic;
using Avalonia.Interactivity;

namespace SmallScript.DesktopUI.Interfaces
{
	internal interface IView
	{
		int CodeFontSize { get; set; }

		string StatusField { get; set; }
		string Title       { get; set; }

		IEnumerable<string> LexemesField   { get; set; }
		IEnumerable<string> WritebackField { get; set; }

		event EventHandler<RoutedEventArgs> OnOpenFileButtonClick;
		event EventHandler<RoutedEventArgs> OnSaveFileButtonClick;
		event EventHandler<RoutedEventArgs> OnCloseFileButtonClick;
		event EventHandler<RoutedEventArgs> OnExitFileButtonClick;
		
		event EventHandler<RoutedEventArgs> OnCompileButtonClick;
		event EventHandler<RoutedEventArgs> OnRunButtonClick;
	}
}
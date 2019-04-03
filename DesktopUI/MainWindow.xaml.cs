using System;
using System.Collections.Generic;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SmallScript.DesktopUI.Details;
using SmallScript.DesktopUI.Details.Logic;
using SmallScript.DesktopUI.Details.Theming;
using SmallScript.DesktopUI.Interfaces;

namespace SmallScript.DesktopUI
{
	public class MainWindow : Window, IView
	{
		public int CodeFontSize { get; set; }
		
		public string StatusField { get; set; }
		
		public IEnumerable<string> LexemesField   { get; set; }
		public IEnumerable<string> WritebackField { get; set; }
		
		public event EventHandler<RoutedEventArgs> OnOpenFileButtonClick;
		public event EventHandler<RoutedEventArgs> OnSaveFileButtonClick;
		public event EventHandler<RoutedEventArgs> OnCloseFileButtonClick;
		public event EventHandler<RoutedEventArgs> OnExitFileButtonClick;
		
		public event EventHandler<RoutedEventArgs> OnCompileButtonClick;
		public event EventHandler<RoutedEventArgs> OnRunButtonClick;
		
		public MainWindow()
		{
			InitializeComponent();
			DarkTheme.Instance.Apply(this);

			var model = new Model(this);
			var presenter = new Presenter(this, model);
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
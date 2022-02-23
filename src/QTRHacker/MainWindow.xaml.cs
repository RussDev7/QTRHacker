﻿using QTRHacker.Assets;
using QTRHacker.Controls;
using QTRHacker.Localization;
using QTRHacker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QTRHacker
{
	public partial class MainWindow : MWindow
	{
		public MainWindowViewModel ViewModel => DataContext as MainWindowViewModel;
		public MainWindow()
		{
			HackGlobal.LoadConfig();
#if DEBUG
#else
			LocalizationManager.Instance.SetCulture(System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
#endif

			InitializeComponent();

			DataContext = new MainWindowViewModel();
			Title = "QTRHacker-" + Assembly.GetExecutingAssembly().GetName().Version.ToString();


			ViewModel.AttachedToGame += ViewModel_AttachedToGame;
			ViewModel.AttachedToGameWorksFinished += ViewModel_AttachedToGameWorksFinished;
		}

		private void ViewModel_AttachedToGameWorksFinished(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			EnableTabs();
		}

		private void ViewModel_AttachedToGame(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				ViewModel.DirectFunctionsPageViewModel.UpdateFunctionsList();
			});
		}

		internal void EnableTabs()
		{
			if (MainTabControl.Items.Count == 0)
				return;
			foreach (TabItem item in MainTabControl.Items)
				item.IsEnabled = true;
			(MainTabControl.Items[0] as TabItem).IsSelected = true;
		}
		static MainWindow()
		{
			GameImages.Touch();
		}
	}
}

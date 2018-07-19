﻿using System;
using System.Collections.Generic;
using System.Linq;
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

namespace yGuiWPF_Demo
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void ShowButtons(object sender, RoutedEventArgs e)
		{
			var window = new Buttons();
			window.ShowDialog();
		}

		private void ShowKnobs(object sender, RoutedEventArgs e)
		{
			var window = new Knobs();
			window.ShowDialog();
		}
	}
}

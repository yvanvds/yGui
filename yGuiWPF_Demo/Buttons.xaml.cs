using System;
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
using System.Windows.Shapes;

namespace yGuiWPF_Demo
{
	/// <summary>
	/// Interaction logic for Buttons.xaml
	/// </summary>
	public partial class Buttons : Window
	{
		public Buttons()
		{
			InitializeComponent();
		}

		private void Button1_Click(object sender, RoutedEventArgs e)
		{
			Button2.Visible = !Button2.Visible;
		}
	}
}

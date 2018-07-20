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
using System.Windows.Threading;

namespace yGuiWPF_Demo
{
	/// <summary>
	/// Interaction logic for Leds.xaml
	/// </summary>
	public partial class Leds : Window
	{
		public Leds()
		{
			InitializeComponent();

			for(int i = 0; i < 10; i++)
			{
				LedGrid.ColumnDefinitions.Add(new ColumnDefinition());
				LedGrid.RowDefinitions.Add(new RowDefinition());
			}

			var random = new Random();

			for(int i = 0; i < 10; i++)
			{
				
				for(int j = 0; j < 10; j++)
				{
					var led = new yGuiWPF.Controls.Led();
					switch(random.Next(5))
					{
						case 0:
							led.Color = yGuiWPF.Brushes.White;
							break;
						case 1:
							led.Color = yGuiWPF.Brushes.Success;
							break;
						case 2:
							led.Color = yGuiWPF.Brushes.Warning;
							break;
						case 3:
							led.Color = yGuiWPF.Brushes.Danger;
							break;
						case 4:
							led.Color = yGuiWPF.Brushes.Accent;
							break;
					}

					led.Scale = 0.2f + (float)random.NextDouble() * 0.8f;
					Grid.SetRow(led, i);
					Grid.SetColumn(led, j);
					LedGrid.Children.Add(led);
				}
			}

			DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
			timer.Tick += (s, e) =>
			{
				foreach(var led in LedGrid.Children)
				{
					if(random.Next(5) > 3)
					{
						(led as yGuiWPF.Controls.Led).Blink(random.Next(200, 1000));
					}
				}
			};
			timer.Start();
		}

	}
}

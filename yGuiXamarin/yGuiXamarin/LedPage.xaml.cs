using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace yGuiXamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LedPage : ContentPage
	{
		public LedPage ()
		{
			InitializeComponent ();

			for (int i = 0; i < 5; i++)
			{
				LedGrid.ColumnDefinitions.Add(new ColumnDefinition());
				LedGrid.RowDefinitions.Add(new RowDefinition());
			}

			var random = new Random();

			for (int i = 0; i < 5; i++)
			{

				for (int j = 0; j < 5; j++)
				{
					var led = new yGui.Led();
					switch (random.Next(5))
					{
						case 0:
							led.Color = yGui.Colors.White;
							break;
						case 1:
							led.Color = yGui.Colors.Success;
							break;
						case 2:
							led.Color = yGui.Colors.Warning;
							break;
						case 3:
							led.Color = yGui.Colors.Danger;
							break;
						case 4:
							led.Color = yGui.Colors.Accent;
							break;
					}

					led.Scale = 0.2f + (float)random.NextDouble() * 0.8f;
					Grid.SetRow(led, i);
					Grid.SetColumn(led, j);
					LedGrid.Children.Add(led);
				}
			}

			Timer timer = new Timer(
				timerElapsed,
				null,
				new TimeSpan(0, 0, 0, 0, 200),
				new TimeSpan(0, 0, 0, 0, 200)
				);
		}

		private void timerElapsed(Object stateInfo)
		{
			var random = new Random();
			Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
			{
				foreach (var led in LedGrid.Children)
				{
					if (random.Next(5) > 3)
					{
						(led as yGui.Led).Blink(random.Next(200, 1000));
					}
				}
			});
			
		}
	}
}
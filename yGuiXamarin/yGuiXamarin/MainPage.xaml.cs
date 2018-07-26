using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace yGuiXamarin
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void ShowButtons(object sender, EventArgs e)
		{
			var page = new ButtonPage();
			Navigation.PushAsync(page);
		}

		private void ShowKnobs(object sender, EventArgs e)
		{
			var page = new KnobPage();
			Navigation.PushAsync(page);
		}

		private void ShowSliders(object sender, EventArgs e)
		{
			var page = new SliderPage();
			Navigation.PushAsync(page);
		}

		private void ShowLeds(object sender, EventArgs e)
		{
			var page = new LedPage();
			Navigation.PushAsync(page);
		}

		private void ShowXYPads(object sender, EventArgs e)
		{
			var page = new XYPadPage();
			Navigation.PushAsync(page);
		}

		private void ShowMTPad(object sender, EventArgs e)
		{
			var page = new MTPadPage();
			Navigation.PushAsync(page);
		}
	}
}

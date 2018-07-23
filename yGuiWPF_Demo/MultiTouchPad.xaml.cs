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
	/// Interaction logic for MultiTouchPad.xaml
	/// </summary>
	public partial class MultiTouchPad : Window
	{
		public MultiTouchPad()
		{
			InitializeComponent();

			Pad.OnTouch += (s, e) =>
			{
				var args = e as yGuiWPF.Controls.MTPad.TouchArgs;
				Label labelX = null;
				Label labelY = null;
				switch(args.Id)
				{
					case 0:
						labelX = Point0x;
						labelY = Point0y;
						break;
					case 1:
						labelX = Point1x;
						labelY = Point1y;
						break;
					case 2:
						labelX = Point2x;
						labelY = Point2y;
						break;
					case 3:
						labelX = Point3x;
						labelY = Point3y;
						break;
					case 4:
						labelX = Point4x;
						labelY = Point4y;
						break;
				}

				if(labelX != null)
				{
					if(args.Action == TouchAction.Up)
					{
						labelX.Content = "X:";
						labelY.Content = "Y:";
					} else
					{
						labelX.Content = "X: " + args.Pos.X.ToString("n2");
						labelY.Content = "Y: " + args.Pos.Y.ToString("n2");
					}
				}
			};
		}
	}
}

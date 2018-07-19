using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace yGuiWPF
{
	public static class Brushes
	{
		public readonly static SolidColorBrush Default = new SolidColorBrush(Colors.Default);
		public readonly static SolidColorBrush Success = new SolidColorBrush(Colors.Success);
		public readonly static SolidColorBrush Warning = new SolidColorBrush(Colors.Warning);
		public readonly static SolidColorBrush Danger = new SolidColorBrush(Colors.Danger);
		public readonly static SolidColorBrush Accent = new SolidColorBrush(Colors.Accent);

		public readonly static SolidColorBrush WindowBackground = new SolidColorBrush(Colors.WindowBackground);
		public readonly static SolidColorBrush AreaBackground = new SolidColorBrush(Colors.AreaBackground);
		public readonly static SolidColorBrush ElementBackground = new SolidColorBrush(Colors.ElementBackground);
		public readonly static SolidColorBrush HighlightBackground = new SolidColorBrush(Colors.HighlightBackground);

		public readonly static SolidColorBrush White = new SolidColorBrush(Colors.White);
	}
}

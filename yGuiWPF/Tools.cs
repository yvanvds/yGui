using SkiaSharp;
using SkiaSharp.Extended.Iconify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace yGuiWPF
{
	internal static class Tools
	{
		static Tools()
		{
			SKTextRunLookup.Instance.AddMaterialDesignIcons();
		}

		public static SKColor ToSkia(SolidColorBrush brush)
		{
			Color color = brush.Color;
			return new SKColor(color.R, color.G, color.B, color.A);
		}
	}
}

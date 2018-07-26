using SkiaSharp;
using SkiaSharp.Extended.Iconify;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace yGui
{
	internal static class Tools
	{
		static Tools()
		{
			SKTextRunLookup.Instance.AddMaterialDesignIcons();
		}

		public static SKColor ToSkia(Color color)
		{
			return new SKColor((byte)(color.R * 255), (byte)(color.G * 255), (byte)(color.B * 255), (byte)(color.A * 255));
		}
	}
}

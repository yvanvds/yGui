using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace yGui
{
	public class Led : SkiaSharp.Views.Forms.SKCanvasView
	{
		private static SKPaint border;

		static Led()
		{
			border = new SKPaint()
			{
				Style = SKPaintStyle.Stroke,
				Color = Tools.ToSkia(Colors.HighlightBackground),
				StrokeWidth = 2,
				IsAntialias = true
			};
		}

		protected SKPaint ledPaint;

		public Led()
		{
			ledPaint = new SKPaint()
			{
				Style = SKPaintStyle.Fill,
				IsAntialias = true
			};
		}

		#region Color
		private Color color = Colors.White;

		public Color Color
		{
			get { return color; }
			set { color = value; InvalidateSurface(); }
		}
		#endregion Color

		#region Scale
		private float scale;

		public new float Scale
		{
			get { return scale; }
			set { scale = value; InvalidateSurface(); }
		}
		#endregion Scale

		#region Blink
		private float alpha = 0;
		private float stepSize;

		public void Blink(int milliSeconds)
		{
			stepSize = 255f / (milliSeconds / 20f);
			alpha = 255f;
			InvalidateSurface();
		}
		#endregion Blink

		protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
		{
			int width = e.Info.Width;
			int height = e.Info.Height;
			int size = width < height ? width : height;
			float multiplier = width / (float)Width;
			float margin = size * 0.01f * multiplier;

			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;
			border.StrokeWidth = margin;

			canvas.Clear();

			SKPoint center = new SKPoint(width / 2f, height / 2f);
			float radius = (size * 0.5f - margin) * Scale;

			ledPaint.Color = new SKColor((byte)(color.R * 255), (byte)(color.G * 255), (byte)(color.B * 255), (byte)alpha);
			canvas.DrawCircle(center, radius, ledPaint);
			canvas.DrawCircle(center, radius, border);

			if (alpha > 0)
			{
				alpha -= stepSize;
				if (alpha < 0) alpha = 0;
				Invalidator.Add(this);
			}
		}
	}

}


using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;
using SkiaSharp.Extended.Iconify;
using SkiaSharp.Views.Forms;

namespace yGui
{
	public class RoundButton : Button
	{
		protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
		{
			int width = e.Info.Size.Width;
			int height = e.Info.Size.Height;
			int size = width < height ? width : height;
			float multiplier = width / (float)Width;
			float margin = size * 0.01f * multiplier;

			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();
			if (!Visible) return;

			SKPoint center = new SKPoint();
			center.X = width / 2.0f;
			center.Y = height / 2.0f;

			SKRect outerRect = new SKRect(margin, margin, width - margin, height - margin);
			SKRect innerRect = new SKRect(margin + 4 * multiplier, margin + 4 * multiplier, width - margin - 4 * multiplier, height - margin - 4 * multiplier);

			buttonPaint.Color = Tools.ToSkia(BackGround);
			borderPaint.Color = Tools.ToSkia(ForeGround);
			borderPaint.StrokeWidth = margin;
			togglePaint.Color = borderPaint.Color;
			textPaint.Color = Tools.ToSkia(TextColor);
			textPaint.TextSize = margin * multiplier;

			switch (TextScale)
			{
				case yGui.Scale.Smallest:
					textPaint.TextSize *= 0.5f;
					break;
				case yGui.Scale.Small:
					textPaint.TextSize *= 0.75f;
					break;
				case yGui.Scale.Big:
					textPaint.TextSize *= 2f;
					break;
				case yGui.Scale.Huge:
					textPaint.TextSize *= 3f;
					break;
			}

			canvas.DrawCircle(center, size * 0.5f - margin, buttonPaint);
			canvas.DrawCircle(center, size * 0.5f - margin, borderPaint);

			if (IsToggle && Toggled)
			{
				canvas.DrawCircle(center, size * 0.5f - margin - 4 * multiplier, togglePaint);
			}

			if (Text != string.Empty)
			{
				center.Y += textPaint.TextSize * 0.35f;
				canvas.DrawIconifiedText(Text, center.X, center.Y, textPaint);
			}
		}
	}
}

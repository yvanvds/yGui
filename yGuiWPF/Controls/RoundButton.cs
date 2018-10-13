using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SkiaSharp;
using SkiaSharp.Extended.Iconify;
using SkiaSharp.Views.Desktop;

namespace yGuiWPF.Controls
{
	public class RoundButton : Button
	{
		static RoundButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(RoundButton), new FrameworkPropertyMetadata(typeof(Button)));
		}

		protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
		{
			int width = e.Info.Width;
			int height = e.Info.Height;
			int size = width < height ? width : height;
			float margin = size * 0.05f;

			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();
			if (!Visible) return;

			SKPoint center = new SKPoint();
			center.X = width / 2.0f;
			center.Y = height / 2.0f;

			SKRect outerRect = new SKRect(margin, margin, width - margin, height - margin);
			SKRect innerRect = new SKRect(margin + 4, margin + 4, width - margin - 4, height - margin - 4);

			buttonPaint.Color = Tools.ToSkia(BackGround);
			borderPaint.Color = Tools.ToSkia(ForeGround);
			togglePaint.Color = borderPaint.Color;
			textPaint.Color = Tools.ToSkia(TextColor);
			textPaint.TextSize = margin * 5;

			switch (TextScale)
			{
				case TextScales.Smallest:
					textPaint.TextSize *= 0.5f;
					break;
				case TextScales.Small:
					textPaint.TextSize *= 0.75f;
					break;
				case TextScales.Big:
					textPaint.TextSize *= 2f;
					break;
				case TextScales.Huge:
					textPaint.TextSize *= 3f;
					break;
			}

			canvas.DrawCircle(center, size * 0.5f - margin, buttonPaint);
			canvas.DrawCircle(center, size * 0.5f - margin, borderPaint);

			if(IsToggle && Toggled)
			{
				canvas.DrawCircle(center, size * 0.5f - margin - 4, togglePaint);
			}

			if (Text != string.Empty)
			{
				center.Y += textPaint.TextSize * 0.35f;
				canvas.DrawIconifiedText(Text, center.X, center.Y, textPaint);
			}
		}
	}
}

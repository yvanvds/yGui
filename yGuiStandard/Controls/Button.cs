using SkiaSharp;
using SkiaSharp.Extended.Iconify;
using SkiaSharp.Views.Forms;
using System;
using System.Drawing;

namespace yGui
{
	public class Button : SkiaSharp.Views.Forms.SKCanvasView
	{

		protected SKPaint buttonPaint;
		protected SKPaint borderPaint;
		protected SKPaint togglePaint;
		protected SKPaint textPaint;

		public Button()
		{
			EnableTouchEvents = true;
			buttonPaint = new SKPaint()
			{
				Style = SKPaintStyle.Fill,
			};

			borderPaint = new SKPaint()
			{
				Style = SKPaintStyle.Stroke,
				StrokeWidth = 4,
				IsAntialias = true
			};

			togglePaint = new SKPaint()
			{
				Style = SKPaintStyle.Fill,
				IsAntialias = true
			};

			textPaint = new SKPaint()
			{
				TextSize = 16,
				IsAntialias = true,
				IsStroke = false,
				TextAlign = SKTextAlign.Center,
				SubpixelText = true
			};
		}

		#region ForeGround
		Xamarin.Forms.Color foreGround = Colors.Default;
		public Xamarin.Forms.Color ForeGround
		{
			get => foreGround;
			set
			{
				foreGround = value;
				InvalidateSurface();
			}
		}
		#endregion ForeGround

		#region BackGround
		Color backGround = Colors.ElementBackground;
		public Color BackGround
		{
			get => backGround;
			set
			{
				backGround = value;
				InvalidateSurface();
			}
		}
		#endregion BackGround

		#region Text
		private string text = "button";
		public string Text
		{
			get => text;
			set
			{
				text = value;
				InvalidateSurface();
			}
		}
		#endregion Text

		#region visible
		private bool visible = true;
		public bool Visible
		{
			get => visible;
			set
			{
				visible = value;
				InvalidateSurface();
			}
		}
		#endregion visible


		#region TextColor
		private Color textColor = Colors.White;

		public Color TextColor
		{
			get { return textColor; }
			set {
				textColor = value;
				InvalidateSurface();
			}
		}
		#endregion TextColor

		#region IsToggle
		private bool isToggle;

		public bool IsToggle
		{
			get { return isToggle; }
			set { isToggle = value; InvalidateSurface(); }
		}
		#endregion IsToggle

		#region Toggled
		private bool toggled;

		public bool Toggled
		{
			get { return toggled; }
			set { toggled = value; InvalidateSurface(); }
		}
		#endregion Toggled

		#region Rounded
		private bool isRounded;

		public bool IsRounded
		{
			get { return isRounded; }
			set { isRounded = value; InvalidateSurface(); }
		}
		#endregion Rounded

		#region TextScale
		private Scale textScale = yGui.Scale.Normal;		

		public Scale TextScale
		{
			get { return textScale; }
			set { textScale = value; InvalidateSurface(); }
		}
		#endregion TextScale

		#region Touch
		protected override void OnTouch(SKTouchEventArgs e)
		{
			if (!visible) return;
			if(e.ActionType == SKTouchAction.Pressed)
			{
				if (IsToggle) Toggled = !Toggled;
				Pressed?.Invoke(this, EventArgs.Empty);
				e.Handled = true;
			}
		}

		public event EventHandler Pressed;
		#endregion Touch

		

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
			if (!visible) return;

			SKPoint center = new SKPoint();
			center.X = width / 2.0f;
			center.Y = height / 2.0f;

			SKRect outerRect = new SKRect(margin, margin, width - margin, height - margin);
			SKRect innerRect = new SKRect(margin + 4*multiplier, margin + 4*multiplier, width - margin - 4 * multiplier, height - margin - 4 * multiplier);

			buttonPaint.Color = Tools.ToSkia(backGround);
			borderPaint.Color = Tools.ToSkia(foreGround);
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

			float corner = IsRounded ? 15 : 0;

			canvas.DrawRoundRect(outerRect, corner, corner, buttonPaint);
			canvas.DrawRoundRect(outerRect, corner, corner, borderPaint);

			if (IsToggle && Toggled)
			{
				corner = IsRounded ? corner * 0.85f : 0;
				canvas.DrawRoundRect(innerRect, corner, corner, togglePaint);
			}
			if (Text != string.Empty)
			{
				center.Y += textPaint.TextSize * 0.35f;
				canvas.DrawIconifiedText(Text, center.X, center.Y, textPaint);
			}
		}
	}
}

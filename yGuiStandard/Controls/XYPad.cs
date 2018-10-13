using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace yGui
{
	public class XYPad : SkiaSharp.Views.Forms.SKCanvasView
	{
		private static SKPaint valuePaint;

		static XYPad()
		{
			valuePaint = new SKPaint()
			{
				TextSize = 10,
				IsAntialias = true,
				Color = new SKColor(255, 255, 255),
				IsStroke = false
			};
		}

		private SKPaint backgroundPaint;
		private SKPaint centerPaint;
		private SKPaint borderPaint;
		private SKPaint indicatorPaint;

		public XYPad()
		{
			EnableTouchEvents = true;
			backgroundPaint = new SKPaint()
			{
				Style = SKPaintStyle.Fill,
			};

			centerPaint = new SKPaint()
			{
				Style = SKPaintStyle.Stroke,
				StrokeWidth = 1,
			};

			borderPaint = new SKPaint()
			{
				Style = SKPaintStyle.Stroke,
				StrokeWidth = 4,
				IsAntialias = true,
			};

			indicatorPaint = new SKPaint()
			{
				Style = SKPaintStyle.StrokeAndFill,
				StrokeWidth = 1,
				IsAntialias = true
			};
		}

		private Color foreGround = Colors.Default;
		public Color ForeGround
		{
			get { return foreGround; }
			set { foreGround = value;  InvalidateSurface(); }
		}

		private Color backGround = Colors.ElementBackground;
		public Color BackGround
		{
			get { return backGround; }
			set { backGround = value; InvalidateSurface(); }
		}

		private Color border = Colors.White;				
		public Color Border
		{
			get { return border; }
			set { border = value; InvalidateSurface(); }
		}

		private bool isRounded = false;
		public bool IsRounded
		{
			get { return isRounded; }
			set { isRounded = value; InvalidateSurface(); }
		}

		public bool centered = true;
		public bool Centered
		{
			get { return centered; }
			set { centered = value; InvalidateSurface(); }
		}

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

		public Point value = new Point();
		public Point Value
		{
			get => value;
			set
			{
				this.value = value;

				if (this.value.X > 1) this.value.X = 1;
				if (this.value.Y > 1) this.value.Y = 1;

				if (Centered)
				{
					if (this.value.X < -1) this.value.X = -1;
					if (this.value.Y < -1) this.value.Y = -1;
				}
				else
				{
					if (this.value.X < 0) this.value.X = 0;
					if (this.value.Y < 0) this.value.Y = 0;
				}

				InvalidateSurface();
			}
		}

		Point screenToValue(SKPoint screenPos)
		{
			Point result = new Point();
			
			if (Centered)
			{
				result.X = (screenPos.X - (actualsize.X * 0.5)) / (actualsize.X * 0.5f - margin * 2);
				result.Y = -(screenPos.Y - (actualsize.Y * 0.5)) / (actualsize.Y * 0.5f - margin * 2);
			}
			else
			{
				result.X = (screenPos.X - (margin * 2)) / (actualsize.X - margin * 4);
				result.Y = 1 - (screenPos.Y - (margin * 2)) / (actualsize.Y - margin * 4);
			}
			return result;
		}

		SKPoint valueToScreen(Point valuePos)
		{
			SKPoint result = new SKPoint();

			if (Centered)
			{
				result.X = (float)valuePos.X * ((float)actualsize.X * 0.5f - margin * 2) + ((float)actualsize.X * 0.5f);
				result.Y = (float)-valuePos.Y * ((float)actualsize.Y * 0.5f - margin * 2) + ((float)actualsize.Y * 0.5f);
			}
			else
			{
				result.X = (margin * 2) + ((float)valuePos.X * ((float)actualsize.X - margin * 4));
				result.Y = (margin * 2) + ((1 - (float)valuePos.Y) * ((float)actualsize.Y - margin * 4));
			}
			return result;
		}

		private bool showValue = false;
		public bool ShowValue
		{
			get => showValue;
			set { showValue = value; InvalidateSurface(); }
		}

		#region Touch
		public event EventHandler ValueChanged;

		private void GenerateValueChangedEvent()
		{
			ValueChanged?.Invoke(this, EventArgs.Empty);
		}

		protected override void OnTouch(SKTouchEventArgs e)
		{
			if (!visible) return;
			if (e.ActionType == SKTouchAction.Pressed)
			{
				Value = screenToValue(e.Location);
				GenerateValueChangedEvent();
			} else if (e.ActionType == SKTouchAction.Moved)
			{
				Value = screenToValue(e.Location);
				GenerateValueChangedEvent();
			}
			e.Handled = true;
		}
		#endregion Touch

		private Point actualsize = new Point();
		float margin = 0;
		protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
		{
		  actualsize.X	= e.Info.Width;
			actualsize.Y = e.Info.Height;
			int size = actualsize.X < actualsize.Y ? (int)actualsize.X : (int)actualsize.Y;
			float multiplier = (float)actualsize.X / (float)Width;
			margin = size * 0.01f * multiplier;

			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();
			if (!visible) return;

			SKPoint center = new SKPoint();
			center.X = (float)actualsize.X / 2.0f;
			center.Y = (float)actualsize.Y / 2.0f;

			SKRect outerRect = new SKRect(margin, margin, (float)actualsize.X - margin, (float)actualsize.Y - margin);

			backgroundPaint.Color = Tools.ToSkia(BackGround);
			borderPaint.Color = Tools.ToSkia(Border);
			borderPaint.StrokeWidth = margin * 0.8f;
			indicatorPaint.Color = Tools.ToSkia(ForeGround);
			indicatorPaint.StrokeWidth = multiplier;

			float corner = IsRounded ? 15 : 0;
			canvas.DrawRoundRect(outerRect, corner, corner, backgroundPaint);

			if (Centered)
			{
				centerPaint.Color = new SKColor((byte)(BackGround.R * 0.9), (byte)(BackGround.G * 0.9), (byte)(BackGround.B * 0.9));
				centerPaint.StrokeWidth = multiplier;
				canvas.DrawLine(new SKPoint(margin, center.Y), new SKPoint((float)actualsize.X - margin, center.Y), centerPaint);
				canvas.DrawLine(new SKPoint(center.X, margin), new SKPoint(center.X, (float)actualsize.Y - margin), centerPaint);
			}

			SKPoint position = valueToScreen(Value);


			canvas.DrawLine(new SKPoint(0f + margin, position.Y), new SKPoint((float)actualsize.X - margin, position.Y), indicatorPaint);
			canvas.DrawLine(new SKPoint(position.X, 0f + margin), new SKPoint(position.X, (float)actualsize.Y - margin), indicatorPaint);
			canvas.DrawCircle(new SKPoint(position.X, position.Y), size * 0.05f, indicatorPaint);

			canvas.DrawRoundRect(outerRect, corner, corner, borderPaint);

			if (ShowValue)
			{
				valuePaint.TextSize = 10 * multiplier;
				canvas.DrawText("X: " + value.X.ToString("n2"), new SKPoint((float)actualsize.X - 50*multiplier, 30*multiplier), valuePaint);
				canvas.DrawText("Y: " + value.Y.ToString("n2"), new SKPoint((float)actualsize.X - 50*multiplier, 40*multiplier), valuePaint);
			}
		}
	}
}

using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace yGui
{
	public class Knob : SkiaSharp.Views.Forms.SKCanvasView
	{
		private static SKPaint valuePaint;

		static Knob()
		{
			valuePaint = new SKPaint()
			{
				Style = SKPaintStyle.Stroke,
				Color = new SKColor(80, 80, 80),
				StrokeWidth = 2,
				IsAntialias = true
			};
		}

		private SKPaint valueTextPaint;
		private SKPaint contourPaint;
		private SKPaint knobPaint;
		private SKPaint textPaint;

		public Knob()
		{
			EnableTouchEvents = true;

			valueTextPaint = new SKPaint()
			{
				TextSize = 0, // will be changed at draw
				//FakeBoldText = true,
				IsAntialias = true,
				Color = new SKColor(200, 200, 200),
				IsStroke = false,
				TextAlign = SKTextAlign.Center
			};

			contourPaint = new SKPaint()
			{
				Style = SKPaintStyle.Stroke,
				StrokeWidth = 2,
				IsAntialias = true
			};

			knobPaint = new SKPaint()
			{
				Style = SKPaintStyle.Stroke,
				IsAntialias = true
			};

			textPaint = new SKPaint()
			{
				//FakeBoldText = true,
				IsAntialias = true,
				IsStroke = false,
				TextAlign = SKTextAlign.Center
			};
		}

		private Color color = Colors.Default;
		public Color Color
		{
			get { return color; }
			set { color = value; InvalidateSurface(); }
		}

		private bool showValue = true;
		public bool ShowValue
		{
			get { return showValue; }
			set { showValue = value; InvalidateSurface(); }
		}

		private float minimum = 0f;
		public float Minimum
		{
			get { return minimum; }
			set
			{
				minimum = value;
				if (Value < minimum) value = minimum;
				InvalidateSurface();
			}
		}

		private float  maximum = 100f;
		public float  Maximum
		{
			get { return maximum; }
			set
			{
				maximum = value;
				if (Value > maximum) Value = maximum;
				InvalidateSurface();
			}
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

		private float value = 0f;
		public float Value
		{
			get { return value; }
			set
			{
				this.value = value;
				if (this.value < minimum) this.value = minimum;
				if (this.value > maximum) this.value = maximum;
				InvalidateSurface();
			}
		}

		private string displayName = string.Empty;
		public string DisplayName
		{
			get { return displayName; }
			set { displayName = value; InvalidateSurface(); }
		}

		#region Touch
		public event EventHandler ValueChanged;

		private void generateValueChangedEvent()
		{
			ValueChanged?.Invoke(this, EventArgs.Empty);
		}

		SKPoint touchPos;
		protected override void OnTouch(SKTouchEventArgs e)
		{
			if (!visible) return;
			if (e.ActionType == SKTouchAction.Pressed)
			{
				touchPos = e.Location;
			} else if (e.ActionType == SKTouchAction.Moved)
			{
				SKPoint newPoint = e.Location;
				SKPoint delta = new SKPoint();
				delta.X = (newPoint.X - touchPos.X) * 0.5f;
				delta.Y = -(newPoint.Y - touchPos.Y) * 0.5f;
				touchPos = newPoint;
				float inc = Math.Abs(delta.X) > Math.Abs(delta.Y) ? (float)delta.X : (float)delta.Y;
				if (multiplier != 0) inc /= multiplier;
				float range = maximum - minimum;
				if (range != 0)
				{
					Value += inc * range / 100;
					generateValueChangedEvent();
				}
				
			}

			e.Handled = true;
		}
		#endregion Touch

		float multiplier = 0;
		protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
		{
			int width = e.Info.Width;
			int height = e.Info.Height;
			int size = width < height ? width : height;
			multiplier = width / (float)Width;
			float margin = size * 0.01f * multiplier;

			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();
			if (!visible) return;

			SKPoint center = new SKPoint();
			center.X = width / 2.0f;
			center.Y = height / 2.0f;

			float outerRadius = size / 2.0f - margin;
			float innerRadius = outerRadius - margin * 2;

			SKRect outerRect = new SKRect(center.X - outerRadius, center.Y - outerRadius, center.X + outerRadius, center.Y + outerRadius);
			SKRect innerRect = new SKRect(center.X - innerRadius, center.Y - innerRadius, center.X + innerRadius, center.Y + innerRadius);

			float startAngle = 135.0f;
			float valueRange = maximum - minimum;
			float sweepAngle = (value - minimum) / valueRange * 270.0f;

			SKRect valueRect = new SKRect(
				center.X - margin * 3,
				height - margin * 6,
				center.X + margin * 3,
				height - margin * 3
				);

			SKColor color = Tools.ToSkia(Color);
			contourPaint.Color = color;
			knobPaint.Color = color;
			knobPaint.StrokeWidth = margin * 3.0f;
			textPaint.Color = color;
			textPaint.TextSize = margin * 3.0f;
			valueTextPaint.TextSize = margin * 2;


			using (SKPath path = new SKPath())
			{
				path.AddArc(outerRect, 135, 270);
				canvas.DrawPath(path, contourPaint);
			}

			using (SKPath path = new SKPath())
			{
				path.AddArc(innerRect, startAngle, sweepAngle);
				canvas.DrawPath(path, knobPaint);
			}

			if (DisplayName != string.Empty)
				canvas.DrawText(DisplayName, center, textPaint);

			if (ShowValue)
			{
				canvas.DrawRoundRect(valueRect, 5, 5, valuePaint);
				string format = Value >= 1000 ? "n0" : Value >= 100 ? "n1" : "n2";
				canvas.DrawText(Value.ToString(format), center.X, height - (margin * 3.7f), valueTextPaint);
			}

		}
	}
}

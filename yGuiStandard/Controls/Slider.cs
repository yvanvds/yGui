using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace yGui
{
	public class Slider : SkiaSharp.Views.Forms.SKCanvasView
	{
		private SKPaint foregroundPaint;
		private SKPaint backgroundPaint;
		private SKPaint handlePaint;

		public Slider()
		{
			EnableTouchEvents = true;
			backgroundPaint = new SKPaint()
			{
				Style = SKPaintStyle.Fill,
			};

			foregroundPaint = new SKPaint()
			{
				Style = SKPaintStyle.Fill,
			};

			handlePaint = new SKPaint()
			{
				Style = SKPaintStyle.Fill,
			};
		}

		#region ForeGround
		private Color foreGround = Colors.Default;

		public Color ForeGround
			{
			get { return foreGround; }
			set { foreGround = value;  InvalidateSurface(); }
		}
		#endregion ForeGround

		#region BackGround
		private Color backGround = Colors.ElementBackground;		

		public Color Background
		{
			get { return backGround; }
			set { backGround = value; InvalidateSurface(); }
		}
		#endregion BackGround

		#region Handle
		private Color handle = Colors.White;	

		public Color Handle
		{
			get { return handle; }
			set { handle = value;  InvalidateSurface(); }
		}
		#endregion Handle

		#region Minimum
		private float minimum = 0;

		public float Minimum
		{
			get { return minimum; }
			set { minimum = value; InvalidateSurface(); }
		}
		#endregion

		#region Maximum
		private float maximum = 100;

		public float Maximum
		{
			get { return maximum; }
			set { maximum = value; InvalidateSurface(); }
		}
		#endregion Maximum

		#region Value
		private float value = 0;

		public float Value
		{
			get { return value; }
			set {
				this.value = value;
				if (this.value < minimum) this.value = minimum;
				if (this.value > maximum) this.value = maximum;
				InvalidateSurface();
			}
		}
		#endregion Value

		#region Touch
		public event EventHandler ValueChanged;

		private void generateValueChangedEvent()
		{
			ValueChanged?.Invoke(this, EventArgs.Empty);
		}

		bool touchActive = false;
		protected override void OnTouch(SKTouchEventArgs e)
		{
			if(e.ActionType == SKTouchAction.Pressed)
			{
				touchActive = true;
				SetFromTouchPos(e.Location);
				e.Handled = true;
			} else if (e.ActionType == SKTouchAction.Moved && touchActive)
			{
				SetFromTouchPos(e.Location);
				e.Handled = true;
			} else 
			{
				touchActive = false;
				e.Handled = true;
			} 
		}

		private void SetFromTouchPos(SKPoint pos)
		{
			float range = maximum - minimum;
			float result;
			if (actualHeight > actualWidth)
			{
				result = ((float)actualHeight - pos.Y) / (float)actualHeight * range;
			}
			else
			{
				result = pos.X / (float)actualWidth * range;
			}
			Value = result;
			generateValueChangedEvent();
		}
		#endregion Touch

		int actualWidth;
		int actualHeight;
		protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
		{
			actualWidth = e.Info.Width;
			actualHeight = e.Info.Height;
			int size = actualWidth < actualHeight ? actualWidth : actualHeight;
			float multiplier = actualWidth / (float)Width;
			float margin = size * 0.01f * multiplier;

			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();

			backgroundPaint.Color = Tools.ToSkia(backGround);
			canvas.DrawRect(new SKRect(0, 0, actualWidth, actualHeight), backgroundPaint);

			float range = maximum - minimum;
			foregroundPaint.Color = Tools.ToSkia(foreGround);
			handlePaint.Color = Tools.ToSkia(Handle);

			if (actualHeight > actualWidth)
			{
				float target = Value / range * actualHeight;
				canvas.DrawRect(new SKRect(0, actualHeight, actualWidth, actualHeight - target), foregroundPaint);

				float handle = target;
				if (handle < 8*multiplier)
				{
					handle = 8*multiplier;
				}
				canvas.DrawRect(new SKRect(0, actualHeight - handle + 8*multiplier, actualWidth, actualHeight - handle), handlePaint);
			}
			else
			{
				float target = Value / range * actualWidth;
				canvas.DrawRect(new SKRect(0, 0, target, actualHeight), foregroundPaint);

				float handle = target;
				if (handle < 8 * multiplier)
				{
					handle = 8 * multiplier;
				}
				canvas.DrawRect(new SKRect(handle - 8*multiplier, 0, handle, actualHeight), handlePaint);
			}
		}
	}
}

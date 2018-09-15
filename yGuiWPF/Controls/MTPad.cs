using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace yGuiWPF.Controls
{
	

	public class MTPad : SkiaSharp.Views.WPF.SKElement
	{
		public class TouchArgs : EventArgs
		{
			public TouchArgs(int id, TouchAction action, Point pos, double size)
			{
				Id = id;
				Action = action;
				Pos = pos;
				Size = size;
			}

			public int Id { get; set; }
			public TouchAction Action { get; set; }
			public Point Pos { get; set; }
			public double Size { get; set; }
		}

		static MTPad()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Slider), new FrameworkPropertyMetadata(typeof(Slider)));

		}

		private SKPaint foregroundPaint;
		private SKPaint backgroundPaint;

		public MTPad()
		{
			backgroundPaint = new SKPaint()
			{
				Style = SKPaintStyle.Fill,
			};

			foregroundPaint = new SKPaint()
			{
				Style = SKPaintStyle.StrokeAndFill,
				StrokeWidth = 1,
				IsAntialias = true
			};
		}

		#region ForeGround
		public static readonly DependencyProperty ForeGroundProperty =
		 DependencyProperty.Register(
			 nameof(ForeGround),
			 typeof(SolidColorBrush),
			 typeof(MTPad),
			 new FrameworkPropertyMetadata(Brushes.Default, FrameworkPropertyMetadataOptions.AffectsRender, ForeGroundChanged)
			 );

		public static void SetForeGround(UIElement element, SolidColorBrush value)
		{
			element.SetValue(ForeGroundProperty, value);
		}

		public static SolidColorBrush GetForeGround(UIElement element)
		{
			return (SolidColorBrush)element.GetValue(ForeGroundProperty);
		}

		public SolidColorBrush ForeGround
		{
			get => (SolidColorBrush)GetValue(ForeGroundProperty);
			set
			{
				SetValue(ForeGroundProperty, value);
				InvalidateVisual();
			}
		}

		public static void ForeGroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((MTPad)d).InvalidateVisual();
		}
		#endregion ForeGround

		#region BackGround
		public static readonly DependencyProperty BackGroundProperty =
		 DependencyProperty.Register(
			 nameof(BackGround),
			 typeof(SolidColorBrush),
			 typeof(MTPad),
			 new FrameworkPropertyMetadata(Brushes.ElementBackground, FrameworkPropertyMetadataOptions.AffectsRender, BackGroundChanged)
			 );

		public static void SetBackGround(UIElement element, Color value)
		{
			element.SetValue(BackGroundProperty, value);
		}

		public static SolidColorBrush GetBackGround(UIElement element)
		{
			return (SolidColorBrush)element.GetValue(BackGroundProperty);
		}

		public SolidColorBrush BackGround
		{
			get => (SolidColorBrush)GetValue(BackGroundProperty);
			set
			{
				SetValue(BackGroundProperty, value);
				InvalidateVisual();
			}
		}

		public static void BackGroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((MTPad)d).InvalidateVisual();
		}
		#endregion BackGround

		#region Touches
		private Dictionary<int, TouchPoint> touchPoints = new Dictionary<int, TouchPoint>();
		int[] registeredPoints = new int[10] {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

		public event EventHandler<TouchArgs> OnTouch;

		private void GenerateTouchEvent(TouchPoint p)
		{
			for(int i = 0; i < 10; i++)
			{
				if(registeredPoints[i] == p.TouchDevice.Id)
				{
					double x = p.Position.X / this.ActualWidth;
					double y = p.Position.Y / this.ActualHeight;
					double size = p.Size.Width > p.Size.Height ? p.Size.Width : p.Size.Height;
					OnTouch?.Invoke(this, new TouchArgs(i, p.Action, new Point(x, y), size));
					break;
				}
			}
		}

		protected override void OnTouchDown(TouchEventArgs e)
		{
			TouchPoint p = e.GetTouchPoint(this);
			if(touchPoints.ContainsKey(p.TouchDevice.Id))
			{
				touchPoints.Remove(p.TouchDevice.Id);
			}
			for(int i = 0; i < 10; i++)
			{
				if (registeredPoints[i] == p.TouchDevice.Id)
				{
					registeredPoints[i] = -1;
				}
			}

			for(int i = 0; i < 10; i++)
			{
				if(registeredPoints[i] == -1)
				{
					registeredPoints[i] = p.TouchDevice.Id;
					touchPoints.Add(p.TouchDevice.Id, p);
					GenerateTouchEvent(p);
					break;
				}
			}
			
			e.Handled = true;
			InvalidateVisual();
		}

		protected override void OnTouchMove(TouchEventArgs e)
		{
			TouchPoint p = e.GetTouchPoint(this);
			if (touchPoints.ContainsKey(p.TouchDevice.Id))
			{
				touchPoints[p.TouchDevice.Id] = p;
				GenerateTouchEvent(p);
			}
			
			e.Handled = true;
			InvalidateVisual();
		}

		protected override void OnTouchLeave(TouchEventArgs e)
		{
			TouchPoint p = e.GetTouchPoint(this);
			if (touchPoints.ContainsKey(p.TouchDevice.Id))
			{
				touchPoints.Remove(p.TouchDevice.Id);
				GenerateTouchEvent(p);
			}
			for (int i = 0; i < 10; i++)
			{
				if (registeredPoints[i] == p.TouchDevice.Id)
				{
					registeredPoints[i] = -1;
				}
			}
			e.Handled = true;
			InvalidateVisual();
		}

		protected override void OnTouchUp(TouchEventArgs e)
		{
			TouchPoint p = e.GetTouchPoint(this);
			if (touchPoints.ContainsKey(p.TouchDevice.Id))
			{
				touchPoints.Remove(p.TouchDevice.Id);
				GenerateTouchEvent(p);
			}
			for (int i = 0; i < 10; i++)
			{
				if (registeredPoints[i] == p.TouchDevice.Id)
				{
					registeredPoints[i] = -1;
				}
			}
			e.Handled = true;
			InvalidateVisual();
		}
		#endregion Touches

		protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
		{
			int width = e.Info.Width;
			int height = e.Info.Height;

			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();

			backgroundPaint.Color = Tools.ToSkia(BackGround);
			canvas.DrawRect(new SKRect(0, 0, width, height), backgroundPaint);

			foregroundPaint.Color = Tools.ToSkia(ForeGround);
			foreach (var point in touchPoints.Values)
			{
				float radius = point.Size.Height > point.Size.Width ? (float)point.Size.Height : (float)point.Size.Width;
				canvas.DrawCircle(new SKPoint((float)point.Position.X, (float)point.Position.Y), radius, foregroundPaint);			
			}
		}
	}
}

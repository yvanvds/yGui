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
	public class XYPad : SkiaSharp.Views.WPF.SKElement
	{
		private static SKPaint valuePaint;

		static XYPad()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(XYPad), new FrameworkPropertyMetadata(typeof(XYPad)));
			valuePaint = new SKPaint()
			{
				TextSize = 10,
				IsAntialias = true,
				Color = new SKColor(255,255,255),
				IsStroke = false
			};
		}

		private SKPaint backgroundPaint;
		private SKPaint centerPaint;
		private SKPaint borderPaint;
		private SKPaint indicatorPaint;

		public XYPad()
		{
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
			
		#region ForeGround
		public static readonly DependencyProperty ForeGroundProperty =
		 DependencyProperty.Register(
			 nameof(ForeGround),
			 typeof(SolidColorBrush),
			 typeof(XYPad),
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
			((XYPad)d).InvalidateVisual();
		}
		#endregion ForeGround

		#region BackGround
		public static readonly DependencyProperty BackGroundProperty =
		 DependencyProperty.Register(
			 nameof(BackGround),
			 typeof(SolidColorBrush),
			 typeof(XYPad),
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
			((XYPad)d).InvalidateVisual();
		}
		#endregion BackGround

		#region Border
		public static readonly DependencyProperty BorderProperty =
		 DependencyProperty.Register(
			 nameof(Border),
			 typeof(SolidColorBrush),
			 typeof(XYPad),
			 new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender, BorderChanged)
			 );

		public static void SetBorder(UIElement element, SolidColorBrush value)
		{
			element.SetValue(BorderProperty, value);
		}

		public static SolidColorBrush GetBorder(UIElement element)
		{
			return (SolidColorBrush)element.GetValue(BorderProperty);
		}

		public SolidColorBrush Border
		{
			get => (SolidColorBrush)GetValue(BorderProperty);
			set
			{
				SetValue(BorderProperty, value);
				InvalidateVisual();
			}
		}

		public static void BorderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((XYPad)d).InvalidateVisual();
		}
		#endregion TextColor

		#region IsRounded
		public static readonly DependencyProperty IsRoundedProperty =
		 DependencyProperty.Register(
			 nameof(IsRounded),
			 typeof(bool),
			 typeof(XYPad),
			 new FrameworkPropertyMetadata(false)
			 );

		public static void SetIsRounded(UIElement element, bool value)
		{
			element.SetValue(IsRoundedProperty, value);
		}

		public static bool GetIsRounded(UIElement element)
		{
			return (bool)element.GetValue(IsRoundedProperty);
		}

		public bool IsRounded
		{
			get => (bool)(GetValue(IsRoundedProperty));
			set
			{
				SetValue(IsRoundedProperty, value);
			}
		}
		#endregion IsRounded

		#region Centered
		public static readonly DependencyProperty CenteredProperty =
		 DependencyProperty.Register(
			 nameof(Centered),
			 typeof(bool),
			 typeof(XYPad),
			 new FrameworkPropertyMetadata(true)
			 );

		public static void SetCentered(UIElement element, bool value)
		{
			element.SetValue(CenteredProperty, value);
		}

		public static bool GetCentered(UIElement element)
		{
			return (bool)element.GetValue(CenteredProperty);
		}

		public bool Centered
		{
			get => (bool)(GetValue(CenteredProperty));
			set
			{
				SetValue(CenteredProperty, value);
			}
		}

		#endregion Centered

		#region Value
		private Point value = new Point();
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
				} else
				{
					if (this.value.X < 0) this.value.X = 0;
					if (this.value.Y < 0) this.value.Y = 0;
				}
				
				InvalidateVisual();
			}
		}

		Point screenToValue(Point screenPos)
		{
			Point result = new Point();
			Point size = new Point(this.ActualWidth, this.ActualHeight);

			if (Centered)
			{
				result.X = (screenPos.X - (size.X * 0.5)) / (size.X * 0.5f - margin*2);
				result.Y = -(screenPos.Y - (size.Y * 0.5)) / (size.Y * 0.5f - margin*2);
			}
			else
			{
				result.X = (screenPos.X - (margin * 2)) / (size.X - margin * 4);
				result.Y = 1 - (screenPos.Y - (margin * 2)) / (size.Y - margin * 4);
			}
			return result;
		}

		SKPoint valueToScreen(Point valuePos)
		{
			SKPoint result = new SKPoint();
			Point size = new Point();
			size.X = this.ActualWidth;
			size.Y = this.ActualHeight;
			if (Centered)
			{
				result.X = (float)valuePos.X * ((float)size.X * 0.5f - margin*2) + ((float)size.X * 0.5f);
				result.Y = (float)-valuePos.Y * ((float)size.Y * 0.5f - margin*2) + ((float)size.Y * 0.5f);
			}
			else
			{
				result.X = (margin * 2) + ((float)valuePos.X * ((float)size.X - margin * 4));
				result.Y = (margin * 2) + ((1-(float)valuePos.Y) * ((float)size.Y - margin * 4));
			}
			return result;
		}
		#endregion Value

		#region ShowValue
		public static readonly DependencyProperty ShowValueProperty =
		 DependencyProperty.Register(
			 nameof(ShowValue),
			 typeof(bool),
			 typeof(XYPad),
			 new FrameworkPropertyMetadata(false)
			 );

		public static void SetShowValue(UIElement element, bool value)
		{
			element.SetValue(ShowValueProperty, value);
		}

		public static bool GetShowValue(UIElement element)
		{
			return (bool)element.GetValue(ShowValueProperty);
		}

		public bool ShowValue
		{
			get => (bool)(GetValue(ShowValueProperty));
			set
			{
				SetValue(ShowValueProperty, value);
			}
		}
		#endregion ShowValue

		#region Mouse
		bool mouseDown = false;
		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			Value = screenToValue(e.GetPosition(this));

			Mouse.Capture(this);
			e.Handled = true;
			mouseDown = true;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if(mouseDown)
			{
				Value = screenToValue(e.GetPosition(this));

				e.Handled = true;
			}
		}

		protected override void OnMouseLeave(MouseEventArgs e)
		{
			if(mouseDown)
			{
				Mouse.Capture(null);
				mouseDown = false;
			}
		}

		protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
		{
			if(mouseDown)
			{
				Mouse.Capture(null);
				mouseDown = false;
			}
		}

		#endregion Mouse
		float margin = 0;
		protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
		{
			int width = e.Info.Width;
			int height = e.Info.Height;
			int size = width < height ? width : height;
			margin = size * 0.05f;

			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();

			SKPoint center = new SKPoint();
			center.X = width / 2.0f;
			center.Y = height / 2.0f;

			SKRect outerRect = new SKRect(margin, margin, width - margin, height - margin);

			backgroundPaint.Color = Tools.ToSkia(BackGround);
			borderPaint.Color = Tools.ToSkia(Border);
			indicatorPaint.Color = Tools.ToSkia(ForeGround);

			float corner = IsRounded ? 15 : 0;
			canvas.DrawRoundRect(outerRect, corner, corner, backgroundPaint);

			if(Centered)
			{
				centerPaint.Color = new SKColor((byte)(BackGround.Color.R * 0.9), (byte)(BackGround.Color.G * 0.9), (byte)(BackGround.Color.B * 0.9));
				canvas.DrawLine(new SKPoint(margin, center.Y), new SKPoint(width - margin, center.Y), centerPaint);
				canvas.DrawLine(new SKPoint(center.X, margin), new SKPoint(height - margin, center.X), centerPaint);
			}

			SKPoint position = valueToScreen(Value);
			

			canvas.DrawLine(new SKPoint(0f + margin, position.Y), new SKPoint(width - margin, position.Y), indicatorPaint);
			canvas.DrawLine(new SKPoint(position.X, 0f + margin), new SKPoint(position.X, height - margin), indicatorPaint); 
			canvas.DrawCircle(new SKPoint(position.X, position.Y), size * 0.05f, indicatorPaint);

			canvas.DrawRoundRect(outerRect, corner, corner, borderPaint);

			if(ShowValue)
			{
				canvas.DrawText("X: " + value.X.ToString("n2"), new SKPoint(width - 50, 30), valuePaint);
				canvas.DrawText("Y: " + value.Y.ToString("n2"), new SKPoint(width -50, 40), valuePaint);
			}
		}
	}
}


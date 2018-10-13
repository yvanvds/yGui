using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace yGuiWPF.Controls
{
	public class Slider : SkiaSharp.Views.WPF.SKElement
	{
		static Slider()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Slider), new FrameworkPropertyMetadata(typeof(Slider)));

		}

		private SKPaint foregroundPaint;
		private SKPaint backgroundPaint;
		private SKPaint handlePaint;

		public Slider()
		{
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

			Visible = true;
		}

		#region ForeGround
		public static readonly DependencyProperty ForeGroundProperty =
		 DependencyProperty.Register(
			 nameof(ForeGround),
			 typeof(SolidColorBrush),
			 typeof(Slider),
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
			((Slider)d).InvalidateVisual();
		}
		#endregion ForeGround

		#region BackGround
		public static readonly DependencyProperty BackGroundProperty =
		 DependencyProperty.Register(
			 nameof(BackGround),
			 typeof(SolidColorBrush),
			 typeof(Slider),
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
			((Slider)d).InvalidateVisual();
		}
		#endregion BackGround

		#region Handle
		public static readonly DependencyProperty HandleProperty =
		 DependencyProperty.Register(
			 nameof(Handle),
			 typeof(SolidColorBrush),
			 typeof(Slider),
			 new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender, HandleChanged)
			 );

		public static void SetHandle(UIElement element, SolidColorBrush value)
		{
			element.SetValue(HandleProperty, value);
		}

		public static SolidColorBrush GetHandle(UIElement element)
		{
			return (SolidColorBrush)element.GetValue(HandleProperty);
		}

		public SolidColorBrush Handle
		{
			get => (SolidColorBrush)GetValue(HandleProperty);
			set
			{
				SetValue(HandleProperty, value);
				InvalidateVisual();
			}
		}

		public static void HandleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((Slider)d).InvalidateVisual();
		}
		#endregion TextColor

		#region Minimum
		private float minimum = 0;
		public static readonly DependencyProperty MinimumProperty =
		 DependencyProperty.Register(
			 nameof(Minimum),
			 typeof(float),
			 typeof(Slider),
			 new FrameworkPropertyMetadata(0f, FrameworkPropertyMetadataOptions.AffectsRender, MinimumChanged)
			 );

		public static void SetMinimum(UIElement element, float value)
		{
			element.SetValue(MinimumProperty, value);
		}

		public static float GetMinimum(UIElement element)
		{
			return (float)element.GetValue(MinimumProperty);
		}

		public float Minimum
		{
			get => (float)(GetValue(MinimumProperty));
			set
			{
				this.minimum = value;
				SetValue(MinimumProperty, this.value);
				if (Value < minimum) Value = Minimum;
				InvalidateVisual();
			}
		}

		public static void MinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Slider slider = d as Slider;
		  slider.minimum = (float)d.GetValue(MinimumProperty);
			if (slider.Value < slider.minimum) slider.Value = slider.Minimum;
			((Slider)d).InvalidateVisual();
		}
		#endregion Minimum

		#region Maximum
		private float maximum = 100;
		public static readonly DependencyProperty MaximumProperty =
		 DependencyProperty.Register(
			 nameof(Maximum),
			 typeof(float),
			 typeof(Slider),
			 new FrameworkPropertyMetadata(100f, FrameworkPropertyMetadataOptions.AffectsRender, MaximumChanged)
			 );

		public static void SetMaximum(UIElement element, float value)
		{
			element.SetValue(MaximumProperty, value);
		}

		public static float GetMaximum(UIElement element)
		{
			return (float)element.GetValue(MaximumProperty);
		}

		public float Maximum
		{
			get => (float)(GetValue(MaximumProperty));
			set
			{
				this.maximum = value;
				SetValue(MaximumProperty, value);
				if (Value > this.maximum) Value = this.maximum;
				InvalidateVisual();
			}
		}

		public static void MaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Slider slider = d as Slider;
			slider.maximum = (float)d.GetValue(MaximumProperty);
			if (slider.Value > slider.maximum) slider.Value = slider.maximum;
			((Slider)d).InvalidateVisual();
		}
		#endregion Maximum

		#region Visible
		public static readonly DependencyProperty VisibleProperty =
		 DependencyProperty.Register(
			 nameof(Visible),
			 typeof(bool),
			 typeof(Slider),
			 new FrameworkPropertyMetadata(false)
			 );

		public static void SetVisible(UIElement element, bool value)
		{
			element.SetValue(VisibleProperty, value);
		}

		public static bool GetVisible(UIElement element)
		{
			return (bool)element.GetValue(VisibleProperty);
		}

		public bool Visible
		{
			get => (bool)(GetValue(VisibleProperty));
			set
			{
				SetValue(VisibleProperty, value);
				InvalidateVisual();
			}
		}

		#endregion Visible

		#region Value
		private float value = 0;
		public static readonly DependencyProperty ValueProperty =
		 DependencyProperty.Register(
			 nameof(Value),
			 typeof(float),
			 typeof(Slider),
			 new FrameworkPropertyMetadata(0f, FrameworkPropertyMetadataOptions.AffectsRender, ValueChanged)
			 );

		public static void SetValue(UIElement element, float value)
		{
			element.SetValue(ValueProperty, value);
		}

		public static float GetValue(UIElement element)
		{
			return (float)((Slider)element).value;
		}

		public float Value
		{
			get => value;
			set
			{
				this.value = value;
				if (this.value < minimum) this.value = minimum;
				if (this.value > maximum) this.value = maximum;
				SetValue(ValueProperty, this.value);
				InvalidateVisual();
				GenerateValueChangeEvent();
			}
		}

		public static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Slider slider = d as Slider;
			slider.value = (float)d.GetValue(ValueProperty);
			if (slider.value < slider.minimum)
			{
				slider.value = slider.minimum;
			}
			else if (slider.value > slider.maximum)
			{
				slider.value = slider.maximum;
			}
			((Slider)d).InvalidateVisual();
			slider.GenerateValueChangeEvent();
		}
		#endregion Value

		#region Mouse
		public static RoutedEvent ValueChangedEvent = 
			EventManager.RegisterRoutedEvent(
				"Changed", 
				RoutingStrategy.Bubble, 
				typeof(RoutedEventHandler),
				typeof(Slider));

		public event RoutedEventHandler Changed
		{
			add { AddHandler(ValueChangedEvent, value); }
			remove { RemoveHandler(ValueChangedEvent, value); }
		}

		private void GenerateValueChangeEvent()
		{
			RoutedEventArgs args = new RoutedEventArgs(ValueChangedEvent, this);
			RaiseEvent(args);
		}

		bool mouseDown = false;
		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			mouseDown = true;
			SetValueFromMousePos(e.GetPosition(this));
			Mouse.Capture(this);
			e.Handled = true;
		}

		protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
		{
			mouseDown = false;
			Mouse.Capture(null);
			e.Handled = true;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (!mouseDown) return;
			SetValueFromMousePos(e.GetPosition(this));
			e.Handled = true;
		}

		private void SetValueFromMousePos(Point pos)
		{
			float range = maximum - minimum;
			float result;
			if (this.ActualHeight > this.ActualWidth)
			{
				result = ((float)ActualHeight - (float)pos.Y) / (float)ActualHeight * range;
			} else
			{
				result = (float)pos.X / (float)ActualWidth * range;
			}
			Value = minimum + result;
		}

		#endregion Mouse

		protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
		{
			int width = e.Info.Width;
			int height = e.Info.Height;

			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();
			if (!Visible) return;

			int size = width < height ? width : height;
			float margin = size * 0.05f;

			backgroundPaint.Color = Tools.ToSkia(BackGround);
			canvas.DrawRect(new SKRect(0, 0, width, height), backgroundPaint);

			float range = maximum - minimum;
			foregroundPaint.Color = Tools.ToSkia(ForeGround);
			handlePaint.Color = Tools.ToSkia(Handle);

			if (height > width)
			{
				float target = (Value - minimum) / range * height;
				canvas.DrawRect(new SKRect(0, height, width, height - target), foregroundPaint);

				float handle = target;
				if(handle < 4)
				{
					handle = 4;
				}
				canvas.DrawRect(new SKRect(0, height - handle + 4, width, height - handle), handlePaint);
			} else
			{
				float target = (Value - minimum) / range * width;
				canvas.DrawRect(new SKRect(0, 0, target, height), foregroundPaint);

				float handle = target;
				if (handle < 4)
				{
					handle = 4;
				}
				canvas.DrawRect(new SKRect(handle - 4, height, handle, 0), handlePaint);
			}
		}
	}
}

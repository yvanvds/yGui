using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace yGuiWPF.Controls
{
	public class Knob : SkiaSharp.Views.WPF.SKElement
	{
		private static SKPaint valuePaint;
		

		static Knob()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Knob), new FrameworkPropertyMetadata(typeof(Knob)));

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
			SnapsToDevicePixels = true;

			valueTextPaint = new SKPaint()
			{
				TextSize = 0, // will be changed at draw
				FakeBoldText = true,
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
				FakeBoldText = true,
				IsAntialias = true,
				IsStroke = false,
				TextAlign = SKTextAlign.Center
			};

			Visible = true;
		}

		#region Color
		public static readonly DependencyProperty ColorProperty =
		 DependencyProperty.Register(
			 nameof(Color),
			 typeof(SolidColorBrush),
			 typeof(Knob),
			 new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Default), FrameworkPropertyMetadataOptions.AffectsRender, ColorChanged)
			 );

		public static void SetColor(UIElement element, Color value)
		{
			element.SetValue(ColorProperty, value);
		}

		public static SolidColorBrush GetColor(UIElement element)
		{
			return (SolidColorBrush)element.GetValue(ColorProperty);
		}

		public SolidColorBrush Color
		{
			get => (SolidColorBrush)GetValue(ColorProperty);
			set
			{
				SetValue(ColorProperty, value);
				InvalidateVisual();
			}
		}

		public static void ColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((Knob)d).InvalidateVisual();
		}
		#endregion Color

		#region ShowValue
		public static readonly DependencyProperty ShowValueProperty =
		 DependencyProperty.Register(
			 nameof(ShowValue),
			 typeof(bool),
			 typeof(Knob),
			 new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender, ShowValueChanged)
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
				InvalidateVisual();
			}
		}

		public static void ShowValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Knob knob = d as Knob;
			((Knob)d).InvalidateVisual();
		}
		#endregion ShowValue

		#region Minimum
		private float minimum = 0;
		public static readonly DependencyProperty MinimumProperty =
		 DependencyProperty.Register(
			 nameof(Minimum),
			 typeof(float),
			 typeof(Knob),
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
			Knob knob = d as Knob;
			knob.minimum = (float)d.GetValue(MinimumProperty);
			if (knob.Value < knob.minimum) knob.Value = knob.Minimum;
			((Knob)d).InvalidateVisual();
		}
		#endregion Minimum

		#region Maximum
		private float maximum = 100;
		public static readonly DependencyProperty MaximumProperty =
		 DependencyProperty.Register(
			 nameof(Maximum),
			 typeof(float),
			 typeof(Knob),
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
			Knob knob = d as Knob;
			knob.maximum = (float)d.GetValue(MaximumProperty);
			if (knob.Value > knob.maximum) knob.Value = knob.maximum;
			((Knob)d).InvalidateVisual();
		}
		#endregion Maximum

		#region Visible
		public static readonly DependencyProperty VisibleProperty =
		 DependencyProperty.Register(
			 nameof(Visible),
			 typeof(bool),
			 typeof(Knob),
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
			 typeof(Knob),
			 new FrameworkPropertyMetadata(0f, FrameworkPropertyMetadataOptions.AffectsRender, ValueChanged)
			 );

		public static void SetValue(UIElement element, float value)
		{
			element.SetValue(ValueProperty, value);
		}

		public static float GetValue(UIElement element)
		{
			return (float)((Knob)element).value;
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
			Knob knob = d as Knob;
			knob.value = (float)d.GetValue(ValueProperty);
			if(knob.value < knob.minimum)
			{
				knob.value = knob.minimum;
			} else if(knob.value > knob.maximum)
			{
				knob.value = knob.maximum;
			}
			knob.InvalidateVisual();
			knob.GenerateValueChangeEvent();
		}
		#endregion Value

		#region DisplayName
		public static readonly DependencyProperty DisplayNameProperty =
			DependencyProperty.Register(
				nameof(DisplayName),
				typeof(string),
				typeof(Knob),
				new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender, DisplayNameChanged)
				);

		public static void SetDisplayName(UIElement element, string value)
		{
			element.SetValue(DisplayNameProperty, value);
		}

		public static string GetDisplayName(UIElement element)
		{
			return (string)element.GetValue(DisplayNameProperty);
		}

		public string DisplayName
		{
			get => (string)(GetValue(DisplayNameProperty));
			set
			{
				SetValue(DisplayNameProperty, value);
				InvalidateVisual();
			}
		}

		public static void DisplayNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((Knob)d).InvalidateVisual();
		}
		#endregion DisplayName

		#region Mouse
		public class ValueArgs : EventArgs
		{
			public ValueArgs(float value)
			{
				Value = value;
			}
			public float Value { get; set; }
		}
		public event EventHandler<ValueArgs> OnValueChange;

		private void GenerateValueChangeEvent()
		{
			OnValueChange?.Invoke(this, new ValueArgs(Value));
		}

		bool mouseDown = false;
		Point mousePos = new Point();
		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			mouseDown = true;
			mousePos = e.GetPosition(this);
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
			Point newPoint = e.GetPosition(this);
			Point delta = new Point();
			delta.X = (newPoint.X - mousePos.X) * 0.5f;
			delta.Y = -(newPoint.Y - mousePos.Y) * 0.5f;
			mousePos = newPoint;
			float inc = Math.Abs(delta.X) > Math.Abs(delta.Y) ? (float)delta.X : (float)delta.Y;
			float range = maximum - minimum;
			if (range != 0)
			{
				Value += inc * range / 100;
			}
			e.Handled = true;
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

			if(ShowValue)
			{
				canvas.DrawRoundRect(valueRect, 5, 5, valuePaint);
				string format = Value >= 1000 ? "n0" : Value >= 100 ? "n1" : "n2";
				canvas.DrawText(Value.ToString(format), center.X, height - (margin * 3.7f), valueTextPaint);
			}
			
		}
	}
}

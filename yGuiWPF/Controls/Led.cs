using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace yGuiWPF.Controls
{
	public class Led : SkiaSharp.Views.WPF.SKElement
	{
		private static SKPaint border;

		static Led()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Led), new FrameworkPropertyMetadata(typeof(Led)));
			border = new SKPaint()
			{
				Style = SKPaintStyle.Stroke,
				Color = Tools.ToSkia(Colors.HighlightBackground),
				StrokeWidth = 2,
				IsAntialias = true
			};
		}

		protected SKPaint ledPaint;
		

		public Led()
		{
			ledPaint = new SKPaint()
			{
				Style = SKPaintStyle.Fill,
				IsAntialias = true
			};
		}

		#region Color
		public static readonly DependencyProperty ColorProperty =
		 DependencyProperty.Register(
			 nameof(Color),
			 typeof(SolidColorBrush),
			 typeof(Led),
			 new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender, ColorChanged)
			 );

		public static void SetColor(UIElement element, SolidColorBrush value)
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
			((Led)d).InvalidateVisual();
		}
		#endregion Color

		#region Scale
		public static readonly DependencyProperty ScaleProperty =
			DependencyProperty.Register(
				nameof(Scale),
				typeof(float),
				typeof(Button),
				new FrameworkPropertyMetadata(1f, FrameworkPropertyMetadataOptions.AffectsRender, ScaleChanged)
				);

		public static void SetScale(UIElement element, float value)
		{
			element.SetValue(ScaleProperty, value);
		}

		public static float GetScale(UIElement element)
		{
			return (float)element.GetValue(ScaleProperty);
		}

		public float Scale
		{
			get => (float)(GetValue(ScaleProperty));
			set
			{
				SetValue(ScaleProperty, value);
				InvalidateVisual();
			}
		}

		public static void ScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((Led)d).InvalidateVisual();
		}
		#endregion TextScale

		#region Blink
		private float alpha = 0;
		private float stepSize;

		public void Blink(int milliSeconds)
		{
			stepSize = 255f / (milliSeconds / 10f);
			alpha = 255f;
			InvalidateVisual();
		}
		#endregion Blink

		protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
		{
			int width = e.Info.Width;
			int height = e.Info.Height;
			int size = width < height ? width : height;
			float margin = size * 0.05f;
			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();

			SKPoint center = new SKPoint(width / 2f, height / 2f);
			float radius = (size * 0.5f - margin) * Scale;

			ledPaint.Color = new SKColor(Color.Color.R, Color.Color.G, Color.Color.B, (byte)alpha);
			canvas.DrawCircle(center, radius, ledPaint);
			canvas.DrawCircle(center, radius, border);

			if(alpha > 0)
			{
				alpha -= stepSize;
				if (alpha < 0) alpha = 0;
				Invalidator.Add(this);
			}
		}
	}
}

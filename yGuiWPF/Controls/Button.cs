using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SkiaSharp;
using SkiaSharp.Extended.Iconify;
using SkiaSharp.Views.Desktop;

namespace yGuiWPF.Controls
{
	public class Button : SkiaSharp.Views.WPF.SKElement
	{
		static Button()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Button), new FrameworkPropertyMetadata(typeof(Button)));
		}

		protected SKPaint buttonPaint;
		protected SKPaint borderPaint;
		protected SKPaint togglePaint;
		protected SKPaint textPaint;

		public Button()
		{
			//SnapsToDevicePixels = true;

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
		public static readonly DependencyProperty ForeGroundProperty =
		 DependencyProperty.Register(
			 nameof(ForeGround),
			 typeof(SolidColorBrush),
			 typeof(Button),
			 new FrameworkPropertyMetadata(Brushes.Default, FrameworkPropertyMetadataOptions.AffectsRender, ForeGroundChanged)
			 );

		public static void SetForeGround(UIElement element, Color value)
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
			((Button)d).InvalidateVisual();
		}
		#endregion ForeGround

		#region TextColor
		public static readonly DependencyProperty TextColorProperty =
		 DependencyProperty.Register(
			 nameof(TextColor),
			 typeof(SolidColorBrush),
			 typeof(Button),
			 new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender, TextColorChanged)
			 );

		public static void SetTextColor(UIElement element, SolidColorBrush value)
		{
			element.SetValue(TextColorProperty, value);
		}

		public static SolidColorBrush GetTextColor(UIElement element)
		{
			return (SolidColorBrush)element.GetValue(TextColorProperty);
		}

		public SolidColorBrush TextColor
		{
			get => (SolidColorBrush)GetValue(TextColorProperty);
			set
			{
				SetValue(TextColorProperty, value);
				InvalidateVisual();
			}
		}

		public static void TextColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((Button)d).InvalidateVisual();
		}
		#endregion TextColor

		#region BackGround
		public static readonly DependencyProperty BackGroundProperty =
		 DependencyProperty.Register(
			 nameof(BackGround),
			 typeof(SolidColorBrush),
			 typeof(Button),
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
			((Button)d).InvalidateVisual();
		}
		#endregion BackGround

		#region IsToggle
		public static readonly DependencyProperty IsToggleProperty =
		 DependencyProperty.Register(
			 nameof(IsToggle),
			 typeof(bool),
			 typeof(Button),
			 new FrameworkPropertyMetadata(false)
			 );

		public static void SetIsToggle(UIElement element, bool value)
		{
			element.SetValue(IsToggleProperty, value);
		}

		public static bool GetIsToggle(UIElement element)
		{
			return (bool)element.GetValue(IsToggleProperty);
		}

		public bool IsToggle
		{
			get => (bool)(GetValue(IsToggleProperty));
			set
			{
				SetValue(IsToggleProperty, value);
			}
		}

		#endregion IsToggle

		#region Toggled
		public static readonly DependencyProperty ToggledProperty =
		 DependencyProperty.Register(
			 nameof(Toggled),
			 typeof(bool),
			 typeof(Button),
			 new FrameworkPropertyMetadata(false)
			 );

		public static void SetToggled(UIElement element, bool value)
		{
			element.SetValue(ToggledProperty, value);
		}

		public static bool GetToggled(UIElement element)
		{
			return (bool)element.GetValue(ToggledProperty);
		}

		public bool Toggled
		{
			get => (bool)(GetValue(ToggledProperty));
			set
			{
				SetValue(ToggledProperty, value);
			}
		}

		#endregion Toggled

		#region IsRounded
		public static readonly DependencyProperty IsRoundedProperty =
		 DependencyProperty.Register(
			 nameof(IsRounded),
			 typeof(bool),
			 typeof(Button),
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

		#region Text
		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register(
				nameof(Text),
				typeof(string),
				typeof(Button),
				new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender, TextChanged)
				);

		public static void SetText(UIElement element, string value)
		{
			element.SetValue(TextProperty, value);
		}

		public static string GetText(UIElement element)
		{
			return (string)element.GetValue(TextProperty);
		}

		public string Text
		{
			get => (string)(GetValue(TextProperty));
			set
			{
				SetValue(TextProperty, value);
				InvalidateVisual();
			}
		}

		public static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((Button)d).InvalidateVisual();
		}
		#endregion Text

		#region TextScale
		public static readonly DependencyProperty TextScaleProperty =
			DependencyProperty.Register(
				nameof(TextScale),
				typeof(TextScales),
				typeof(Button),
				new FrameworkPropertyMetadata(TextScales.Normal, FrameworkPropertyMetadataOptions.AffectsRender, TextScaleChanged)
				);

		public static void SetTextScale(UIElement element, TextScales value)
		{
			element.SetValue(TextScaleProperty, value);
		}

		public static TextScales GetTextScale(UIElement element)
		{
			return (TextScales)element.GetValue(TextScaleProperty);
		}

		public TextScales TextScale
		{
			get => (TextScales)(GetValue(TextScaleProperty));
			set
			{
				SetValue(TextScaleProperty, value);
				InvalidateVisual();
			}
		}

		public static void TextScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((Button)d).InvalidateVisual();
		}
		#endregion TextScale

		#region Mouse
		bool mouseDown = false;
		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			mouseDown = true;
			Mouse.Capture(this);
			if (IsToggle)
			{
				Toggled = !Toggled;
				InvalidateVisual();
			}
			e.Handled = true;
		}

		protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
		{
			mouseDown = false;
			Mouse.Capture(null);
			e.Handled = true;
			OnClick();
		}

		public static RoutedEvent ClickEvent = 
			EventManager.RegisterRoutedEvent(
				"Click", 
				RoutingStrategy.Bubble, 
				typeof(RoutedEventHandler), 
				typeof(Button));

		public event RoutedEventHandler Click
		{
			add { AddHandler(ClickEvent, value); }
			remove { RemoveHandler(ClickEvent, value); }
		}

		protected virtual void OnClick()
		{
			RoutedEventArgs args = new RoutedEventArgs(ClickEvent, this);
			RaiseEvent(args);
		}

		#endregion Mouse

		protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
		{
			int width = e.Info.Width;
			int height = e.Info.Height;
			int size = width < height ? width : height;
			float margin = size * 0.05f;

			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();

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

			switch(TextScale)
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

			float corner = IsRounded ? 15 : 0;

			canvas.DrawRoundRect(outerRect, corner, corner, buttonPaint);
			canvas.DrawRoundRect(outerRect, corner, corner, borderPaint);

			if(IsToggle && Toggled)
			{
				corner = IsRounded ? corner * 0.85f : 0;
				canvas.DrawRoundRect(innerRect, corner, corner, togglePaint);
			}

			if(Text != string.Empty)
			{
				center.Y += textPaint.TextSize * 0.35f;
				canvas.DrawIconifiedText(Text, center.X, center.Y, textPaint);
			}
		}
	}
}

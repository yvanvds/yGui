using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace yGui
{
	public class MTPad : SkiaSharp.Views.Forms.SKCanvasView
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


		private SKPaint foregroundPaint;
		private SKPaint backgroundPaint;

		public MTPad()
		{
			EnableTouchEvents = true;
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

		private Color foreGround = Colors.Default;		
		public Color ForeGround
		{
			get { return foreGround; }
			set { foreGround = value; InvalidateSurface(); }
		}

		private Color backGround = Colors.ElementBackground;

		public Color BackGround
		{
			get { return backGround; }
			set { backGround = value; InvalidateSurface(); }
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

		#region Touch
		private Dictionary<long, SKPoint> touchPoints = new Dictionary<long, SKPoint>();
		long[] registeredPoints = new long[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

		public event EventHandler<TouchArgs> TouchChanged;

		private void GenerateTouchEVent(int id, TouchAction action)
		{
			for (int i = 0; i < 10; i++)
			{
				if (registeredPoints[i] == id)
				{
					double x = touchPoints[id].X / actualSize.Width;
					double y = touchPoints[id].Y / actualSize.Height;
					double size = 1; // not supported yet
					TouchChanged?.Invoke(this, new TouchArgs(i, action, new Point(x, y), size));
					break;
				}
			}
		}

		private void clearID(long id)
		{
			if (touchPoints.ContainsKey(id))
			{
				touchPoints.Remove(id);
			}
			for (int i = 0; i < 10; i++)
			{
				if (registeredPoints[i] == id)
				{
					registeredPoints[i] = -1;
				}
			}
		}

		protected override void OnTouch(SKTouchEventArgs e)
		{
			if (!visible) return;
			switch (e.ActionType)
			{
				case SKTouchAction.Pressed:
					clearID(e.Id);
					for(int i = 0; i < 10; i++)
					{
						if(registeredPoints[i] == -1)
						{
							registeredPoints[i] = e.Id;
							touchPoints.Add(e.Id, e.Location);
							GenerateTouchEVent((int)e.Id, TouchAction.Pressed);
							InvalidateSurface();
							break;
						}
					}
					break;

				case SKTouchAction.Moved:
					if(touchPoints.ContainsKey(e.Id))
					{
						touchPoints[e.Id] = e.Location;
						GenerateTouchEVent((int)e.Id, TouchAction.Moved);
						InvalidateSurface();
					}
					break;

				case SKTouchAction.Released:
					if(touchPoints.ContainsKey(e.Id))
					{
						GenerateTouchEVent((int)e.Id, TouchAction.Released);
						clearID(e.Id);
						InvalidateSurface();
					}
					break;
			}
			e.Handled = true;
		}
		#endregion

		SKSize actualSize = new SKSize();
		protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
		{
			int width = e.Info.Width;
			int height = e.Info.Height;
			actualSize.Width = width / (float)Width;
			actualSize.Height = height / (float)Height;
			float multiplier = width / (float)Width;

			SKSurface surface = e.Surface;
			SKCanvas canvas = surface.Canvas;
			canvas.Clear();
			if (!visible) return;

			backgroundPaint.Color = Tools.ToSkia(BackGround);
			canvas.DrawRect(new SKRect(0, 0, width, height), backgroundPaint);

			foregroundPaint.Color = Tools.ToSkia(ForeGround);
			foreach (var point in touchPoints.Values)
			{
				float radius = 35 * multiplier;
				canvas.DrawCircle(new SKPoint((float)point.X, (float)point.Y), radius, foregroundPaint);
			}
		}
	}
}

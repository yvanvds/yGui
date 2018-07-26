using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace yGui
{
	internal static class Invalidator
	{
		private static Timer timer = null;
		private static ConcurrentBag<SkiaSharp.Views.Forms.SKCanvasView> bag;

		static Invalidator()
		{
			bag = new ConcurrentBag<SkiaSharp.Views.Forms.SKCanvasView>();
		}

		private static void timerElapsed(Object stateInfo)
		{
			if (bag.IsEmpty) return;
			Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
			{
				SkiaSharp.Views.Forms.SKCanvasView element;
				while (!bag.IsEmpty)
				{
					if (bag.TryTake(out element))
					{
						element.InvalidateSurface();
					}
				}
			});
			
		}

		public static void Add(SkiaSharp.Views.Forms.SKCanvasView element)
		{
			bag.Add(element);
			if(timer == null)
			{
				var autoEvent = new AutoResetEvent(false);
				timer = new Timer(timerElapsed, autoEvent, new TimeSpan(0, 0, 0, 0, 20), new TimeSpan(0, 0, 0, 0, 20));
			}
		}
	}
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace yGuiWPF
{
	internal static class Invalidator
	{
		private static DispatcherTimer timer;
		private static ConcurrentBag<SkiaSharp.Views.WPF.SKElement> bag;

		static Invalidator()
		{
			bag = new ConcurrentBag<SkiaSharp.Views.WPF.SKElement>();

			timer = new DispatcherTimer(DispatcherPriority.Render);
			timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
			timer.Tick += new EventHandler(timerElapsed);
		}

		private static void timerElapsed(object sender, EventArgs e)
		{
			SkiaSharp.Views.WPF.SKElement element;
			while(!bag.IsEmpty)
			{
				if(bag.TryTake(out element))
				{
					element.InvalidateVisual();
				}
			}
		}

		public static void Add(SkiaSharp.Views.WPF.SKElement element)
		{
			bag.Add(element);
			if(!timer.IsEnabled)
			{
				timer.Start();
			}
		}
	}
}

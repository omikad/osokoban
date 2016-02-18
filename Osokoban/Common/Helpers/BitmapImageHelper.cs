using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Common.Helpers
{
	public static class BitmapImageHelper
	{
		public static ImageSource FromAssemblySource(this BitmapImage image, string assembly, string folder)
		{
			image.BeginInit();
			image.UriSource = new Uri($"pack://application:,,,/{assembly};component/{folder}");
            image.EndInit();
			return image;
		}
	}
}
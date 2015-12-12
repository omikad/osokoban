using System;
using System.ComponentModel.Composition;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Osokoban.Core
{
	[Export]
	public class AssetsManager
	{
		public AssetsManager()
		{
			Diamond = Load("Diamond.png");
			Player = Load("Archer.png");
			TargetPlace = Load("TargetPlace.png");
			Wall = Load("Wall.png");
		}

		public ImageSource Diamond { get; }
		public ImageSource Player { get; }
		public ImageSource TargetPlace { get; }
		public ImageSource Wall { get; }

		private static ImageSource Load(string path)
		{
			var image = new BitmapImage();
			image.BeginInit();
			image.UriSource = new Uri("pack://application:,,,/Osokoban;component/Assets/" + path);
			image.EndInit();
			return image;
		}
	}
}
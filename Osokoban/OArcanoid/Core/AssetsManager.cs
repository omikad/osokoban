using System.ComponentModel.Composition;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Common.Helpers;

namespace OArcanoid.Core
{
	[Export]
	public class AssetsManager
	{
		public AssetsManager()
		{
			Apple = Load("Apple.png");
			Ball = Load("Ball.png");
			Diamond = Load("Diamond.png");
			Marker = Load("Marker.png");
			PadLeft = Load("PadLeft.png");
			PadCenter = Load("PadCenter.png");
			PadRight = Load("PadRight.png");
			Wall = Load("Wall.png");
		}

		public ImageSource Apple { get; }
		public ImageSource Ball { get; }
		public ImageSource Diamond { get; }
		public ImageSource Marker { get; }
		public ImageSource PadLeft { get; }
		public ImageSource PadCenter { get; }
		public ImageSource PadRight { get; }
		public ImageSource Wall { get; }

		private static ImageSource Load(string path)
		{
			return new BitmapImage().FromAssemblySource("OArcanoid", "Assets/" + path);
		}
	}
}
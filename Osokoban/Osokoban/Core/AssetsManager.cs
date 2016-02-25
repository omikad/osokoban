using System.ComponentModel.Composition;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GamesCommon.Helpers;

namespace OSokoban.Core
{
	[Export]
	public class AssetsManager
	{
		public AssetsManager()
		{
			Diamond = Load("Diamond.png");
			Player = Load("Archer.png");
			ChestOpened = Load("ChestOpened.png");
			ChestClosed = Load("ChestClosed.png");
			Wall = Load("Wall.png");
		}

		public ImageSource Diamond { get; }
		public ImageSource Player { get; }
		public ImageSource ChestOpened { get; }
		public ImageSource ChestClosed { get; }
		public ImageSource Wall { get; }

		private static ImageSource Load(string path)
		{
			return new BitmapImage().FromAssemblySource("Osokoban", "Assets/" + path);
		}
	}
}
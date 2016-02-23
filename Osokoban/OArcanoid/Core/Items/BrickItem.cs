using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;

namespace OArcanoid.Core.Items
{
	public enum BrickType
	{
		Wall,
		Diamond,
		Marker,
		Apple,
	}

	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class BrickItem : IGameItem
	{
		private readonly AssetsManager assetsManager;

		[ImportingConstructor]
		public BrickItem(AssetsManager assetsManager)
		{
			this.assetsManager = assetsManager;
		}

		public int ZIndex => 10;

		public BrickType Type { get; }

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			var imageSource = Type == BrickType.Wall ? assetsManager.Wall
				: Type == BrickType.Diamond ? assetsManager.Diamond
				: Type == BrickType.Marker ? assetsManager.Marker
				: assetsManager.Apple;

			dc.DrawImage(imageSource, cellRect);
		}
	}
}
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using GamesCommon.DataTypes;

namespace OSokoban.Core.Items
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class WallItem : IGameItem
	{
		private readonly AssetsManager assetsManager;

		[ImportingConstructor]
		public WallItem(AssetsManager assetsManager)
		{
			this.assetsManager = assetsManager;
		}

		public bool CanMoveHere => false;
		public bool IsPlayer => false;
		public int ZIndex => 10;

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			dc.DrawImage(assetsManager.Wall, cellRect);
		}

		public bool Move(Game game, PointInt currentPoint, PointInt destPoint)
		{
			return false;
		}
	}
}
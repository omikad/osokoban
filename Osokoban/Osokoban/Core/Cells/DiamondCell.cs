using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Osokoban.DataTypes;
using Osokoban.Helpers;

namespace Osokoban.Core.Cells
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class DiamondCell : IGameItem
	{
		private readonly AssetsManager assetsManager;

		[ImportingConstructor]
		public DiamondCell(AssetsManager assetsManager)
		{
			this.assetsManager = assetsManager;
		}

		public bool CanMoveHere => false;
		public bool IsPlayer => false;
		public int ZIndex => 10;

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			dc.DrawImage(assetsManager.Diamond, cellRect);			
		}

		public bool Move(Game game, PointInt currentPoint, PointInt destPoint)
		{
			if (!game.Items.AreIndicesAllowed(destPoint)) return false;

			var destList = game.Items.Get(destPoint);

			if (destList.Count != 1)
				return false;

			var asEmpty = destList[0] as EmptyCell;
			if (asEmpty != null)
			{
				MoveSelf(game, currentPoint, destPoint);
				return true;
			}

			var asChest = destList[0] as ChestCell;
			if (asChest != null)
			{
				asChest.SetDiamond(true);
				MoveSelf(game, currentPoint, destPoint);
				return true;
			}

			return false;
		}

		private void MoveSelf(Game game, PointInt currentPoint, PointInt destPoint)
		{
			game.MoveUnconditionally(this, currentPoint, destPoint);

			var fromList = game.Items.Get(currentPoint);

			var chest = fromList.OfType<ChestCell>().FirstOrDefault();

			chest?.SetDiamond(false);
		}
	}
}
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using GamesCommon.DataTypes;
using GamesCommon.Helpers;

namespace OSokoban.Core.Items
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class PlayerItem : IGameItem
	{
		private readonly AssetsManager assetsManager;

		[ImportingConstructor]
		public PlayerItem(AssetsManager assetsManager)
		{
			this.assetsManager = assetsManager;
		}

		public bool CanMoveHere => false;
		public bool IsPlayer => true;
		public int ZIndex => 100;

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			dc.DrawImage(assetsManager.Player, cellRect);			
		}

		public bool Move(Game game, PointInt currentPoint, PointInt destPoint)
		{
			if (!game.Items.AreIndicesAllowed(destPoint)) return false;

			var destList = game.Items.Get(destPoint);

			IGameItem obstacle = null;

			foreach (var gameItem in destList)
				if (!gameItem.CanMoveHere)
				{
					if (obstacle != null) return false;

					obstacle = gameItem;
				}

			if (obstacle == null
				|| obstacle.Move(game, destPoint, destPoint + (destPoint - currentPoint)))
			{
				game.MoveUnconditionally(this, currentPoint, destPoint);
				return true;
			}

			return false;
		}
	}
}
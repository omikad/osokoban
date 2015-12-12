using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Osokoban.DataTypes;

namespace Osokoban.Core.Cells
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class ChestCell : IGameItem
	{
		private readonly AssetsManager assetsManager;
		private bool hasDiamond;

		[ImportingConstructor]
		public ChestCell(AssetsManager assetsManager)
		{
			this.assetsManager = assetsManager;
		}

		public bool CanMoveHere => true;
		public bool IsPlayer => false;
		public int ZIndex => 10;

		public void SetDiamond(bool hasDiamondNow)
		{
			if (hasDiamond && hasDiamondNow)
				throw new InvalidOperationException("Can't have more than one diamond");

			if (!hasDiamond && !hasDiamondNow)
				throw new InvalidOperationException("Can't remove diamond from empty chest");

			hasDiamond = hasDiamondNow;
		}

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			var img = hasDiamond ? assetsManager.ChestClosed : assetsManager.ChestOpened;
			dc.DrawImage(img, cellRect);
		}

		public bool Move(Game game, PointInt currentPoint, PointInt destPoint)
		{
			return false;
		}
	}
}
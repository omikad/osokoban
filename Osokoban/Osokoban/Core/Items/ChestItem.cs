using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Common.DataTypes;

namespace Osokoban.Core.Items
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class ChestItem : IGameItem
	{
		private readonly AssetsManager assetsManager;

		[ImportingConstructor]
		public ChestItem(AssetsManager assetsManager)
		{
			this.assetsManager = assetsManager;
		}

		public bool CanMoveHere => true;
		public bool IsPlayer => false;
		public int ZIndex => 10;
		public bool HasDiamond { get; private set; }

		public void SetDiamond(bool hasDiamondNow)
		{
			if (HasDiamond && hasDiamondNow)
				throw new InvalidOperationException("Can't have more than one diamond");

			if (!HasDiamond && !hasDiamondNow)
				throw new InvalidOperationException("Can't remove diamond from empty chest");

			HasDiamond = hasDiamondNow;
		}

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			var img = HasDiamond ? assetsManager.ChestClosed : assetsManager.ChestOpened;
			dc.DrawImage(img, cellRect);
		}

		public bool Move(Game game, PointInt currentPoint, PointInt destPoint)
		{
			return false;
		}
	}
}
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Common.DataTypes;
using Common.Helpers;

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

		private static readonly BrickType[] order = {
			BrickType.Wall,
			BrickType.Apple,
			BrickType.Diamond,
		};

		public int ZIndex => 10;
		public bool IsObstacle => true;

		public BrickType Type { get; private set; }

		[ImportingConstructor]
		public BrickItem(AssetsManager assetsManager)
		{
			this.assetsManager = assetsManager;
			Type = order[0];
		}

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			var imageSource = Type == BrickType.Wall ? assetsManager.Wall
				: Type == BrickType.Diamond ? assetsManager.Diamond
				: Type == BrickType.Marker ? assetsManager.Marker
				: assetsManager.Apple;

			dc.DrawImage(imageSource, cellRect);
		}

		public void OnHit(Game game, PointInt brickPoint)
		{
			var orderIndex = Array.IndexOf(order, Type);

			if (orderIndex == order.Length - 1)
				game.Items.Get(brickPoint).Remove(this);
			else
				Type = order[orderIndex + 1];
		}
	}
}
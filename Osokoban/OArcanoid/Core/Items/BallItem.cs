using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Common.DataTypes;
using Common.Helpers;

namespace OArcanoid.Core.Items
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class BallItem : IGameItem
	{
		private readonly AssetsManager assetsManager;

		[ImportingConstructor]
		public BallItem(AssetsManager assetsManager)
		{
			this.assetsManager = assetsManager;
		}

		public int ZIndex => 20;

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			dc.DrawImage(assetsManager.Ball, cellRect);
		}

		public void Init(PointInt startPoint, Random random)
		{
			Point = startPoint;
			Velocity = new[]
			{
				new PointInt(1, 1),
				new PointInt(1, -1),
				new PointInt(-1, 1),
				new PointInt(-1, -1),
			}.Random(random);
		}

		public PointInt Point { get; private set; }
		public PointInt Velocity { get; private set; }

		public void Move(Game game)
		{
			//if (Point.Y == game.CellsY - 1) return;

			var peekPoint = Point + Velocity;

			var isTop = peekPoint.Y == 0;
			var isLeft = peekPoint.X == 0;
			var isRight = peekPoint.X == game.CellsX - 1;
			var isBottom = peekPoint.Y == game.CellsY - 1;

			if (isTop || isBottom) FlipVertical();
			if (isLeft || isRight) FlipHorizontal();

			game.MoveUnconditionally(this, Point, peekPoint);
			Point = peekPoint;
		}

		private void FlipHorizontal()
		{
			Velocity = new PointInt(-Velocity.X, Velocity.Y);
		}

		private void FlipVertical()
		{
			Velocity = new PointInt(Velocity.X, -Velocity.Y);
		}
	}
}
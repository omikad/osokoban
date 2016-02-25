using System;
using System.ComponentModel.Composition;
using System.Linq;
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

		public int ZIndex => 5;
		public bool IsObstacle => false;

		[ImportingConstructor]
		public BallItem(AssetsManager assetsManager)
		{
			this.assetsManager = assetsManager;
		}

		public void Draw(DrawingContext dc, Rect cellRect)
		{
			dc.DrawImage(assetsManager.Ball, cellRect);
		}

		public void Init(PointInt startPoint, Random random)
		{
			Point = startPoint;
			Velocity = new[]
			{
				new PointInt(1, -1),
				new PointInt(-1, -1),
			}.Random(random);
		}

		public PointInt Point { get; private set; }
		public PointInt Velocity { get; private set; }

		public void Move(Game game)
		{
			if (Point.Y == game.CellsY - 1) return;

			while (MakeFlips(game)) {}

			var destPoint = Point + Velocity;
			game.MoveUnconditionally(this, Point, destPoint);
			Point = destPoint;
		}

		private bool MakeFlips(Game game)
		{
			var pointX = new PointInt(Point.X + Velocity.X, Point.Y);
			var pointY = new PointInt(Point.X, Point.Y + Velocity.Y);
			var pointF = Point + Velocity;

			var listX = game.Items.AreIndicesAllowed(pointX) ? game.Items.Get(pointX) : null;
			var listY = game.Items.AreIndicesAllowed(pointY) ? game.Items.Get(pointY) : null;
			var listF = game.Items.AreIndicesAllowed(pointF) ? game.Items.Get(pointF) : null;

			var isObstacleX = listX == null || listX.Any(gi => gi.IsObstacle);
			var isObstacleY = listY == null || listY.Any(gi => gi.IsObstacle);
			var isObstacleF = listF == null || listF.Any(gi => gi.IsObstacle);

			if (isObstacleX && isObstacleY)
			{
				FlipHorizontal();
				FlipVertical();
				OnHit(game, pointX);
				OnHit(game, pointY);
				OnHit(game, pointF);
				return true;
			}
			if (!isObstacleX && !isObstacleY && isObstacleF)
			{
				FlipHorizontal();
				FlipVertical();
				OnHit(game, pointF);
				return true;
			}
			if (isObstacleX)
			{
				FlipHorizontal();
				OnHit(game, pointX);
				return true;
			}
			if (isObstacleY)
			{
				FlipVertical();
				OnHit(game, pointY);
				return true;
			}

			return false;
		}

		private static void OnHit(Game game, PointInt hitPoint)
		{
			if (!game.Items.AreIndicesAllowed(hitPoint)) return;

			foreach (var brick in game.Items.Get(hitPoint).OfType<BrickItem>().ToArray())
				brick.OnHit(game, hitPoint);
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
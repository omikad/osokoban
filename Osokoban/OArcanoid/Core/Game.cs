using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Common.DataTypes;
using Common.GameCore;
using Common.Helpers;
using Common.UI;
using OArcanoid.Core.Items;

namespace OArcanoid.Core
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class Game : IBoard
	{
		public readonly List<IGameItem>[,] Items;

		public int CellsX => Items.GetLength(0);
		public int CellsY => Items.GetLength(1);

		private readonly Pad pad;
		private readonly BallItem ball;

		[ImportingConstructor]
		public Game(LevelReader levelReader, Func<Random> random)
		{
			Items = levelReader.GenerateRandomLevel(random());

			pad = new Pad(Items);

			var ballPoint = Items.IndicesWhere(l => l.Any(gi => gi is BallItem)).First();
			ball = Items.Get(ballPoint).OfType<BallItem>().First();
			ball.Init(ballPoint, random());
		}

		public IEnumerable<IDrawable> DrawableContent(int x, int y)
		{
			return Items[x, y];
		}

		public void MoveLeft()
		{
			pad.MoveLeft(this);
			ball.Move(this);
		}

		public void MoveRight()
		{
			pad.MoveRight(this);
			ball.Move(this);
		}

		public void SkipTurn()
		{
			ball.Move(this);
		}

		public void MoveUnconditionally(IGameItem gameItem, PointInt currentPoint, PointInt destPoint)
		{
			Items.Get(currentPoint).Remove(gameItem);
			Items.Get(destPoint).Add(gameItem);
		}
	}
}
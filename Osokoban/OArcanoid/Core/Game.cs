using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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

		[ImportingConstructor]
		public Game(LevelReader levelReader, Func<Random> random)
		{
			Items = levelReader.GenerateRandomLevel(random());
		}

		public IEnumerable<IDrawable> DrawableContent(int x, int y)
		{
			return Items[x, y];
		}

		public void MoveLeft()
		{
		}

		public void MoveRight()
		{
		}

		public void MoveUnconditionally(IGameItem gameItem, PointInt currentPoint, PointInt destPoint)
		{
			Items.Get(currentPoint).Remove(gameItem);
			Items.Get(destPoint).Add(gameItem);
		}
	}
}
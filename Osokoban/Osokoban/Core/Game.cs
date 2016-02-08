using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Osokoban.Core.Items;
using Osokoban.DataTypes;
using Osokoban.Helpers;

namespace Osokoban.Core
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class Game
	{
		public readonly List<IGameItem>[,] Items;
		public int CellsX => Items.GetLength(0);
		public int CellsY => Items.GetLength(1);

		private PointInt playerPoint;

		[ImportingConstructor]
		public Game(LevelReader levelReader, Func<Random> random)
		{
			Items = levelReader.GenerateRandomLevel(random());
			playerPoint = Items.EnumerateIndices().First(p => Items.Get(p).Any(i => i.IsPlayer));
		}

		public void MovePlayer(PointInt delta)
		{
			var destPoint = playerPoint + delta;

			var player = Items.Get(playerPoint).First(i => i.IsPlayer);

			if (player.Move(this, playerPoint, destPoint))
				playerPoint = destPoint;
		}

		public void MoveUnconditionally(IGameItem gameItem, PointInt currentPoint, PointInt destPoint)
		{
			Items.Get(currentPoint).Remove(gameItem);
			Items.Get(destPoint).Add(gameItem);
		}
	}
}
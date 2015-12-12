using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Osokoban.Core.Cells;
using Osokoban.DataTypes;
using Osokoban.Helpers;

namespace Osokoban.Core
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class Game
	{
		public readonly ICell[,] Cells;
		public int CellsX => Cells.GetLength(0);
		public int CellsY => Cells.GetLength(1);

		private PointInt playerPoint;
		private ICell cellBehindPlayer;

		[ImportingConstructor]
		public Game(LevelReader levelReader, ExportProvider container)
		{
			Cells = levelReader.GenerateRandomLevel(new Random());
			playerPoint = Cells.EnumerateIndices().First(p => Cells.Get(p).IsPlayer);
			cellBehindPlayer = container.GetExportedValue<EmptyCell>();
		}

		public void MovePlayer(PointInt delta)
		{
			var toPoint = playerPoint + delta;
			if (!Cells.AreIndicesAllowed(toPoint)) return;

			var to = Cells.Get(toPoint);
			if (!to.CanMoveHere) return;

			var player = Cells.Get(playerPoint);

			Cells.Set(playerPoint, cellBehindPlayer);
			Cells.Set(toPoint, player);

			cellBehindPlayer = to;

			playerPoint = toPoint;
		}
	}
}
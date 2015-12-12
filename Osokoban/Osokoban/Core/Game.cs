using System;
using System.ComponentModel.Composition;
using Osokoban.Core.Cells;

namespace Osokoban.Core
{
	[Export, PartCreationPolicy(CreationPolicy.NonShared)]
	public class Game
	{
		private readonly ICell[,] cells;

		public readonly int CellsX;
		public readonly int CellsY;

		public ICell this[int x, int y] => cells[x, y];

		[ImportingConstructor]
		public Game(LevelReader levelReader)
		{
			cells = levelReader.GenerateRandomLevel(new Random());
			CellsX = cells.GetLength(0);
			CellsY = cells.GetLength(1);
		}
	}
}
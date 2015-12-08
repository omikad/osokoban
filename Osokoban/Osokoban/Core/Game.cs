using Osokoban.Core.Cells;

namespace Osokoban.Core
{
	public class Game
	{
		private readonly ICell[,] cells;

		public int CellsX;
		public int CellsY;

		public ICell this[int x, int y] => cells[x, y];

		public Game()
		{
			CellsX = 20;
			CellsY = 14;
			cells = new ICell[CellsX, CellsY];

			for (var i = 0; i < CellsX; i++)
				for (var j = 0; j < CellsY; j++)
					cells[i, j] = new EmptyCell();
		}
	}
}
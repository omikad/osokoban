using System.Collections.Generic;
using GamesCommon.UI;

namespace GamesCommon.GameCore
{
	public interface IBoard
	{
		int CellsX { get; }
		int CellsY { get; }

		IEnumerable<IDrawable> DrawableContent(int x, int y);
	}
}
using System.Collections.Generic;
using Common.UI;

namespace Common.GameCore
{
	public interface IBoard
	{
		int CellsX { get; }
		int CellsY { get; }

		IEnumerable<IDrawable> DrawableContent(int x, int y);
	}
}
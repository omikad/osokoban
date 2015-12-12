using System.Windows;
using System.Windows.Media;

namespace Osokoban.Core.Cells
{
	public interface ICell
	{
		bool CanMoveHere { get; }
		bool IsPlayer { get; }

		void Draw(DrawingContext dc, Rect cellRect);
	}
}
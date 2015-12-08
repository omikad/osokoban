using System.Windows;
using System.Windows.Media;

namespace Osokoban.Core.Cells
{
	public interface ICell
	{
		void Draw(DrawingContext dc, Rect cellRect);
	}
}
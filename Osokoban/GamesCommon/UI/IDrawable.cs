using System.Windows;
using System.Windows.Media;

namespace GamesCommon.UI
{
	public interface IDrawable
	{
		int ZIndex { get; }

		void Draw(DrawingContext dc, Rect cellRect);
	}
}
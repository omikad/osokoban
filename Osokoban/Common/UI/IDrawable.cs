using System.Windows;
using System.Windows.Media;

namespace Common.UI
{
	public interface IDrawable
	{
		int ZIndex { get; }

		void Draw(DrawingContext dc, Rect cellRect);
	}
}
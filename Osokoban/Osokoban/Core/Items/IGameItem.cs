using System.Windows;
using System.Windows.Media;
using Osokoban.DataTypes;

namespace Osokoban.Core.Items
{
	public interface IGameItem
	{
		bool CanMoveHere { get; }
		bool IsPlayer { get; }

		int ZIndex { get; }

		void Draw(DrawingContext dc, Rect cellRect);

		bool Move(Game game, PointInt currentPoint, PointInt destPoint);
	}
}
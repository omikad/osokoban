using Common.DataTypes;
using Common.UI;

namespace OSokoban.Core.Items
{
	public interface IGameItem : IDrawable
	{
		bool CanMoveHere { get; }
		bool IsPlayer { get; }

		bool Move(Game game, PointInt currentPoint, PointInt destPoint);
	}
}
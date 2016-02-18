using Common.DataTypes;
using Common.UI;

namespace Osokoban.Core.Items
{
	public interface IGameItem : IDrawable
	{
		bool CanMoveHere { get; }
		bool IsPlayer { get; }

		bool Move(Game game, PointInt currentPoint, PointInt destPoint);
	}
}
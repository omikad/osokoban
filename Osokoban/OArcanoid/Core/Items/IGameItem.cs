using GamesCommon.UI;

namespace OArcanoid.Core.Items
{
	public interface IGameItem : IDrawable
	{
		bool IsObstacle { get; }
	}
}
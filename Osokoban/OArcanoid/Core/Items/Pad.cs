using System.Collections.Generic;
using System.Linq;
using GamesCommon.DataTypes;
using GamesCommon.Helpers;

namespace OArcanoid.Core.Items
{
	public class Pad
	{
		private PointInt leftPoint;
		private readonly PadItem[] parts;
		private readonly PointInt[] partPoints;
		private readonly PointInt left;
		private readonly PointInt right;

		public Pad(List<IGameItem>[,] items)
		{
			leftPoint = items.IndicesWhere(l => l.OfType<PadItem>().Any(p => p.Part == PadPart.Left)).First();
			partPoints = Enumerable.Range(0, 3).Select(dx => new PointInt(dx, 0)).ToArray();
			parts = partPoints.Select(pp => items.Get(leftPoint + pp).OfType<PadItem>().First()).ToArray();
			left = new PointInt(-1, 0);
			right = new PointInt(1, 0);
		}

		public void MoveLeft(Game game)
		{
			if (leftPoint.X <= 0) return;

			for (var dx = 0; dx < parts.Length; dx++)
				game.MoveUnconditionally(parts[dx], leftPoint + partPoints[dx], leftPoint + partPoints[dx] + left);

			leftPoint += left;
		}

		public void MoveRight(Game game)
		{
			if (leftPoint.X + parts.Length >= game.CellsX) return;

			for (var dx = 0; dx < parts.Length; dx++)
				game.MoveUnconditionally(parts[dx], leftPoint + partPoints[dx], leftPoint + partPoints[dx] + right);

			leftPoint += right;
		}
	}
}
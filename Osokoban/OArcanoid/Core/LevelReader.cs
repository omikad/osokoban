using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Common.DataTypes;
using Common.Helpers;
using OArcanoid.Core.Items;

namespace OArcanoid.Core
{
	[Export]
	public class LevelReader
	{
		private readonly CompositionContainer container;

		[ImportingConstructor]
		public LevelReader(CompositionContainer container)
		{
			this.container = container;
		}

		public List<IGameItem>[,] GenerateRandomLevel(Random random)
		{
			const int cellsX = 10;
			const int cellsY = 8;

			var result = new List<IGameItem>[cellsX, cellsY];

			foreach (var point in result.EnumerateIndices())
				result.Set(point, new List<IGameItem> { container.GetExportedValue<EmptyItem>() });

			FillBricks(random, result, 22, 1, 4);

			FillPad(result, cellsX / 2 - 1, cellsY - 1);

			result[cellsX / 2, cellsY - 2].Add(container.GetExportedValue<BallItem>());

			return result;
		}

		private void FillPad(List<IGameItem>[,] result, int padX, int padY)
		{
			var parts = new[]
			{
				container.GetExportedValue<PadItem>(),
				container.GetExportedValue<PadItem>(),
				container.GetExportedValue<PadItem>()
			};

			parts[0].Part = PadPart.Left;
			parts[1].Part = PadPart.Middle;
			parts[2].Part = PadPart.Right;

			result[padX, padY].Add(parts[0]);
			result[padX + 1, padY].Add(parts[1]);
			result[padX + 2, padY].Add(parts[2]);
		}

		private void FillBricks(Random random, List<IGameItem>[,] result, int count, int minY, int maxY)
		{
			while (count > 0)
			{
				var x = result.GetLength(0) == 1 ? 0 : random.Next(result.GetLength(0));
				var y = random.Next(minY, maxY);
				var point = new PointInt(x, y);

				var list = result.Get(point);
				if (list.Count > 1) continue;

				list.Add(container.GetExportedValue<BrickItem>());
				count--;
			}
		}
	}
}
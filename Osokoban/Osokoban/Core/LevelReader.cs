using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Osokoban.Core.Cells;
using Osokoban.Helpers;

namespace Osokoban.Core
{
	[Export]
	public class LevelReader
	{
		private readonly ExportProvider container;

		[ImportingConstructor]
		public LevelReader(ExportProvider container)
		{
			this.container = container;
		}

		public ICell[,] GenerateRandomLevel(Random random)
		{
			const int cellsX = 20;
			const int cellsY = 14;

			var result = new ICell[cellsX, cellsY];

			var wallsCount = random.Next(10, 50);
			var applesCount = random.Next(2, 10);

			FillRandom(random, result, wallsCount, () => container.GetExportedValue<WallCell>());
			FillRandom(random, result, applesCount, () => container.GetExportedValue<TargetPlaceCell>());
			FillRandom(random, result, applesCount, () => container.GetExportedValue<DiamondCell>());
			FillRandom(random, result, 1, () => container.GetExportedValue<PlayerCell>());

			foreach (var point in result.EnumerateIndices())
				if (result.Get(point) == null)
					result.Set(point, container.GetExportedValue<EmptyCell>());

			return result;
		}

		private static void FillRandom(Random random, ICell[,] result, int count, Func<ICell> createCell)
		{
			while (count > 0)
			{
				var point = result.RandomIndex(random);

				if (result.Get(point) != null) continue;

				result.Set(point, createCell());
				count--;
			}
		}
	}
}
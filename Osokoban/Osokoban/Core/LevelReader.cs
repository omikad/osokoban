using System;
using System.Collections.Generic;
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

		public List<IGameItem>[,] GenerateRandomLevel(Random random)
		{
			const int cellsX = 20;
			const int cellsY = 14;

			var result = new List<IGameItem>[cellsX, cellsY];

			var wallsCount = random.Next(10, 50);
			var applesCount = random.Next(2, 10);

			FillRandom(random, result, wallsCount, () => container.GetExportedValue<WallCell>());
			FillRandom(random, result, applesCount, () => container.GetExportedValue<ChestCell>());
			FillRandom(random, result, applesCount, () => container.GetExportedValue<DiamondCell>());
			FillRandom(random, result, 1, () => container.GetExportedValue<PlayerCell>());

			foreach (var point in result.EnumerateIndices())
				if (result.Get(point) == null)
					result.Set(point, new List<IGameItem> {container.GetExportedValue<EmptyCell>()});

			foreach (var point in result.EnumerateIndices())
			{
				var item = result.Get(point)[0];
				if (item.IsPlayer || item is DiamondCell)
					result.Get(point).Insert(0, container.GetExportedValue<EmptyCell>());
			}

			return result;
		}

		private static void FillRandom(Random random, List<IGameItem>[,] result, int count, Func<IGameItem> createItem)
		{
			while (count > 0)
			{
				var point = result.RandomIndex(random);

				if (result.Get(point) != null) continue;

				result.Set(point, new List<IGameItem> { createItem() });
				count--;
			}
		}
	}
}
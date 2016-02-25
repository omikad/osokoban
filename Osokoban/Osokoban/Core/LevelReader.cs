using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using GamesCommon.Helpers;
using OSokoban.Core.Items;

namespace OSokoban.Core
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

			var wallsCount = random.Next(10, 30);
			var applesCount = random.Next(2, 10);

			FillRandom(random, result, wallsCount, () => container.GetExportedValue<WallItem>());
			FillRandom(random, result, applesCount, () => container.GetExportedValue<ChestItem>());
			FillRandom(random, result, applesCount, () => container.GetExportedValue<DiamondItem>());
			FillRandom(random, result, 1, () => container.GetExportedValue<PlayerItem>());

			foreach (var point in result.EnumerateIndices())
				if (result.Get(point) == null)
					result.Set(point, new List<IGameItem> { container.GetExportedValue<EmptyItem>()});

			foreach (var point in result.EnumerateIndices())
			{
				var item = result.Get(point)[0];
				if (item.IsPlayer || item is DiamondItem)
					result.Get(point).Insert(0, container.GetExportedValue<EmptyItem>());
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
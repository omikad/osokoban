using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Osokoban.Core.Items;
using Osokoban.Helpers;

namespace Osokoban.Core
{
	[Export]
	public class LevelReader
	{
		private readonly ItemsFactory itemsFactory;

		[ImportingConstructor]
		public LevelReader(ItemsFactory itemsFactory)
		{
			this.itemsFactory = itemsFactory;
		}

		public List<IGameItem>[,] GenerateRandomLevel(Random random)
		{
			const int cellsX = 20;
			const int cellsY = 14;

			var result = new List<IGameItem>[cellsX, cellsY];

			var wallsCount = random.Next(10, 50);
			var applesCount = random.Next(2, 10);

			FillRandom(random, result, wallsCount, () => itemsFactory.CreateItem<WallItem>());
			FillRandom(random, result, applesCount, () => itemsFactory.CreateItem<ChestItem>());
			FillRandom(random, result, applesCount, () => itemsFactory.CreateItem<DiamondItem>());
			FillRandom(random, result, 1, () => itemsFactory.CreateItem<PlayerItem>());

			foreach (var point in result.EnumerateIndices())
				if (result.Get(point) == null)
					result.Set(point, new List<IGameItem> {itemsFactory.CreateItem<EmptyItem>()});

			foreach (var point in result.EnumerateIndices())
			{
				var item = result.Get(point)[0];
				if (item.IsPlayer || item is DiamondItem)
					result.Get(point).Insert(0, itemsFactory.CreateItem<EmptyItem>());
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
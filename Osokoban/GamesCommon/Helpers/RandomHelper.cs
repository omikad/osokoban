using System;
using System.Collections.Generic;

namespace GamesCommon.Helpers
{
	public static class RandomHelper
	{
		public static T Random<T>(this IList<T> arr, Random rnd)
		{
			return arr[RandomIndex(arr, rnd)];
		}

		public static int RandomIndex<T>(this IList<T> arr, Random rnd)
		{
			return arr.Count == 1 ? 0 : rnd.Next(arr.Count);
		}

		public static T RandomElement<T>(this IEnumerable<T> source, Random rnd)
		{
			var current = default(T);
			var count = 0;
			foreach (var element in source)
			{
				count++;
				if (rnd.Next(count) == 0)
					current = element;
			}

			if (count == 0)
				throw new InvalidOperationException("Sequence is empty");

			return current;
		}
	}
}
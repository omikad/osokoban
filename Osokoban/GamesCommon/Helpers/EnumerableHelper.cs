using System;
using System.Collections.Generic;

namespace GamesCommon.Helpers
{
	public static class EnumerableHelper
	{
		public static T ElementWithMax<T, TScore>(this IEnumerable<T> seq, Func<T, TScore> getScore) where TScore : IComparable
		{
			var best = default(T);
			var bestScore = default(TScore);
			var isFirst = true;

			foreach (var item in seq)
			{
				var score = getScore(item);

				if (isFirst)
				{
					best = item;
					bestScore = score;
					isFirst = false;
				}
				else if (score.CompareTo(bestScore) > 0)
				{
					best = item;
					bestScore = score;
				}
			}

			return best;
		}
	}
}
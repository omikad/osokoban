using System;
using System.Collections.Generic;
using Osokoban.DataTypes;

namespace Osokoban.Helpers
{
	public static class ArrayHelper
	{
		public static bool AreIndicesAllowed<T>(this T[,] arr, int i, int j)
		{
			if (i < 0 || j < 0) return false;
			if (i >= arr.GetLength(0) || j >= arr.GetLength(1)) return false;
			return true;
		}

		public static bool AreIndicesAllowed<T>(this T[,] arr, PointInt point)
		{
			return AreIndicesAllowed(arr, point.X, point.Y);
		}

		public static PointInt RandomIndex<T>(this T[,] arr, Random rnd)
		{
			var x = arr.GetLength(0) == 1 ? 0 : rnd.Next(arr.GetLength(0));
			var y = arr.GetLength(1) == 1 ? 0 : rnd.Next(arr.GetLength(1));
			return new PointInt(x, y);
		}

		public static T Get<T>(this T[,] arr, PointInt point)
		{
			return arr[point.X, point.Y];
		}

		public static void Set<T>(this T[,] arr, PointInt point, T val)
		{
			arr[point.X, point.Y] = val;
		}

		public static IEnumerable<PointInt> EnumerateIndices<T>(this T[,] arr)
		{
			for (var i = 0; i < arr.GetLength(0); i++)
				for (var j = 0; j < arr.GetLength(1); j++)
					yield return new PointInt(i, j);
		} 
	}
}
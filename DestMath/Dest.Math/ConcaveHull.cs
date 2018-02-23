using System;
using UnityEngine;

namespace Dest.Math
{
	public static class ConcaveHull
	{
		public static bool Create2D(Vector2[] points, out int[] concaveHull, out int[] convexHull, float algorithmThreshold, float epsilon = 1E-05f)
		{
			if (algorithmThreshold <= 0f)
			{
				Logger.LogError("algorithmThreshold must be positive number");
				int[] array;
				convexHull = (array = null);
				concaveHull = array;
				return false;
			}
			int num;
			if (!ConvexHull.Create2D(points, out convexHull, out num, epsilon))
			{
				Logger.LogError("Convex hull creation failed, can't create concave hull");
				int[] array2;
				convexHull = (array2 = null);
				concaveHull = array2;
				return false;
			}
			if (num != 2)
			{
				Logger.LogWarning("Convex hull dimension is less than 2, can't create concave hull");
				int[] array3;
				convexHull = (array3 = null);
				concaveHull = array3;
				return false;
			}
			bool flag = ConcaveHull2.Create(points, out concaveHull, convexHull, algorithmThreshold, epsilon);
			if (!flag)
			{
				convexHull = null;
			}
			return flag;
		}

		public static bool Create2D(Vector2[] points, out int[] concaveHull, float algorithmThreshold, float epsilon = 1E-05f)
		{
			int[] array;
			return ConcaveHull.Create2D(points, out concaveHull, out array, algorithmThreshold, epsilon);
		}
	}
}

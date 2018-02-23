using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dest.Math
{
	public static class ConvexHull
	{
		public static bool Create2D(IList<Vector2> points, out int[] indices, out int dimension, float epsilon = 1E-05f)
		{
			if (points == null || points.Count == 0)
			{
				indices = null;
				dimension = -1;
				return false;
			}
			epsilon = ((epsilon >= 0f) ? epsilon : 0f);
			return ConvexHull2.Create(points, epsilon, out dimension, out indices);
		}

		public static bool Create3D(IList<Vector3> points, out int[] indices, out int dimension, float epsilon = 1E-05f)
		{
			if (points == null || points.Count == 0)
			{
				indices = null;
				dimension = -1;
				return false;
			}
			epsilon = ((epsilon >= 0f) ? epsilon : 0f);
			return ConvexHull3.Create(points, epsilon, out dimension, out indices);
		}
	}
}

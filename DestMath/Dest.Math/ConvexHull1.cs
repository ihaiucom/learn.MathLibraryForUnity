using System;
using System.Collections.Generic;

namespace Dest.Math
{
	internal class ConvexHull1
	{
		private class SortedVertex
		{
			public float Value;

			public int Index;
		}

		public static void Create(float[] vertices, float epsilon, out int dimension, out int[] indices)
		{
			int num = vertices.Length;
			ConvexHull1.SortedVertex[] array = new ConvexHull1.SortedVertex[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new ConvexHull1.SortedVertex
				{
					Value = vertices[i],
					Index = i
				};
			}
			Array.Sort<ConvexHull1.SortedVertex>(array, (ConvexHull1.SortedVertex e1, ConvexHull1.SortedVertex e2) => Comparer<float>.Default.Compare(e1.Value, e2.Value));
			float num2 = array[num - 1].Value - array[0].Value;
			if (num2 >= epsilon)
			{
				dimension = 1;
				indices = new int[]
				{
					array[0].Index,
					array[num - 1].Index
				};
				return;
			}
			dimension = 0;
			int[] array2 = new int[1];
			indices = array2;
		}
	}
}

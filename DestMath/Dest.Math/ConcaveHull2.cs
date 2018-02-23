using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dest.Math
{
	internal class ConcaveHull2
	{
		private struct Edge
		{
			public int V0;

			public int V1;

			public Edge(int v0, int v1)
			{
				this.V0 = v0;
				this.V1 = v1;
			}
		}

		private struct InnerPoint
		{
			public float AverageDistance;

			public float Distance0;

			public float Distance1;

			public int Index;
		}

		private static void Quicksort(ConcaveHull2.InnerPoint[] x, int first, int last)
		{
			if (first < last)
			{
				int i = first;
				int num = last;
				ConcaveHull2.InnerPoint innerPoint;
				while (i < num)
				{
					while (x[i].AverageDistance <= x[first].AverageDistance)
					{
						if (i >= last)
						{
							break;
						}
						i++;
					}
					while (x[num].AverageDistance > x[first].AverageDistance)
					{
						num--;
					}
					if (i < num)
					{
						innerPoint = x[i];
						x[i] = x[num];
						x[num] = innerPoint;
					}
				}
				innerPoint = x[first];
				x[first] = x[num];
				x[num] = innerPoint;
				ConcaveHull2.Quicksort(x, first, num - 1);
				ConcaveHull2.Quicksort(x, num + 1, last);
			}
		}

		private static float CalcDistanceFromPointToEdge(ref Vector2 pointA, ref Vector2 v0, ref Vector2 v1)
		{
			float num = v0.x - pointA.x;
			float num2 = v0.y - pointA.y;
			float num3 = num * num + num2 * num2;
			num = v1.x - pointA.x;
			num2 = v1.y - pointA.y;
			float num4 = num * num + num2 * num2;
			num = v0.x - v1.x;
			num2 = v0.y - v1.y;
			float num5 = num * num + num2 * num2;
			if (num3 < num4)
			{
				float num6 = num3;
				num3 = num4;
				num4 = num6;
			}
			if (num3 > num4 + num5 || num5 < 1E-05f)
			{
				return Mathf.Sqrt(num4);
			}
			float f = v0.x * v1.y - v1.x * v0.y + v1.x * pointA.y - pointA.x * v1.y + pointA.x * v0.y - v0.x * pointA.y;
			return Mathf.Abs(f) / Mathf.Sqrt(num5);
		}

		public static bool Create(Vector2[] points, out int[] concaveHull, int[] convexHull, float N, float epsilon = 1E-05f)
		{
			LinkedList<ConcaveHull2.Edge> linkedList = new LinkedList<ConcaveHull2.Edge>();
			int num = convexHull.Length;
			HashSet<int> hashSet = new HashSet<int>();
			int num2 = points.Length;
			int i;
			for (i = 0; i < num2; i++)
			{
				hashSet.Add(i);
			}
			int num3 = num - 1;
			for (int j = 0; j < num; j++)
			{
				int num4 = convexHull[j];
				linkedList.AddLast(new ConcaveHull2.Edge(convexHull[num3], num4));
				hashSet.Remove(num4);
				num3 = j;
			}
			ConcaveHull2.InnerPoint[] array = new ConcaveHull2.InnerPoint[hashSet.Count];
			LinkedListNode<ConcaveHull2.Edge> linkedListNode = linkedList.First;
			while (linkedListNode != null && hashSet.Count != 0)
			{
				int v = linkedListNode.Value.V0;
				int v2 = linkedListNode.Value.V1;
				Vector2 a = points[v];
				Vector2 b = points[v2];
				int num5 = 0;
				foreach (int current in hashSet)
				{
					Vector2 vector = points[current];
					float num6 = vector.x - a.x;
					float num7 = vector.y - a.y;
					float num8 = Mathf.Sqrt(num6 * num6 + num7 * num7);
					num6 = vector.x - b.x;
					num7 = vector.y - b.y;
					float num9 = Mathf.Sqrt(num6 * num6 + num7 * num7);
					float averageDistance = (num8 + num9) * 0.5f;
					array[num5] = new ConcaveHull2.InnerPoint
					{
						Distance0 = num8,
						Distance1 = num9,
						AverageDistance = averageDistance,
						Index = current
					};
					num5++;
				}
				ConcaveHull2.Quicksort(array, 0, num5 - 1);
				ConcaveHull2.InnerPoint innerPoint = default(ConcaveHull2.InnerPoint);
				bool flag = false;
				int k = 0;
				int num10 = num5;
				while (k < num10)
				{
					ConcaveHull2.InnerPoint innerPoint2 = array[k];
					Vector2 vector2 = points[innerPoint2.Index];
					int num11 = (innerPoint2.Distance0 < innerPoint2.Distance1) ? v : v2;
					LinkedListNode<ConcaveHull2.Edge> linkedListNode2 = linkedList.First;
					LinkedListNode<ConcaveHull2.Edge> linkedListNode3 = null;
					while (linkedListNode2 != null)
					{
						if (linkedListNode2 != linkedListNode && (linkedListNode2.Value.V0 == num11 || linkedListNode2.Value.V1 == num11))
						{
							linkedListNode3 = linkedListNode2;
							break;
						}
						linkedListNode2 = linkedListNode2.Next;
					}
					float num12 = ConcaveHull2.CalcDistanceFromPointToEdge(ref vector2, ref a, ref b);
					float num13 = ConcaveHull2.CalcDistanceFromPointToEdge(ref vector2, ref points[linkedListNode3.Value.V0], ref points[linkedListNode3.Value.V1]);
					if (num12 < num13)
					{
						innerPoint = innerPoint2;
						flag = true;
						break;
					}
					k++;
				}
				if (!flag)
				{
					linkedListNode = linkedListNode.Next;
				}
				else
				{
					float num14 = (innerPoint.Distance0 < innerPoint.Distance1) ? innerPoint.Distance0 : innerPoint.Distance1;
					float magnitude = (a - b).magnitude;
					if (num14 > 0f && magnitude / num14 > N)
					{
						LinkedListNode<ConcaveHull2.Edge> node = linkedListNode;
						linkedListNode = linkedListNode.Next;
						linkedList.Remove(node);
						int index = innerPoint.Index;
						linkedList.AddLast(new ConcaveHull2.Edge(v, index));
						linkedList.AddLast(new ConcaveHull2.Edge(index, v2));
						hashSet.Remove(index);
					}
					else
					{
						linkedListNode = linkedListNode.Next;
					}
				}
			}
			LinkedListNode<ConcaveHull2.Edge> linkedListNode4 = linkedList.First;
			bool flag2;
			do
			{
				flag2 = false;
				for (LinkedListNode<ConcaveHull2.Edge> next = linkedListNode4.Next; next != null; next = next.Next)
				{
					if (linkedListNode4.Value.V1 == next.Value.V0)
					{
						linkedList.Remove(next);
						linkedList.AddAfter(linkedListNode4, next);
						linkedListNode4 = next;
						flag2 = true;
						break;
					}
				}
			}
			while (flag2);
			concaveHull = new int[linkedList.Count];
			i = 0;
			foreach (ConcaveHull2.Edge current2 in linkedList)
			{
				concaveHull[i] = current2.V0;
				i++;
			}
			return true;
		}
	}
}

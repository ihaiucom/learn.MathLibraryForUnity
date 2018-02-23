using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dest.Math
{
	internal class ConvexHull3
	{
		private class Triangle
		{
			public int V0;

			public int V1;

			public int V2;

			public ConvexHull3.Triangle Adj0;

			public ConvexHull3.Triangle Adj1;

			public ConvexHull3.Triangle Adj2;

			public int Sign;

			public int Time;

			public bool OnStack;

			public Triangle(int v0, int v1, int v2)
			{
				this.V0 = v0;
				this.V1 = v1;
				this.V2 = v2;
				this.Time = -1;
			}

			public ConvexHull3.Triangle GetAdj(int index)
			{
				if (index == 0)
				{
					return this.Adj0;
				}
				if (index == 1)
				{
					return this.Adj1;
				}
				return this.Adj2;
			}

			public void SetAdj(int index, ConvexHull3.Triangle value)
			{
				if (index == 0)
				{
					this.Adj0 = value;
					return;
				}
				if (index == 1)
				{
					this.Adj1 = value;
					return;
				}
				this.Adj2 = value;
			}

			public int GetV(int index)
			{
				if (index == 0)
				{
					return this.V0;
				}
				if (index == 1)
				{
					return this.V1;
				}
				return this.V2;
			}

			public int GetSign(int i, Query3 query)
			{
				if (i != this.Time)
				{
					this.Time = i;
					this.Sign = query.ToPlane(i, this.V0, this.V1, this.V2);
				}
				return this.Sign;
			}

			public void AttachTo(ConvexHull3.Triangle adj0, ConvexHull3.Triangle adj1, ConvexHull3.Triangle adj2)
			{
				this.Adj0 = adj0;
				this.Adj1 = adj1;
				this.Adj2 = adj2;
			}

			public int DetachFrom(int adjIndex, ConvexHull3.Triangle adj)
			{
				if (adjIndex == 0)
				{
					this.Adj0 = null;
				}
				else if (adjIndex == 1)
				{
					this.Adj1 = null;
				}
				else
				{
					this.Adj2 = null;
				}
				if (adj.Adj0 == this)
				{
					adj.Adj0 = null;
					return 0;
				}
				if (adj.Adj1 == this)
				{
					adj.Adj1 = null;
					return 1;
				}
				if (adj.Adj2 == this)
				{
					adj.Adj2 = null;
					return 2;
				}
				return -1;
			}
		}

		private class TerminatorData
		{
			public int V0;

			public int V1;

			public int NullIndex;

			public ConvexHull3.Triangle T;

			public TerminatorData(int v0 = -1, int v1 = -1, int nullIndex = -1, ConvexHull3.Triangle tri = null)
			{
				this.NullIndex = nullIndex;
				this.T = tri;
				this.V0 = v0;
				this.V1 = v1;
			}
		}

		public static bool Create(IList<Vector3> vertices, float epsilon, out int dimension, out int[] indices)
		{
			Vector3ex.Information information = Vector3ex.GetInformation(vertices, epsilon);
			if (information == null)
			{
				dimension = -1;
				indices = null;
				return false;
			}
			int count = vertices.Count;
			if (information.Dimension == 0)
			{
				dimension = 0;
				int[] array = new int[1];
				indices = array;
				return true;
			}
			if (information.Dimension == 1)
			{
				float[] array2 = new float[count];
				Vector3 origin = information.Origin;
				Vector3 vector = information.Direction[0];
				for (int i = 0; i < count; i++)
				{
					Vector3 value = vertices[i] - origin;
					array2[i] = vector.Dot(value);
				}
				ConvexHull1.Create(array2, epsilon, out dimension, out indices);
				return true;
			}
			if (information.Dimension == 2)
			{
				Vector2[] array3 = new Vector2[count];
				Vector3 origin2 = information.Origin;
				Vector3 vector2 = information.Direction[0];
				Vector3 vector3 = information.Direction[1];
				for (int j = 0; j < count; j++)
				{
					Vector3 value2 = vertices[j] - origin2;
					array3[j] = new Vector2(vector2.Dot(value2), vector3.Dot(value2));
				}
				return ConvexHull2.Create(array3, epsilon, out dimension, out indices);
			}
			dimension = 3;
			Vector3[] array4 = new Vector3[count];
			Vector3 min = information.Min;
			float d = 1f / information.MaxRange;
			for (int k = 0; k < count; k++)
			{
				array4[k] = (vertices[k] - min) * d;
			}
			Query3 query = new Query3(array4);
			int v = information.Extreme[0];
			int num = information.Extreme[1];
			int num2 = information.Extreme[2];
			int num3 = information.Extreme[3];
			ConvexHull3.Triangle triangle;
			ConvexHull3.Triangle triangle2;
			ConvexHull3.Triangle triangle3;
			ConvexHull3.Triangle triangle4;
			if (information.ExtremeCCW)
			{
				triangle = new ConvexHull3.Triangle(v, num, num3);
				triangle2 = new ConvexHull3.Triangle(v, num2, num);
				triangle3 = new ConvexHull3.Triangle(v, num3, num2);
				triangle4 = new ConvexHull3.Triangle(num, num2, num3);
				triangle.AttachTo(triangle2, triangle4, triangle3);
				triangle2.AttachTo(triangle3, triangle4, triangle);
				triangle3.AttachTo(triangle, triangle4, triangle2);
				triangle4.AttachTo(triangle2, triangle3, triangle);
			}
			else
			{
				triangle = new ConvexHull3.Triangle(v, num3, num);
				triangle2 = new ConvexHull3.Triangle(v, num, num2);
				triangle3 = new ConvexHull3.Triangle(v, num2, num3);
				triangle4 = new ConvexHull3.Triangle(num, num3, num2);
				triangle.AttachTo(triangle3, triangle4, triangle2);
				triangle2.AttachTo(triangle, triangle4, triangle3);
				triangle3.AttachTo(triangle2, triangle4, triangle);
				triangle4.AttachTo(triangle, triangle3, triangle2);
			}
			HashSet<ConvexHull3.Triangle> hashSet = new HashSet<ConvexHull3.Triangle>();
			hashSet.Add(triangle);
			hashSet.Add(triangle2);
			hashSet.Add(triangle3);
			hashSet.Add(triangle4);
			for (int l = 0; l < count; l++)
			{
				if (!ConvexHull3.Update(hashSet, l, query))
				{
					dimension = -1;
					indices = null;
					return false;
				}
			}
			ConvexHull3.ExtractIndices(hashSet, out indices);
			return true;
		}

		private static bool Update(HashSet<ConvexHull3.Triangle> hull, int i, Query3 query)
		{
			ConvexHull3.Triangle triangle = null;
			foreach (ConvexHull3.Triangle current in hull)
			{
				if (current.GetSign(i, query) > 0)
				{
					triangle = current;
					break;
				}
			}
			if (triangle == null)
			{
				return true;
			}
			Stack<ConvexHull3.Triangle> stack = new Stack<ConvexHull3.Triangle>();
			stack.Push(triangle);
			triangle.OnStack = true;
			Dictionary<int, ConvexHull3.TerminatorData> dictionary = new Dictionary<int, ConvexHull3.TerminatorData>();
			ConvexHull3.Triangle triangle2;
			int num;
			int num2;
			while (stack.Count != 0)
			{
				triangle2 = stack.Pop();
				triangle2.OnStack = false;
				for (int j = 0; j < 3; j++)
				{
					ConvexHull3.Triangle adj = triangle2.GetAdj(j);
					if (adj != null)
					{
						int nullIndex = triangle2.DetachFrom(j, adj);
						if (adj.GetSign(i, query) > 0)
						{
							if (!adj.OnStack)
							{
								stack.Push(adj);
								adj.OnStack = true;
							}
						}
						else
						{
							num = triangle2.GetV(j);
							num2 = triangle2.GetV((j + 1) % 3);
							dictionary[num] = new ConvexHull3.TerminatorData(num, num2, nullIndex, adj);
						}
					}
				}
				hull.Remove(triangle2);
			}
			int count = dictionary.Count;
			if (count < 3)
			{
				Logger.LogError("Terminator must be at least a triangle");
				return false;
			}
			Dictionary<int, ConvexHull3.TerminatorData>.Enumerator enumerator2 = dictionary.GetEnumerator();
			enumerator2.MoveNext();
			KeyValuePair<int, ConvexHull3.TerminatorData> current2 = enumerator2.Current;
			num = current2.Value.V0;
			num2 = current2.Value.V1;
			triangle2 = new ConvexHull3.Triangle(i, num, num2);
			hull.Add(triangle2);
			int v = current2.Value.V0;
			ConvexHull3.Triangle triangle3 = triangle2;
			triangle2.Adj1 = current2.Value.T;
			current2.Value.T.SetAdj(current2.Value.NullIndex, triangle2);
			for (int j = 1; j < count; j++)
			{
				ConvexHull3.TerminatorData terminatorData;
				if (!dictionary.TryGetValue(num2, out terminatorData))
				{
					Logger.LogError("Unexpected condition");
					return false;
				}
				num = num2;
				num2 = terminatorData.V1;
				ConvexHull3.Triangle triangle4 = new ConvexHull3.Triangle(i, num, num2);
				hull.Add(triangle4);
				triangle4.Adj1 = terminatorData.T;
				terminatorData.T.SetAdj(terminatorData.NullIndex, triangle4);
				triangle4.Adj0 = triangle2;
				triangle2.Adj2 = triangle4;
				triangle2 = triangle4;
			}
			if (num2 != v)
			{
				Logger.LogError("Expecting initial vertex");
				return false;
			}
			triangle3.Adj0 = triangle2;
			triangle2.Adj2 = triangle3;
			return true;
		}

		private static void ExtractIndices(HashSet<ConvexHull3.Triangle> hull, out int[] indices)
		{
			int count = hull.Count;
			indices = new int[3 * count];
			int num = 0;
			foreach (ConvexHull3.Triangle current in hull)
			{
				indices[num] = current.V0;
				num++;
				indices[num] = current.V1;
				num++;
				indices[num] = current.V2;
				num++;
			}
			hull.Clear();
		}
	}
}

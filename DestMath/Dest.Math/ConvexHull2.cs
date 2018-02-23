using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dest.Math
{
	internal class ConvexHull2
	{
		private class Edge
		{
			public int V0;

			public int V1;

			public ConvexHull2.Edge E0;

			public ConvexHull2.Edge E1;

			public int Sign;

			public int Time;

			public Edge(int v0, int v1)
			{
				this.V0 = v0;
				this.V1 = v1;
				this.Time = -1;
			}

			public int GetSign(int i, Query2 query)
			{
				if (i != this.Time)
				{
					this.Time = i;
					this.Sign = query.ToLine(i, this.V0, this.V1);
				}
				return this.Sign;
			}

			public void Insert(ConvexHull2.Edge adj0, ConvexHull2.Edge adj1)
			{
				adj0.E1 = this;
				adj1.E0 = this;
				this.E0 = adj0;
				this.E1 = adj1;
			}

			public void DeleteSelf()
			{
				if (this.E0 != null)
				{
					this.E0.E1 = null;
				}
				if (this.E1 != null)
				{
					this.E1.E0 = null;
				}
			}

			public void GetIndices(out int[] indices)
			{
				int num = 0;
				ConvexHull2.Edge edge = this;
				do
				{
					num++;
					edge = edge.E1;
				}
				while (edge != this);
				indices = new int[num];
				num = 0;
				edge = this;
				do
				{
					indices[num] = edge.V0;
					num++;
					edge = edge.E1;
				}
				while (edge != this);
			}
		}

		public static bool Create(IList<Vector2> vertices, float epsilon, out int dimension, out int[] indices)
		{
			Vector2ex.Information information = Vector2ex.GetInformation(vertices, epsilon);
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
				Vector2 origin = information.Origin;
				Vector2 vector = information.Direction[0];
				for (int i = 0; i < count; i++)
				{
					Vector2 value = vertices[i] - origin;
					array2[i] = vector.Dot(value);
				}
				ConvexHull1.Create(array2, epsilon, out dimension, out indices);
				return true;
			}
			dimension = 2;
			Vector2[] array3 = new Vector2[count];
			Vector2 min = information.Min;
			float d = 1f / information.MaxRange;
			for (int j = 0; j < count; j++)
			{
				array3[j] = (vertices[j] - min) * d;
			}
			Query2 query = new Query2(array3);
			int num = information.Extreme[0];
			int num2 = information.Extreme[1];
			int num3 = information.Extreme[2];
			ConvexHull2.Edge edge;
			ConvexHull2.Edge edge2;
			ConvexHull2.Edge edge3;
			if (information.ExtremeCCW)
			{
				edge = new ConvexHull2.Edge(num, num2);
				edge2 = new ConvexHull2.Edge(num2, num3);
				edge3 = new ConvexHull2.Edge(num3, num);
			}
			else
			{
				edge = new ConvexHull2.Edge(num, num3);
				edge2 = new ConvexHull2.Edge(num3, num2);
				edge3 = new ConvexHull2.Edge(num2, num);
			}
			edge.Insert(edge3, edge2);
			edge2.Insert(edge, edge3);
			edge3.Insert(edge2, edge);
			ConvexHull2.Edge edge4 = edge;
			for (int k = 0; k < count; k++)
			{
				if (!ConvexHull2.Update(ref edge4, k, query))
				{
					dimension = -1;
					indices = null;
					return false;
				}
			}
			edge4.GetIndices(out indices);
			return true;
		}

		private static bool Update(ref ConvexHull2.Edge hull, int i, Query2 query)
		{
			ConvexHull2.Edge edge = null;
			ConvexHull2.Edge edge2 = hull;
			while (edge2.GetSign(i, query) <= 0)
			{
				edge2 = edge2.E1;
				if (edge2 == hull)
				{
					IL_20:
					if (edge == null)
					{
						return true;
					}
					ConvexHull2.Edge e = edge.E0;
					if (e == null)
					{
						Logger.LogError("Expecting nonnull adjacent");
						return false;
					}
					ConvexHull2.Edge e2 = edge.E1;
					if (e2 == null)
					{
						Logger.LogError("Expecting nonnull adjacent");
						return false;
					}
					edge.DeleteSelf();
					while (e.GetSign(i, query) > 0)
					{
						hull = e;
						e = e.E0;
						if (e == null)
						{
							Logger.LogError("Expecting nonnull adjacent");
							return false;
						}
						e.E1.DeleteSelf();
					}
					while (e2.GetSign(i, query) > 0)
					{
						hull = e2;
						e2 = e2.E1;
						if (e2 == null)
						{
							Logger.LogError("Expecting nonnull adjacent");
							return false;
						}
						e2.E0.DeleteSelf();
					}
					ConvexHull2.Edge edge3 = new ConvexHull2.Edge(e.V1, i);
					ConvexHull2.Edge edge4 = new ConvexHull2.Edge(i, e2.V0);
					edge3.Insert(e, edge4);
					edge4.Insert(edge3, e2);
					hull = edge3;
					return true;
				}
			}
			edge = edge2;
			goto IL_20;
		}
	}
}

using System;
using System.Text;
using UnityEngine;

namespace Dest.Math
{
	public class Polygon2
	{
		private Vector2[] _vertices;

		private Edge2[] _edges;

		public Vector2[] Vertices
		{
			get
			{
				return this._vertices;
			}
		}

		public Edge2[] Edges
		{
			get
			{
				return this._edges;
			}
		}

		public int VertexCount
		{
			get
			{
				return this._vertices.Length;
			}
		}

		public Vector2 this[int vertexIndex]
		{
			get
			{
				return this._vertices[vertexIndex];
			}
			set
			{
				this._vertices[vertexIndex] = value;
			}
		}

		private Polygon2()
		{
		}

		public Polygon2(Vector2[] vertices)
		{
			this._vertices = new Vector2[vertices.Length];
			this._edges = new Edge2[vertices.Length];
			Array.Copy(vertices, this._vertices, vertices.Length);
			this.UpdateEdges();
		}

		public Polygon2(int vertexCount)
		{
			this._vertices = new Vector2[vertexCount];
			this._edges = new Edge2[vertexCount];
		}

		public static Polygon2 CreateProjected(Polygon3 polygon, ProjectionPlanes projectionPlane)
		{
			Polygon2 polygon2 = new Polygon2(polygon.VertexCount);
			if (projectionPlane == ProjectionPlanes.XY)
			{
				int i = 0;
				int vertexCount = polygon.VertexCount;
				while (i < vertexCount)
				{
					polygon2._vertices[i] = polygon[i].ToVector2XY();
					i++;
				}
			}
			else if (projectionPlane == ProjectionPlanes.XZ)
			{
				int j = 0;
				int vertexCount2 = polygon.VertexCount;
				while (j < vertexCount2)
				{
					polygon2._vertices[j] = polygon[j].ToVector2XZ();
					j++;
				}
			}
			else
			{
				int k = 0;
				int vertexCount3 = polygon.VertexCount;
				while (k < vertexCount3)
				{
					polygon2._vertices[k] = polygon[k].ToVector2YZ();
					k++;
				}
			}
			polygon2.UpdateEdges();
			return polygon2;
		}

		public Edge2 GetEdge(int edgeIndex)
		{
			return this._edges[edgeIndex];
		}

		public void UpdateEdges()
		{
			int num = this._vertices.Length;
			int num2 = num - 1;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = (this._edges[num2].Point1 = this._vertices[i]) - (this._edges[num2].Point0 = this._vertices[num2]);
				this._edges[num2].Length = Vector2ex.Normalize(ref vector, 1E-05f);
				this._edges[num2].Direction = vector;
				this._edges[num2].Normal = vector.Perp();
				num2 = i;
			}
		}

		public void UpdateEdge(int edgeIndex)
		{
			Vector2 vector = (this._edges[edgeIndex].Point1 = this._vertices[(edgeIndex + 1) % this._vertices.Length]) - (this._edges[edgeIndex].Point0 = this._vertices[edgeIndex]);
			this._edges[edgeIndex].Length = Vector2ex.Normalize(ref vector, 1E-05f);
			this._edges[edgeIndex].Direction = vector;
			this._edges[edgeIndex].Normal = vector.Perp();
		}

		public Vector2 CalcCenter()
		{
			Vector2 a = this._vertices[0];
			int num = this._vertices.Length;
			for (int i = 1; i < num; i++)
			{
				a += this._vertices[i];
			}
			return a / (float)num;
		}

		public float CalcPerimeter()
		{
			float num = 0f;
			int i = 0;
			int num2 = this._edges.Length;
			while (i < num2)
			{
				num += this._edges[i].Length;
				i++;
			}
			return num;
		}

		public float CalcArea()
		{
			int num = this._vertices.Length - 1;
			float num2 = this._vertices[0][0] * (this._vertices[1][1] - this._vertices[num][1]) + this._vertices[num][0] * (this._vertices[0][1] - this._vertices[num - 1][1]);
			int num3 = 0;
			int i = 1;
			int num4 = 2;
			while (i < num)
			{
				num2 += this._vertices[i][0] * (this._vertices[num4][1] - this._vertices[num3][1]);
				num3++;
				i++;
				num4++;
			}
			num2 *= 0.5f;
			return Mathf.Abs(num2);
		}

		public bool IsConvex(out Orientations orientation, float threshold = 1E-05f)
		{
			orientation = Orientations.None;
			int num = this._edges.Length;
			int num2 = 0;
			int num3 = num - 1;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = -this._edges[num3].Direction;
				Vector2 direction = this._edges[i].Direction;
				float num4 = vector.DotPerp(direction);
				int num5 = (num4 < -threshold || num4 > threshold) ? ((num4 > 0f) ? 1 : -1) : 0;
				if (num5 != 0)
				{
					if (num2 != 0)
					{
						if ((num2 > 0 && num5 < 0) || (num2 < 0 && num5 > 0))
						{
							return false;
						}
					}
					else
					{
						num2 += num5;
					}
				}
				num3 = i;
			}
			orientation = ((num2 == 0) ? Orientations.None : ((num2 > 0) ? Orientations.CW : Orientations.CCW));
			return orientation != Orientations.None;
		}

		public bool IsConvex(float threshold = 1E-05f)
		{
			Orientations orientations;
			return this.IsConvex(out orientations, 1E-05f);
		}

		public bool HasZeroCorners(float threshold = 1E-05f)
		{
			int num = this._edges.Length;
			float num2 = 1f - threshold;
			int num3 = num - 1;
			for (int i = 0; i < num; i++)
			{
				Vector2 lhs = -this._edges[num3].Direction;
				Vector2 direction = this._edges[i].Direction;
				float num4 = Vector2.Dot(lhs, direction);
				if (num4 >= num2)
				{
					return true;
				}
				num3 = i;
			}
			return false;
		}

		public void ReverseVertices()
		{
			int num = this._vertices.Length;
			int num2 = num / 2;
			num--;
			for (int i = 0; i < num2; i++)
			{
				Vector2 vector = this._vertices[i];
				int num3 = num - i;
				this._vertices[i] = this._vertices[num3];
				this._vertices[num3] = vector;
			}
			this.UpdateEdges();
		}

		public bool ContainsConvexQuadCCW(ref Vector2 point)
		{
			if (this._vertices.Length != 4)
			{
				return false;
			}
			float num = this._vertices[2].y - this._vertices[0].y;
			float num2 = this._vertices[0].x - this._vertices[2].x;
			float num3 = point.x - this._vertices[0].x;
			float num4 = point.y - this._vertices[0].y;
			if (num * num3 + num2 * num4 > 0f)
			{
				num = this._vertices[1].y - this._vertices[0].y;
				num2 = this._vertices[0].x - this._vertices[1].x;
				if (num * num3 + num2 * num4 > 0f)
				{
					return false;
				}
				num = this._vertices[2].y - this._vertices[1].y;
				num2 = this._vertices[1].x - this._vertices[2].x;
				num3 = point.x - this._vertices[1].x;
				num4 = point.y - this._vertices[1].y;
				if (num * num3 + num2 * num4 > 0f)
				{
					return false;
				}
			}
			else
			{
				num = this._vertices[0].y - this._vertices[3].y;
				num2 = this._vertices[3].x - this._vertices[0].x;
				if (num * num3 + num2 * num4 > 0f)
				{
					return false;
				}
				num = this._vertices[3].y - this._vertices[2].y;
				num2 = this._vertices[2].x - this._vertices[3].x;
				num3 = point.x - this._vertices[3].x;
				num4 = point.y - this._vertices[3].y;
				if (num * num3 + num2 * num4 > 0f)
				{
					return false;
				}
			}
			return true;
		}

		public bool ContainsConvexQuadCCW(Vector2 point)
		{
			return this.ContainsConvexQuadCCW(ref point);
		}

		public bool ContainsConvexQuadCW(ref Vector2 point)
		{
			if (this._vertices.Length != 4)
			{
				return false;
			}
			float num = this._vertices[2].y - this._vertices[0].y;
			float num2 = this._vertices[0].x - this._vertices[2].x;
			float num3 = point.x - this._vertices[0].x;
			float num4 = point.y - this._vertices[0].y;
			if (num * num3 + num2 * num4 < 0f)
			{
				num = this._vertices[1].y - this._vertices[0].y;
				num2 = this._vertices[0].x - this._vertices[1].x;
				if (num * num3 + num2 * num4 < 0f)
				{
					return false;
				}
				num = this._vertices[2].y - this._vertices[1].y;
				num2 = this._vertices[1].x - this._vertices[2].x;
				num3 = point.x - this._vertices[1].x;
				num4 = point.y - this._vertices[1].y;
				if (num * num3 + num2 * num4 < 0f)
				{
					return false;
				}
			}
			else
			{
				num = this._vertices[0].y - this._vertices[3].y;
				num2 = this._vertices[3].x - this._vertices[0].x;
				if (num * num3 + num2 * num4 < 0f)
				{
					return false;
				}
				num = this._vertices[3].y - this._vertices[2].y;
				num2 = this._vertices[2].x - this._vertices[3].x;
				num3 = point.x - this._vertices[3].x;
				num4 = point.y - this._vertices[3].y;
				if (num * num3 + num2 * num4 < 0f)
				{
					return false;
				}
			}
			return true;
		}

		public bool ContainsConvexQuadCW(Vector2 point)
		{
			return this.ContainsConvexQuadCW(ref point);
		}

		public bool ContainsConvexCCW(ref Vector2 point)
		{
			return this.SubContainsPointCCW(ref point, 0, 0);
		}

		public bool ContainsConvexCCW(Vector2 point)
		{
			return this.ContainsConvexCCW(ref point);
		}

		private bool SubContainsPointCCW(ref Vector2 p, int i0, int i1)
		{
			int num = this._vertices.Length;
			int num2 = i1 - i0;
			float num3;
			float num4;
			float num5;
			float num6;
			if (num2 == 1 || (num2 < 0 && num2 + num == 1))
			{
				num3 = this._vertices[i1].y - this._vertices[i0].y;
				num4 = this._vertices[i0].x - this._vertices[i1].x;
				num5 = p.x - this._vertices[i0].x;
				num6 = p.y - this._vertices[i0].y;
				return num3 * num5 + num4 * num6 <= 0f;
			}
			int num7;
			if (i0 < i1)
			{
				num7 = i0 + i1 >> 1;
			}
			else
			{
				num7 = i0 + i1 + num >> 1;
				if (num7 >= num)
				{
					num7 -= num;
				}
			}
			num3 = this._vertices[num7].y - this._vertices[i0].y;
			num4 = this._vertices[i0].x - this._vertices[num7].x;
			num5 = p.x - this._vertices[i0].x;
			num6 = p.y - this._vertices[i0].y;
			if (num3 * num5 + num4 * num6 > 0f)
			{
				return this.SubContainsPointCCW(ref p, i0, num7);
			}
			return this.SubContainsPointCCW(ref p, num7, i1);
		}

		public bool ContainsConvexCW(ref Vector2 point)
		{
			return this.SubContainsPointCW(ref point, 0, 0);
		}

		public bool ContainsConvexCW(Vector2 point)
		{
			return this.ContainsConvexCW(ref point);
		}

		private bool SubContainsPointCW(ref Vector2 p, int i0, int i1)
		{
			int num = this._vertices.Length;
			int num2 = i1 - i0;
			float num3;
			float num4;
			float num5;
			float num6;
			if (num2 == 1 || (num2 < 0 && num2 + num == 1))
			{
				num3 = this._vertices[i1].y - this._vertices[i0].y;
				num4 = this._vertices[i0].x - this._vertices[i1].x;
				num5 = p.x - this._vertices[i0].x;
				num6 = p.y - this._vertices[i0].y;
				return num3 * num5 + num4 * num6 >= 0f;
			}
			int num7;
			if (i0 < i1)
			{
				num7 = i0 + i1 >> 1;
			}
			else
			{
				num7 = i0 + i1 + num >> 1;
				if (num7 >= num)
				{
					num7 -= num;
				}
			}
			num3 = this._vertices[num7].y - this._vertices[i0].y;
			num4 = this._vertices[i0].x - this._vertices[num7].x;
			num5 = p.x - this._vertices[i0].x;
			num6 = p.y - this._vertices[i0].y;
			if (num3 * num5 + num4 * num6 < 0f)
			{
				return this.SubContainsPointCW(ref p, i0, num7);
			}
			return this.SubContainsPointCW(ref p, num7, i1);
		}

		public bool ContainsSimple(ref Vector2 point)
		{
			bool flag = false;
			int num = this._vertices.Length;
			int i = 0;
			int num2 = num - 1;
			while (i < num)
			{
				Vector2 vector = this._vertices[i];
				Vector2 vector2 = this._vertices[num2];
				if (point.y < vector2.y)
				{
					if (vector.y <= point.y)
					{
						float num3 = (point.y - vector.y) * (vector2.x - vector.x);
						float num4 = (point.x - vector.x) * (vector2.y - vector.y);
						if (num3 > num4)
						{
							flag = !flag;
						}
					}
				}
				else if (point.y < vector.y)
				{
					float num3 = (point.y - vector.y) * (vector2.x - vector.x);
					float num4 = (point.x - vector.x) * (vector2.y - vector.y);
					if (num3 < num4)
					{
						flag = !flag;
					}
				}
				num2 = i;
				i++;
			}
			return flag;
		}

		public bool ContainsSimple(Vector2 point)
		{
			return this.ContainsSimple(ref point);
		}

		public Segment2[] ToSegmentArray()
		{
			Segment2[] array = new Segment2[this._edges.Length];
			int i = 0;
			int num = array.Length;
			while (i < num)
			{
				array[i] = new Segment2(this._edges[i].Point0, this._edges[i].Point1);
				i++;
			}
			return array;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[VertexCount: " + this._vertices.Length.ToString());
			int i = 0;
			int num = this._vertices.Length;
			while (i < num)
			{
				stringBuilder.Append(string.Format(" V{0}: {1}", i.ToString(), this._vertices[i].ToStringEx()));
				i++;
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}

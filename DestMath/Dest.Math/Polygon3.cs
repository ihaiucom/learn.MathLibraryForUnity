using System;
using System.Text;
using UnityEngine;

namespace Dest.Math
{
	public class Polygon3
	{
		private Vector3[] _vertices;

		private Edge3[] _edges;

		private Plane3 _plane;

		public Vector3[] Vertices
		{
			get
			{
				return this._vertices;
			}
		}

		public Edge3[] Edges
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

		public Vector3 this[int vertexIndex]
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

		public Plane3 Plane
		{
			get
			{
				return this._plane;
			}
			set
			{
				this._plane = value;
			}
		}

		private Polygon3()
		{
		}

		public Polygon3(Vector3[] vertices, Plane3 plane)
		{
			this._vertices = new Vector3[vertices.Length];
			this._edges = new Edge3[vertices.Length];
			Array.Copy(vertices, this._vertices, vertices.Length);
			this._plane = plane;
			this.UpdateEdges();
		}

		public Polygon3(int vertexCount, Plane3 plane)
		{
			this._vertices = new Vector3[vertexCount];
			this._edges = new Edge3[vertexCount];
			this._plane = plane;
		}

		public void SetVertexProjected(int vertexIndex, Vector3 vertex)
		{
			float d = this._plane.Normal.Dot(vertex) - this._plane.Constant;
			this._vertices[vertexIndex] = vertex - d * this._plane.Normal;
		}

		public void ProjectVertices()
		{
			int i = 0;
			int num = this._vertices.Length;
			while (i < num)
			{
				float d = this._plane.Normal.Dot(this._vertices[i]) - this._plane.Constant;
				this._vertices[i] -= d * this._plane.Normal;
				i++;
			}
		}

		public Edge3 GetEdge(int edgeIndex)
		{
			return this._edges[edgeIndex];
		}

		public void UpdateEdges()
		{
			int num = this._vertices.Length;
			int num2 = num - 1;
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = (this._edges[num2].Point1 = this._vertices[i]) - (this._edges[num2].Point0 = this._vertices[num2]);
				this._edges[num2].Length = Vector3ex.Normalize(ref vector, 1E-05f);
				this._edges[num2].Direction = vector;
				this._edges[num2].Normal = this._plane.Normal.Cross(vector);
				num2 = i;
			}
		}

		public void UpdateEdge(int edgeIndex)
		{
			Vector3 vector = (this._edges[edgeIndex].Point1 = this._vertices[(edgeIndex + 1) % this._vertices.Length]) - (this._edges[edgeIndex].Point0 = this._vertices[edgeIndex]);
			this._edges[edgeIndex].Length = Vector3ex.Normalize(ref vector, 1E-05f);
			this._edges[edgeIndex].Direction = vector;
			this._edges[edgeIndex].Normal = this._plane.Normal.Cross(vector);
		}

		public Vector3 CalcCenter()
		{
			Vector3 a = this._vertices[0];
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

		public bool HasZeroCorners(float threshold = 1E-05f)
		{
			int num = this._edges.Length;
			float num2 = 1f - threshold;
			int num3 = num - 1;
			for (int i = 0; i < num; i++)
			{
				Vector3 lhs = -this._edges[num3].Direction;
				Vector3 direction = this._edges[i].Direction;
				float num4 = Vector3.Dot(lhs, direction);
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
				Vector3 vector = this._vertices[i];
				int num3 = num - i;
				this._vertices[i] = this._vertices[num3];
				this._vertices[num3] = vector;
			}
			this.UpdateEdges();
		}

		public Segment3[] ToSegmentArray()
		{
			Segment3[] array = new Segment3[this._edges.Length];
			int i = 0;
			int num = array.Length;
			while (i < num)
			{
				array[i] = new Segment3(this._edges[i].Point0, this._edges[i].Point1);
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

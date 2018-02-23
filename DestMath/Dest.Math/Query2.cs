using System;
using UnityEngine;

namespace Dest.Math
{
	internal class Query2 : Query
	{
		private static float Zero;

		private Vector2[] _vertices;

		public Query2(Vector2[] vertices)
		{
			this._vertices = vertices;
		}

		public int ToLine(int i, int v0, int v1)
		{
			return this.ToLine(ref this._vertices[i], v0, v1);
		}

		public int ToLine(ref Vector2 test, int v0, int v1)
		{
			bool flag = Query.Sort(ref v0, ref v1);
			Vector2 vector = this._vertices[v0];
			Vector2 vector2 = this._vertices[v1];
			float x = test.x - vector.x;
			float y = test.y - vector.y;
			float x2 = vector2.x - vector.x;
			float y2 = vector2.y - vector.y;
			float num = this.Det2(x, y, x2, y2);
			if (!flag)
			{
				num = -num;
			}
			if (num > Query2.Zero)
			{
				return 1;
			}
			if (num >= Query2.Zero)
			{
				return 0;
			}
			return -1;
		}

		public int ToTriangle(int i, int v0, int v1, int v2)
		{
			return this.ToTriangle(ref this._vertices[i], v0, v1, v2);
		}

		public int ToTriangle(ref Vector2 test, int v0, int v1, int v2)
		{
			int num = this.ToLine(ref test, v1, v2);
			if (num > 0)
			{
				return 1;
			}
			int num2 = this.ToLine(ref test, v0, v2);
			if (num2 < 0)
			{
				return 1;
			}
			int num3 = this.ToLine(ref test, v0, v1);
			if (num3 > 0)
			{
				return 1;
			}
			if (num == 0 || num2 == 0 || num3 == 0)
			{
				return 0;
			}
			return -1;
		}

		public int ToCircumcircle(int i, int v0, int v1, int v2)
		{
			return this.ToCircumcircle(ref this._vertices[i], v0, v1, v2);
		}

		public int ToCircumcircle(ref Vector2 test, int v0, int v1, int v2)
		{
			bool flag = Query.Sort(ref v0, ref v1, ref v2);
			Vector2 vector = this._vertices[v0];
			Vector2 vector2 = this._vertices[v1];
			Vector2 vector3 = this._vertices[v2];
			float num = vector.x + test.x;
			float num2 = vector.x - test.x;
			float num3 = vector.y + test.y;
			float num4 = vector.y - test.y;
			float num5 = vector2.x + test.x;
			float num6 = vector2.x - test.x;
			float num7 = vector2.y + test.y;
			float num8 = vector2.y - test.y;
			float num9 = vector3.x + test.x;
			float num10 = vector3.x - test.x;
			float num11 = vector3.y + test.y;
			float num12 = vector3.y - test.y;
			float z = num * num2 + num3 * num4;
			float z2 = num5 * num6 + num7 * num8;
			float z3 = num9 * num10 + num11 * num12;
			float num13 = this.Det3(num2, num4, z, num6, num8, z2, num10, num12, z3);
			if (!flag)
			{
				num13 = -num13;
			}
			if (num13 < Query2.Zero)
			{
				return 1;
			}
			if (num13 <= Query2.Zero)
			{
				return 0;
			}
			return -1;
		}

		public float Dot(float x0, float y0, float x1, float y1)
		{
			return x0 * x1 + y0 * y1;
		}

		public float Det2(float x0, float y0, float x1, float y1)
		{
			return x0 * y1 - x1 * y0;
		}

		public float Det3(float x0, float y0, float z0, float x1, float y1, float z1, float x2, float y2, float z2)
		{
			float num = y1 * z2 - y2 * z1;
			float num2 = y2 * z0 - y0 * z2;
			float num3 = y0 * z1 - y1 * z0;
			return x0 * num + x1 * num2 + x2 * num3;
		}
	}
}

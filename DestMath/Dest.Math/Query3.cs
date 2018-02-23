using System;
using UnityEngine;

namespace Dest.Math
{
	internal class Query3 : Query
	{
		private static float Zero;

		private Vector3[] _vertices;

		public Query3(Vector3[] vertices)
		{
			this._vertices = vertices;
		}

		public int ToPlane(int i, int v0, int v1, int v2)
		{
			return this.ToPlane(ref this._vertices[i], v0, v1, v2);
		}

		public int ToPlane(ref Vector3 test, int v0, int v1, int v2)
		{
			bool flag = Query.Sort(ref v0, ref v1, ref v2);
			Vector3 vector = this._vertices[v0];
			Vector3 vector2 = this._vertices[v1];
			Vector3 vector3 = this._vertices[v2];
			float x = test.x - vector.x;
			float y = test.y - vector.y;
			float z = test.z - vector.z;
			float x2 = vector2.x - vector.x;
			float y2 = vector2.y - vector.y;
			float z2 = vector2.z - vector.z;
			float x3 = vector3.x - vector.x;
			float y3 = vector3.y - vector.y;
			float z3 = vector3.z - vector.z;
			float num = this.Det3(x, y, z, x2, y2, z2, x3, y3, z3);
			if (!flag)
			{
				num = -num;
			}
			if (num > Query3.Zero)
			{
				return 1;
			}
			if (num >= Query3.Zero)
			{
				return 0;
			}
			return -1;
		}

		public int ToTetrahedron(int i, int v0, int v1, int v2, int v3)
		{
			return this.ToTetrahedron(ref this._vertices[i], v0, v1, v2, v3);
		}

		public int ToTetrahedron(ref Vector3 test, int v0, int v1, int v2, int v3)
		{
			int num = this.ToPlane(ref test, v1, v2, v3);
			if (num > 0)
			{
				return 1;
			}
			int num2 = this.ToPlane(ref test, v0, v2, v3);
			if (num2 < 0)
			{
				return 1;
			}
			int num3 = this.ToPlane(ref test, v0, v1, v3);
			if (num3 > 0)
			{
				return 1;
			}
			int num4 = this.ToPlane(ref test, v0, v1, v2);
			if (num4 < 0)
			{
				return 1;
			}
			if (num == 0 || num2 == 0 || num3 == 0 || num4 == 0)
			{
				return 0;
			}
			return -1;
		}

		public int ToCircumsphere(int i, int v0, int v1, int v2, int v3)
		{
			return this.ToCircumsphere(ref this._vertices[i], v0, v1, v2, v3);
		}

		public int ToCircumsphere(ref Vector3 test, int v0, int v1, int v2, int v3)
		{
			bool flag = Query.Sort(ref v0, ref v1, ref v2, ref v3);
			Vector3 vector = this._vertices[v0];
			Vector3 vector2 = this._vertices[v1];
			Vector3 vector3 = this._vertices[v2];
			Vector3 vector4 = this._vertices[v3];
			float num = vector.x + test.x;
			float num2 = vector.x - test.x;
			float num3 = vector.y + test.y;
			float num4 = vector.y - test.y;
			float num5 = vector.z + test.z;
			float num6 = vector.z - test.z;
			float num7 = vector2.x + test.x;
			float num8 = vector2.x - test.x;
			float num9 = vector2.y + test.y;
			float num10 = vector2.y - test.y;
			float num11 = vector2.z + test.z;
			float num12 = vector2.z - test.z;
			float num13 = vector3.x + test.x;
			float num14 = vector3.x - test.x;
			float num15 = vector3.y + test.y;
			float num16 = vector3.y - test.y;
			float num17 = vector3.z + test.z;
			float num18 = vector3.z - test.z;
			float num19 = vector4.x + test.x;
			float num20 = vector4.x - test.x;
			float num21 = vector4.y + test.y;
			float num22 = vector4.y - test.y;
			float num23 = vector4.z + test.z;
			float num24 = vector4.z - test.z;
			float w = num * num2 + num3 * num4 + num5 * num6;
			float w2 = num7 * num8 + num9 * num10 + num11 * num12;
			float w3 = num13 * num14 + num15 * num16 + num17 * num18;
			float w4 = num19 * num20 + num21 * num22 + num23 * num24;
			float num25 = this.Det4(num2, num4, num6, w, num8, num10, num12, w2, num14, num16, num18, w3, num20, num22, num24, w4);
			if (!flag)
			{
				num25 = -num25;
			}
			if (num25 > Query3.Zero)
			{
				return 1;
			}
			if (num25 >= Query3.Zero)
			{
				return 0;
			}
			return -1;
		}

		public float Dot(float x0, float y0, float z0, float x1, float y1, float z1)
		{
			return x0 * x1 + y0 * y1 + z0 * z1;
		}

		public float Det3(float x0, float y0, float z0, float x1, float y1, float z1, float x2, float y2, float z2)
		{
			float num = y1 * z2 - y2 * z1;
			float num2 = y2 * z0 - y0 * z2;
			float num3 = y0 * z1 - y1 * z0;
			return x0 * num + x1 * num2 + x2 * num3;
		}

		public float Det4(float x0, float y0, float z0, float w0, float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float x3, float y3, float z3, float w3)
		{
			float num = x0 * y1 - x1 * y0;
			float num2 = x0 * y2 - x2 * y0;
			float num3 = x0 * y3 - x3 * y0;
			float num4 = x1 * y2 - x2 * y1;
			float num5 = x1 * y3 - x3 * y1;
			float num6 = x2 * y3 - x3 * y2;
			float num7 = z0 * w1 - z1 * w0;
			float num8 = z0 * w2 - z2 * w0;
			float num9 = z0 * w3 - z3 * w0;
			float num10 = z1 * w2 - z2 * w1;
			float num11 = z1 * w3 - z3 * w1;
			float num12 = z2 * w3 - z3 * w2;
			return num * num12 - num2 * num11 + num3 * num10 + num4 * num9 - num5 * num8 + num6 * num7;
		}
	}
}

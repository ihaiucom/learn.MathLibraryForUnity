using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Triangle3
	{
		public Vector3 V0;

		public Vector3 V1;

		public Vector3 V2;

		public Vector3 this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.V0;
				case 1:
					return this.V1;
				case 2:
					return this.V2;
				default:
					return Vector3.zero;
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.V0 = value;
					return;
				case 1:
					this.V1 = value;
					return;
				case 2:
					this.V2 = value;
					return;
				default:
					return;
				}
			}
		}

		public Triangle3(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2)
		{
			this.V0 = v0;
			this.V1 = v1;
			this.V2 = v2;
		}

		public Triangle3(Vector3 v0, Vector3 v1, Vector3 v2)
		{
			this.V0 = v0;
			this.V1 = v1;
			this.V2 = v2;
		}

		public Vector3 CalcEdge(int edgeIndex)
		{
			switch (edgeIndex)
			{
			case 0:
				return this.V1 - this.V0;
			case 1:
				return this.V2 - this.V1;
			case 2:
				return this.V0 - this.V2;
			default:
				return Vector2.zero;
			}
		}

		public Vector3 CalcNormal()
		{
			return Vector3.Cross(this.V1 - this.V0, this.V2 - this.V0);
		}

		public float CalcArea()
		{
			return 0.5f * Vector3.Cross(this.V1 - this.V0, this.V2 - this.V0).magnitude;
		}

		public static float CalcArea(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2)
		{
			return 0.5f * Vector3.Cross(v1 - v0, v2 - v0).magnitude;
		}

		public static float CalcArea(Vector3 v0, Vector3 v1, Vector3 v2)
		{
			return 0.5f * Vector3.Cross(v1 - v0, v2 - v0).magnitude;
		}

		public Vector3 CalcAnglesDeg()
		{
			float num = this.V2.x - this.V1.x;
			float num2 = this.V2.y - this.V1.y;
			float num3 = this.V2.z - this.V1.z;
			float num4 = num * num + num2 * num2 + num3 * num3;
			num = this.V2.x - this.V0.x;
			num2 = this.V2.y - this.V0.y;
			num3 = this.V2.z - this.V0.z;
			float num5 = num * num + num2 * num2 + num3 * num3;
			num = this.V1.x - this.V0.x;
			num2 = this.V1.y - this.V0.y;
			num3 = this.V1.z - this.V0.z;
			float num6 = num * num + num2 * num2 + num3 * num3;
			float num7 = 2f * Mathf.Sqrt(num6);
			Vector3 result;
			result.x = Mathf.Acos((num5 + num6 - num4) / (Mathf.Sqrt(num5) * num7)) * 57.29578f;
			result.y = Mathf.Acos((num4 + num6 - num5) / (Mathf.Sqrt(num4) * num7)) * 57.29578f;
			result.z = 180f - result.x - result.y;
			return result;
		}

		public static Vector3 CalcAnglesDeg(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2)
		{
			float num = v2.x - v1.x;
			float num2 = v2.y - v1.y;
			float num3 = v2.z - v1.z;
			float num4 = num * num + num2 * num2 + num3 * num3;
			num = v2.x - v0.x;
			num2 = v2.y - v0.y;
			num3 = v2.z - v0.z;
			float num5 = num * num + num2 * num2 + num3 * num3;
			num = v1.x - v0.x;
			num2 = v1.y - v0.y;
			num3 = v1.z - v0.z;
			float num6 = num * num + num2 * num2 + num3 * num3;
			float num7 = 2f * Mathf.Sqrt(num6);
			Vector3 result;
			result.x = Mathf.Acos((num5 + num6 - num4) / (Mathf.Sqrt(num5) * num7)) * 57.29578f;
			result.y = Mathf.Acos((num4 + num6 - num5) / (Mathf.Sqrt(num4) * num7)) * 57.29578f;
			result.z = 180f - result.x - result.y;
			return result;
		}

		public static Vector3 CalcAnglesDeg(Vector3 v0, Vector3 v1, Vector3 v2)
		{
			return Triangle3.CalcAnglesDeg(ref v0, ref v1, ref v2);
		}

		public Vector3 CalcAnglesRad()
		{
			float num = this.V2.x - this.V1.x;
			float num2 = this.V2.y - this.V1.y;
			float num3 = this.V2.z - this.V1.z;
			float num4 = num * num + num2 * num2 + num3 * num3;
			num = this.V2.x - this.V0.x;
			num2 = this.V2.y - this.V0.y;
			num3 = this.V2.z - this.V0.z;
			float num5 = num * num + num2 * num2 + num3 * num3;
			num = this.V1.x - this.V0.x;
			num2 = this.V1.y - this.V0.y;
			num3 = this.V1.z - this.V0.z;
			float num6 = num * num + num2 * num2 + num3 * num3;
			float num7 = 2f * Mathf.Sqrt(num6);
			Vector3 result;
			result.x = Mathf.Acos((num5 + num6 - num4) / (Mathf.Sqrt(num5) * num7));
			result.y = Mathf.Acos((num4 + num6 - num5) / (Mathf.Sqrt(num4) * num7));
			result.z = 3.14159274f - result.x - result.y;
			return result;
		}

		public static Vector3 CalcAnglesRad(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2)
		{
			float num = v2.x - v1.x;
			float num2 = v2.y - v1.y;
			float num3 = v2.z - v1.z;
			float num4 = num * num + num2 * num2 + num3 * num3;
			num = v2.x - v0.x;
			num2 = v2.y - v0.y;
			num3 = v2.z - v0.z;
			float num5 = num * num + num2 * num2 + num3 * num3;
			num = v1.x - v0.x;
			num2 = v1.y - v0.y;
			num3 = v1.z - v0.z;
			float num6 = num * num + num2 * num2 + num3 * num3;
			float num7 = 2f * Mathf.Sqrt(num6);
			Vector3 result;
			result.x = Mathf.Acos((num5 + num6 - num4) / (Mathf.Sqrt(num5) * num7));
			result.y = Mathf.Acos((num4 + num6 - num5) / (Mathf.Sqrt(num4) * num7));
			result.z = 3.14159274f - result.x - result.y;
			return result;
		}

		public static Vector3 CalcAnglesRad(Vector3 v0, Vector3 v1, Vector3 v2)
		{
			return Triangle3.CalcAnglesRad(ref v0, ref v1, ref v2);
		}

		public Vector3 EvalBarycentric(float c0, float c1)
		{
			float d = 1f - c0 - c1;
			return c0 * this.V0 + c1 * this.V1 + d * this.V2;
		}

		public Vector3 EvalBarycentric(ref Vector3 baryCoords)
		{
			return baryCoords.x * this.V0 + baryCoords.y * this.V1 + baryCoords.z * this.V2;
		}

		public Vector3 EvalBarycentric(Vector3 baryCoords)
		{
			return baryCoords.x * this.V0 + baryCoords.y * this.V1 + baryCoords.z * this.V2;
		}

		public static void CalcBarycentricCoords(ref Vector3 point, ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, out Vector3 baryCoords)
		{
			Vector3 vector = v1 - v0;
			Vector3 vector2 = v2 - v0;
			Vector3 vector3 = point - v0;
			float num = Vector3ex.Dot(ref vector, ref vector);
			float num2 = Vector3ex.Dot(ref vector, ref vector2);
			float num3 = Vector3ex.Dot(ref vector2, ref vector2);
			float num4 = Vector3ex.Dot(ref vector3, ref vector);
			float num5 = Vector3ex.Dot(ref vector3, ref vector2);
			float num6 = 1f / (num * num3 - num2 * num2);
			baryCoords.y = (num3 * num4 - num2 * num5) * num6;
			baryCoords.z = (num * num5 - num2 * num4) * num6;
			baryCoords.x = 1f - baryCoords.y - baryCoords.z;
		}

		public Vector3 CalcBarycentricCoords(ref Vector3 point)
		{
			Vector3 result;
			Triangle3.CalcBarycentricCoords(ref point, ref this.V0, ref this.V1, ref this.V2, out result);
			return result;
		}

		public Vector3 CalcBarycentricCoords(Vector3 point)
		{
			Vector3 result;
			Triangle3.CalcBarycentricCoords(ref point, ref this.V0, ref this.V1, ref this.V2, out result);
			return result;
		}

		public override string ToString()
		{
			return string.Format("[V0: {0} V1: {1} V2: {2}]", this.V0.ToStringEx(), this.V1.ToStringEx(), this.V2.ToStringEx());
		}
	}
}

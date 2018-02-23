using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Triangle2
	{
		public Vector2 V0;

		public Vector2 V1;

		public Vector2 V2;

		public Vector2 this[int index]
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
					return Vector2.zero;
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

		public Triangle2(ref Vector2 v0, ref Vector2 v1, ref Vector2 v2)
		{
			this.V0 = v0;
			this.V1 = v1;
			this.V2 = v2;
		}

		public Triangle2(Vector2 v0, Vector2 v1, Vector2 v2)
		{
			this.V0 = v0;
			this.V1 = v1;
			this.V2 = v2;
		}

		public Vector2 CalcEdge(int edgeIndex)
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

		public float CalcDeterminant()
		{
			return this.V1.x * this.V2.y + this.V0.x * this.V1.y + this.V2.x * this.V0.y - this.V1.x * this.V0.y - this.V2.x * this.V1.y - this.V0.x * this.V2.y;
		}

		public Orientations CalcOrientation(float threshold = 1E-05f)
		{
			float num = this.CalcDeterminant();
			if (num > threshold)
			{
				return Orientations.CCW;
			}
			if (num < -threshold)
			{
				return Orientations.CW;
			}
			return Orientations.None;
		}

		public float CalcArea()
		{
			return 0.5f * Mathf.Abs(this.CalcDeterminant());
		}

		public static float CalcArea(ref Vector2 v0, ref Vector2 v1, ref Vector2 v2)
		{
			return 0.5f * Mathf.Abs(v1.x * v2.y + v0.x * v1.y + v2.x * v0.y - v1.x * v0.y - v2.x * v1.y - v0.x * v2.y);
		}

		public static float CalcArea(Vector2 v0, Vector2 v1, Vector2 v2)
		{
			return 0.5f * Mathf.Abs(v1.x * v2.y + v0.x * v1.y + v2.x * v0.y - v1.x * v0.y - v2.x * v1.y - v0.x * v2.y);
		}

		public Vector3 CalcAnglesDeg()
		{
			float num = this.V2.x - this.V1.x;
			float num2 = this.V2.y - this.V1.y;
			float num3 = num * num + num2 * num2;
			num = this.V2.x - this.V0.x;
			num2 = this.V2.y - this.V0.y;
			float num4 = num * num + num2 * num2;
			num = this.V1.x - this.V0.x;
			num2 = this.V1.y - this.V0.y;
			float num5 = num * num + num2 * num2;
			float num6 = 2f * Mathf.Sqrt(num5);
			Vector3 result;
			result.x = Mathf.Acos((num4 + num5 - num3) / (Mathf.Sqrt(num4) * num6)) * 57.29578f;
			result.y = Mathf.Acos((num3 + num5 - num4) / (Mathf.Sqrt(num3) * num6)) * 57.29578f;
			result.z = 180f - result.x - result.y;
			return result;
		}

		public static Vector3 CalcAnglesDeg(ref Vector2 v0, ref Vector2 v1, ref Vector2 v2)
		{
			float num = v2.x - v1.x;
			float num2 = v2.y - v1.y;
			float num3 = num * num + num2 * num2;
			num = v2.x - v0.x;
			num2 = v2.y - v0.y;
			float num4 = num * num + num2 * num2;
			num = v1.x - v0.x;
			num2 = v1.y - v0.y;
			float num5 = num * num + num2 * num2;
			float num6 = 2f * Mathf.Sqrt(num5);
			Vector3 result;
			result.x = Mathf.Acos((num4 + num5 - num3) / (Mathf.Sqrt(num4) * num6)) * 57.29578f;
			result.y = Mathf.Acos((num3 + num5 - num4) / (Mathf.Sqrt(num3) * num6)) * 57.29578f;
			result.z = 180f - result.x - result.y;
			return result;
		}

		public static Vector3 CalcAnglesDeg(Vector2 v0, Vector2 v1, Vector2 v2)
		{
			return Triangle2.CalcAnglesDeg(ref v0, ref v1, ref v2);
		}

		public Vector3 CalcAnglesRad()
		{
			float num = this.V2.x - this.V1.x;
			float num2 = this.V2.y - this.V1.y;
			float num3 = num * num + num2 * num2;
			num = this.V2.x - this.V0.x;
			num2 = this.V2.y - this.V0.y;
			float num4 = num * num + num2 * num2;
			num = this.V1.x - this.V0.x;
			num2 = this.V1.y - this.V0.y;
			float num5 = num * num + num2 * num2;
			float num6 = 2f * Mathf.Sqrt(num5);
			Vector3 result;
			result.x = Mathf.Acos((num4 + num5 - num3) / (Mathf.Sqrt(num4) * num6));
			result.y = Mathf.Acos((num3 + num5 - num4) / (Mathf.Sqrt(num3) * num6));
			result.z = 3.14159274f - result.x - result.y;
			return result;
		}

		public static Vector3 CalcAnglesRad(ref Vector2 v0, ref Vector2 v1, ref Vector2 v2)
		{
			float num = v2.x - v1.x;
			float num2 = v2.y - v1.y;
			float num3 = num * num + num2 * num2;
			num = v2.x - v0.x;
			num2 = v2.y - v0.y;
			float num4 = num * num + num2 * num2;
			num = v1.x - v0.x;
			num2 = v1.y - v0.y;
			float num5 = num * num + num2 * num2;
			float num6 = 2f * Mathf.Sqrt(num5);
			Vector3 result;
			result.x = Mathf.Acos((num4 + num5 - num3) / (Mathf.Sqrt(num4) * num6));
			result.y = Mathf.Acos((num3 + num5 - num4) / (Mathf.Sqrt(num3) * num6));
			result.z = 3.14159274f - result.x - result.y;
			return result;
		}

		public static Vector3 CalcAnglesRad(Vector2 v0, Vector2 v1, Vector2 v2)
		{
			return Triangle2.CalcAnglesRad(ref v0, ref v1, ref v2);
		}

		public Vector2 EvalBarycentric(float c0, float c1)
		{
			float d = 1f - c0 - c1;
			return c0 * this.V0 + c1 * this.V1 + d * this.V2;
		}

		public Vector2 EvalBarycentric(ref Vector3 baryCoords)
		{
			return baryCoords.x * this.V0 + baryCoords.y * this.V1 + baryCoords.z * this.V2;
		}

		public Vector2 EvalBarycentric(Vector3 baryCoords)
		{
			return baryCoords.x * this.V0 + baryCoords.y * this.V1 + baryCoords.z * this.V2;
		}

		public static void CalcBarycentricCoords(ref Vector2 point, ref Vector2 v0, ref Vector2 v1, ref Vector2 v2, out Vector3 baryCoords)
		{
			Vector2 vector = v1 - v0;
			Vector2 vector2 = v2 - v0;
			Vector2 vector3 = point - v0;
			float num = Vector2ex.Dot(ref vector, ref vector);
			float num2 = Vector2ex.Dot(ref vector, ref vector2);
			float num3 = Vector2ex.Dot(ref vector2, ref vector2);
			float num4 = Vector2ex.Dot(ref vector3, ref vector);
			float num5 = Vector2ex.Dot(ref vector3, ref vector2);
			float num6 = 1f / (num * num3 - num2 * num2);
			baryCoords.y = (num3 * num4 - num2 * num5) * num6;
			baryCoords.z = (num * num5 - num2 * num4) * num6;
			baryCoords.x = 1f - baryCoords.y - baryCoords.z;
		}

		public Vector3 CalcBarycentricCoords(ref Vector2 point)
		{
			Vector3 result;
			Triangle2.CalcBarycentricCoords(ref point, ref this.V0, ref this.V1, ref this.V2, out result);
			return result;
		}

		public Vector3 CalcBarycentricCoords(Vector2 point)
		{
			Vector3 result;
			Triangle2.CalcBarycentricCoords(ref point, ref this.V0, ref this.V1, ref this.V2, out result);
			return result;
		}

		public float DistanceTo(Vector2 point)
		{
			return Distance.Point2Triangle2(ref point, ref this);
		}

		public int QuerySideCCW(Vector2 point, float epsilon = 1E-05f)
		{
			float num = (point.x - this.V0.x) * (this.V1.y - this.V0.y) - (point.y - this.V0.y) * (this.V1.x - this.V0.x);
			if (num > epsilon)
			{
				return 1;
			}
			float num2 = (point.x - this.V1.x) * (this.V2.y - this.V1.y) - (point.y - this.V1.y) * (this.V2.x - this.V1.x);
			if (num2 > epsilon)
			{
				return 1;
			}
			float num3 = (point.x - this.V2.x) * (this.V0.y - this.V2.y) - (point.y - this.V2.y) * (this.V0.x - this.V2.x);
			if (num3 > epsilon)
			{
				return 1;
			}
			float num4 = -epsilon;
			if (num >= num4 || num2 >= num4 || num3 >= num4)
			{
				return 0;
			}
			return -1;
		}

		public int QuerySideCW(Vector2 point, float epsilon = 1E-05f)
		{
			float num = -epsilon;
			float num2 = (point.x - this.V0.x) * (this.V1.y - this.V0.y) - (point.y - this.V0.y) * (this.V1.x - this.V0.x);
			if (num2 < num)
			{
				return 1;
			}
			float num3 = (point.x - this.V1.x) * (this.V2.y - this.V1.y) - (point.y - this.V1.y) * (this.V2.x - this.V1.x);
			if (num3 < num)
			{
				return 1;
			}
			float num4 = (point.x - this.V2.x) * (this.V0.y - this.V2.y) - (point.y - this.V2.y) * (this.V0.x - this.V2.x);
			if (num4 < num)
			{
				return 1;
			}
			if (num2 <= epsilon || num3 <= epsilon || num4 <= epsilon)
			{
				return 0;
			}
			return -1;
		}

		public Vector2 Project(Vector2 point)
		{
			Vector2 result;
			Distance.SqrPoint2Triangle2(ref point, ref this, out result);
			return result;
		}

		public bool Contains(ref Vector2 point)
		{
			bool flag = (point.x - this.V1.x) * (this.V0.y - this.V1.y) - (point.y - this.V1.y) * (this.V0.x - this.V1.x) < 0f;
			bool flag2 = (point.x - this.V2.x) * (this.V1.y - this.V2.y) - (point.y - this.V2.y) * (this.V1.x - this.V2.x) < 0f;
			if (flag != flag2)
			{
				return false;
			}
			bool flag3 = (point.x - this.V0.x) * (this.V2.y - this.V0.y) - (point.y - this.V0.y) * (this.V2.x - this.V0.x) < 0f;
			return flag2 == flag3;
		}

		public bool Contains(Vector2 point)
		{
			return this.Contains(ref point);
		}

		public bool ContainsCCW(ref Vector2 point)
		{
			return (point.x - this.V0.x) * (this.V1.y - this.V0.y) - (point.y - this.V0.y) * (this.V1.x - this.V0.x) <= 0f && (point.x - this.V1.x) * (this.V2.y - this.V1.y) - (point.y - this.V1.y) * (this.V2.x - this.V1.x) <= 0f && (point.x - this.V2.x) * (this.V0.y - this.V2.y) - (point.y - this.V2.y) * (this.V0.x - this.V2.x) <= 0f;
		}

		public bool ContainsCCW(Vector2 point)
		{
			return this.ContainsCCW(ref point);
		}

		public bool ContainsCW(ref Vector2 point)
		{
			return (point.x - this.V0.x) * (this.V1.y - this.V0.y) - (point.y - this.V0.y) * (this.V1.x - this.V0.x) >= 0f && (point.x - this.V1.x) * (this.V2.y - this.V1.y) - (point.y - this.V1.y) * (this.V2.x - this.V1.x) >= 0f && (point.x - this.V2.x) * (this.V0.y - this.V2.y) - (point.y - this.V2.y) * (this.V0.x - this.V2.x) >= 0f;
		}

		public bool ContainsCW(Vector2 point)
		{
			return this.ContainsCW(ref point);
		}

		public override string ToString()
		{
			return string.Format("[V0: {0} V1: {1} V2: {2}]", this.V0.ToStringEx(), this.V1.ToStringEx(), this.V2.ToStringEx());
		}
	}
}

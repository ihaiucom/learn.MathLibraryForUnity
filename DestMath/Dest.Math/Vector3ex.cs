using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dest.Math
{
	public static class Vector3ex
	{
		internal class Information
		{
			public int Dimension;

			public Vector3 Min;

			public Vector3 Max;

			public float MaxRange;

			public Vector3 Origin;

			public Vector3[] Direction = new Vector3[3];

			public int[] Extreme = new int[4];

			public bool ExtremeCCW;
		}

		public static readonly Vector3 Zero = new Vector3(0f, 0f, 0f);

		public static readonly Vector3 One = new Vector3(1f, 1f, 1f);

		public static readonly Vector3 UnitX = new Vector3(1f, 0f, 0f);

		public static readonly Vector3 UnitY = new Vector3(0f, 1f, 0f);

		public static readonly Vector3 UnitZ = new Vector3(0f, 0f, 1f);

		public static readonly Vector3 PositiveInfinity = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

		public static readonly Vector3 NegativeInfinity = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

		internal static Vector3ex.Information GetInformation(IList<Vector3> points, float epsilon)
		{
			if (points == null)
			{
				return null;
			}
			int count = points.Count;
			if (count == 0 || epsilon < 0f)
			{
				return null;
			}
			Vector3ex.Information information = new Vector3ex.Information();
			information.ExtremeCCW = false;
			float num2;
			float num = num2 = points[0].x;
			int num3 = 0;
			int num4 = 0;
			float num6;
			float num5 = num6 = points[0].y;
			int num7 = 0;
			int num8 = 0;
			float num10;
			float num9 = num10 = points[0].z;
			int num11 = 0;
			int num12 = 0;
			for (int i = 1; i < count; i++)
			{
				float num13 = points[i].x;
				if (num13 < num2)
				{
					num2 = num13;
					num3 = i;
				}
				else if (num13 > num)
				{
					num = num13;
					num4 = i;
				}
				num13 = points[i].y;
				if (num13 < num6)
				{
					num6 = num13;
					num7 = i;
				}
				else if (num13 > num5)
				{
					num5 = num13;
					num8 = i;
				}
				num13 = points[i].z;
				if (num13 < num10)
				{
					num10 = num13;
					num11 = i;
				}
				else if (num13 > num9)
				{
					num9 = num13;
					num12 = i;
				}
			}
			information.Min.x = num2;
			information.Min.y = num6;
			information.Min.z = num10;
			information.Max.x = num;
			information.Max.y = num5;
			information.Max.z = num9;
			information.MaxRange = num - num2;
			information.Extreme[0] = num3;
			information.Extreme[1] = num4;
			float num14 = num5 - num6;
			if (num14 > information.MaxRange)
			{
				information.MaxRange = num14;
				information.Extreme[0] = num7;
				information.Extreme[1] = num8;
			}
			num14 = num9 - num10;
			if (num14 > information.MaxRange)
			{
				information.MaxRange = num14;
				information.Extreme[0] = num11;
				information.Extreme[1] = num12;
			}
			information.Origin = points[information.Extreme[0]];
			if (information.MaxRange < epsilon)
			{
				information.Dimension = 0;
				information.Extreme[1] = information.Extreme[0];
				information.Extreme[2] = information.Extreme[0];
				information.Extreme[3] = information.Extreme[0];
				information.Direction[0] = Vector3ex.Zero;
				information.Direction[1] = Vector3ex.Zero;
				information.Direction[2] = Vector3ex.Zero;
				return information;
			}
			information.Direction[0] = points[information.Extreme[1]] - information.Origin;
			information.Direction[0].Normalize();
			float num15 = 0f;
			information.Extreme[2] = information.Extreme[0];
			float d;
			for (int j = 0; j < count; j++)
			{
				Vector3 vector = points[j] - information.Origin;
				d = information.Direction[0].Dot(vector);
				float num16 = (vector - d * information.Direction[0]).magnitude;
				if (num16 > num15)
				{
					num15 = num16;
					information.Extreme[2] = j;
				}
			}
			if (num15 < epsilon * information.MaxRange)
			{
				information.Dimension = 1;
				information.Extreme[2] = information.Extreme[1];
				information.Extreme[3] = information.Extreme[1];
				return information;
			}
			information.Direction[1] = points[information.Extreme[2]] - information.Origin;
			d = information.Direction[0].Dot(information.Direction[1]);
			information.Direction[1] -= d * information.Direction[0];
			information.Direction[1].Normalize();
			information.Direction[2] = information.Direction[0].Cross(information.Direction[1]);
			num15 = 0f;
			float num17 = 0f;
			information.Extreme[3] = information.Extreme[0];
			for (int k = 0; k < count; k++)
			{
				Vector3 value = points[k] - information.Origin;
				float num16 = information.Direction[2].Dot(value);
				float num18 = Mathf.Sign(num16);
				num16 = Mathf.Abs(num16);
				if (num16 > num15)
				{
					num15 = num16;
					num17 = num18;
					information.Extreme[3] = k;
				}
			}
			if (num15 < epsilon * information.MaxRange)
			{
				information.Dimension = 2;
				information.Extreme[3] = information.Extreme[2];
				return information;
			}
			information.Dimension = 3;
			information.ExtremeCCW = (num17 > 0f);
			return information;
		}

		public static float Length(this Vector3 vector)
		{
			return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
		}

		public static float LengthSqr(this Vector3 vector)
		{
			return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
		}

		public static float Dot(this Vector3 vector, Vector3 value)
		{
			return vector.x * value.x + vector.y * value.y + vector.z * value.z;
		}

		public static float Dot(this Vector3 vector, ref Vector3 value)
		{
			return vector.x * value.x + vector.y * value.y + vector.z * value.z;
		}

		public static float Dot(ref Vector3 vector, ref Vector3 value)
		{
			return vector.x * value.x + vector.y * value.y + vector.z * value.z;
		}

		public static float AngleDeg(this Vector3 vector, Vector3 target)
		{
			Vector3ex.Normalize(ref vector, 1E-05f);
			Vector3ex.Normalize(ref target, 1E-05f);
			float num = vector.x * target.x + vector.y * target.y + vector.z * target.z;
			if (num > 1f)
			{
				num = 1f;
			}
			else if (num < -1f)
			{
				num = -1f;
			}
			return Mathf.Acos(num) * 57.29578f;
		}

		public static float AngleRad(this Vector3 vector, Vector3 target)
		{
			Vector3ex.Normalize(ref vector, 1E-05f);
			Vector3ex.Normalize(ref target, 1E-05f);
			float num = vector.x * target.x + vector.y * target.y + vector.z * target.z;
			if (num > 1f)
			{
				num = 1f;
			}
			else if (num < -1f)
			{
				num = -1f;
			}
			return Mathf.Acos(num);
		}

		public static float SignedAngleDeg(this Vector3 vector, Vector3 target, Vector3 normal)
		{
			Vector3ex.Normalize(ref vector, 1E-05f);
			Vector3ex.Normalize(ref target, 1E-05f);
			float num = vector.x * target.x + vector.y * target.y + vector.z * target.z;
			if (num > 1f)
			{
				num = 1f;
			}
			else if (num < -1f)
			{
				num = -1f;
			}
			float num2 = Mathf.Acos(num) * 57.29578f;
			Vector3 value = vector.Cross(target);
			if (normal.Dot(value) < 0f)
			{
				num2 = -num2;
			}
			return num2;
		}

		public static float SignedAngleRad(this Vector3 vector, Vector3 target, Vector3 normal)
		{
			Vector3ex.Normalize(ref vector, 1E-05f);
			Vector3ex.Normalize(ref target, 1E-05f);
			float num = vector.x * target.x + vector.y * target.y + vector.z * target.z;
			if (num > 1f)
			{
				num = 1f;
			}
			else if (num < -1f)
			{
				num = -1f;
			}
			float num2 = Mathf.Acos(num);
			Vector3 value = vector.Cross(target);
			if (normal.Dot(value) < 0f)
			{
				num2 = -num2;
			}
			return num2;
		}

		public static Vector3 Cross(this Vector3 vector, Vector3 value)
		{
			return new Vector3(vector.y * value.z - vector.z * value.y, vector.z * value.x - vector.x * value.z, vector.x * value.y - vector.y * value.x);
		}

		public static Vector3 Cross(this Vector3 vector, ref Vector3 value)
		{
			return new Vector3(vector.y * value.z - vector.z * value.y, vector.z * value.x - vector.x * value.z, vector.x * value.y - vector.y * value.x);
		}

		public static Vector3 Cross(ref Vector3 vector, ref Vector3 value)
		{
			return new Vector3(vector.y * value.z - vector.z * value.y, vector.z * value.x - vector.x * value.z, vector.x * value.y - vector.y * value.x);
		}

		public static Vector3 UnitCross(this Vector3 vector, Vector3 value)
		{
			Vector3 result = new Vector3(vector.y * value.z - vector.z * value.y, vector.z * value.x - vector.x * value.z, vector.x * value.y - vector.y * value.x);
			Vector3ex.Normalize(ref result, 1E-05f);
			return result;
		}

		public static Vector3 UnitCross(this Vector3 vector, ref Vector3 value)
		{
			Vector3 result = new Vector3(vector.y * value.z - vector.z * value.y, vector.z * value.x - vector.x * value.z, vector.x * value.y - vector.y * value.x);
			Vector3ex.Normalize(ref result, 1E-05f);
			return result;
		}

		public static Vector3 UnitCross(ref Vector3 vector, ref Vector3 value)
		{
			Vector3 result = new Vector3(vector.y * value.z - vector.z * value.y, vector.z * value.x - vector.x * value.z, vector.x * value.y - vector.y * value.x);
			Vector3ex.Normalize(ref result, 1E-05f);
			return result;
		}

		public static float Normalize(ref Vector3 vector, float epsilon = 1E-05f)
		{
			float num = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
			if (num >= epsilon)
			{
				float num2 = 1f / num;
				vector.x *= num2;
				vector.y *= num2;
				vector.z *= num2;
				return num;
			}
			vector.x = 0f;
			vector.y = 0f;
			vector.z = 0f;
			return 0f;
		}

		public static float SetLength(ref Vector3 vector, float lengthValue, float epsilon = 1E-05f)
		{
			float num = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
			if (num >= epsilon)
			{
				float num2 = lengthValue / num;
				vector.x *= num2;
				vector.y *= num2;
				vector.z *= num2;
				return lengthValue;
			}
			vector.x = 0f;
			vector.y = 0f;
			vector.z = 0f;
			return 0f;
		}

		public static float GrowLength(ref Vector3 vector, float lengthDelta, float epsilon = 1E-05f)
		{
			float num = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
			if (num >= epsilon)
			{
				float num2 = num + lengthDelta;
				float num3 = num2 / num;
				vector.x *= num3;
				vector.y *= num3;
				vector.z *= num3;
				return num2;
			}
			vector.x = 0f;
			vector.y = 0f;
			vector.z = 0f;
			return 0f;
		}

		public static Vector3 Replicate(float value)
		{
			return new Vector3(value, value, value);
		}

		public static void CreateOrthonormalBasis(out Vector3 u, out Vector3 v, ref Vector3 w)
		{
			if (Mathf.Abs(w.x) >= Mathf.Abs(w.y))
			{
				float num = Mathfex.InvSqrt(w.x * w.x + w.z * w.z);
				u.x = -w.z * num;
				u.y = 0f;
				u.z = w.x * num;
				v.x = w.y * u.z;
				v.y = w.z * u.x - w.x * u.z;
				v.z = -w.y * u.x;
				return;
			}
			float num2 = Mathfex.InvSqrt(w.y * w.y + w.z * w.z);
			u.x = 0f;
			u.y = w.z * num2;
			u.z = -w.y * num2;
			v.x = w.y * u.z - w.z * u.y;
			v.y = -w.x * u.z;
			v.z = w.x * u.y;
		}

		public static bool SameDirection(Vector3 value0, Vector3 value1)
		{
			return value0.Dot(value1) > 1E-05f;
		}

		public static Vector2 ToVector2XY(this Vector3 vector)
		{
			return new Vector2(vector.x, vector.y);
		}

		public static Vector2 ToVector2XZ(this Vector3 vector)
		{
			return new Vector2(vector.x, vector.z);
		}

		public static Vector2 ToVector2YZ(this Vector3 vector)
		{
			return new Vector2(vector.y, vector.z);
		}

		public static Vector2 ToVector2(this Vector3 vector, ProjectionPlanes projectionPlane)
		{
			if (projectionPlane == ProjectionPlanes.XY)
			{
				return new Vector2(vector.x, vector.y);
			}
			if (projectionPlane == ProjectionPlanes.XZ)
			{
				return new Vector2(vector.x, vector.z);
			}
			return new Vector2(vector.y, vector.z);
		}

		public static ProjectionPlanes GetProjectionPlane(this Vector3 vector)
		{
			ProjectionPlanes result = ProjectionPlanes.YZ;
			float num = Mathf.Abs(vector.x);
			float num2 = Mathf.Abs(vector.y);
			if (num2 > num)
			{
				result = ProjectionPlanes.XZ;
				num = num2;
			}
			num2 = Mathf.Abs(vector.z);
			if (num2 > num)
			{
				result = ProjectionPlanes.XY;
			}
			return result;
		}

		public static string ToStringEx(this Vector3 vector)
		{
			return string.Format("({0}, {1}, {2})", vector.x.ToString(), vector.y.ToString(), vector.z.ToString());
		}
	}
}

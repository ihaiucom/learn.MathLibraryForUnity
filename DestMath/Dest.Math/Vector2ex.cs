using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dest.Math
{
	public static class Vector2ex
	{
		internal class Information
		{
			public int Dimension;

			public Vector2 Min;

			public Vector2 Max;

			public float MaxRange;

			public Vector2 Origin;

			public Vector2[] Direction = new Vector2[2];

			public int[] Extreme = new int[3];

			public bool ExtremeCCW;
		}

		public static readonly Vector2 Zero = new Vector2(0f, 0f);

		public static readonly Vector2 One = new Vector2(1f, 1f);

		public static readonly Vector2 UnitX = new Vector2(1f, 0f);

		public static readonly Vector2 UnitY = new Vector2(0f, 1f);

		public static readonly Vector2 PositiveInfinity = new Vector2(float.PositiveInfinity, float.PositiveInfinity);

		public static readonly Vector2 NegativeInfinity = new Vector2(float.NegativeInfinity, float.NegativeInfinity);

		internal static Vector2ex.Information GetInformation(IList<Vector2> points, float epsilon)
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
			Vector2ex.Information information = new Vector2ex.Information();
			information.ExtremeCCW = false;
			float num2;
			float num = num2 = points[0].x;
			int num3 = 0;
			int num4 = 0;
			float num6;
			float num5 = num6 = points[0].y;
			int num7 = 0;
			int num8 = 0;
			for (int i = 1; i < count; i++)
			{
				float num9 = points[i].x;
				if (num9 < num2)
				{
					num2 = num9;
					num3 = i;
				}
				else if (num9 > num)
				{
					num = num9;
					num4 = i;
				}
				num9 = points[i].y;
				if (num9 < num6)
				{
					num6 = num9;
					num7 = i;
				}
				else if (num9 > num5)
				{
					num5 = num9;
					num8 = i;
				}
			}
			information.Min.x = num2;
			information.Min.y = num6;
			information.Max.x = num;
			information.Max.y = num5;
			information.MaxRange = num - num2;
			information.Extreme[0] = num3;
			information.Extreme[1] = num4;
			float num10 = num5 - num6;
			if (num10 > information.MaxRange)
			{
				information.MaxRange = num10;
				information.Extreme[0] = num7;
				information.Extreme[1] = num8;
			}
			information.Origin = points[information.Extreme[0]];
			if (information.MaxRange < epsilon)
			{
				information.Dimension = 0;
				information.Extreme[1] = information.Extreme[0];
				information.Extreme[2] = information.Extreme[0];
				information.Direction[0] = Vector2ex.Zero;
				information.Direction[1] = Vector2ex.Zero;
				return information;
			}
			information.Direction[0] = points[information.Extreme[1]] - information.Origin;
			information.Direction[0].Normalize();
			information.Direction[1] = -information.Direction[0].Perp();
			float num11 = 0f;
			float num12 = 0f;
			information.Extreme[2] = information.Extreme[0];
			for (int j = 0; j < count; j++)
			{
				Vector2 value = points[j] - information.Origin;
				float num13 = information.Direction[1].Dot(value);
				float num14 = Mathf.Sign(num13);
				num13 = Mathf.Abs(num13);
				if (num13 > num11)
				{
					num11 = num13;
					num12 = num14;
					information.Extreme[2] = j;
				}
			}
			if (num11 < epsilon * information.MaxRange)
			{
				information.Dimension = 1;
				information.Extreme[2] = information.Extreme[1];
				return information;
			}
			information.Dimension = 2;
			information.ExtremeCCW = (num12 > 0f);
			return information;
		}

		public static float Length(this Vector2 vector)
		{
			return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
		}

		public static float LengthSqr(this Vector2 vector)
		{
			return vector.x * vector.x + vector.y * vector.y;
		}

		public static float DotPerp(this Vector2 vector, Vector2 value)
		{
			return vector.x * value.y - vector.y * value.x;
		}

		public static float DotPerp(this Vector2 vector, ref Vector2 value)
		{
			return vector.x * value.y - vector.y * value.x;
		}

		public static float DotPerp(ref Vector2 vector, ref Vector2 value)
		{
			return vector.x * value.y - vector.y * value.x;
		}

		public static float Dot(this Vector2 vector, Vector2 value)
		{
			return vector.x * value.x + vector.y * value.y;
		}

		public static float Dot(this Vector2 vector, ref Vector2 value)
		{
			return vector.x * value.x + vector.y * value.y;
		}

		public static float Dot(ref Vector2 vector, ref Vector2 value)
		{
			return vector.x * value.x + vector.y * value.y;
		}

		public static Vector2 Perp(this Vector2 vector)
		{
			return new Vector2(vector.y, -vector.x);
		}

		public static float AngleDeg(this Vector2 vector, Vector2 target)
		{
			Vector2ex.Normalize(ref vector, 1E-05f);
			Vector2ex.Normalize(ref target, 1E-05f);
			float num = vector.x * target.x + vector.y * target.y;
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

		public static float AngleRad(this Vector2 vector, Vector2 target)
		{
			Vector2ex.Normalize(ref vector, 1E-05f);
			Vector2ex.Normalize(ref target, 1E-05f);
			float num = vector.x * target.x + vector.y * target.y;
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

		public static float Normalize(ref Vector2 vector, float epsilon = 1E-05f)
		{
			float num = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
			if (num >= epsilon)
			{
				float num2 = 1f / num;
				vector.x *= num2;
				vector.y *= num2;
				return num;
			}
			vector.x = 0f;
			vector.y = 0f;
			return 0f;
		}

		public static float SetLength(ref Vector2 vector, float lengthValue, float epsilon = 1E-05f)
		{
			float num = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
			if (num >= epsilon)
			{
				float num2 = lengthValue / num;
				vector.x *= num2;
				vector.y *= num2;
				return lengthValue;
			}
			vector.x = 0f;
			vector.y = 0f;
			return 0f;
		}

		public static float GrowLength(ref Vector2 vector, float lengthDelta, float epsilon = 1E-05f)
		{
			float num = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
			if (num >= epsilon)
			{
				float num2 = num + lengthDelta;
				float num3 = num2 / num;
				vector.x *= num3;
				vector.y *= num3;
				return num2;
			}
			vector.x = 0f;
			vector.y = 0f;
			return 0f;
		}

		public static Vector2 Replicate(float value)
		{
			return new Vector2(value, value);
		}

		public static Vector3 ToVector3XY(this Vector2 vector)
		{
			return new Vector3(vector.x, vector.y, 0f);
		}

		public static Vector3 ToVector3XZ(this Vector2 vector)
		{
			return new Vector3(vector.x, 0f, vector.y);
		}

		public static Vector3 ToVector3YZ(this Vector2 vector)
		{
			return new Vector3(0f, vector.x, vector.y);
		}

		public static string ToStringEx(this Vector2 vector)
		{
			return string.Format("({0}, {1})", vector.x.ToString(), vector.y.ToString());
		}
	}
}

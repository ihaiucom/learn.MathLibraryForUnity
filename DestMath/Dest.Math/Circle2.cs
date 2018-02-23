using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dest.Math
{
	public struct Circle2
	{
		public Vector2 Center;

		public float Radius;

		public Circle2(ref Vector2 center, float radius)
		{
			this.Center = center;
			this.Radius = radius;
		}

		public Circle2(Vector2 center, float radius)
		{
			this.Center = center;
			this.Radius = radius;
		}

		public static Circle2 CreateFromPointsAAB(IEnumerable<Vector2> points)
		{
			IEnumerator<Vector2> enumerator = points.GetEnumerator();
			enumerator.Reset();
			if (!enumerator.MoveNext())
			{
				return default(Circle2);
			}
			Vector2 center;
			Vector2 vector;
			AAB2.CreateFromPoints(points).CalcCenterExtents(out center, out vector);
			Circle2 result;
			result.Center = center;
			result.Radius = vector.magnitude;
			return result;
		}

		public static Circle2 CreateFromPointsAAB(IList<Vector2> points)
		{
			if (points.Count == 0)
			{
				return default(Circle2);
			}
			Vector2 center;
			Vector2 vector;
			AAB2.CreateFromPoints(points).CalcCenterExtents(out center, out vector);
			Circle2 result;
			result.Center = center;
			result.Radius = vector.magnitude;
			return result;
		}

		public static Circle2 CreateFromPointsAverage(IEnumerable<Vector2> points)
		{
			IEnumerator<Vector2> enumerator = points.GetEnumerator();
			enumerator.Reset();
			if (!enumerator.MoveNext())
			{
				return default(Circle2);
			}
			Vector2 vector = enumerator.Current;
			int num = 1;
			while (enumerator.MoveNext())
			{
				vector += enumerator.Current;
				num++;
			}
			vector /= (float)num;
			float num2 = 0f;
			foreach (Vector2 current in points)
			{
				float sqrMagnitude = (current - vector).sqrMagnitude;
				if (sqrMagnitude > num2)
				{
					num2 = sqrMagnitude;
				}
			}
			Circle2 result;
			result.Center = vector;
			result.Radius = Mathf.Sqrt(num2);
			return result;
		}

		public static Circle2 CreateFromPointsAverage(IList<Vector2> points)
		{
			int count = points.Count;
			if (count == 0)
			{
				return default(Circle2);
			}
			Vector2 vector = points[0];
			for (int i = 1; i < count; i++)
			{
				vector += points[i];
			}
			vector /= (float)count;
			float num = 0f;
			for (int j = 0; j < count; j++)
			{
				float sqrMagnitude = (points[j] - vector).sqrMagnitude;
				if (sqrMagnitude > num)
				{
					num = sqrMagnitude;
				}
			}
			Circle2 result;
			result.Center = vector;
			result.Radius = Mathf.Sqrt(num);
			return result;
		}

		public static bool CreateCircumscribed(Vector2 v0, Vector2 v1, Vector2 v2, out Circle2 circle)
		{
			Vector2 vector = v1 - v0;
			Vector2 vector2 = v2 - v0;
			float[,] array = new float[2, 2];
			array[0, 0] = vector.x;
			array[0, 1] = vector.y;
			array[1, 0] = vector2.x;
			array[1, 1] = vector2.y;
			float[,] a = array;
			float[] b = new float[]
			{
				0.5f * vector.sqrMagnitude,
				0.5f * vector2.sqrMagnitude
			};
			Vector2 b2;
			if (LinearSystem.Solve2(a, b, out b2, 1E-05f))
			{
				circle.Center = v0 + b2;
				circle.Radius = b2.magnitude;
				return true;
			}
			circle = default(Circle2);
			return false;
		}

		public static bool CreateInscribed(Vector2 v0, Vector2 v1, Vector2 v2, out Circle2 circle)
		{
			Vector2 vector = v1 - v0;
			Vector2 value = v2 - v0;
			Vector2 vector2 = v2 - v1;
			float num = vector.magnitude;
			float num2 = value.magnitude;
			float num3 = vector2.magnitude;
			float num4 = num + num2 + num3;
			if (num4 > 1E-05f)
			{
				float num5 = 1f / num4;
				num *= num5;
				num2 *= num5;
				num3 *= num5;
				circle.Center = num3 * v0 + num2 * v1 + num * v2;
				circle.Radius = num5 * Mathf.Abs(vector.DotPerp(value));
				if (circle.Radius > 1E-05f)
				{
					return true;
				}
			}
			circle = default(Circle2);
			return false;
		}

		public float CalcPerimeter()
		{
			return 6.28318548f * this.Radius;
		}

		public float CalcArea()
		{
			return 3.14159274f * this.Radius * this.Radius;
		}

		public Vector2 Eval(float t)
		{
			return new Vector2(this.Center.x + this.Radius * Mathf.Cos(t), this.Center.y + this.Radius * Mathf.Sin(t));
		}

		public Vector2 Eval(float t, float radius)
		{
			return new Vector2(this.Center.x + radius * Mathf.Cos(t), this.Center.y + radius * Mathf.Sin(t));
		}

		public float DistanceTo(Vector2 point)
		{
			return Distance.Point2Circle2(ref point, ref this);
		}

		public Vector2 Project(Vector2 point)
		{
			Vector2 result;
			Distance.SqrPoint2Circle2(ref point, ref this, out result);
			return result;
		}

		public bool Contains(ref Vector2 point)
		{
			return (point - this.Center).sqrMagnitude <= this.Radius * this.Radius;
		}

		public bool Contains(Vector2 point)
		{
			return (point - this.Center).sqrMagnitude <= this.Radius * this.Radius;
		}

		public void Include(ref Circle2 circle)
		{
			Vector2 a = circle.Center - this.Center;
			float sqrMagnitude = a.sqrMagnitude;
			float num = circle.Radius - this.Radius;
			float num2 = num * num;
			if (num2 >= sqrMagnitude)
			{
				if (num >= 0f)
				{
					this = circle;
				}
				return;
			}
			float num3 = Mathf.Sqrt(sqrMagnitude);
			if (num3 > 1E-05f)
			{
				float d = (num3 + num) / (2f * num3);
				this.Center += d * a;
			}
			this.Radius = 0.5f * (num3 + this.Radius + circle.Radius);
		}

		public void Include(Circle2 circle)
		{
			this.Include(ref circle);
		}

		public override string ToString()
		{
			return string.Format("[Center: {0} Radius: {1}]", this.Center.ToStringEx(), this.Radius.ToString());
		}
	}
}

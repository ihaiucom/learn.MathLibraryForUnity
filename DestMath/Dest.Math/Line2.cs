using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Line2
	{
		public Vector2 Center;

		public Vector2 Direction;

		public Line2(ref Vector2 center, ref Vector2 direction)
		{
			this.Center = center;
			this.Direction = direction;
		}

		public Line2(Vector2 center, Vector2 direction)
		{
			this.Center = center;
			this.Direction = direction;
		}

		public static Line2 CreateFromTwoPoints(ref Vector2 p0, ref Vector2 p1)
		{
			Line2 result;
			result.Center = p0;
			result.Direction = (p1 - p0).normalized;
			return result;
		}

		public static Line2 CreateFromTwoPoints(Vector2 p0, Vector2 p1)
		{
			Line2 result;
			result.Center = p0;
			result.Direction = (p1 - p0).normalized;
			return result;
		}

		public static Line2 CreatePerpToLineTrhoughPoint(Line2 line, Vector2 point)
		{
			Line2 result;
			result.Center = point;
			result.Direction = line.Direction.Perp();
			return result;
		}

		public static Line2 CreateBetweenAndEquidistantToPoints(Vector2 point0, Vector2 point1)
		{
			Line2 result;
			result.Center.x = (point0.x + point1.x) * 0.5f;
			result.Center.y = (point0.y + point1.y) * 0.5f;
			result.Direction.x = point1.y - point0.y;
			result.Direction.y = point0.x - point1.x;
			return result;
		}

		public static Line2 CreateParallelToGivenLineAtGivenDistance(Line2 line, float distance)
		{
			Line2 result;
			result.Direction = line.Direction;
			result.Center = line.Center + distance * new Vector2(line.Direction.y, -line.Direction.x);
			return result;
		}

		public Vector2 Eval(float t)
		{
			return this.Center + this.Direction * t;
		}

		public float SignedDistanceTo(Vector2 point)
		{
			return (point - this.Center).DotPerp(this.Direction);
		}

		public float DistanceTo(Vector2 point)
		{
			return Distance.Point2Line2(ref point, ref this);
		}

		public int QuerySide(Vector2 point, float epsilon = 1E-05f)
		{
			float num = (point - this.Center).DotPerp(this.Direction);
			if (num < -epsilon)
			{
				return -1;
			}
			if (num > epsilon)
			{
				return 1;
			}
			return 0;
		}

		public bool QuerySideNegative(Vector2 point, float epsilon = 1E-05f)
		{
			float num = (point - this.Center).DotPerp(this.Direction);
			return num <= epsilon;
		}

		public bool QuerySidePositive(Vector2 point, float epsilon = 1E-05f)
		{
			float num = (point - this.Center).DotPerp(this.Direction);
			return num >= -epsilon;
		}

		public int QuerySide(ref Box2 box, float epsilon = 1E-05f)
		{
			float f = box.Extents.x * box.Axis0.DotPerp(this.Direction);
			float f2 = box.Extents.y * box.Axis1.DotPerp(this.Direction);
			float num = Mathf.Abs(f) + Mathf.Abs(f2);
			float num2 = (box.Center - this.Center).DotPerp(this.Direction);
			if (num2 < -num + epsilon)
			{
				return -1;
			}
			if (num2 <= num - epsilon)
			{
				return 0;
			}
			return 1;
		}

		public bool QuerySideNegative(ref Box2 box, float epsilon = 1E-05f)
		{
			float f = box.Extents.x * box.Axis0.DotPerp(this.Direction);
			float f2 = box.Extents.y * box.Axis1.DotPerp(this.Direction);
			float num = Mathf.Abs(f) + Mathf.Abs(f2);
			float num2 = (box.Center - this.Center).DotPerp(this.Direction);
			return num2 <= -num + epsilon;
		}

		public bool QuerySidePositive(ref Box2 box, float epsilon = 1E-05f)
		{
			float f = box.Extents.x * box.Axis0.DotPerp(this.Direction);
			float f2 = box.Extents.y * box.Axis1.DotPerp(this.Direction);
			float num = Mathf.Abs(f) + Mathf.Abs(f2);
			float num2 = (box.Center - this.Center).DotPerp(this.Direction);
			return num2 >= num - epsilon;
		}

		public int QuerySide(ref AAB2 box, float epsilon = 1E-05f)
		{
			Vector2 vector;
			vector.x = this.Direction.y;
			vector.y = -this.Direction.x;
			Vector2 a;
			Vector2 a2;
			if (vector.x >= 0f)
			{
				a.x = box.Min.x;
				a2.x = box.Max.x;
			}
			else
			{
				a.x = box.Max.x;
				a2.x = box.Min.x;
			}
			if (vector.y >= 0f)
			{
				a.y = box.Min.y;
				a2.y = box.Max.y;
			}
			else
			{
				a.y = box.Max.y;
				a2.y = box.Min.y;
			}
			if (vector.Dot(a - this.Center) > -epsilon)
			{
				return 1;
			}
			if (vector.Dot(a2 - this.Center) < epsilon)
			{
				return -1;
			}
			return 0;
		}

		public bool QuerySideNegative(ref AAB2 box, float epsilon = 1E-05f)
		{
			Vector2 vector;
			vector.x = this.Direction.y;
			vector.y = -this.Direction.x;
			Vector2 a;
			if (vector.x >= 0f)
			{
				a.x = box.Max.x;
			}
			else
			{
				a.x = box.Min.x;
			}
			if (vector.y >= 0f)
			{
				a.y = box.Max.y;
			}
			else
			{
				a.y = box.Min.y;
			}
			return vector.Dot(a - this.Center) <= epsilon;
		}

		public bool QuerySidePositive(ref AAB2 box, float epsilon = 1E-05f)
		{
			Vector2 vector;
			vector.x = this.Direction.y;
			vector.y = -this.Direction.x;
			Vector2 a;
			if (vector.x >= 0f)
			{
				a.x = box.Min.x;
			}
			else
			{
				a.x = box.Max.x;
			}
			if (vector.y >= 0f)
			{
				a.y = box.Min.y;
			}
			else
			{
				a.y = box.Max.y;
			}
			return vector.Dot(a - this.Center) >= -epsilon;
		}

		public int QuerySide(ref Circle2 circle, float epsilon = 1E-05f)
		{
			float num = (circle.Center - this.Center).DotPerp(this.Direction);
			if (num > circle.Radius - epsilon)
			{
				return 1;
			}
			if (num >= -circle.Radius + epsilon)
			{
				return 0;
			}
			return -1;
		}

		public bool QuerySideNegative(ref Circle2 circle, float epsilon = 1E-05f)
		{
			float num = (circle.Center - this.Center).DotPerp(this.Direction);
			return num <= -circle.Radius + epsilon;
		}

		public bool QuerySidePositive(ref Circle2 circle, float epsilon = 1E-05f)
		{
			float num = (circle.Center - this.Center).DotPerp(this.Direction);
			return num >= circle.Radius - epsilon;
		}

		public Vector2 Project(Vector2 point)
		{
			Vector2 result;
			Distance.SqrPoint2Line2(ref point, ref this, out result);
			return result;
		}

		public float AngleBetweenTwoLines(Line2 anotherLine, bool acuteAngleDesired = false)
		{
			float num = Mathf.Acos(this.Direction.Dot(anotherLine.Direction));
			if (acuteAngleDesired && num > 1.57079637f)
			{
				return 3.14159274f - num;
			}
			return num;
		}

		public override string ToString()
		{
			return string.Format("[Origin: {0} Direction: {1}]", this.Center.ToStringEx(), this.Direction.ToStringEx());
		}
	}
}

using System;
using UnityEngine;

namespace Dest.Math
{
	public static class Distance
	{
		public static float Line2Line2(ref Line2 line0, ref Line2 line1)
		{
			return Mathf.Sqrt(Distance.SqrLine2Line2(ref line0, ref line1));
		}

		public static float Line2Line2(ref Line2 line0, ref Line2 line1, out Vector2 closestPoint0, out Vector2 closestPoint1)
		{
			return Mathf.Sqrt(Distance.SqrLine2Line2(ref line0, ref line1, out closestPoint0, out closestPoint1));
		}

		public static float SqrLine2Line2(ref Line2 line0, ref Line2 line1)
		{
			Vector2 vector = line0.Center - line1.Center;
			float num = -line0.Direction.Dot(line1.Direction);
			float num2 = vector.Dot(line0.Direction);
			float sqrMagnitude = vector.sqrMagnitude;
			float num3 = Mathf.Abs(1f - num * num);
			float num4;
			if (num3 >= 1E-05f)
			{
				num4 = 0f;
			}
			else
			{
				float num5 = -num2;
				num4 = num2 * num5 + sqrMagnitude;
				if (num4 < 0f)
				{
					num4 = 0f;
				}
			}
			return num4;
		}

		public static float SqrLine2Line2(ref Line2 line0, ref Line2 line1, out Vector2 closestPoint0, out Vector2 closestPoint1)
		{
			Vector2 vector = line0.Center - line1.Center;
			float num = -line0.Direction.Dot(line1.Direction);
			float num2 = vector.Dot(line0.Direction);
			float sqrMagnitude = vector.sqrMagnitude;
			float num3 = Mathf.Abs(1f - num * num);
			float num6;
			float d;
			float num7;
			if (num3 >= 1E-05f)
			{
				float num4 = -vector.Dot(line1.Direction);
				float num5 = 1f / num3;
				num6 = (num * num4 - num2) * num5;
				d = (num * num2 - num4) * num5;
				num7 = 0f;
			}
			else
			{
				num6 = -num2;
				d = 0f;
				num7 = num2 * num6 + sqrMagnitude;
				if (num7 < 0f)
				{
					num7 = 0f;
				}
			}
			closestPoint0 = line0.Center + num6 * line0.Direction;
			closestPoint1 = line1.Center + d * line1.Direction;
			return num7;
		}

		public static float Line2Ray2(ref Line2 line, ref Ray2 ray)
		{
			return Mathf.Sqrt(Distance.SqrLine2Ray2(ref line, ref ray));
		}

		public static float Line2Ray2(ref Line2 line, ref Ray2 ray, out Vector2 closestPoint0, out Vector2 closestPoint1)
		{
			return Mathf.Sqrt(Distance.SqrLine2Ray2(ref line, ref ray, out closestPoint0, out closestPoint1));
		}

		public static float SqrLine2Ray2(ref Line2 line, ref Ray2 ray)
		{
			Vector2 vector = line.Center - ray.Center;
			float num = -line.Direction.Dot(ray.Direction);
			float num2 = vector.Dot(line.Direction);
			float sqrMagnitude = vector.sqrMagnitude;
			float num3 = Mathf.Abs(1f - num * num);
			float num6;
			if (num3 >= 1E-05f)
			{
				float num4 = -vector.Dot(ray.Direction);
				float num5 = num * num2 - num4;
				if (num5 >= 0f)
				{
					num6 = 0f;
				}
				else
				{
					float num7 = -num2;
					num6 = num2 * num7 + sqrMagnitude;
					if (num6 < 0f)
					{
						num6 = 0f;
					}
				}
			}
			else
			{
				float num7 = -num2;
				num6 = num2 * num7 + sqrMagnitude;
				if (num6 < 0f)
				{
					num6 = 0f;
				}
			}
			return num6;
		}

		public static float SqrLine2Ray2(ref Line2 line, ref Ray2 ray, out Vector2 closestPoint0, out Vector2 closestPoint1)
		{
			Vector2 vector = line.Center - ray.Center;
			float num = -line.Direction.Dot(ray.Direction);
			float num2 = vector.Dot(line.Direction);
			float sqrMagnitude = vector.sqrMagnitude;
			float num3 = Mathf.Abs(1f - num * num);
			float num5;
			float num7;
			float num8;
			if (num3 >= 1E-05f)
			{
				float num4 = -vector.Dot(ray.Direction);
				num5 = num * num2 - num4;
				if (num5 >= 0f)
				{
					float num6 = 1f / num3;
					num7 = (num * num4 - num2) * num6;
					num5 *= num6;
					num8 = 0f;
				}
				else
				{
					num7 = -num2;
					num5 = 0f;
					num8 = num2 * num7 + sqrMagnitude;
					if (num8 < 0f)
					{
						num8 = 0f;
					}
				}
			}
			else
			{
				num7 = -num2;
				num5 = 0f;
				num8 = num2 * num7 + sqrMagnitude;
				if (num8 < 0f)
				{
					num8 = 0f;
				}
			}
			closestPoint0 = line.Center + num7 * line.Direction;
			closestPoint1 = ray.Center + num5 * ray.Direction;
			return num8;
		}

		public static float Line2Segment2(ref Line2 line, ref Segment2 segment)
		{
			return Mathf.Sqrt(Distance.SqrLine2Segment2(ref line, ref segment));
		}

		public static float Line2Segment2(ref Line2 line, ref Segment2 segment, out Vector2 closestPoint0, out Vector2 closestPoint1)
		{
			return Mathf.Sqrt(Distance.SqrLine2Segment2(ref line, ref segment, out closestPoint0, out closestPoint1));
		}

		public static float SqrLine2Segment2(ref Line2 line, ref Segment2 segment)
		{
			Vector2 vector = line.Center - segment.Center;
			float num = -line.Direction.Dot(segment.Direction);
			float num2 = vector.Dot(line.Direction);
			float sqrMagnitude = vector.sqrMagnitude;
			float num3 = Mathf.Abs(1f - num * num);
			float num7;
			if (num3 >= 1E-05f)
			{
				float num4 = -vector.Dot(segment.Direction);
				float num5 = num * num2 - num4;
				float num6 = segment.Extent * num3;
				if (num5 >= -num6)
				{
					if (num5 <= num6)
					{
						num7 = 0f;
					}
					else
					{
						num5 = segment.Extent;
						float num8 = -(num * num5 + num2);
						num7 = -num8 * num8 + num5 * (num5 + 2f * num4) + sqrMagnitude;
					}
				}
				else
				{
					num5 = -segment.Extent;
					float num8 = -(num * num5 + num2);
					num7 = -num8 * num8 + num5 * (num5 + 2f * num4) + sqrMagnitude;
				}
			}
			else
			{
				float num8 = -num2;
				num7 = num2 * num8 + sqrMagnitude;
			}
			if (num7 < 0f)
			{
				num7 = 0f;
			}
			return num7;
		}

		public static float SqrLine2Segment2(ref Line2 line, ref Segment2 segment, out Vector2 closestPoint0, out Vector2 closestPoint1)
		{
			Vector2 vector = line.Center - segment.Center;
			float num = -line.Direction.Dot(segment.Direction);
			float num2 = vector.Dot(line.Direction);
			float sqrMagnitude = vector.sqrMagnitude;
			float num3 = Mathf.Abs(1f - num * num);
			float num5;
			float num8;
			float num9;
			if (num3 >= 1E-05f)
			{
				float num4 = -vector.Dot(segment.Direction);
				num5 = num * num2 - num4;
				float num6 = segment.Extent * num3;
				if (num5 >= -num6)
				{
					if (num5 <= num6)
					{
						float num7 = 1f / num3;
						num8 = (num * num4 - num2) * num7;
						num5 *= num7;
						num9 = 0f;
					}
					else
					{
						num5 = segment.Extent;
						num8 = -(num * num5 + num2);
						num9 = -num8 * num8 + num5 * (num5 + 2f * num4) + sqrMagnitude;
					}
				}
				else
				{
					num5 = -segment.Extent;
					num8 = -(num * num5 + num2);
					num9 = -num8 * num8 + num5 * (num5 + 2f * num4) + sqrMagnitude;
				}
			}
			else
			{
				num5 = 0f;
				num8 = -num2;
				num9 = num2 * num8 + sqrMagnitude;
			}
			closestPoint0 = line.Center + num8 * line.Direction;
			closestPoint1 = segment.Center + num5 * segment.Direction;
			if (num9 < 0f)
			{
				num9 = 0f;
			}
			return num9;
		}

		public static float Point2AAB2(ref Vector2 point, ref AAB2 box)
		{
			float num = 0f;
			float num2 = point.x;
			if (num2 < box.Min.x)
			{
				float num3 = box.Min.x - num2;
				num += num3 * num3;
			}
			else if (num2 > box.Max.x)
			{
				float num3 = num2 - box.Max.x;
				num += num3 * num3;
			}
			num2 = point.y;
			if (num2 < box.Min.y)
			{
				float num3 = box.Min.y - num2;
				num += num3 * num3;
			}
			else if (num2 > box.Max.y)
			{
				float num3 = num2 - box.Max.y;
				num += num3 * num3;
			}
			return Mathf.Sqrt(num);
		}

		public static float Point2AAB2(ref Vector2 point, ref AAB2 box, out Vector2 closestPoint)
		{
			float num = 0f;
			closestPoint = point;
			float num2 = point.x;
			if (num2 < box.Min.x)
			{
				float num3 = box.Min.x - num2;
				num += num3 * num3;
				closestPoint.x += num3;
			}
			else if (num2 > box.Max.x)
			{
				float num3 = num2 - box.Max.x;
				num += num3 * num3;
				closestPoint.x -= num3;
			}
			num2 = point.y;
			if (num2 < box.Min.y)
			{
				float num3 = box.Min.y - num2;
				num += num3 * num3;
				closestPoint.y += num3;
			}
			else if (num2 > box.Max.y)
			{
				float num3 = num2 - box.Max.y;
				num += num3 * num3;
				closestPoint.y -= num3;
			}
			return Mathf.Sqrt(num);
		}

		public static float SqrPoint2AAB2(ref Vector2 point, ref AAB2 box)
		{
			float num = 0f;
			float num2 = point.x;
			if (num2 < box.Min.x)
			{
				float num3 = box.Min.x - num2;
				num += num3 * num3;
			}
			else if (num2 > box.Max.x)
			{
				float num3 = num2 - box.Max.x;
				num += num3 * num3;
			}
			num2 = point.y;
			if (num2 < box.Min.y)
			{
				float num3 = box.Min.y - num2;
				num += num3 * num3;
			}
			else if (num2 > box.Max.y)
			{
				float num3 = num2 - box.Max.y;
				num += num3 * num3;
			}
			return num;
		}

		public static float SqrPoint2AAB2(ref Vector2 point, ref AAB2 box, out Vector2 closestPoint)
		{
			float num = 0f;
			closestPoint = point;
			float num2 = point.x;
			if (num2 < box.Min.x)
			{
				float num3 = box.Min.x - num2;
				num += num3 * num3;
				closestPoint.x += num3;
			}
			else if (num2 > box.Max.x)
			{
				float num3 = num2 - box.Max.x;
				num += num3 * num3;
				closestPoint.x -= num3;
			}
			num2 = point.y;
			if (num2 < box.Min.y)
			{
				float num3 = box.Min.y - num2;
				num += num3 * num3;
				closestPoint.y += num3;
			}
			else if (num2 > box.Max.y)
			{
				float num3 = num2 - box.Max.y;
				num += num3 * num3;
				closestPoint.y -= num3;
			}
			return num;
		}

		public static float Point2Box2(ref Vector2 point, ref Box2 box)
		{
			return Mathf.Sqrt(Distance.SqrPoint2Box2(ref point, ref box));
		}

		public static float Point2Box2(ref Vector2 point, ref Box2 box, out Vector2 closestPoint)
		{
			return Mathf.Sqrt(Distance.SqrPoint2Box2(ref point, ref box, out closestPoint));
		}

		public static float SqrPoint2Box2(ref Vector2 point, ref Box2 box)
		{
			Vector2 vector = point - box.Center;
			float num = 0f;
			float num2 = vector.Dot(box.Axis0);
			float num3 = box.Extents.x;
			if (num2 < -num3)
			{
				float num4 = num2 + num3;
				num += num4 * num4;
			}
			else if (num2 > num3)
			{
				float num4 = num2 - num3;
				num += num4 * num4;
			}
			float num5 = vector.Dot(box.Axis1);
			num3 = box.Extents.y;
			if (num5 < -num3)
			{
				float num4 = num5 + num3;
				num += num4 * num4;
			}
			else if (num5 > num3)
			{
				float num4 = num5 - num3;
				num += num4 * num4;
			}
			return num;
		}

		public static float SqrPoint2Box2(ref Vector2 point, ref Box2 box, out Vector2 closestPoint)
		{
			Vector2 vector = point - box.Center;
			float num = 0f;
			float num2 = vector.Dot(box.Axis0);
			float num3 = box.Extents.x;
			if (num2 < -num3)
			{
				float num4 = num2 + num3;
				num += num4 * num4;
				num2 = -num3;
			}
			else if (num2 > num3)
			{
				float num4 = num2 - num3;
				num += num4 * num4;
				num2 = num3;
			}
			float num5 = vector.Dot(box.Axis1);
			num3 = box.Extents.y;
			if (num5 < -num3)
			{
				float num4 = num5 + num3;
				num += num4 * num4;
				num5 = -num3;
			}
			else if (num5 > num3)
			{
				float num4 = num5 - num3;
				num += num4 * num4;
				num5 = num3;
			}
			closestPoint = box.Center + num2 * box.Axis0 + num5 * box.Axis1;
			return num;
		}

		public static float Point2Circle2(ref Vector2 point, ref Circle2 circle)
		{
			float num = (point - circle.Center).magnitude - circle.Radius;
			if (num <= 0f)
			{
				return 0f;
			}
			return num;
		}

		public static float Point2Circle2(ref Vector2 point, ref Circle2 circle, out Vector2 closestPoint)
		{
			Vector2 a = point - circle.Center;
			float sqrMagnitude = a.sqrMagnitude;
			if (sqrMagnitude > circle.Radius * circle.Radius)
			{
				float num = Mathf.Sqrt(sqrMagnitude);
				closestPoint = circle.Center + a * (circle.Radius / num);
				return num - circle.Radius;
			}
			closestPoint = point;
			return 0f;
		}

		public static float SqrPoint2Circle2(ref Vector2 point, ref Circle2 circle)
		{
			float num = (point - circle.Center).magnitude - circle.Radius;
			if (num <= 0f)
			{
				return 0f;
			}
			return num * num;
		}

		public static float SqrPoint2Circle2(ref Vector2 point, ref Circle2 circle, out Vector2 closestPoint)
		{
			Vector2 a = point - circle.Center;
			float sqrMagnitude = a.sqrMagnitude;
			if (sqrMagnitude > circle.Radius * circle.Radius)
			{
				float num = Mathf.Sqrt(sqrMagnitude);
				closestPoint = circle.Center + a * (circle.Radius / num);
				float num2 = num - circle.Radius;
				return num2 * num2;
			}
			closestPoint = point;
			return 0f;
		}

		public static float Point2Line2(ref Vector2 point, ref Line2 line)
		{
			return Mathf.Sqrt(Distance.SqrPoint2Line2(ref point, ref line));
		}

		public static float Point2Line2(ref Vector2 point, ref Line2 line, out Vector2 closestPoint)
		{
			return Mathf.Sqrt(Distance.SqrPoint2Line2(ref point, ref line, out closestPoint));
		}

		public static float SqrPoint2Line2(ref Vector2 point, ref Line2 line)
		{
			Vector2 value = point - line.Center;
			float d = line.Direction.Dot(value);
			Vector2 a = line.Center + d * line.Direction;
			return (a - point).sqrMagnitude;
		}

		public static float SqrPoint2Line2(ref Vector2 point, ref Line2 line, out Vector2 closestPoint)
		{
			Vector2 value = point - line.Center;
			float d = line.Direction.Dot(value);
			closestPoint = line.Center + d * line.Direction;
			return (closestPoint - point).sqrMagnitude;
		}

		public static float Point2Ray2(ref Vector2 point, ref Ray2 ray)
		{
			return Mathf.Sqrt(Distance.SqrPoint2Ray2(ref point, ref ray));
		}

		public static float Point2Ray2(ref Vector2 point, ref Ray2 ray, out Vector2 closestPoint)
		{
			return Mathf.Sqrt(Distance.SqrPoint2Ray2(ref point, ref ray, out closestPoint));
		}

		public static float SqrPoint2Ray2(ref Vector2 point, ref Ray2 ray)
		{
			Vector2 value = point - ray.Center;
			float num = ray.Direction.Dot(value);
			Vector2 a;
			if (num > 0f)
			{
				a = ray.Center + num * ray.Direction;
			}
			else
			{
				a = ray.Center;
			}
			return (a - point).sqrMagnitude;
		}

		public static float SqrPoint2Ray2(ref Vector2 point, ref Ray2 ray, out Vector2 closestPoint)
		{
			Vector2 value = point - ray.Center;
			float num = ray.Direction.Dot(value);
			if (num > 0f)
			{
				closestPoint = ray.Center + num * ray.Direction;
			}
			else
			{
				closestPoint = ray.Center;
			}
			return (closestPoint - point).sqrMagnitude;
		}

		public static float Point2Segment2(ref Vector2 point, ref Segment2 segment)
		{
			return Mathf.Sqrt(Distance.SqrPoint2Segment2(ref point, ref segment));
		}

		public static float Point2Segment2(ref Vector2 point, ref Segment2 segment, out Vector2 closestPoint)
		{
			return Mathf.Sqrt(Distance.SqrPoint2Segment2(ref point, ref segment, out closestPoint));
		}

		public static float SqrPoint2Segment2(ref Vector2 point, ref Segment2 segment)
		{
			Vector2 value = point - segment.Center;
			float num = segment.Direction.Dot(value);
			Vector2 a;
			if (-segment.Extent < num)
			{
				if (num < segment.Extent)
				{
					a = segment.Center + num * segment.Direction;
				}
				else
				{
					a = segment.P1;
				}
			}
			else
			{
				a = segment.P0;
			}
			return (a - point).sqrMagnitude;
		}

		public static float SqrPoint2Segment2(ref Vector2 point, ref Segment2 segment, out Vector2 closestPoint)
		{
			Vector2 value = point - segment.Center;
			float num = segment.Direction.Dot(value);
			if (-segment.Extent < num)
			{
				if (num < segment.Extent)
				{
					closestPoint = segment.Center + num * segment.Direction;
				}
				else
				{
					closestPoint = segment.P1;
				}
			}
			else
			{
				closestPoint = segment.P0;
			}
			return (closestPoint - point).sqrMagnitude;
		}

		public static float Point2Triangle2(ref Vector2 point, ref Triangle2 triangle)
		{
			if (triangle.Contains(point))
			{
				return 0f;
			}
			Segment2 segment = new Segment2(ref triangle.V0, ref triangle.V1);
			float num = Distance.Point2Segment2(ref point, ref segment);
			Segment2 segment2 = new Segment2(ref triangle.V1, ref triangle.V2);
			float num2 = Distance.Point2Segment2(ref point, ref segment2);
			Segment2 segment3 = new Segment2(ref triangle.V2, ref triangle.V0);
			float num3 = Distance.Point2Segment2(ref point, ref segment3);
			if (num < num2)
			{
				if (num < num3)
				{
					return num;
				}
				if (num2 < num3)
				{
					return num2;
				}
				return num3;
			}
			else
			{
				if (num2 < num3)
				{
					return num2;
				}
				if (num < num3)
				{
					return num;
				}
				return num3;
			}
		}

		public static float Point2Triangle2(ref Vector2 point, ref Triangle2 triangle, out Vector2 closestPoint)
		{
			if (triangle.Contains(point))
			{
				closestPoint = point;
				return 0f;
			}
			Segment2 segment = new Segment2(ref triangle.V0, ref triangle.V1);
			Vector2 vector;
			float num = Distance.Point2Segment2(ref point, ref segment, out vector);
			Segment2 segment2 = new Segment2(ref triangle.V1, ref triangle.V2);
			Vector2 vector2;
			float num2 = Distance.Point2Segment2(ref point, ref segment2, out vector2);
			Segment2 segment3 = new Segment2(ref triangle.V2, ref triangle.V0);
			Vector2 vector3;
			float num3 = Distance.Point2Segment2(ref point, ref segment3, out vector3);
			if (num < num2)
			{
				if (num < num3)
				{
					closestPoint = vector;
					return num;
				}
				if (num2 < num3)
				{
					closestPoint = vector2;
					return num2;
				}
				closestPoint = vector3;
				return num3;
			}
			else
			{
				if (num2 < num3)
				{
					closestPoint = vector2;
					return num2;
				}
				if (num < num3)
				{
					closestPoint = vector;
					return num;
				}
				closestPoint = vector3;
				return num3;
			}
		}

		public static float SqrPoint2Triangle2(ref Vector2 point, ref Triangle2 triangle)
		{
			if (triangle.Contains(point))
			{
				return 0f;
			}
			Segment2 segment = new Segment2(ref triangle.V0, ref triangle.V1);
			float num = Distance.SqrPoint2Segment2(ref point, ref segment);
			Segment2 segment2 = new Segment2(ref triangle.V1, ref triangle.V2);
			float num2 = Distance.SqrPoint2Segment2(ref point, ref segment2);
			Segment2 segment3 = new Segment2(ref triangle.V2, ref triangle.V0);
			float num3 = Distance.SqrPoint2Segment2(ref point, ref segment3);
			if (num < num2)
			{
				if (num < num3)
				{
					return num;
				}
				if (num2 < num3)
				{
					return num2;
				}
				return num3;
			}
			else
			{
				if (num2 < num3)
				{
					return num2;
				}
				if (num < num3)
				{
					return num;
				}
				return num3;
			}
		}

		public static float SqrPoint2Triangle2(ref Vector2 point, ref Triangle2 triangle, out Vector2 closestPoint)
		{
			if (triangle.Contains(point))
			{
				closestPoint = point;
				return 0f;
			}
			Segment2 segment = new Segment2(ref triangle.V0, ref triangle.V1);
			Vector2 vector;
			float num = Distance.SqrPoint2Segment2(ref point, ref segment, out vector);
			Segment2 segment2 = new Segment2(ref triangle.V1, ref triangle.V2);
			Vector2 vector2;
			float num2 = Distance.SqrPoint2Segment2(ref point, ref segment2, out vector2);
			Segment2 segment3 = new Segment2(ref triangle.V2, ref triangle.V0);
			Vector2 vector3;
			float num3 = Distance.SqrPoint2Segment2(ref point, ref segment3, out vector3);
			if (num < num2)
			{
				if (num < num3)
				{
					closestPoint = vector;
					return num;
				}
				if (num2 < num3)
				{
					closestPoint = vector2;
					return num2;
				}
				closestPoint = vector3;
				return num3;
			}
			else
			{
				if (num2 < num3)
				{
					closestPoint = vector2;
					return num2;
				}
				if (num < num3)
				{
					closestPoint = vector;
					return num;
				}
				closestPoint = vector3;
				return num3;
			}
		}

		public static float Ray2Ray2(ref Ray2 ray0, ref Ray2 ray1)
		{
			Vector2 vector;
			Vector2 vector2;
			return Mathf.Sqrt(Distance.SqrRay2Ray2(ref ray0, ref ray1, out vector, out vector2));
		}

		public static float Ray2Ray2(ref Ray2 ray0, ref Ray2 ray1, out Vector2 closestPoint0, out Vector2 closestPoint1)
		{
			return Mathf.Sqrt(Distance.SqrRay2Ray2(ref ray0, ref ray1, out closestPoint0, out closestPoint1));
		}

		public static float SqrRay2Ray2(ref Ray2 ray0, ref Ray2 ray1)
		{
			Vector2 vector;
			Vector2 vector2;
			return Distance.SqrRay2Ray2(ref ray0, ref ray1, out vector, out vector2);
		}

		public static float SqrRay2Ray2(ref Ray2 ray0, ref Ray2 ray1, out Vector2 closestPoint0, out Vector2 closestPoint1)
		{
			Vector2 vector = ray0.Center - ray1.Center;
			float num = -ray0.Direction.Dot(ray1.Direction);
			float num2 = vector.Dot(ray0.Direction);
			float sqrMagnitude = vector.sqrMagnitude;
			float num3 = Mathf.Abs(1f - num * num);
			float num5;
			float num6;
			float num8;
			if (num3 >= 1E-05f)
			{
				float num4 = -vector.Dot(ray1.Direction);
				num5 = num * num4 - num2;
				num6 = num * num2 - num4;
				if (num5 >= 0f)
				{
					if (num6 >= 0f)
					{
						float num7 = 1f / num3;
						num5 *= num7;
						num6 *= num7;
						num8 = 0f;
					}
					else
					{
						num6 = 0f;
						if (num2 >= 0f)
						{
							num5 = 0f;
							num8 = sqrMagnitude;
						}
						else
						{
							num5 = -num2;
							num8 = num2 * num5 + sqrMagnitude;
						}
					}
				}
				else if (num6 >= 0f)
				{
					num5 = 0f;
					if (num4 >= 0f)
					{
						num6 = 0f;
						num8 = sqrMagnitude;
					}
					else
					{
						num6 = -num4;
						num8 = num4 * num6 + sqrMagnitude;
					}
				}
				else if (num2 < 0f)
				{
					num5 = -num2;
					num6 = 0f;
					num8 = num2 * num5 + sqrMagnitude;
				}
				else
				{
					num5 = 0f;
					if (num4 >= 0f)
					{
						num6 = 0f;
						num8 = sqrMagnitude;
					}
					else
					{
						num6 = -num4;
						num8 = num4 * num6 + sqrMagnitude;
					}
				}
			}
			else if (num > 0f)
			{
				num6 = 0f;
				if (num2 >= 0f)
				{
					num5 = 0f;
					num8 = sqrMagnitude;
				}
				else
				{
					num5 = -num2;
					num8 = num2 * num5 + sqrMagnitude;
				}
			}
			else if (num2 >= 0f)
			{
				float num4 = -vector.Dot(ray1.Direction);
				num5 = 0f;
				num6 = -num4;
				num8 = num4 * num6 + sqrMagnitude;
			}
			else
			{
				num5 = -num2;
				num6 = 0f;
				num8 = num2 * num5 + sqrMagnitude;
			}
			closestPoint0 = ray0.Center + num5 * ray0.Direction;
			closestPoint1 = ray1.Center + num6 * ray1.Direction;
			if (num8 < 0f)
			{
				num8 = 0f;
			}
			return num8;
		}

		public static float Ray2Segment2(ref Ray2 ray, ref Segment2 segment)
		{
			Vector2 vector;
			Vector2 vector2;
			return Mathf.Sqrt(Distance.SqrRay2Segment2(ref ray, ref segment, out vector, out vector2));
		}

		public static float Ray2Segment2(ref Ray2 ray, ref Segment2 segment, out Vector2 closestPoint0, out Vector2 closestPoint1)
		{
			return Mathf.Sqrt(Distance.SqrRay2Segment2(ref ray, ref segment, out closestPoint0, out closestPoint1));
		}

		public static float SqrRay2Segment2(ref Ray2 ray, ref Segment2 segment)
		{
			Vector2 vector;
			Vector2 vector2;
			return Distance.SqrRay2Segment2(ref ray, ref segment, out vector, out vector2);
		}

		public static float SqrRay2Segment2(ref Ray2 ray, ref Segment2 segment, out Vector2 closestPoint0, out Vector2 closestPoint1)
		{
			Vector2 vector = ray.Center - segment.Center;
			float num = -ray.Direction.Dot(segment.Direction);
			float num2 = vector.Dot(ray.Direction);
			float num3 = -vector.Dot(segment.Direction);
			float sqrMagnitude = vector.sqrMagnitude;
			float num4 = Mathf.Abs(1f - num * num);
			float num5;
			float num6;
			float num9;
			if (num4 >= 1E-05f)
			{
				num5 = num * num3 - num2;
				num6 = num * num2 - num3;
				float num7 = segment.Extent * num4;
				if (num5 >= 0f)
				{
					if (num6 >= -num7)
					{
						if (num6 <= num7)
						{
							float num8 = 1f / num4;
							num5 *= num8;
							num6 *= num8;
							num9 = 0f;
						}
						else
						{
							num6 = segment.Extent;
							num5 = -(num * num6 + num2);
							if (num5 > 0f)
							{
								num9 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
							}
							else
							{
								num5 = 0f;
								num9 = num6 * (num6 + 2f * num3) + sqrMagnitude;
							}
						}
					}
					else
					{
						num6 = -segment.Extent;
						num5 = -(num * num6 + num2);
						if (num5 > 0f)
						{
							num9 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
						}
						else
						{
							num5 = 0f;
							num9 = num6 * (num6 + 2f * num3) + sqrMagnitude;
						}
					}
				}
				else if (num6 <= -num7)
				{
					num5 = -(-num * segment.Extent + num2);
					if (num5 > 0f)
					{
						num6 = -segment.Extent;
						num9 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
					}
					else
					{
						num5 = 0f;
						num6 = -num3;
						if (num6 < -segment.Extent)
						{
							num6 = -segment.Extent;
						}
						else if (num6 > segment.Extent)
						{
							num6 = segment.Extent;
						}
						num9 = num6 * (num6 + 2f * num3) + sqrMagnitude;
					}
				}
				else if (num6 <= num7)
				{
					num5 = 0f;
					num6 = -num3;
					if (num6 < -segment.Extent)
					{
						num6 = -segment.Extent;
					}
					else if (num6 > segment.Extent)
					{
						num6 = segment.Extent;
					}
					num9 = num6 * (num6 + 2f * num3) + sqrMagnitude;
				}
				else
				{
					num5 = -(num * segment.Extent + num2);
					if (num5 > 0f)
					{
						num6 = segment.Extent;
						num9 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
					}
					else
					{
						num5 = 0f;
						num6 = -num3;
						if (num6 < -segment.Extent)
						{
							num6 = -segment.Extent;
						}
						else if (num6 > segment.Extent)
						{
							num6 = segment.Extent;
						}
						num9 = num6 * (num6 + 2f * num3) + sqrMagnitude;
					}
				}
			}
			else
			{
				if (num > 0f)
				{
					num6 = -segment.Extent;
				}
				else
				{
					num6 = segment.Extent;
				}
				num5 = -(num * num6 + num2);
				if (num5 > 0f)
				{
					num9 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
				}
				else
				{
					num5 = 0f;
					num9 = num6 * (num6 + 2f * num3) + sqrMagnitude;
				}
			}
			closestPoint0 = ray.Center + num5 * ray.Direction;
			closestPoint1 = segment.Center + num6 * segment.Direction;
			if (num9 < 0f)
			{
				num9 = 0f;
			}
			return num9;
		}

		public static float Segment2Segment2(ref Segment2 segment0, ref Segment2 segment1)
		{
			Vector2 vector;
			Vector2 vector2;
			return Mathf.Sqrt(Distance.SqrSegment2Segment2(ref segment0, ref segment1, out vector, out vector2));
		}

		public static float Segment2Segment2(ref Segment2 segment0, ref Segment2 segment1, out Vector2 closestPoint0, out Vector2 closestPoint1)
		{
			return Mathf.Sqrt(Distance.SqrSegment2Segment2(ref segment0, ref segment1, out closestPoint0, out closestPoint1));
		}

		public static float SqrSegment2Segment2(ref Segment2 segment0, ref Segment2 segment1)
		{
			Vector2 vector;
			Vector2 vector2;
			return Distance.SqrSegment2Segment2(ref segment0, ref segment1, out vector, out vector2);
		}

		public static float SqrSegment2Segment2(ref Segment2 segment0, ref Segment2 segment1, out Vector2 closestPoint0, out Vector2 closestPoint1)
		{
			Vector2 vector = segment0.Center - segment1.Center;
			float num = -segment0.Direction.Dot(segment1.Direction);
			float num2 = vector.Dot(segment0.Direction);
			float num3 = -vector.Dot(segment1.Direction);
			float sqrMagnitude = vector.sqrMagnitude;
			float num4 = Mathf.Abs(1f - num * num);
			float num5;
			float num6;
			float num10;
			if (num4 >= 1E-05f)
			{
				num5 = num * num3 - num2;
				num6 = num * num2 - num3;
				float num7 = segment0.Extent * num4;
				float num8 = segment1.Extent * num4;
				if (num5 >= -num7)
				{
					if (num5 <= num7)
					{
						if (num6 >= -num8)
						{
							if (num6 <= num8)
							{
								float num9 = 1f / num4;
								num5 *= num9;
								num6 *= num9;
								num10 = 0f;
							}
							else
							{
								num6 = segment1.Extent;
								float num11 = -(num * num6 + num2);
								if (num11 < -segment0.Extent)
								{
									num5 = -segment0.Extent;
									num10 = num5 * (num5 - 2f * num11) + num6 * (num6 + 2f * num3) + sqrMagnitude;
								}
								else if (num11 <= segment0.Extent)
								{
									num5 = num11;
									num10 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
								}
								else
								{
									num5 = segment0.Extent;
									num10 = num5 * (num5 - 2f * num11) + num6 * (num6 + 2f * num3) + sqrMagnitude;
								}
							}
						}
						else
						{
							num6 = -segment1.Extent;
							float num11 = -(num * num6 + num2);
							if (num11 < -segment0.Extent)
							{
								num5 = -segment0.Extent;
								num10 = num5 * (num5 - 2f * num11) + num6 * (num6 + 2f * num3) + sqrMagnitude;
							}
							else if (num11 <= segment0.Extent)
							{
								num5 = num11;
								num10 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
							}
							else
							{
								num5 = segment0.Extent;
								num10 = num5 * (num5 - 2f * num11) + num6 * (num6 + 2f * num3) + sqrMagnitude;
							}
						}
					}
					else if (num6 >= -num8)
					{
						if (num6 <= num8)
						{
							num5 = segment0.Extent;
							float num12 = -(num * num5 + num3);
							if (num12 < -segment1.Extent)
							{
								num6 = -segment1.Extent;
								num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
							else if (num12 <= segment1.Extent)
							{
								num6 = num12;
								num10 = -num6 * num6 + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
							else
							{
								num6 = segment1.Extent;
								num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
						}
						else
						{
							num6 = segment1.Extent;
							float num11 = -(num * num6 + num2);
							if (num11 < -segment0.Extent)
							{
								num5 = -segment0.Extent;
								num10 = num5 * (num5 - 2f * num11) + num6 * (num6 + 2f * num3) + sqrMagnitude;
							}
							else if (num11 <= segment0.Extent)
							{
								num5 = num11;
								num10 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
							}
							else
							{
								num5 = segment0.Extent;
								float num12 = -(num * num5 + num3);
								if (num12 < -segment1.Extent)
								{
									num6 = -segment1.Extent;
									num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
								}
								else if (num12 <= segment1.Extent)
								{
									num6 = num12;
									num10 = -num6 * num6 + num5 * (num5 + 2f * num2) + sqrMagnitude;
								}
								else
								{
									num6 = segment1.Extent;
									num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
								}
							}
						}
					}
					else
					{
						num6 = -segment1.Extent;
						float num11 = -(num * num6 + num2);
						if (num11 < -segment0.Extent)
						{
							num5 = -segment0.Extent;
							num10 = num5 * (num5 - 2f * num11) + num6 * (num6 + 2f * num3) + sqrMagnitude;
						}
						else if (num11 <= segment0.Extent)
						{
							num5 = num11;
							num10 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
						}
						else
						{
							num5 = segment0.Extent;
							float num12 = -(num * num5 + num3);
							if (num12 > segment1.Extent)
							{
								num6 = segment1.Extent;
								num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
							else if (num12 >= -segment1.Extent)
							{
								num6 = num12;
								num10 = -num6 * num6 + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
							else
							{
								num6 = -segment1.Extent;
								num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
						}
					}
				}
				else if (num6 >= -num8)
				{
					if (num6 <= num8)
					{
						num5 = -segment0.Extent;
						float num12 = -(num * num5 + num3);
						if (num12 < -segment1.Extent)
						{
							num6 = -segment1.Extent;
							num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
						}
						else if (num12 <= segment1.Extent)
						{
							num6 = num12;
							num10 = -num6 * num6 + num5 * (num5 + 2f * num2) + sqrMagnitude;
						}
						else
						{
							num6 = segment1.Extent;
							num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
						}
					}
					else
					{
						num6 = segment1.Extent;
						float num11 = -(num * num6 + num2);
						if (num11 > segment0.Extent)
						{
							num5 = segment0.Extent;
							num10 = num5 * (num5 - 2f * num11) + num6 * (num6 + 2f * num3) + sqrMagnitude;
						}
						else if (num11 >= -segment0.Extent)
						{
							num5 = num11;
							num10 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
						}
						else
						{
							num5 = -segment0.Extent;
							float num12 = -(num * num5 + num3);
							if (num12 < -segment1.Extent)
							{
								num6 = -segment1.Extent;
								num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
							else if (num12 <= segment1.Extent)
							{
								num6 = num12;
								num10 = -num6 * num6 + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
							else
							{
								num6 = segment1.Extent;
								num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
						}
					}
				}
				else
				{
					num6 = -segment1.Extent;
					float num11 = -(num * num6 + num2);
					if (num11 > segment0.Extent)
					{
						num5 = segment0.Extent;
						num10 = num5 * (num5 - 2f * num11) + num6 * (num6 + 2f * num3) + sqrMagnitude;
					}
					else if (num11 >= -segment0.Extent)
					{
						num5 = num11;
						num10 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
					}
					else
					{
						num5 = -segment0.Extent;
						float num12 = -(num * num5 + num3);
						if (num12 < -segment1.Extent)
						{
							num6 = -segment1.Extent;
							num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
						}
						else if (num12 <= segment1.Extent)
						{
							num6 = num12;
							num10 = -num6 * num6 + num5 * (num5 + 2f * num2) + sqrMagnitude;
						}
						else
						{
							num6 = segment1.Extent;
							num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
						}
					}
				}
			}
			else
			{
				float num13 = segment0.Extent + segment1.Extent;
				float num14 = (num > 0f) ? -1f : 1f;
				float num15 = 0.5f * (num2 - num14 * num3);
				float num16 = -num15;
				if (num16 < -num13)
				{
					num16 = -num13;
				}
				else if (num16 > num13)
				{
					num16 = num13;
				}
				num6 = -num14 * num16 * segment1.Extent / num13;
				num5 = num16 + num14 * num6;
				num10 = num16 * (num16 + 2f * num15) + sqrMagnitude;
			}
			closestPoint0 = segment0.Center + num5 * segment0.Direction;
			closestPoint1 = segment1.Center + num6 * segment1.Direction;
			if (num10 < 0f)
			{
				num10 = 0f;
			}
			return num10;
		}

		private static void Face(ref Box3 mBox, ref float mLineParameter, int i0, int i1, int i2, ref Vector3 pnt, ref Vector3 dir, ref Vector3 PmE, ref float sqrDistance)
		{
			Vector3 vector = default(Vector3);
			vector[i1] = pnt[i1] + mBox.Extents[i1];
			vector[i2] = pnt[i2] + mBox.Extents[i2];
			if (dir[i0] * vector[i1] >= dir[i1] * PmE[i0])
			{
				if (dir[i0] * vector[i2] >= dir[i2] * PmE[i0])
				{
					pnt[i0] = mBox.Extents[i0];
					float num = 1f / dir[i0];
					pnt[i1] -= dir[i1] * PmE[i0] * num;
					pnt[i2] -= dir[i2] * PmE[i0] * num;
					mLineParameter = -PmE[i0] * num;
					return;
				}
				float num2 = dir[i0] * dir[i0] + dir[i2] * dir[i2];
				float num3 = num2 * vector[i1] - dir[i1] * (dir[i0] * PmE[i0] + dir[i2] * vector[i2]);
				float num5;
				float num6;
				if (num3 <= 2f * num2 * mBox.Extents[i1])
				{
					float num4 = num3 / num2;
					num2 += dir[i1] * dir[i1];
					num3 = vector[i1] - num4;
					num5 = dir[i0] * PmE[i0] + dir[i1] * num3 + dir[i2] * vector[i2];
					num6 = -num5 / num2;
					sqrDistance += PmE[i0] * PmE[i0] + num3 * num3 + vector[i2] * vector[i2] + num5 * num6;
					mLineParameter = num6;
					pnt[i0] = mBox.Extents[i0];
					pnt[i1] = num4 - mBox.Extents[i1];
					pnt[i2] = -mBox.Extents[i2];
					return;
				}
				num2 += dir[i1] * dir[i1];
				num5 = dir[i0] * PmE[i0] + dir[i1] * PmE[i1] + dir[i2] * vector[i2];
				num6 = -num5 / num2;
				sqrDistance += PmE[i0] * PmE[i0] + PmE[i1] * PmE[i1] + vector[i2] * vector[i2] + num5 * num6;
				mLineParameter = num6;
				pnt[i0] = mBox.Extents[i0];
				pnt[i1] = mBox.Extents[i1];
				pnt[i2] = -mBox.Extents[i2];
				return;
			}
			else if (dir[i0] * vector[i2] >= dir[i2] * PmE[i0])
			{
				float num2 = dir[i0] * dir[i0] + dir[i1] * dir[i1];
				float num3 = num2 * vector[i2] - dir[i2] * (dir[i0] * PmE[i0] + dir[i1] * vector[i1]);
				float num5;
				float num6;
				if (num3 <= 2f * num2 * mBox.Extents[i2])
				{
					float num4 = num3 / num2;
					num2 += dir[i2] * dir[i2];
					num3 = vector[i2] - num4;
					num5 = dir[i0] * PmE[i0] + dir[i1] * vector[i1] + dir[i2] * num3;
					num6 = -num5 / num2;
					sqrDistance += PmE[i0] * PmE[i0] + vector[i1] * vector[i1] + num3 * num3 + num5 * num6;
					mLineParameter = num6;
					pnt[i0] = mBox.Extents[i0];
					pnt[i1] = -mBox.Extents[i1];
					pnt[i2] = num4 - mBox.Extents[i2];
					return;
				}
				num2 += dir[i2] * dir[i2];
				num5 = dir[i0] * PmE[i0] + dir[i1] * vector[i1] + dir[i2] * PmE[i2];
				num6 = -num5 / num2;
				sqrDistance += PmE[i0] * PmE[i0] + vector[i1] * vector[i1] + PmE[i2] * PmE[i2] + num5 * num6;
				mLineParameter = num6;
				pnt[i0] = mBox.Extents[i0];
				pnt[i1] = -mBox.Extents[i1];
				pnt[i2] = mBox.Extents[i2];
				return;
			}
			else
			{
				float num2 = dir[i0] * dir[i0] + dir[i2] * dir[i2];
				float num3 = num2 * vector[i1] - dir[i1] * (dir[i0] * PmE[i0] + dir[i2] * vector[i2]);
				if (num3 >= 0f)
				{
					float num5;
					float num6;
					if (num3 <= 2f * num2 * mBox.Extents[i1])
					{
						float num4 = num3 / num2;
						num2 += dir[i1] * dir[i1];
						num3 = vector[i1] - num4;
						num5 = dir[i0] * PmE[i0] + dir[i1] * num3 + dir[i2] * vector[i2];
						num6 = -num5 / num2;
						sqrDistance += PmE[i0] * PmE[i0] + num3 * num3 + vector[i2] * vector[i2] + num5 * num6;
						mLineParameter = num6;
						pnt[i0] = mBox.Extents[i0];
						pnt[i1] = num4 - mBox.Extents[i1];
						pnt[i2] = -mBox.Extents[i2];
						return;
					}
					num2 += dir[i1] * dir[i1];
					num5 = dir[i0] * PmE[i0] + dir[i1] * PmE[i1] + dir[i2] * vector[i2];
					num6 = -num5 / num2;
					sqrDistance += PmE[i0] * PmE[i0] + PmE[i1] * PmE[i1] + vector[i2] * vector[i2] + num5 * num6;
					mLineParameter = num6;
					pnt[i0] = mBox.Extents[i0];
					pnt[i1] = mBox.Extents[i1];
					pnt[i2] = -mBox.Extents[i2];
					return;
				}
				else
				{
					num2 = dir[i0] * dir[i0] + dir[i1] * dir[i1];
					num3 = num2 * vector[i2] - dir[i2] * (dir[i0] * PmE[i0] + dir[i1] * vector[i1]);
					float num5;
					float num6;
					if (num3 < 0f)
					{
						num2 += dir[i2] * dir[i2];
						num5 = dir[i0] * PmE[i0] + dir[i1] * vector[i1] + dir[i2] * vector[i2];
						num6 = -num5 / num2;
						sqrDistance += PmE[i0] * PmE[i0] + vector[i1] * vector[i1] + vector[i2] * vector[i2] + num5 * num6;
						mLineParameter = num6;
						pnt[i0] = mBox.Extents[i0];
						pnt[i1] = -mBox.Extents[i1];
						pnt[i2] = -mBox.Extents[i2];
						return;
					}
					if (num3 <= 2f * num2 * mBox.Extents[i2])
					{
						float num4 = num3 / num2;
						num2 += dir[i2] * dir[i2];
						num3 = vector[i2] - num4;
						num5 = dir[i0] * PmE[i0] + dir[i1] * vector[i1] + dir[i2] * num3;
						num6 = -num5 / num2;
						sqrDistance += PmE[i0] * PmE[i0] + vector[i1] * vector[i1] + num3 * num3 + num5 * num6;
						mLineParameter = num6;
						pnt[i0] = mBox.Extents[i0];
						pnt[i1] = -mBox.Extents[i1];
						pnt[i2] = num4 - mBox.Extents[i2];
						return;
					}
					num2 += dir[i2] * dir[i2];
					num5 = dir[i0] * PmE[i0] + dir[i1] * vector[i1] + dir[i2] * PmE[i2];
					num6 = -num5 / num2;
					sqrDistance += PmE[i0] * PmE[i0] + vector[i1] * vector[i1] + PmE[i2] * PmE[i2] + num5 * num6;
					mLineParameter = num6;
					pnt[i0] = mBox.Extents[i0];
					pnt[i1] = -mBox.Extents[i1];
					pnt[i2] = mBox.Extents[i2];
					return;
				}
			}
		}

		private static void CaseNoZeros(ref Box3 mBox, ref float mLineParameter, ref Vector3 pnt, ref Vector3 dir, ref float sqrDistance)
		{
			Vector3 vector = new Vector3(pnt.x - mBox.Extents[0], pnt.y - mBox.Extents[1], pnt.z - mBox.Extents[2]);
			float num = dir.x * vector.y;
			float num2 = dir.y * vector.x;
			if (num2 >= num)
			{
				float num3 = dir.z * vector.x;
				float num4 = dir.x * vector.z;
				if (num3 >= num4)
				{
					Distance.Face(ref mBox, ref mLineParameter, 0, 1, 2, ref pnt, ref dir, ref vector, ref sqrDistance);
					return;
				}
				Distance.Face(ref mBox, ref mLineParameter, 2, 0, 1, ref pnt, ref dir, ref vector, ref sqrDistance);
				return;
			}
			else
			{
				float num5 = dir.z * vector.y;
				float num6 = dir.y * vector.z;
				if (num5 >= num6)
				{
					Distance.Face(ref mBox, ref mLineParameter, 1, 2, 0, ref pnt, ref dir, ref vector, ref sqrDistance);
					return;
				}
				Distance.Face(ref mBox, ref mLineParameter, 2, 0, 1, ref pnt, ref dir, ref vector, ref sqrDistance);
				return;
			}
		}

		private static void Case0(ref Box3 mBox, ref float mLineParameter, int i0, int i1, int i2, ref Vector3 pnt, ref Vector3 dir, ref float sqrDistance)
		{
			float num = pnt[i0] - mBox.Extents[i0];
			float num2 = pnt[i1] - mBox.Extents[i1];
			float num3 = dir[i1] * num;
			float num4 = dir[i0] * num2;
			if (num3 >= num4)
			{
				pnt[i0] = mBox.Extents[i0];
				float num5 = pnt[i1] + mBox.Extents[i1];
				float num6 = num3 - dir[i0] * num5;
				if (num6 >= 0f)
				{
					float num7 = 1f / (dir[i0] * dir[i0] + dir[i1] * dir[i1]);
					sqrDistance += num6 * num6 * num7;
					pnt[i1] = -mBox.Extents[i1];
					mLineParameter = -(dir[i0] * num + dir[i1] * num5) * num7;
				}
				else
				{
					float num8 = 1f / dir[i0];
					pnt[i1] -= num3 * num8;
					mLineParameter = -num * num8;
				}
			}
			else
			{
				pnt[i1] = mBox.Extents[i1];
				float num9 = pnt[i0] + mBox.Extents[i0];
				float num6 = num4 - dir[i1] * num9;
				if (num6 >= 0f)
				{
					float num7 = 1f / (dir[i0] * dir[i0] + dir[i1] * dir[i1]);
					sqrDistance += num6 * num6 * num7;
					pnt[i0] = -mBox.Extents[i0];
					mLineParameter = -(dir[i0] * num9 + dir[i1] * num2) * num7;
				}
				else
				{
					float num8 = 1f / dir[i1];
					pnt[i0] -= num4 * num8;
					mLineParameter = -num2 * num8;
				}
			}
			if (pnt[i2] < -mBox.Extents[i2])
			{
				float num6 = pnt[i2] + mBox.Extents[i2];
				sqrDistance += num6 * num6;
				pnt[i2] = -mBox.Extents[i2];
				return;
			}
			if (pnt[i2] > mBox.Extents[i2])
			{
				float num6 = pnt[i2] - mBox.Extents[i2];
				sqrDistance += num6 * num6;
				pnt[i2] = mBox.Extents[i2];
			}
		}

		private static void Case00(ref Box3 mBox, ref float mLineParameter, int i0, int i1, int i2, ref Vector3 pnt, ref Vector3 dir, ref float sqrDistance)
		{
			mLineParameter = (mBox.Extents[i0] - pnt[i0]) / dir[i0];
			pnt[i0] = mBox.Extents[i0];
			if (pnt[i1] < -mBox.Extents[i1])
			{
				float num = pnt[i1] + mBox.Extents[i1];
				sqrDistance += num * num;
				pnt[i1] = -mBox.Extents[i1];
			}
			else if (pnt[i1] > mBox.Extents[i1])
			{
				float num = pnt[i1] - mBox.Extents[i1];
				sqrDistance += num * num;
				pnt[i1] = mBox.Extents[i1];
			}
			if (pnt[i2] < -mBox.Extents[i2])
			{
				float num = pnt[i2] + mBox.Extents[i2];
				sqrDistance += num * num;
				pnt[i2] = -mBox.Extents[i2];
				return;
			}
			if (pnt[i2] > mBox.Extents[i2])
			{
				float num = pnt[i2] - mBox.Extents[i2];
				sqrDistance += num * num;
				pnt[i2] = mBox.Extents[i2];
			}
		}

		private static void Case000(ref Box3 mBox, ref float mLineParameter, ref Vector3 pnt, ref float sqrDistance)
		{
			if (pnt.x < -mBox.Extents[0])
			{
				float num = pnt.x + mBox.Extents[0];
				sqrDistance += num * num;
				pnt.x = -mBox.Extents[0];
			}
			else if (pnt.x > mBox.Extents[0])
			{
				float num = pnt.x - mBox.Extents[0];
				sqrDistance += num * num;
				pnt.x = mBox.Extents[0];
			}
			if (pnt.y < -mBox.Extents[1])
			{
				float num = pnt.y + mBox.Extents[1];
				sqrDistance += num * num;
				pnt.y = -mBox.Extents[1];
			}
			else if (pnt.y > mBox.Extents[1])
			{
				float num = pnt.y - mBox.Extents[1];
				sqrDistance += num * num;
				pnt.y = mBox.Extents[1];
			}
			if (pnt.z < -mBox.Extents[2])
			{
				float num = pnt.z + mBox.Extents[2];
				sqrDistance += num * num;
				pnt.z = -mBox.Extents[2];
				return;
			}
			if (pnt.z > mBox.Extents[2])
			{
				float num = pnt.z - mBox.Extents[2];
				sqrDistance += num * num;
				pnt.z = mBox.Extents[2];
			}
		}

		public static float Line3Box3(ref Line3 line, ref Box3 box, out Line3Box3Dist info)
		{
			return Mathf.Sqrt(Distance.SqrLine3Box3(ref line, ref box, out info));
		}

		public static float Line3Box3(ref Line3 line, ref Box3 box)
		{
			Line3Box3Dist line3Box3Dist;
			return Mathf.Sqrt(Distance.SqrLine3Box3(ref line, ref box, out line3Box3Dist));
		}

		public static float SqrLine3Box3(ref Line3 line, ref Box3 box, out Line3Box3Dist info)
		{
			Vector3 vector = line.Center - box.Center;
			Vector3 vector2 = new Vector3(vector.Dot(box.Axis0), vector.Dot(box.Axis1), vector.Dot(box.Axis2));
			Vector3 vector3 = new Vector3(line.Direction.Dot(box.Axis0), line.Direction.Dot(box.Axis1), line.Direction.Dot(box.Axis2));
			bool flag;
			if (vector3.x < 0f)
			{
				vector2.x = -vector2.x;
				vector3.x = -vector3.x;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2;
			if (vector3.y < 0f)
			{
				vector2.y = -vector2.y;
				vector3.y = -vector3.y;
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			bool flag3;
			if (vector3.z < 0f)
			{
				vector2.z = -vector2.z;
				vector3.z = -vector3.z;
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			float result = 0f;
			float num = 0f;
			if (vector3.x > 0f)
			{
				if (vector3.y > 0f)
				{
					if (vector3.z > 0f)
					{
						Distance.CaseNoZeros(ref box, ref num, ref vector2, ref vector3, ref result);
					}
					else
					{
						Distance.Case0(ref box, ref num, 0, 1, 2, ref vector2, ref vector3, ref result);
					}
				}
				else if (vector3.z > 0f)
				{
					Distance.Case0(ref box, ref num, 0, 2, 1, ref vector2, ref vector3, ref result);
				}
				else
				{
					Distance.Case00(ref box, ref num, 0, 1, 2, ref vector2, ref vector3, ref result);
				}
			}
			else if (vector3.y > 0f)
			{
				if (vector3.z > 0f)
				{
					Distance.Case0(ref box, ref num, 1, 2, 0, ref vector2, ref vector3, ref result);
				}
				else
				{
					Distance.Case00(ref box, ref num, 1, 0, 2, ref vector2, ref vector3, ref result);
				}
			}
			else if (vector3.z > 0f)
			{
				Distance.Case00(ref box, ref num, 2, 0, 1, ref vector2, ref vector3, ref result);
			}
			else
			{
				Distance.Case000(ref box, ref num, ref vector2, ref result);
			}
			Vector3 closestPoint = line.Center + num * line.Direction;
			Vector3 vector4 = box.Center;
			if (flag)
			{
				vector2.x = -vector2.x;
			}
			vector4 += vector2.x * box.Axis0;
			if (flag2)
			{
				vector2.y = -vector2.y;
			}
			vector4 += vector2.y * box.Axis1;
			if (flag3)
			{
				vector2.z = -vector2.z;
			}
			vector4 += vector2.z * box.Axis2;
			info.ClosestPoint0 = closestPoint;
			info.ClosestPoint1 = vector4;
			info.LineParameter = num;
			return result;
		}

		public static float SqrLine3Box3(ref Line3 line, ref Box3 box)
		{
			Line3Box3Dist line3Box3Dist;
			return Distance.SqrLine3Box3(ref line, ref box, out line3Box3Dist);
		}

		public static float Line3Line3(ref Line3 line0, ref Line3 line1)
		{
			Vector3 vector;
			Vector3 vector2;
			return Mathf.Sqrt(Distance.SqrLine3Line3(ref line0, ref line1, out vector, out vector2));
		}

		public static float Line3Line3(ref Line3 line0, ref Line3 line1, out Vector3 closestPoint0, out Vector3 closestPoint1)
		{
			return Mathf.Sqrt(Distance.SqrLine3Line3(ref line0, ref line1, out closestPoint0, out closestPoint1));
		}

		public static float SqrLine3Line3(ref Line3 line0, ref Line3 line1)
		{
			Vector3 vector;
			Vector3 vector2;
			return Distance.SqrLine3Line3(ref line0, ref line1, out vector, out vector2);
		}

		public static float SqrLine3Line3(ref Line3 line0, ref Line3 line1, out Vector3 closestPoint0, out Vector3 closestPoint1)
		{
			Vector3 vector = line0.Center - line1.Center;
			float num = -line0.Direction.Dot(line1.Direction);
			float num2 = vector.Dot(line0.Direction);
			float sqrMagnitude = vector.sqrMagnitude;
			float num3 = Mathf.Abs(1f - num * num);
			float num6;
			float num7;
			float num8;
			if (num3 >= 1E-05f)
			{
				float num4 = -vector.Dot(line1.Direction);
				float num5 = 1f / num3;
				num6 = (num * num4 - num2) * num5;
				num7 = (num * num2 - num4) * num5;
				num8 = num6 * (num6 + num * num7 + 2f * num2) + num7 * (num * num6 + num7 + 2f * num4) + sqrMagnitude;
			}
			else
			{
				num6 = -num2;
				num7 = 0f;
				num8 = num2 * num6 + sqrMagnitude;
			}
			closestPoint0 = line0.Center + num6 * line0.Direction;
			closestPoint1 = line1.Center + num7 * line1.Direction;
			if (num8 < 0f)
			{
				num8 = 0f;
			}
			return num8;
		}

		public static float Line3Ray3(ref Line3 line, ref Ray3 ray)
		{
			Vector3 vector;
			Vector3 vector2;
			return Mathf.Sqrt(Distance.SqrLine3Ray3(ref line, ref ray, out vector, out vector2));
		}

		public static float Line3Ray3(ref Line3 line, ref Ray3 ray, out Vector3 closestPoint0, out Vector3 closestPoint1)
		{
			return Mathf.Sqrt(Distance.SqrLine3Ray3(ref line, ref ray, out closestPoint0, out closestPoint1));
		}

		public static float SqrLine3Ray3(ref Line3 line, ref Ray3 ray)
		{
			Vector3 vector;
			Vector3 vector2;
			return Distance.SqrLine3Ray3(ref line, ref ray, out vector, out vector2);
		}

		public static float SqrLine3Ray3(ref Line3 line, ref Ray3 ray, out Vector3 closestPoint0, out Vector3 closestPoint1)
		{
			Vector3 vector = line.Center - ray.Center;
			float num = -line.Direction.Dot(ray.Direction);
			float num2 = vector.Dot(line.Direction);
			float sqrMagnitude = vector.sqrMagnitude;
			float num3 = Mathf.Abs(1f - num * num);
			float num5;
			float num7;
			float num8;
			if (num3 >= 1E-05f)
			{
				float num4 = -vector.Dot(ray.Direction);
				num5 = num * num2 - num4;
				if (num5 >= 0f)
				{
					float num6 = 1f / num3;
					num7 = (num * num4 - num2) * num6;
					num5 *= num6;
					num8 = num7 * (num7 + num * num5 + 2f * num2) + num5 * (num * num7 + num5 + 2f * num4) + sqrMagnitude;
				}
				else
				{
					num7 = -num2;
					num5 = 0f;
					num8 = num2 * num7 + sqrMagnitude;
				}
			}
			else
			{
				num7 = -num2;
				num5 = 0f;
				num8 = num2 * num7 + sqrMagnitude;
			}
			closestPoint0 = line.Center + num7 * line.Direction;
			closestPoint1 = ray.Center + num5 * ray.Direction;
			if (num8 < 0f)
			{
				num8 = 0f;
			}
			return num8;
		}

		public static float Line3Segment3(ref Line3 line, ref Segment3 segment)
		{
			Vector3 vector;
			Vector3 vector2;
			return Mathf.Sqrt(Distance.SqrLine3Segment3(ref line, ref segment, out vector, out vector2));
		}

		public static float Line3Segment3(ref Line3 line, ref Segment3 segment, out Vector3 closestPoint0, out Vector3 closestPoint1)
		{
			return Mathf.Sqrt(Distance.SqrLine3Segment3(ref line, ref segment, out closestPoint0, out closestPoint1));
		}

		public static float SqrLine3Segment3(ref Line3 line, ref Segment3 segment)
		{
			Vector3 vector;
			Vector3 vector2;
			return Distance.SqrLine3Segment3(ref line, ref segment, out vector, out vector2);
		}

		public static float SqrLine3Segment3(ref Line3 line, ref Segment3 segment, out Vector3 closestPoint0, out Vector3 closestPoint1)
		{
			Vector3 vector = line.Center - segment.Center;
			float num = -line.Direction.Dot(segment.Direction);
			float num2 = vector.Dot(line.Direction);
			float sqrMagnitude = vector.sqrMagnitude;
			float num3 = Mathf.Abs(1f - num * num);
			float num5;
			float num8;
			float num9;
			if (num3 >= 1E-05f)
			{
				float num4 = -vector.Dot(segment.Direction);
				num5 = num * num2 - num4;
				float num6 = segment.Extent * num3;
				if (num5 >= -num6)
				{
					if (num5 <= num6)
					{
						float num7 = 1f / num3;
						num8 = (num * num4 - num2) * num7;
						num5 *= num7;
						num9 = num8 * (num8 + num * num5 + 2f * num2) + num5 * (num * num8 + num5 + 2f * num4) + sqrMagnitude;
					}
					else
					{
						num5 = segment.Extent;
						num8 = -(num * num5 + num2);
						num9 = -num8 * num8 + num5 * (num5 + 2f * num4) + sqrMagnitude;
					}
				}
				else
				{
					num5 = -segment.Extent;
					num8 = -(num * num5 + num2);
					num9 = -num8 * num8 + num5 * (num5 + 2f * num4) + sqrMagnitude;
				}
			}
			else
			{
				num5 = 0f;
				num8 = -num2;
				num9 = num2 * num8 + sqrMagnitude;
			}
			closestPoint0 = line.Center + num8 * line.Direction;
			closestPoint1 = segment.Center + num5 * segment.Direction;
			if (num9 < 0f)
			{
				num9 = 0f;
			}
			return num9;
		}

		public static float Point3AAB3(ref Vector3 point, ref AAB3 box)
		{
			float num = 0f;
			float num2 = point.x;
			if (num2 < box.Min.x)
			{
				float num3 = box.Min.x - num2;
				num += num3 * num3;
			}
			else if (num2 > box.Max.x)
			{
				float num3 = num2 - box.Max.x;
				num += num3 * num3;
			}
			num2 = point.y;
			if (num2 < box.Min.y)
			{
				float num3 = box.Min.y - num2;
				num += num3 * num3;
			}
			else if (num2 > box.Max.y)
			{
				float num3 = num2 - box.Max.y;
				num += num3 * num3;
			}
			num2 = point.z;
			if (num2 < box.Min.z)
			{
				float num3 = box.Min.z - num2;
				num += num3 * num3;
			}
			else if (num2 > box.Max.z)
			{
				float num3 = num2 - box.Max.z;
				num += num3 * num3;
			}
			return Mathf.Sqrt(num);
		}

		public static float Point3AAB3(ref Vector3 point, ref AAB3 box, out Vector3 closestPoint)
		{
			float num = 0f;
			closestPoint = point;
			float num2 = point.x;
			if (num2 < box.Min.x)
			{
				float num3 = box.Min.x - num2;
				num += num3 * num3;
				closestPoint.x += num3;
			}
			else if (num2 > box.Max.x)
			{
				float num3 = num2 - box.Max.x;
				num += num3 * num3;
				closestPoint.x -= num3;
			}
			num2 = point.y;
			if (num2 < box.Min.y)
			{
				float num3 = box.Min.y - num2;
				num += num3 * num3;
				closestPoint.y += num3;
			}
			else if (num2 > box.Max.y)
			{
				float num3 = num2 - box.Max.y;
				num += num3 * num3;
				closestPoint.y -= num3;
			}
			num2 = point.z;
			if (num2 < box.Min.z)
			{
				float num3 = box.Min.z - num2;
				num += num3 * num3;
				closestPoint.z += num3;
			}
			else if (num2 > box.Max.z)
			{
				float num3 = num2 - box.Max.z;
				num += num3 * num3;
				closestPoint.z -= num3;
			}
			return Mathf.Sqrt(num);
		}

		public static float SqrPoint3AAB3(ref Vector3 point, ref AAB3 box)
		{
			float num = 0f;
			float num2 = point.x;
			if (num2 < box.Min.x)
			{
				float num3 = box.Min.x - num2;
				num += num3 * num3;
			}
			else if (num2 > box.Max.x)
			{
				float num3 = num2 - box.Max.x;
				num += num3 * num3;
			}
			num2 = point.y;
			if (num2 < box.Min.y)
			{
				float num3 = box.Min.y - num2;
				num += num3 * num3;
			}
			else if (num2 > box.Max.y)
			{
				float num3 = num2 - box.Max.y;
				num += num3 * num3;
			}
			num2 = point.z;
			if (num2 < box.Min.z)
			{
				float num3 = box.Min.z - num2;
				num += num3 * num3;
			}
			else if (num2 > box.Max.z)
			{
				float num3 = num2 - box.Max.z;
				num += num3 * num3;
			}
			return num;
		}

		public static float SqrPoint3AAB3(ref Vector3 point, ref AAB3 box, out Vector3 closestPoint)
		{
			float num = 0f;
			closestPoint = point;
			float num2 = point.x;
			if (num2 < box.Min.x)
			{
				float num3 = box.Min.x - num2;
				num += num3 * num3;
				closestPoint.x += num3;
			}
			else if (num2 > box.Max.x)
			{
				float num3 = num2 - box.Max.x;
				num += num3 * num3;
				closestPoint.x -= num3;
			}
			num2 = point.y;
			if (num2 < box.Min.y)
			{
				float num3 = box.Min.y - num2;
				num += num3 * num3;
				closestPoint.y += num3;
			}
			else if (num2 > box.Max.y)
			{
				float num3 = num2 - box.Max.y;
				num += num3 * num3;
				closestPoint.y -= num3;
			}
			num2 = point.z;
			if (num2 < box.Min.z)
			{
				float num3 = box.Min.z - num2;
				num += num3 * num3;
				closestPoint.z += num3;
			}
			else if (num2 > box.Max.z)
			{
				float num3 = num2 - box.Max.z;
				num += num3 * num3;
				closestPoint.z -= num3;
			}
			return num;
		}

		public static float Point3Box3(ref Vector3 point, ref Box3 box)
		{
			return Mathf.Sqrt(Distance.SqrPoint3Box3(ref point, ref box));
		}

		public static float Point3Box3(ref Vector3 point, ref Box3 box, out Vector3 closestPoint)
		{
			return Mathf.Sqrt(Distance.SqrPoint3Box3(ref point, ref box, out closestPoint));
		}

		public static float SqrPoint3Box3(ref Vector3 point, ref Box3 box)
		{
			Vector3 vector = point - box.Center;
			float num = 0f;
			float num2 = vector.Dot(box.Axis0);
			float num3 = box.Extents.x;
			if (num2 < -num3)
			{
				float num4 = num2 + num3;
				num += num4 * num4;
			}
			else if (num2 > num3)
			{
				float num4 = num2 - num3;
				num += num4 * num4;
			}
			float num5 = vector.Dot(box.Axis1);
			num3 = box.Extents.y;
			if (num5 < -num3)
			{
				float num4 = num5 + num3;
				num += num4 * num4;
			}
			else if (num5 > num3)
			{
				float num4 = num5 - num3;
				num += num4 * num4;
			}
			float num6 = vector.Dot(box.Axis2);
			num3 = box.Extents.z;
			if (num6 < -num3)
			{
				float num4 = num6 + num3;
				num += num4 * num4;
			}
			else if (num6 > num3)
			{
				float num4 = num6 - num3;
				num += num4 * num4;
			}
			return num;
		}

		public static float SqrPoint3Box3(ref Vector3 point, ref Box3 box, out Vector3 closestPoint)
		{
			Vector3 vector = point - box.Center;
			float num = 0f;
			float num2 = vector.Dot(box.Axis0);
			float num3 = box.Extents.x;
			if (num2 < -num3)
			{
				float num4 = num2 + num3;
				num += num4 * num4;
				num2 = -num3;
			}
			else if (num2 > num3)
			{
				float num4 = num2 - num3;
				num += num4 * num4;
				num2 = num3;
			}
			float num5 = vector.Dot(box.Axis1);
			num3 = box.Extents.y;
			if (num5 < -num3)
			{
				float num4 = num5 + num3;
				num += num4 * num4;
				num5 = -num3;
			}
			else if (num5 > num3)
			{
				float num4 = num5 - num3;
				num += num4 * num4;
				num5 = num3;
			}
			float num6 = vector.Dot(box.Axis2);
			num3 = box.Extents.z;
			if (num6 < -num3)
			{
				float num4 = num6 + num3;
				num += num4 * num4;
				num6 = -num3;
			}
			else if (num6 > num3)
			{
				float num4 = num6 - num3;
				num += num4 * num4;
				num6 = num3;
			}
			closestPoint = box.Center + num2 * box.Axis0 + num5 * box.Axis1 + num6 * box.Axis2;
			return num;
		}

		public static float Point3Circle3(ref Vector3 point, ref Circle3 circle, bool solid = true)
		{
			Vector3 vector;
			return Mathf.Sqrt(Distance.SqrPoint3Circle3(ref point, ref circle, out vector, solid));
		}

		public static float Point3Circle3(ref Vector3 point, ref Circle3 circle, out Vector3 closestPoint, bool solid = true)
		{
			return Mathf.Sqrt(Distance.SqrPoint3Circle3(ref point, ref circle, out closestPoint, solid));
		}

		public static float SqrPoint3Circle3(ref Vector3 point, ref Circle3 circle, bool solid = true)
		{
			Vector3 vector;
			return Distance.SqrPoint3Circle3(ref point, ref circle, out vector, solid);
		}

		public static float SqrPoint3Circle3(ref Vector3 point, ref Circle3 circle, out Vector3 closestPoint, bool solid = true)
		{
			if (solid)
			{
				Vector3 vector = point - circle.Center;
				float num = vector.Dot(circle.Normal);
				Vector3 vector2 = vector - num * circle.Normal;
				float sqrMagnitude = vector2.sqrMagnitude;
				float result;
				if (sqrMagnitude > circle.Radius)
				{
					closestPoint = circle.Center + circle.Radius / Mathf.Sqrt(sqrMagnitude) * vector2;
					result = (point - closestPoint).sqrMagnitude;
				}
				else
				{
					closestPoint = circle.Center + vector2;
					result = num * num;
				}
				return result;
			}
			Vector3 vector3 = point - circle.Center;
			float num2 = vector3.Dot(circle.Normal);
			Vector3 a = vector3 - num2 * circle.Normal;
			float sqrMagnitude2 = a.sqrMagnitude;
			float result2;
			if (sqrMagnitude2 >= 1E-05f)
			{
				closestPoint = circle.Center + circle.Radius / Mathf.Sqrt(sqrMagnitude2) * a;
				result2 = (point - closestPoint).sqrMagnitude;
			}
			else
			{
				closestPoint = circle.Eval(0f);
				result2 = circle.Radius * circle.Radius + num2 * num2;
			}
			return result2;
		}

		public static float Point3Line3(ref Vector3 point, ref Line3 line)
		{
			return Mathf.Sqrt(Distance.SqrPoint3Line3(ref point, ref line));
		}

		public static float Point3Line3(ref Vector3 point, ref Line3 line, out Vector3 closestPoint)
		{
			return Mathf.Sqrt(Distance.SqrPoint3Line3(ref point, ref line, out closestPoint));
		}

		public static float SqrPoint3Line3(ref Vector3 point, ref Line3 line)
		{
			Vector3 value = point - line.Center;
			float d = line.Direction.Dot(value);
			Vector3 a = line.Center + d * line.Direction;
			return (a - point).sqrMagnitude;
		}

		public static float SqrPoint3Line3(ref Vector3 point, ref Line3 line, out Vector3 closestPoint)
		{
			Vector3 value = point - line.Center;
			float d = line.Direction.Dot(value);
			closestPoint = line.Center + d * line.Direction;
			return (closestPoint - point).sqrMagnitude;
		}

		public static float Point3Plane3(ref Vector3 point, ref Plane3 plane)
		{
			float f = plane.Normal.Dot(point) - plane.Constant;
			return Mathf.Abs(f);
		}

		public static float Point3Plane3(ref Vector3 point, ref Plane3 plane, out Vector3 closestPoint)
		{
			float num = plane.Normal.Dot(point) - plane.Constant;
			closestPoint = point - num * plane.Normal;
			return Mathf.Abs(num);
		}

		public static float SqrPoint3Plane3(ref Vector3 point, ref Plane3 plane)
		{
			float num = plane.Normal.Dot(point) - plane.Constant;
			return num * num;
		}

		public static float SqrPoint3Plane3(ref Vector3 point, ref Plane3 plane, out Vector3 closestPoint)
		{
			float num = plane.Normal.Dot(point) - plane.Constant;
			closestPoint = point - num * plane.Normal;
			return num * num;
		}

		public static float Point3Ray3(ref Vector3 point, ref Ray3 ray)
		{
			return Mathf.Sqrt(Distance.SqrPoint3Ray3(ref point, ref ray));
		}

		public static float Point3Ray3(ref Vector3 point, ref Ray3 ray, out Vector3 closestPoint)
		{
			return Mathf.Sqrt(Distance.SqrPoint3Ray3(ref point, ref ray, out closestPoint));
		}

		public static float SqrPoint3Ray3(ref Vector3 point, ref Ray3 ray)
		{
			Vector3 value = point - ray.Center;
			float num = ray.Direction.Dot(value);
			Vector3 a;
			if (num > 0f)
			{
				a = ray.Center + num * ray.Direction;
			}
			else
			{
				a = ray.Center;
			}
			return (a - point).sqrMagnitude;
		}

		public static float SqrPoint3Ray3(ref Vector3 point, ref Ray3 ray, out Vector3 closestPoint)
		{
			Vector3 value = point - ray.Center;
			float num = ray.Direction.Dot(value);
			if (num > 0f)
			{
				closestPoint = ray.Center + num * ray.Direction;
			}
			else
			{
				closestPoint = ray.Center;
			}
			return (closestPoint - point).sqrMagnitude;
		}

		public static float Point3Rectangle3(ref Vector3 point, ref Rectangle3 rectangle)
		{
			return Mathf.Sqrt(Distance.SqrPoint3Rectangle3(ref point, ref rectangle));
		}

		public static float Point3Rectangle3(ref Vector3 point, ref Rectangle3 rectangle, out Vector3 closestPoint)
		{
			return Mathf.Sqrt(Distance.SqrPoint3Rectangle3(ref point, ref rectangle, out closestPoint));
		}

		public static float SqrPoint3Rectangle3(ref Vector3 point, ref Rectangle3 rectangle)
		{
			Vector3 vector = rectangle.Center - point;
			float num = vector.Dot(rectangle.Axis0);
			float num2 = vector.Dot(rectangle.Axis1);
			float num3 = -num;
			float num4 = -num2;
			float num5 = vector.sqrMagnitude;
			float num6 = rectangle.Extents.x;
			if (num3 < -num6)
			{
				num3 = -num6;
			}
			else if (num3 > num6)
			{
				num3 = num6;
			}
			num5 += num3 * (num3 + 2f * num);
			num6 = rectangle.Extents.y;
			if (num4 < -num6)
			{
				num4 = -num6;
			}
			else if (num4 > num6)
			{
				num4 = num6;
			}
			num5 += num4 * (num4 + 2f * num2);
			if (num5 < 0f)
			{
				num5 = 0f;
			}
			return num5;
		}

		public static float SqrPoint3Rectangle3(ref Vector3 point, ref Rectangle3 rectangle, out Vector3 closestPoint)
		{
			Vector3 vector = rectangle.Center - point;
			float num = vector.Dot(rectangle.Axis0);
			float num2 = vector.Dot(rectangle.Axis1);
			float num3 = -num;
			float num4 = -num2;
			float num5 = vector.sqrMagnitude;
			float num6 = rectangle.Extents.x;
			if (num3 < -num6)
			{
				num3 = -num6;
			}
			else if (num3 > num6)
			{
				num3 = num6;
			}
			num5 += num3 * (num3 + 2f * num);
			num6 = rectangle.Extents.y;
			if (num4 < -num6)
			{
				num4 = -num6;
			}
			else if (num4 > num6)
			{
				num4 = num6;
			}
			num5 += num4 * (num4 + 2f * num2);
			if (num5 < 0f)
			{
				num5 = 0f;
			}
			closestPoint = rectangle.Center + num3 * rectangle.Axis0 + num4 * rectangle.Axis1;
			return num5;
		}

		public static float Point3Segment3(ref Vector3 point, ref Segment3 segment)
		{
			return Mathf.Sqrt(Distance.SqrPoint3Segment3(ref point, ref segment));
		}

		public static float Point3Segment3(ref Vector3 point, ref Segment3 segment, out Vector3 closestPoint)
		{
			return Mathf.Sqrt(Distance.SqrPoint3Segment3(ref point, ref segment, out closestPoint));
		}

		public static float SqrPoint3Segment3(ref Vector3 point, ref Segment3 segment)
		{
			Vector3 value = point - segment.Center;
			float num = segment.Direction.Dot(value);
			Vector3 a;
			if (-segment.Extent < num)
			{
				if (num < segment.Extent)
				{
					a = segment.Center + num * segment.Direction;
				}
				else
				{
					a = segment.P1;
				}
			}
			else
			{
				a = segment.P0;
			}
			return (a - point).sqrMagnitude;
		}

		public static float SqrPoint3Segment3(ref Vector3 point, ref Segment3 segment, out Vector3 closestPoint)
		{
			Vector3 value = point - segment.Center;
			float num = segment.Direction.Dot(value);
			if (-segment.Extent < num)
			{
				if (num < segment.Extent)
				{
					closestPoint = segment.Center + num * segment.Direction;
				}
				else
				{
					closestPoint = segment.P1;
				}
			}
			else
			{
				closestPoint = segment.P0;
			}
			return (closestPoint - point).sqrMagnitude;
		}

		public static float Point3Sphere3(ref Vector3 point, ref Sphere3 sphere)
		{
			float num = (point - sphere.Center).magnitude - sphere.Radius;
			if (num <= 0f)
			{
				return 0f;
			}
			return num;
		}

		public static float Point3Sphere3(ref Vector3 point, ref Sphere3 sphere, out Vector3 closestPoint)
		{
			Vector3 a = point - sphere.Center;
			float sqrMagnitude = a.sqrMagnitude;
			if (sqrMagnitude > sphere.Radius * sphere.Radius)
			{
				float num = Mathf.Sqrt(sqrMagnitude);
				closestPoint = sphere.Center + a * (sphere.Radius / num);
				return num - sphere.Radius;
			}
			closestPoint = point;
			return 0f;
		}

		public static float SqrPoint3Sphere3(ref Vector3 point, ref Sphere3 sphere)
		{
			float num = (point - sphere.Center).magnitude - sphere.Radius;
			if (num <= 0f)
			{
				return 0f;
			}
			return num * num;
		}

		public static float SqrPoint3Sphere3(ref Vector3 point, ref Sphere3 sphere, out Vector3 closestPoint)
		{
			Vector3 a = point - sphere.Center;
			float sqrMagnitude = a.sqrMagnitude;
			if (sqrMagnitude > sphere.Radius * sphere.Radius)
			{
				float num = Mathf.Sqrt(sqrMagnitude);
				closestPoint = sphere.Center + a * (sphere.Radius / num);
				float num2 = num - sphere.Radius;
				return num2 * num2;
			}
			closestPoint = point;
			return 0f;
		}

		public static float Ray3Ray3(ref Ray3 ray0, ref Ray3 ray1)
		{
			Vector3 vector;
			Vector3 vector2;
			return Mathf.Sqrt(Distance.SqrRay3Ray3(ref ray0, ref ray1, out vector, out vector2));
		}

		public static float Ray3Ray3(ref Ray3 ray0, ref Ray3 ray1, out Vector3 closestPoint0, out Vector3 closestPoint1)
		{
			return Mathf.Sqrt(Distance.SqrRay3Ray3(ref ray0, ref ray1, out closestPoint0, out closestPoint1));
		}

		public static float SqrRay3Ray3(ref Ray3 ray0, ref Ray3 ray1)
		{
			Vector3 vector;
			Vector3 vector2;
			return Distance.SqrRay3Ray3(ref ray0, ref ray1, out vector, out vector2);
		}

		public static float SqrRay3Ray3(ref Ray3 ray0, ref Ray3 ray1, out Vector3 closestPoint0, out Vector3 closestPoint1)
		{
			Vector3 vector = ray0.Center - ray1.Center;
			float num = -ray0.Direction.Dot(ray1.Direction);
			float num2 = vector.Dot(ray0.Direction);
			float sqrMagnitude = vector.sqrMagnitude;
			float num3 = Mathf.Abs(1f - num * num);
			float num5;
			float num6;
			float num8;
			if (num3 >= 1E-05f)
			{
				float num4 = -vector.Dot(ray1.Direction);
				num5 = num * num4 - num2;
				num6 = num * num2 - num4;
				if (num5 >= 0f)
				{
					if (num6 >= 0f)
					{
						float num7 = 1f / num3;
						num5 *= num7;
						num6 *= num7;
						num8 = num5 * (num5 + num * num6 + 2f * num2) + num6 * (num * num5 + num6 + 2f * num4) + sqrMagnitude;
					}
					else
					{
						num6 = 0f;
						if (num2 >= 0f)
						{
							num5 = 0f;
							num8 = sqrMagnitude;
						}
						else
						{
							num5 = -num2;
							num8 = num2 * num5 + sqrMagnitude;
						}
					}
				}
				else if (num6 >= 0f)
				{
					num5 = 0f;
					if (num4 >= 0f)
					{
						num6 = 0f;
						num8 = sqrMagnitude;
					}
					else
					{
						num6 = -num4;
						num8 = num4 * num6 + sqrMagnitude;
					}
				}
				else if (num2 < 0f)
				{
					num5 = -num2;
					num6 = 0f;
					num8 = num2 * num5 + sqrMagnitude;
				}
				else
				{
					num5 = 0f;
					if (num4 >= 0f)
					{
						num6 = 0f;
						num8 = sqrMagnitude;
					}
					else
					{
						num6 = -num4;
						num8 = num4 * num6 + sqrMagnitude;
					}
				}
			}
			else if (num > 0f)
			{
				num6 = 0f;
				if (num2 >= 0f)
				{
					num5 = 0f;
					num8 = sqrMagnitude;
				}
				else
				{
					num5 = -num2;
					num8 = num2 * num5 + sqrMagnitude;
				}
			}
			else if (num2 >= 0f)
			{
				float num4 = -vector.Dot(ray1.Direction);
				num5 = 0f;
				num6 = -num4;
				num8 = num4 * num6 + sqrMagnitude;
			}
			else
			{
				num5 = -num2;
				num6 = 0f;
				num8 = num2 * num5 + sqrMagnitude;
			}
			closestPoint0 = ray0.Center + num5 * ray0.Direction;
			closestPoint1 = ray1.Center + num6 * ray1.Direction;
			if (num8 < 0f)
			{
				num8 = 0f;
			}
			return num8;
		}

		public static float Ray3Segment3(ref Ray3 ray, ref Segment3 segment)
		{
			Vector3 vector;
			Vector3 vector2;
			return Mathf.Sqrt(Distance.SqrRay3Segment3(ref ray, ref segment, out vector, out vector2));
		}

		public static float Ray3Segment3(ref Ray3 ray, ref Segment3 segment, out Vector3 closestPoint0, out Vector3 closestPoint1)
		{
			return Mathf.Sqrt(Distance.SqrRay3Segment3(ref ray, ref segment, out closestPoint0, out closestPoint1));
		}

		public static float SqrRay3Segment3(ref Ray3 ray, ref Segment3 segment)
		{
			Vector3 vector;
			Vector3 vector2;
			return Distance.SqrRay3Segment3(ref ray, ref segment, out vector, out vector2);
		}

		public static float SqrRay3Segment3(ref Ray3 ray, ref Segment3 segment, out Vector3 closestPoint0, out Vector3 closestPoint1)
		{
			Vector3 vector = ray.Center - segment.Center;
			float num = -ray.Direction.Dot(segment.Direction);
			float num2 = vector.Dot(ray.Direction);
			float num3 = -vector.Dot(segment.Direction);
			float sqrMagnitude = vector.sqrMagnitude;
			float num4 = Mathf.Abs(1f - num * num);
			float num5;
			float num6;
			float num9;
			if (num4 >= 1E-05f)
			{
				num5 = num * num3 - num2;
				num6 = num * num2 - num3;
				float num7 = segment.Extent * num4;
				if (num5 >= 0f)
				{
					if (num6 >= -num7)
					{
						if (num6 <= num7)
						{
							float num8 = 1f / num4;
							num5 *= num8;
							num6 *= num8;
							num9 = num5 * (num5 + num * num6 + 2f * num2) + num6 * (num * num5 + num6 + 2f * num3) + sqrMagnitude;
						}
						else
						{
							num6 = segment.Extent;
							num5 = -(num * num6 + num2);
							if (num5 > 0f)
							{
								num9 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
							}
							else
							{
								num5 = 0f;
								num9 = num6 * (num6 + 2f * num3) + sqrMagnitude;
							}
						}
					}
					else
					{
						num6 = -segment.Extent;
						num5 = -(num * num6 + num2);
						if (num5 > 0f)
						{
							num9 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
						}
						else
						{
							num5 = 0f;
							num9 = num6 * (num6 + 2f * num3) + sqrMagnitude;
						}
					}
				}
				else if (num6 <= -num7)
				{
					num5 = -(-num * segment.Extent + num2);
					if (num5 > 0f)
					{
						num6 = -segment.Extent;
						num9 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
					}
					else
					{
						num5 = 0f;
						num6 = -num3;
						if (num6 < -segment.Extent)
						{
							num6 = -segment.Extent;
						}
						else if (num6 > segment.Extent)
						{
							num6 = segment.Extent;
						}
						num9 = num6 * (num6 + 2f * num3) + sqrMagnitude;
					}
				}
				else if (num6 <= num7)
				{
					num5 = 0f;
					num6 = -num3;
					if (num6 < -segment.Extent)
					{
						num6 = -segment.Extent;
					}
					else if (num6 > segment.Extent)
					{
						num6 = segment.Extent;
					}
					num9 = num6 * (num6 + 2f * num3) + sqrMagnitude;
				}
				else
				{
					num5 = -(num * segment.Extent + num2);
					if (num5 > 0f)
					{
						num6 = segment.Extent;
						num9 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
					}
					else
					{
						num5 = 0f;
						num6 = -num3;
						if (num6 < -segment.Extent)
						{
							num6 = -segment.Extent;
						}
						else if (num6 > segment.Extent)
						{
							num6 = segment.Extent;
						}
						num9 = num6 * (num6 + 2f * num3) + sqrMagnitude;
					}
				}
			}
			else
			{
				if (num > 0f)
				{
					num6 = -segment.Extent;
				}
				else
				{
					num6 = segment.Extent;
				}
				num5 = -(num * num6 + num2);
				if (num5 > 0f)
				{
					num9 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
				}
				else
				{
					num5 = 0f;
					num9 = num6 * (num6 + 2f * num3) + sqrMagnitude;
				}
			}
			closestPoint0 = ray.Center + num5 * ray.Direction;
			closestPoint1 = segment.Center + num6 * segment.Direction;
			if (num9 < 0f)
			{
				num9 = 0f;
			}
			return num9;
		}

		public static float Segment3Box3(ref Segment3 segment, ref Box3 box, out Vector3 closestPoint0, out Vector3 closestPoint1)
		{
			return Mathf.Sqrt(Distance.SqrSegment3Box3(ref segment, ref box, out closestPoint0, out closestPoint1));
		}

		public static float Segment3Box3(ref Segment3 segment, ref Box3 box)
		{
			Vector3 vector;
			Vector3 vector2;
			return Mathf.Sqrt(Distance.SqrSegment3Box3(ref segment, ref box, out vector, out vector2));
		}

		public static float SqrSegment3Box3(ref Segment3 segment, ref Box3 box, out Vector3 closestPoint0, out Vector3 closestPoint1)
		{
			Line3 line = new Line3(segment.Center, segment.Direction);
			Line3Box3Dist line3Box3Dist;
			float result = Distance.SqrLine3Box3(ref line, ref box, out line3Box3Dist);
			float lineParameter = line3Box3Dist.LineParameter;
			if (lineParameter >= -segment.Extent)
			{
				if (lineParameter <= segment.Extent)
				{
					closestPoint0 = line3Box3Dist.ClosestPoint0;
					closestPoint1 = line3Box3Dist.ClosestPoint1;
				}
				else
				{
					closestPoint0 = segment.P1;
					result = Distance.SqrPoint3Box3(ref segment.P1, ref box, out closestPoint1);
				}
			}
			else
			{
				closestPoint0 = segment.P0;
				result = Distance.SqrPoint3Box3(ref segment.P0, ref box, out closestPoint1);
			}
			return result;
		}

		public static float SqrSegment3Box3(ref Segment3 segment, ref Box3 box)
		{
			Vector3 vector;
			Vector3 vector2;
			return Distance.SqrSegment3Box3(ref segment, ref box, out vector, out vector2);
		}

		public static float Segment3Segment3(ref Segment3 segment0, ref Segment3 segment1)
		{
			Vector3 vector;
			Vector3 vector2;
			return Mathf.Sqrt(Distance.SqrSegment3Segment3(ref segment0, ref segment1, out vector, out vector2));
		}

		public static float Segment3Segment3(ref Segment3 segment0, ref Segment3 segment1, out Vector3 closestPoint0, out Vector3 closestPoint1)
		{
			return Mathf.Sqrt(Distance.SqrSegment3Segment3(ref segment0, ref segment1, out closestPoint0, out closestPoint1));
		}

		public static float SqrSegment3Segment3(ref Segment3 segment0, ref Segment3 segment1)
		{
			Vector3 vector;
			Vector3 vector2;
			return Distance.SqrSegment3Segment3(ref segment0, ref segment1, out vector, out vector2);
		}

		public static float SqrSegment3Segment3(ref Segment3 segment0, ref Segment3 segment1, out Vector3 closestPoint0, out Vector3 closestPoint1)
		{
			Vector3 vector = segment0.Center - segment1.Center;
			float num = -segment0.Direction.Dot(segment1.Direction);
			float num2 = vector.Dot(segment0.Direction);
			float num3 = -vector.Dot(segment1.Direction);
			float sqrMagnitude = vector.sqrMagnitude;
			float num4 = Mathf.Abs(1f - num * num);
			float num5;
			float num6;
			float num10;
			if (num4 >= 1E-05f)
			{
				num5 = num * num3 - num2;
				num6 = num * num2 - num3;
				float num7 = segment0.Extent * num4;
				float num8 = segment1.Extent * num4;
				if (num5 >= -num7)
				{
					if (num5 <= num7)
					{
						if (num6 >= -num8)
						{
							if (num6 <= num8)
							{
								float num9 = 1f / num4;
								num5 *= num9;
								num6 *= num9;
								num10 = num5 * (num5 + num * num6 + 2f * num2) + num6 * (num * num5 + num6 + 2f * num3) + sqrMagnitude;
							}
							else
							{
								num6 = segment1.Extent;
								float num11 = -(num * num6 + num2);
								if (num11 < -segment0.Extent)
								{
									num5 = -segment0.Extent;
									num10 = num5 * (num5 - 2f * num11) + num6 * (num6 + 2f * num3) + sqrMagnitude;
								}
								else if (num11 <= segment0.Extent)
								{
									num5 = num11;
									num10 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
								}
								else
								{
									num5 = segment0.Extent;
									num10 = num5 * (num5 - 2f * num11) + num6 * (num6 + 2f * num3) + sqrMagnitude;
								}
							}
						}
						else
						{
							num6 = -segment1.Extent;
							float num11 = -(num * num6 + num2);
							if (num11 < -segment0.Extent)
							{
								num5 = -segment0.Extent;
								num10 = num5 * (num5 - 2f * num11) + num6 * (num6 + 2f * num3) + sqrMagnitude;
							}
							else if (num11 <= segment0.Extent)
							{
								num5 = num11;
								num10 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
							}
							else
							{
								num5 = segment0.Extent;
								num10 = num5 * (num5 - 2f * num11) + num6 * (num6 + 2f * num3) + sqrMagnitude;
							}
						}
					}
					else if (num6 >= -num8)
					{
						if (num6 <= num8)
						{
							num5 = segment0.Extent;
							float num12 = -(num * num5 + num3);
							if (num12 < -segment1.Extent)
							{
								num6 = -segment1.Extent;
								num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
							else if (num12 <= segment1.Extent)
							{
								num6 = num12;
								num10 = -num6 * num6 + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
							else
							{
								num6 = segment1.Extent;
								num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
						}
						else
						{
							num6 = segment1.Extent;
							float num11 = -(num * num6 + num2);
							if (num11 < -segment0.Extent)
							{
								num5 = -segment0.Extent;
								num10 = num5 * (num5 - 2f * num11) + num6 * (num6 + 2f * num3) + sqrMagnitude;
							}
							else if (num11 <= segment0.Extent)
							{
								num5 = num11;
								num10 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
							}
							else
							{
								num5 = segment0.Extent;
								float num12 = -(num * num5 + num3);
								if (num12 < -segment1.Extent)
								{
									num6 = -segment1.Extent;
									num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
								}
								else if (num12 <= segment1.Extent)
								{
									num6 = num12;
									num10 = -num6 * num6 + num5 * (num5 + 2f * num2) + sqrMagnitude;
								}
								else
								{
									num6 = segment1.Extent;
									num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
								}
							}
						}
					}
					else
					{
						num6 = -segment1.Extent;
						float num11 = -(num * num6 + num2);
						if (num11 < -segment0.Extent)
						{
							num5 = -segment0.Extent;
							num10 = num5 * (num5 - 2f * num11) + num6 * (num6 + 2f * num3) + sqrMagnitude;
						}
						else if (num11 <= segment0.Extent)
						{
							num5 = num11;
							num10 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
						}
						else
						{
							num5 = segment0.Extent;
							float num12 = -(num * num5 + num3);
							if (num12 > segment1.Extent)
							{
								num6 = segment1.Extent;
								num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
							else if (num12 >= -segment1.Extent)
							{
								num6 = num12;
								num10 = -num6 * num6 + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
							else
							{
								num6 = -segment1.Extent;
								num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
						}
					}
				}
				else if (num6 >= -num8)
				{
					if (num6 <= num8)
					{
						num5 = -segment0.Extent;
						float num12 = -(num * num5 + num3);
						if (num12 < -segment1.Extent)
						{
							num6 = -segment1.Extent;
							num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
						}
						else if (num12 <= segment1.Extent)
						{
							num6 = num12;
							num10 = -num6 * num6 + num5 * (num5 + 2f * num2) + sqrMagnitude;
						}
						else
						{
							num6 = segment1.Extent;
							num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
						}
					}
					else
					{
						num6 = segment1.Extent;
						float num11 = -(num * num6 + num2);
						if (num11 > segment0.Extent)
						{
							num5 = segment0.Extent;
							num10 = num5 * (num5 - 2f * num11) + num6 * (num6 + 2f * num3) + sqrMagnitude;
						}
						else if (num11 >= -segment0.Extent)
						{
							num5 = num11;
							num10 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
						}
						else
						{
							num5 = -segment0.Extent;
							float num12 = -(num * num5 + num3);
							if (num12 < -segment1.Extent)
							{
								num6 = -segment1.Extent;
								num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
							else if (num12 <= segment1.Extent)
							{
								num6 = num12;
								num10 = -num6 * num6 + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
							else
							{
								num6 = segment1.Extent;
								num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
							}
						}
					}
				}
				else
				{
					num6 = -segment1.Extent;
					float num11 = -(num * num6 + num2);
					if (num11 > segment0.Extent)
					{
						num5 = segment0.Extent;
						num10 = num5 * (num5 - 2f * num11) + num6 * (num6 + 2f * num3) + sqrMagnitude;
					}
					else if (num11 >= -segment0.Extent)
					{
						num5 = num11;
						num10 = -num5 * num5 + num6 * (num6 + 2f * num3) + sqrMagnitude;
					}
					else
					{
						num5 = -segment0.Extent;
						float num12 = -(num * num5 + num3);
						if (num12 < -segment1.Extent)
						{
							num6 = -segment1.Extent;
							num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
						}
						else if (num12 <= segment1.Extent)
						{
							num6 = num12;
							num10 = -num6 * num6 + num5 * (num5 + 2f * num2) + sqrMagnitude;
						}
						else
						{
							num6 = segment1.Extent;
							num10 = num6 * (num6 - 2f * num12) + num5 * (num5 + 2f * num2) + sqrMagnitude;
						}
					}
				}
			}
			else
			{
				float num13 = segment0.Extent + segment1.Extent;
				float num14 = (num > 0f) ? -1f : 1f;
				float num15 = 0.5f * (num2 - num14 * num3);
				float num16 = -num15;
				if (num16 < -num13)
				{
					num16 = -num13;
				}
				else if (num16 > num13)
				{
					num16 = num13;
				}
				num6 = -num14 * num16 * segment1.Extent / num13;
				num5 = num16 + num14 * num6;
				num10 = num16 * (num16 + 2f * num15) + sqrMagnitude;
			}
			closestPoint0 = segment0.Center + num5 * segment0.Direction;
			closestPoint1 = segment1.Center + num6 * segment1.Direction;
			if (num10 < 0f)
			{
				num10 = 0f;
			}
			return num10;
		}
	}
}

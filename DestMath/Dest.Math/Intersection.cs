using System;
using UnityEngine;

namespace Dest.Math
{
	public static class Intersection
	{
		private static float _intervalThreshold;

		private static float _dotThreshold;

		private static float _distanceThreshold;

		public static float IntervalThreshold
		{
			get
			{
				return Intersection._intervalThreshold;
			}
			set
			{
				if (value >= 0f)
				{
					Intersection._intervalThreshold = value;
					return;
				}
				Logger.LogWarning("Interval threshold must be nonnegative.");
			}
		}

		public static float DotThreshold
		{
			get
			{
				return Intersection._dotThreshold;
			}
			set
			{
				if (value >= 0f)
				{
					Intersection._dotThreshold = value;
					return;
				}
				Logger.LogWarning("Dot threshold must be nonnegative.");
			}
		}

		public static float DistanceThreshold
		{
			get
			{
				return Intersection._distanceThreshold;
			}
			set
			{
				if (value >= 0f)
				{
					Intersection._distanceThreshold = value;
					return;
				}
				Logger.LogWarning("Distance threshold must be nonnegative.");
			}
		}

		public static bool TestAAB2AAB2(ref AAB2 box0, ref AAB2 box1)
		{
			return box0.Max.x >= box1.Min.x && box0.Min.x <= box1.Max.x && box0.Max.y >= box1.Min.y && box0.Min.y <= box1.Max.y;
		}

		public static bool FindAAB2AAB2(ref AAB2 box0, ref AAB2 box1, out AAB2 intersection)
		{
			if (box0.Max.x < box1.Min.x || box0.Min.x > box1.Max.x)
			{
				intersection = default(AAB2);
				return false;
			}
			if (box0.Max.y < box1.Min.y || box0.Min.y > box1.Max.y)
			{
				intersection = default(AAB2);
				return false;
			}
			intersection.Max.x = ((box0.Max.x <= box1.Max.x) ? box0.Max.x : box1.Max.x);
			intersection.Min.x = ((box0.Min.x <= box1.Min.x) ? box1.Min.x : box0.Min.x);
			intersection.Max.y = ((box0.Max.y <= box1.Max.y) ? box0.Max.y : box1.Max.y);
			intersection.Min.y = ((box0.Min.y <= box1.Min.y) ? box1.Min.y : box0.Min.y);
			return true;
		}

		public static bool TestAAB2AAB2OverlapX(ref AAB2 box0, ref AAB2 box1)
		{
			return box0.Max.x >= box1.Min.x && box0.Min.x <= box1.Max.x;
		}

		public static bool TestAAB2AAB2OverlapY(ref AAB2 box0, ref AAB2 box1)
		{
			return box0.Max.y >= box1.Min.y && box0.Min.y <= box1.Max.y;
		}

		public static bool TestAAB2Circle2(ref AAB2 box, ref Circle2 circle)
		{
			float num = 0f;
			float num2 = circle.Center.x;
			if (num2 < box.Min.x)
			{
				float num3 = num2 - box.Min.x;
				num += num3 * num3;
			}
			else if (num2 > box.Max.x)
			{
				float num3 = num2 - box.Max.x;
				num += num3 * num3;
			}
			num2 = circle.Center.y;
			if (num2 < box.Min.y)
			{
				float num3 = num2 - box.Min.y;
				num += num3 * num3;
			}
			else if (num2 > box.Max.y)
			{
				float num3 = num2 - box.Max.y;
				num += num3 * num3;
			}
			return num <= circle.Radius * circle.Radius;
		}

		public static bool TestBox2Box2(ref Box2 box0, ref Box2 box1)
		{
			Vector2 axis = box0.Axis0;
			Vector2 axis2 = box0.Axis1;
			Vector2 axis3 = box1.Axis0;
			Vector2 axis4 = box1.Axis1;
			float x = box0.Extents.x;
			float y = box0.Extents.y;
			float x2 = box1.Extents.x;
			float y2 = box1.Extents.y;
			Vector2 value = box1.Center - box0.Center;
			float num = Mathf.Abs(axis.Dot(axis3));
			float num2 = Mathf.Abs(axis.Dot(axis4));
			float num3 = Mathf.Abs(axis.Dot(value));
			float num4 = x + x2 * num + y2 * num2;
			if (num3 > num4)
			{
				return false;
			}
			float num5 = Mathf.Abs(axis2.Dot(axis3));
			float num6 = Mathf.Abs(axis2.Dot(axis4));
			num3 = Mathf.Abs(axis2.Dot(value));
			num4 = y + x2 * num5 + y2 * num6;
			if (num3 > num4)
			{
				return false;
			}
			num3 = Mathf.Abs(axis3.Dot(value));
			num4 = x2 + x * num + y * num5;
			if (num3 > num4)
			{
				return false;
			}
			num3 = Mathf.Abs(axis4.Dot(value));
			num4 = y2 + x * num2 + y * num6;
			return num3 <= num4;
		}

		public static bool TestBox2Circle2(ref Box2 box, ref Circle2 circle)
		{
			float num = 0f;
			Vector2 vector = circle.Center - box.Center;
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
			num2 = vector.Dot(box.Axis1);
			num3 = box.Extents.y;
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
			return num <= circle.Radius * circle.Radius;
		}

		public static bool TestCircle2Circle2(ref Circle2 circle0, ref Circle2 circle1)
		{
			Vector2 vector = circle0.Center - circle1.Center;
			float num = circle0.Radius + circle1.Radius;
			return vector.sqrMagnitude <= num * num;
		}

		public static bool FindCircle2Circle2(ref Circle2 circle0, ref Circle2 circle1, out Circle2Circle2Intr info)
		{
			info.Point0 = (info.Point1 = Vector2.zero);
			Vector2 a = circle1.Center - circle0.Center;
			float sqrMagnitude = a.sqrMagnitude;
			float radius = circle0.Radius;
			float radius2 = circle1.Radius;
			float num = radius - radius2;
			if (sqrMagnitude < 9.99999944E-11f && Mathf.Abs(num) < 1E-05f)
			{
				info.IntersectionType = IntersectionTypes.Other;
				info.Quantity = 0;
				return true;
			}
			float num2 = num * num;
			if (sqrMagnitude < num2)
			{
				info.IntersectionType = IntersectionTypes.Empty;
				info.Quantity = 0;
				return false;
			}
			float num3 = radius + radius2;
			float num4 = num3 * num3;
			if (sqrMagnitude > num4)
			{
				info.IntersectionType = IntersectionTypes.Empty;
				info.Quantity = 0;
				return false;
			}
			if (sqrMagnitude < num4)
			{
				if (num2 < sqrMagnitude)
				{
					float num5 = 1f / sqrMagnitude;
					float num6 = 0.5f * ((radius * radius - radius2 * radius2) * num5 + 1f);
					Vector2 a2 = circle0.Center + num6 * a;
					float num7 = radius * radius * num5 - num6 * num6;
					if (num7 < 0f)
					{
						num7 = 0f;
					}
					float d = Mathf.Sqrt(num7);
					Vector2 a3 = new Vector2(a.y, -a.x);
					info.Quantity = 2;
					info.Point0 = a2 - d * a3;
					info.Point1 = a2 + d * a3;
				}
				else
				{
					info.Quantity = 1;
					info.Point0 = circle0.Center + radius / num * a;
				}
			}
			else
			{
				info.Quantity = 1;
				info.Point0 = circle0.Center + radius / num3 * a;
			}
			info.IntersectionType = IntersectionTypes.Point;
			return true;
		}

		private static int WhichSide(Polygon2 V, Vector2 P, ref Vector2 D)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < V.VertexCount; i++)
			{
				float num4 = D.Dot(V[i] - P);
				if (num4 > 0f)
				{
					num++;
				}
				else if (num4 < 0f)
				{
					num2++;
				}
				else
				{
					num3++;
				}
				if (num > 0 && num2 > 0)
				{
					return 0;
				}
			}
			if (num3 != 0)
			{
				return 0;
			}
			if (num <= 0)
			{
				return -1;
			}
			return 1;
		}

		public static bool TestConvexPolygon2ConvexPolygon2(Polygon2 convexPolygon0, Polygon2 convexPolygon1)
		{
			int i = 0;
			int vertexIndex = convexPolygon0.VertexCount - 1;
			while (i < convexPolygon0.VertexCount)
			{
				Vector2 vector = (convexPolygon0[i] - convexPolygon0[vertexIndex]).Perp();
				if (Intersection.WhichSide(convexPolygon1, convexPolygon0[i], ref vector) > 0)
				{
					return false;
				}
				vertexIndex = i;
				i++;
			}
			int j = 0;
			int vertexIndex2 = convexPolygon1.VertexCount - 1;
			while (j < convexPolygon1.VertexCount)
			{
				Vector2 vector = (convexPolygon1[j] - convexPolygon1[vertexIndex2]).Perp();
				if (Intersection.WhichSide(convexPolygon0, convexPolygon1[j], ref vector) > 0)
				{
					return false;
				}
				vertexIndex2 = j;
				j++;
			}
			return true;
		}

		private static bool DoClipping(float t0, float t1, ref Vector2 origin, ref Vector2 direction, ref AAB2 box, bool solid, out int quantity, out Vector2 point0, out Vector2 point1, out IntersectionTypes intrType)
		{
			Vector2 vector;
			Vector2 vector2;
			box.CalcCenterExtents(out vector, out vector2);
			Vector2 vector3 = new Vector2(origin.x - vector.x, origin.y - vector.y);
			float num = t0;
			float num2 = t1;
			bool flag = Intersection.Clip(direction.x, -vector3.x - vector2.x, ref t0, ref t1) && Intersection.Clip(-direction.x, vector3.x - vector2.x, ref t0, ref t1) && Intersection.Clip(direction.y, -vector3.y - vector2.y, ref t0, ref t1) && Intersection.Clip(-direction.y, vector3.y - vector2.y, ref t0, ref t1);
			if (flag && (solid || t0 != num || t1 != num2))
			{
				if (t1 > t0)
				{
					intrType = IntersectionTypes.Segment;
					quantity = 2;
					point0 = origin + t0 * direction;
					point1 = origin + t1 * direction;
				}
				else
				{
					intrType = IntersectionTypes.Point;
					quantity = 1;
					point0 = origin + t0 * direction;
					point1 = Vector2ex.Zero;
				}
			}
			else
			{
				intrType = IntersectionTypes.Empty;
				quantity = 0;
				point0 = Vector2ex.Zero;
				point1 = Vector2ex.Zero;
			}
			return intrType != IntersectionTypes.Empty;
		}

		public static bool TestLine2AAB2(ref Line2 line, ref AAB2 box)
		{
			Vector2 vector = line.Direction.Perp();
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
			float num = vector.Dot(a - line.Center);
			if (num >= 0f)
			{
				return false;
			}
			float num2 = vector.Dot(a2 - line.Center);
			return num2 > 0f;
		}

		public static bool FindLine2AAB2(ref Line2 line, ref AAB2 box, out Line2AAB2Intr info)
		{
			return Intersection.DoClipping(float.NegativeInfinity, float.PositiveInfinity, ref line.Center, ref line.Direction, ref box, true, out info.Quantity, out info.Point0, out info.Point1, out info.IntersectionType);
		}

		private static bool Clip(float denom, float numer, ref float t0, ref float t1)
		{
			if (denom > 0f)
			{
				if (numer > denom * t1)
				{
					return false;
				}
				if (numer > denom * t0)
				{
					t0 = numer / denom;
				}
				return true;
			}
			else
			{
				if (denom >= 0f)
				{
					return numer <= 0f;
				}
				if (numer > denom * t0)
				{
					return false;
				}
				if (numer > denom * t1)
				{
					t1 = numer / denom;
				}
				return true;
			}
		}

		private static bool DoClipping(float t0, float t1, ref Vector2 origin, ref Vector2 direction, ref Box2 box, bool solid, out int quantity, out Vector2 point0, out Vector2 point1, out IntersectionTypes intrType)
		{
			Vector2 vector = new Vector2(origin.x - box.Center.x, origin.y - box.Center.y);
			Vector2 vector2 = new Vector2(vector.Dot(box.Axis0), vector.Dot(box.Axis1));
			Vector2 vector3 = new Vector2(direction.Dot(box.Axis0), direction.Dot(box.Axis1));
			float num = t0;
			float num2 = t1;
			bool flag = Intersection.Clip(vector3.x, -vector2.x - box.Extents.x, ref t0, ref t1) && Intersection.Clip(-vector3.x, vector2.x - box.Extents.x, ref t0, ref t1) && Intersection.Clip(vector3.y, -vector2.y - box.Extents.y, ref t0, ref t1) && Intersection.Clip(-vector3.y, vector2.y - box.Extents.y, ref t0, ref t1);
			if (flag && (solid || t0 != num || t1 != num2))
			{
				if (t1 > t0)
				{
					intrType = IntersectionTypes.Segment;
					quantity = 2;
					point0 = origin + t0 * direction;
					point1 = origin + t1 * direction;
				}
				else
				{
					intrType = IntersectionTypes.Point;
					quantity = 1;
					point0 = origin + t0 * direction;
					point1 = Vector2ex.Zero;
				}
			}
			else
			{
				intrType = IntersectionTypes.Empty;
				quantity = 0;
				point0 = Vector2ex.Zero;
				point1 = Vector2ex.Zero;
			}
			return intrType != IntersectionTypes.Empty;
		}

		public static bool TestLine2Box2(ref Line2 line, ref Box2 box)
		{
			Vector2 value = line.Center - box.Center;
			Vector2 vector = line.Direction.Perp();
			float num = Mathf.Abs(vector.Dot(value));
			float num2 = Mathf.Abs(vector.Dot(box.Axis0));
			float num3 = Mathf.Abs(vector.Dot(box.Axis1));
			float num4 = box.Extents.x * num2 + box.Extents.y * num3;
			return num <= num4;
		}

		public static bool FindLine2Box2(ref Line2 line, ref Box2 box, out Line2Box2Intr info)
		{
			return Intersection.DoClipping(float.NegativeInfinity, float.PositiveInfinity, ref line.Center, ref line.Direction, ref box, true, out info.Quantity, out info.Point0, out info.Point1, out info.IntersectionType);
		}

		private static bool Find(ref Vector2 origin, ref Vector2 direction, ref Vector2 center, float radius, out int rootCount, out float t0, out float t1)
		{
			Vector2 value = origin - center;
			float num = value.sqrMagnitude - radius * radius;
			float num2 = direction.Dot(value);
			float num3 = num2 * num2 - num;
			if (num3 > 1E-05f)
			{
				rootCount = 2;
				num3 = Mathf.Sqrt(num3);
				t0 = -num2 - num3;
				t1 = -num2 + num3;
			}
			else if (num3 < -1E-05f)
			{
				rootCount = 0;
				t0 = (t1 = 0f);
			}
			else
			{
				rootCount = 1;
				t0 = -num2;
				t1 = 0f;
			}
			return rootCount != 0;
		}

		public static bool TestLine2Circle2(ref Line2 line, ref Circle2 circle)
		{
			Vector2 rhs = line.Center - circle.Center;
			float num = rhs.sqrMagnitude - circle.Radius * circle.Radius;
			if (num <= 1E-05f)
			{
				return true;
			}
			float num2 = Vector2.Dot(line.Direction, rhs);
			float num3 = num2 * num2 - num;
			return num3 >= -1E-05f;
		}

		public static bool FindLine2Circle2(ref Line2 line, ref Circle2 circle, out Line2Circle2Intr info)
		{
			int num;
			float d;
			float d2;
			bool flag = Intersection.Find(ref line.Center, ref line.Direction, ref circle.Center, circle.Radius, out num, out d, out d2);
			if (flag)
			{
				if (num == 1)
				{
					info.IntersectionType = IntersectionTypes.Point;
					info.Point0 = line.Center + d * line.Direction;
					info.Point1 = Vector2.zero;
				}
				else
				{
					info.IntersectionType = IntersectionTypes.Segment;
					info.Point0 = line.Center + d * line.Direction;
					info.Point1 = line.Center + d2 * line.Direction;
				}
			}
			else
			{
				info.IntersectionType = IntersectionTypes.Empty;
				info.Point0 = (info.Point1 = Vector2.zero);
			}
			return info.IntersectionType != IntersectionTypes.Empty;
		}

		public static bool TestLine2ConvexPolygon2(ref Line2 line, Polygon2 convexPolygon)
		{
			Edge2[] edges = convexPolygon.Edges;
			int num = edges.Length;
			for (int i = 0; i < num; i++)
			{
				Segment2 segment = new Segment2(ref edges[i].Point0, ref edges[i].Point1);
				if (Intersection.TestLine2Segment2(ref line, ref segment))
				{
					return true;
				}
			}
			return false;
		}

		public static bool FindLine2ConvexPolygon2(ref Line2 line, Polygon2 convexPolygon, out Line2ConvexPolygon2Intr info)
		{
			Edge2[] edges = convexPolygon.Edges;
			int num = edges.Length;
			float num2 = float.NegativeInfinity;
			float num3 = float.PositiveInfinity;
			Vector2 direction = line.Direction;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = edges[i].Point1 - edges[i].Point0;
				Vector2 vector2 = new Vector2(vector.y, -vector.x);
				float num4 = vector2.Dot(edges[i].Point0 - line.Center);
				float num5 = vector2.Dot(direction);
				if (Mathf.Abs(num5) < 1E-05f)
				{
					if (num4 < 0f)
					{
						info = default(Line2ConvexPolygon2Intr);
						return false;
					}
				}
				else
				{
					float num6 = num4 / num5;
					if (num5 < 0f)
					{
						if (num6 > num2)
						{
							num2 = num6;
							if (num2 > num3)
							{
								info = default(Line2ConvexPolygon2Intr);
								return false;
							}
						}
					}
					else if (num6 < num3)
					{
						num3 = num6;
						if (num3 < num2)
						{
							info = default(Line2ConvexPolygon2Intr);
							return false;
						}
					}
				}
			}
			if (num3 - num2 > 1E-05f)
			{
				info.IntersectionType = IntersectionTypes.Segment;
				info.Quantity = 2;
				info.Point0 = line.Center + num2 * direction;
				info.Point1 = line.Center + num3 * direction;
				info.Parameter0 = num2;
				info.Parameter1 = num3;
			}
			else
			{
				info.IntersectionType = IntersectionTypes.Point;
				info.Quantity = 1;
				info.Point0 = line.Center + num2 * direction;
				info.Point1 = Vector2ex.Zero;
				info.Parameter0 = num2;
				info.Parameter1 = 0f;
			}
			return true;
		}

		private static IntersectionTypes Classify(ref Line2 line0, ref Line2 line1, out float s0)
		{
			Vector2 vector = line1.Center - line0.Center;
			s0 = 0f;
			float num = line0.Direction.DotPerp(line1.Direction);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float num2 = vector.DotPerp(line1.Direction);
				s0 = num2 / num;
				return IntersectionTypes.Point;
			}
			vector.Normalize();
			float f = vector.DotPerp(line1.Direction);
			if (Mathf.Abs(f) <= Intersection._dotThreshold)
			{
				return IntersectionTypes.Line;
			}
			return IntersectionTypes.Empty;
		}

		public static bool TestLine2Line2(ref Line2 line0, ref Line2 line1, out IntersectionTypes intersectionType)
		{
			float num;
			intersectionType = Intersection.Classify(ref line0, ref line1, out num);
			return intersectionType != IntersectionTypes.Empty;
		}

		public static bool TestLine2Line2(ref Line2 line0, ref Line2 line1)
		{
			IntersectionTypes intersectionTypes;
			return Intersection.TestLine2Line2(ref line0, ref line1, out intersectionTypes);
		}

		public static bool FindLine2Line2(ref Line2 line0, ref Line2 line1, out Line2Line2Intr info)
		{
			float num;
			info.IntersectionType = Intersection.Classify(ref line0, ref line1, out num);
			if (info.IntersectionType == IntersectionTypes.Point)
			{
				info.Point = line0.Center + num * line0.Direction;
				info.Parameter = num;
			}
			else
			{
				info.Point = Vector2.zero;
				info.Parameter = 0f;
			}
			return info.IntersectionType != IntersectionTypes.Empty;
		}

		private static IntersectionTypes Classify(ref Line2 line, ref Ray2 ray, out float s0, out float s1)
		{
			Vector2 vector = ray.Center - line.Center;
			s0 = (s1 = 0f);
			float num = line.Direction.DotPerp(ray.Direction);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float num2 = 1f / num;
				float num3 = vector.DotPerp(ray.Direction);
				s0 = num3 * num2;
				float num4 = vector.DotPerp(line.Direction);
				s1 = num4 * num2;
				return IntersectionTypes.Point;
			}
			vector.Normalize();
			float f = vector.DotPerp(ray.Direction);
			if (Mathf.Abs(f) <= Intersection._dotThreshold)
			{
				return IntersectionTypes.Ray;
			}
			return IntersectionTypes.Empty;
		}

		public static bool TestLine2Ray2(ref Line2 line, ref Ray2 ray, out IntersectionTypes intersectionType)
		{
			float num;
			float num2;
			intersectionType = Intersection.Classify(ref line, ref ray, out num, out num2);
			if (intersectionType == IntersectionTypes.Point && num2 < -Intersection._intervalThreshold)
			{
				intersectionType = IntersectionTypes.Empty;
			}
			return intersectionType != IntersectionTypes.Empty;
		}

		public static bool TestLine2Ray2(ref Line2 line, ref Ray2 ray)
		{
			IntersectionTypes intersectionTypes;
			return Intersection.TestLine2Ray2(ref line, ref ray, out intersectionTypes);
		}

		public static bool FindLine2Ray2(ref Line2 line, ref Ray2 ray, out Line2Ray2Intr info)
		{
			float num;
			float num2;
			info.IntersectionType = Intersection.Classify(ref line, ref ray, out num, out num2);
			info.Point = Vector2.zero;
			info.Parameter = 0f;
			if (info.IntersectionType == IntersectionTypes.Point)
			{
				if (num2 >= -Intersection._intervalThreshold)
				{
					info.Point = line.Center + num * line.Direction;
					info.Parameter = num;
				}
				else
				{
					info.IntersectionType = IntersectionTypes.Empty;
				}
			}
			else if (info.IntersectionType == IntersectionTypes.Ray)
			{
				info.Point = ray.Center;
				info.Parameter = num;
			}
			return info.IntersectionType != IntersectionTypes.Empty;
		}

		private static IntersectionTypes Classify(ref Segment2 segment, ref Line2 line, out float s0, out float s1)
		{
			Vector2 vector = segment.Center - line.Center;
			s0 = (s1 = 0f);
			float num = line.Direction.DotPerp(segment.Direction);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float num2 = 1f / num;
				float num3 = vector.DotPerp(segment.Direction);
				s0 = num3 * num2;
				float num4 = vector.DotPerp(line.Direction);
				s1 = num4 * num2;
				return IntersectionTypes.Point;
			}
			vector.Normalize();
			float f = vector.DotPerp(segment.Direction);
			if (Mathf.Abs(f) <= Intersection._dotThreshold)
			{
				return IntersectionTypes.Segment;
			}
			return IntersectionTypes.Empty;
		}

		public static bool TestLine2Segment2(ref Line2 line, ref Segment2 segment, out IntersectionTypes intersectionType)
		{
			float num;
			float f;
			intersectionType = Intersection.Classify(ref segment, ref line, out num, out f);
			if (intersectionType == IntersectionTypes.Point && Mathf.Abs(f) > segment.Extent)
			{
				intersectionType = IntersectionTypes.Empty;
			}
			return intersectionType != IntersectionTypes.Empty;
		}

		public static bool TestLine2Segment2(ref Line2 line, ref Segment2 segment)
		{
			IntersectionTypes intersectionTypes;
			return Intersection.TestLine2Segment2(ref line, ref segment, out intersectionTypes);
		}

		public static bool FindLine2Segment2(ref Line2 line, ref Segment2 segment, out Line2Segment2Intr info)
		{
			float num;
			float f;
			info.IntersectionType = Intersection.Classify(ref segment, ref line, out num, out f);
			info.Point = Vector2.zero;
			info.Parameter = 0f;
			if (info.IntersectionType == IntersectionTypes.Point)
			{
				if (Mathf.Abs(f) <= segment.Extent + Intersection._intervalThreshold)
				{
					info.Point = line.Center + num * line.Direction;
					info.Parameter = num;
				}
				else
				{
					info.IntersectionType = IntersectionTypes.Empty;
				}
			}
			return info.IntersectionType != IntersectionTypes.Empty;
		}

		private static void TriangleLineRelations(ref Vector2 origin, ref Vector2 direction, ref Triangle2 triangle, out float dist0, out float dist1, out float dist2, out int sign0, out int sign1, out int sign2, out int positive, out int negative, out int zero)
		{
			positive = 0;
			negative = 0;
			zero = 0;
			Vector2 vector = triangle.V0 - origin;
			dist0 = vector.DotPerp(direction);
			if (dist0 > 1E-05f)
			{
				sign0 = 1;
				positive++;
			}
			else if (dist0 < -1E-05f)
			{
				sign0 = -1;
				negative++;
			}
			else
			{
				dist0 = 0f;
				sign0 = 0;
				zero++;
			}
			vector = triangle.V1 - origin;
			dist1 = vector.DotPerp(direction);
			if (dist1 > 1E-05f)
			{
				sign1 = 1;
				positive++;
			}
			else if (dist1 < -1E-05f)
			{
				sign1 = -1;
				negative++;
			}
			else
			{
				dist1 = 0f;
				sign1 = 0;
				zero++;
			}
			vector = triangle.V2 - origin;
			dist2 = vector.DotPerp(direction);
			if (dist2 > 1E-05f)
			{
				sign2 = 1;
				positive++;
				return;
			}
			if (dist2 < -1E-05f)
			{
				sign2 = -1;
				negative++;
				return;
			}
			dist2 = 0f;
			sign2 = 0;
			zero++;
		}

		private static bool GetInterval(ref Vector2 origin, ref Vector2 direction, ref Triangle2 triangle, float dist0, float dist1, float dist2, int sign0, int sign1, int sign2, out float param0, out float param1)
		{
			Vector2 value = triangle.V0 - origin;
			float num = direction.Dot(value);
			value = triangle.V1 - origin;
			float num2 = direction.Dot(value);
			value = triangle.V2 - origin;
			float num3 = direction.Dot(value);
			param0 = 0f;
			param1 = 0f;
			int num4 = 0;
			if (sign2 * sign0 < 0)
			{
				param0 = (dist2 * num - dist0 * num3) / (dist2 - dist0);
				num4++;
			}
			if (sign0 * sign1 < 0)
			{
				if (num4 == 0)
				{
					param0 = (dist0 * num2 - dist1 * num) / (dist0 - dist1);
				}
				else
				{
					param1 = (dist0 * num2 - dist1 * num) / (dist0 - dist1);
				}
				num4++;
			}
			if (sign1 * sign2 < 0)
			{
				if (num4 > 1)
				{
					return true;
				}
				if (num4 == 0)
				{
					param0 = (dist1 * num3 - dist2 * num2) / (dist1 - dist2);
				}
				else
				{
					param1 = (dist1 * num3 - dist2 * num2) / (dist1 - dist2);
				}
				num4++;
			}
			if (num4 < 2)
			{
				if (sign0 == 0)
				{
					if (num4 > 1)
					{
						return true;
					}
					if (num4 == 0)
					{
						param0 = num;
					}
					else
					{
						param1 = num;
					}
					num4++;
				}
				if (sign1 == 0)
				{
					if (num4 > 1)
					{
						return true;
					}
					if (num4 == 0)
					{
						param0 = num2;
					}
					else
					{
						param1 = num2;
					}
					num4++;
				}
				if (sign2 == 0)
				{
					if (num4 > 1)
					{
						return true;
					}
					if (num4 == 0)
					{
						param0 = num3;
					}
					else
					{
						param1 = num3;
					}
					num4++;
				}
			}
			if (num4 < 1)
			{
				return true;
			}
			if (num4 == 2)
			{
				if (param0 > param1)
				{
					float num5 = param0;
					param0 = param1;
					param1 = num5;
				}
			}
			else
			{
				param1 = param0;
			}
			return false;
		}

		public static bool TestLine2Triangle2(ref Line2 line, ref Triangle2 triangle, out IntersectionTypes intersectionType)
		{
			float dist;
			float dist2;
			float dist3;
			int sign;
			int sign2;
			int sign3;
			int num;
			int num2;
			int num3;
			Intersection.TriangleLineRelations(ref line.Center, ref line.Direction, ref triangle, out dist, out dist2, out dist3, out sign, out sign2, out sign3, out num, out num2, out num3);
			if (num == 3 || num2 == 3)
			{
				intersectionType = IntersectionTypes.Empty;
			}
			else
			{
				float seg0Start;
				float seg0End;
				bool interval = Intersection.GetInterval(ref line.Center, ref line.Direction, ref triangle, dist, dist2, dist3, sign, sign2, sign3, out seg0Start, out seg0End);
				if (interval)
				{
					intersectionType = IntersectionTypes.Empty;
				}
				else
				{
					float num5;
					float num6;
					int num4 = Intersection.FindSegment1Segment1(seg0Start, seg0End, float.NegativeInfinity, float.PositiveInfinity, out num5, out num6);
					if (num4 == 2)
					{
						intersectionType = IntersectionTypes.Segment;
					}
					else if (num4 == 1)
					{
						intersectionType = IntersectionTypes.Point;
					}
					else
					{
						intersectionType = IntersectionTypes.Empty;
					}
				}
			}
			return intersectionType != IntersectionTypes.Empty;
		}

		public static bool TestLine2Triangle2(ref Line2 line, ref Triangle2 triangle)
		{
			IntersectionTypes intersectionTypes;
			return Intersection.TestLine2Triangle2(ref line, ref triangle, out intersectionTypes);
		}

		public static bool FindLine2Triangle2(ref Line2 line, ref Triangle2 triangle, out Line2Triangle2Intr info)
		{
			float dist;
			float dist2;
			float dist3;
			int sign;
			int sign2;
			int sign3;
			int num;
			int num2;
			int num3;
			Intersection.TriangleLineRelations(ref line.Center, ref line.Direction, ref triangle, out dist, out dist2, out dist3, out sign, out sign2, out sign3, out num, out num2, out num3);
			if (num == 3 || num2 == 3)
			{
				info.IntersectionType = IntersectionTypes.Empty;
				info.Quantity = 0;
				info.Point0 = Vector2.zero;
				info.Point1 = Vector2.zero;
			}
			else
			{
				float seg0Start;
				float seg0End;
				bool interval = Intersection.GetInterval(ref line.Center, ref line.Direction, ref triangle, dist, dist2, dist3, sign, sign2, sign3, out seg0Start, out seg0End);
				if (interval)
				{
					info.IntersectionType = IntersectionTypes.Empty;
					info.Quantity = 0;
					info.Point0 = Vector2.zero;
					info.Point1 = Vector2.zero;
				}
				else
				{
					float d;
					float d2;
					info.Quantity = Intersection.FindSegment1Segment1(seg0Start, seg0End, float.NegativeInfinity, float.PositiveInfinity, out d, out d2);
					if (info.Quantity == 2)
					{
						info.IntersectionType = IntersectionTypes.Segment;
						info.Point0 = line.Center + d * line.Direction;
						info.Point1 = line.Center + d2 * line.Direction;
					}
					else if (info.Quantity == 1)
					{
						info.IntersectionType = IntersectionTypes.Point;
						info.Point0 = line.Center + d * line.Direction;
						info.Point1 = Vector2.zero;
					}
					else
					{
						info.IntersectionType = IntersectionTypes.Empty;
						info.Point0 = Vector2.zero;
						info.Point1 = Vector2.zero;
					}
				}
			}
			return info.IntersectionType != IntersectionTypes.Empty;
		}

		public static bool TestRay2AAB2(ref Ray2 ray, ref AAB2 box)
		{
			Vector2 b;
			Vector2 vector;
			box.CalcCenterExtents(out b, out vector);
			Vector2 value = ray.Center - b;
			float x = ray.Direction.x;
			float x2 = value.x;
			float num = Mathf.Abs(x2);
			if (num > vector.x && x2 * x >= 0f)
			{
				return false;
			}
			float y = ray.Direction.y;
			float y2 = value.y;
			float num2 = Mathf.Abs(y2);
			if (num2 > vector.y && y2 * y >= 0f)
			{
				return false;
			}
			Vector2 vector2 = ray.Direction.Perp();
			float num3 = Mathf.Abs(vector2.Dot(value));
			float num4 = Mathf.Abs(vector2.x);
			float num5 = Mathf.Abs(vector2.y);
			float num6 = vector.x * num4 + vector.y * num5;
			return num3 <= num6;
		}

		public static bool FindRay2AAB2(ref Ray2 ray, ref AAB2 box, out Ray2AAB2Intr info)
		{
			return Intersection.DoClipping(0f, float.PositiveInfinity, ref ray.Center, ref ray.Direction, ref box, true, out info.Quantity, out info.Point0, out info.Point1, out info.IntersectionType);
		}

		public static bool TestRay2Box2(ref Ray2 ray, ref Box2 box)
		{
			Vector2 vector = ray.Center - box.Center;
			float num = ray.Direction.Dot(box.Axis0);
			float num2 = vector.Dot(box.Axis0);
			float num3 = Mathf.Abs(num2);
			if (num3 > box.Extents.x && num2 * num >= 0f)
			{
				return false;
			}
			float num4 = ray.Direction.Dot(box.Axis1);
			float num5 = vector.Dot(box.Axis1);
			float num6 = Mathf.Abs(num5);
			if (num6 > box.Extents.y && num5 * num4 >= 0f)
			{
				return false;
			}
			Vector2 vector2 = ray.Direction.Perp();
			float num7 = Mathf.Abs(vector2.Dot(vector));
			float num8 = Mathf.Abs(vector2.Dot(box.Axis0));
			float num9 = Mathf.Abs(vector2.Dot(box.Axis1));
			float num10 = box.Extents.x * num8 + box.Extents.y * num9;
			return num7 <= num10;
		}

		public static bool FindRay2Box2(ref Ray2 ray, ref Box2 box, out Ray2Box2Intr info)
		{
			return Intersection.DoClipping(0f, float.PositiveInfinity, ref ray.Center, ref ray.Direction, ref box, true, out info.Quantity, out info.Point0, out info.Point1, out info.IntersectionType);
		}

		public static bool TestRay2Circle2(ref Ray2 ray, ref Circle2 circle)
		{
			Vector2 rhs = ray.Center - circle.Center;
			float num = rhs.sqrMagnitude - circle.Radius * circle.Radius;
			if (num <= 1E-05f)
			{
				return true;
			}
			float num2 = Vector2.Dot(ray.Direction, rhs);
			if (num2 >= 0f)
			{
				return false;
			}
			float num3 = num2 * num2 - num;
			return num3 >= -1E-05f;
		}

		public static bool FindRay2Circle2(ref Ray2 ray, ref Circle2 circle, out Ray2Circle2Intr info)
		{
			int num;
			float num2;
			float num3;
			bool flag = Intersection.Find(ref ray.Center, ref ray.Direction, ref circle.Center, circle.Radius, out num, out num2, out num3);
			info.Point0 = (info.Point1 = Vector2.zero);
			if (flag)
			{
				if (num == 1)
				{
					if (num2 < 0f)
					{
						info.IntersectionType = IntersectionTypes.Empty;
					}
					else
					{
						info.IntersectionType = IntersectionTypes.Point;
						info.Point0 = ray.Center + num2 * ray.Direction;
					}
				}
				else if (num3 < 0f)
				{
					info.IntersectionType = IntersectionTypes.Empty;
				}
				else if (num2 < 0f)
				{
					info.IntersectionType = IntersectionTypes.Point;
					info.Point0 = ray.Center + num3 * ray.Direction;
				}
				else
				{
					info.IntersectionType = IntersectionTypes.Segment;
					info.Point0 = ray.Center + num2 * ray.Direction;
					info.Point1 = ray.Center + num3 * ray.Direction;
				}
			}
			else
			{
				info.IntersectionType = IntersectionTypes.Empty;
			}
			return info.IntersectionType != IntersectionTypes.Empty;
		}

		public static bool TestRay2ConvexPolygon2(ref Ray2 ray, Polygon2 convexPolygon)
		{
			Edge2[] edges = convexPolygon.Edges;
			int num = edges.Length;
			for (int i = 0; i < num; i++)
			{
				Segment2 segment = new Segment2(ref edges[i].Point0, ref edges[i].Point1);
				if (Intersection.TestRay2Segment2(ref ray, ref segment))
				{
					return true;
				}
			}
			return false;
		}

		public static bool FindRay2ConvexPolygon2(ref Ray2 ray, Polygon2 convexPolygon, out Ray2ConvexPolygon2Intr info)
		{
			Edge2[] edges = convexPolygon.Edges;
			int num = edges.Length;
			float num2 = 0f;
			float num3 = float.PositiveInfinity;
			Vector2 direction = ray.Direction;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector = edges[i].Point1 - edges[i].Point0;
				Vector2 vector2 = new Vector2(vector.y, -vector.x);
				float num4 = vector2.Dot(edges[i].Point0 - ray.Center);
				float num5 = vector2.Dot(direction);
				if (Mathf.Abs(num5) < 1E-05f)
				{
					if (num4 < 0f)
					{
						info = default(Ray2ConvexPolygon2Intr);
						return false;
					}
				}
				else
				{
					float num6 = num4 / num5;
					if (num5 < 0f)
					{
						if (num6 > num2)
						{
							num2 = num6;
							if (num2 > num3)
							{
								info = default(Ray2ConvexPolygon2Intr);
								return false;
							}
						}
					}
					else if (num6 < num3)
					{
						num3 = num6;
						if (num3 < num2)
						{
							info = default(Ray2ConvexPolygon2Intr);
							return false;
						}
					}
				}
			}
			if (num3 - num2 > 1E-05f)
			{
				info.IntersectionType = IntersectionTypes.Segment;
				info.Quantity = 2;
				info.Point0 = ray.Center + num2 * direction;
				info.Point1 = ray.Center + num3 * direction;
				info.Parameter0 = num2;
				info.Parameter1 = num3;
			}
			else
			{
				info.IntersectionType = IntersectionTypes.Point;
				info.Quantity = 1;
				info.Point0 = ray.Center + num2 * direction;
				info.Point1 = Vector2ex.Zero;
				info.Parameter0 = num2;
				info.Parameter1 = 0f;
			}
			return true;
		}

		public static bool TestRay2Polygon2(ref Ray2 ray, Polygon2 polygon)
		{
			Edge2[] edges = polygon.Edges;
			int num = edges.Length;
			for (int i = 0; i < num; i++)
			{
				Segment2 segment = new Segment2(ref edges[i].Point0, ref edges[i].Point1);
				if (Intersection.TestRay2Segment2(ref ray, ref segment))
				{
					return true;
				}
			}
			return false;
		}

		public static bool TestRay2Polygon2(ref Ray2 ray, Segment2[] segments)
		{
			int num = segments.Length;
			for (int i = 0; i < num; i++)
			{
				if (Intersection.TestRay2Segment2(ref ray, ref segments[i]))
				{
					return true;
				}
			}
			return false;
		}

		public static bool FindRay2Polygon2(ref Ray2 ray, Polygon2 polygon, out Ray2Polygon2Intr info)
		{
			Edge2[] edges = polygon.Edges;
			int num = edges.Length;
			Ray2Segment2Intr ray2Segment2Intr = default(Ray2Segment2Intr);
			float num2 = float.PositiveInfinity;
			for (int i = 0; i < num; i++)
			{
				Segment2 segment = new Segment2(edges[i].Point0, edges[i].Point1);
				Ray2Segment2Intr ray2Segment2Intr2;
				if (Intersection.FindRay2Segment2(ref ray, ref segment, out ray2Segment2Intr2) && ray2Segment2Intr2.Parameter0 < num2)
				{
					if (ray2Segment2Intr2.IntersectionType == IntersectionTypes.Segment)
					{
						num2 = ray2Segment2Intr2.Parameter0;
						ray2Segment2Intr = ray2Segment2Intr2;
					}
					else if (num2 - ray2Segment2Intr2.Parameter0 > 1E-05f)
					{
						num2 = ray2Segment2Intr2.Parameter0;
						ray2Segment2Intr = ray2Segment2Intr2;
					}
				}
			}
			if (num2 != float.PositiveInfinity)
			{
				info.IntersectionType = ray2Segment2Intr.IntersectionType;
				info.Point0 = ray2Segment2Intr.Point0;
				info.Point1 = ray2Segment2Intr.Point1;
				info.Parameter0 = ray2Segment2Intr.Parameter0;
				info.Parameter1 = ray2Segment2Intr.Parameter1;
				return true;
			}
			info = default(Ray2Polygon2Intr);
			return false;
		}

		public static bool FindRay2Polygon2(ref Ray2 ray, Segment2[] segments, out Ray2Polygon2Intr info)
		{
			int num = segments.Length;
			Ray2Segment2Intr ray2Segment2Intr = default(Ray2Segment2Intr);
			float num2 = float.PositiveInfinity;
			for (int i = 0; i < num; i++)
			{
				Ray2Segment2Intr ray2Segment2Intr2;
				if (Intersection.FindRay2Segment2(ref ray, ref segments[i], out ray2Segment2Intr2) && ray2Segment2Intr2.Parameter0 < num2)
				{
					if (ray2Segment2Intr2.IntersectionType == IntersectionTypes.Segment)
					{
						num2 = ray2Segment2Intr2.Parameter0;
						ray2Segment2Intr = ray2Segment2Intr2;
					}
					else if (num2 - ray2Segment2Intr2.Parameter0 > 1E-05f)
					{
						num2 = ray2Segment2Intr2.Parameter0;
						ray2Segment2Intr = ray2Segment2Intr2;
					}
				}
			}
			if (num2 != float.PositiveInfinity)
			{
				info.IntersectionType = ray2Segment2Intr.IntersectionType;
				info.Point0 = ray2Segment2Intr.Point0;
				info.Point1 = ray2Segment2Intr.Point1;
				info.Parameter0 = ray2Segment2Intr.Parameter0;
				info.Parameter1 = ray2Segment2Intr.Parameter1;
				return true;
			}
			info = default(Ray2Polygon2Intr);
			return false;
		}

		private static IntersectionTypes Classify(ref Ray2 ray0, ref Ray2 ray1, out float s0, out float s1)
		{
			Vector2 vector = ray1.Center - ray0.Center;
			s0 = (s1 = 0f);
			float num = ray0.Direction.DotPerp(ray1.Direction);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float num2 = 1f / num;
				float num3 = vector.DotPerp(ray1.Direction);
				s0 = num3 * num2;
				float num4 = vector.DotPerp(ray0.Direction);
				s1 = num4 * num2;
				return IntersectionTypes.Point;
			}
			vector.Normalize();
			float f = vector.DotPerp(ray1.Direction);
			if (Mathf.Abs(f) <= Intersection._dotThreshold)
			{
				s0 = Vector2.Dot(ray1.Center - ray0.Center, ray0.Direction);
				return IntersectionTypes.Ray;
			}
			return IntersectionTypes.Empty;
		}

		public static bool TestRay2Ray2(ref Ray2 ray0, ref Ray2 ray1, out IntersectionTypes intersectionType)
		{
			float num;
			float num2;
			intersectionType = Intersection.Classify(ref ray0, ref ray1, out num, out num2);
			if (intersectionType == IntersectionTypes.Point)
			{
				if (num < -Intersection._intervalThreshold || num2 < -Intersection._intervalThreshold)
				{
					intersectionType = IntersectionTypes.Empty;
				}
			}
			else if (intersectionType == IntersectionTypes.Ray)
			{
				if (Mathf.Abs(num) == 0f)
				{
					float num3 = Vector2.Dot(ray0.Direction, ray1.Direction);
					if (num3 < 0f)
					{
						intersectionType = IntersectionTypes.Point;
					}
				}
				else if (num < 0f)
				{
					float num4 = Vector2.Dot(ray0.Direction, ray1.Direction);
					if (num4 < 0f)
					{
						intersectionType = IntersectionTypes.Empty;
					}
				}
			}
			return intersectionType != IntersectionTypes.Empty;
		}

		public static bool TestRay2Ray2(ref Ray2 ray0, ref Ray2 ray1)
		{
			IntersectionTypes intersectionTypes;
			return Intersection.TestRay2Ray2(ref ray0, ref ray1, out intersectionTypes);
		}

		public static bool FindRay2Ray2(ref Ray2 ray0, ref Ray2 ray1, out Ray2Ray2Intr info)
		{
			float num;
			float num2;
			info.IntersectionType = Intersection.Classify(ref ray0, ref ray1, out num, out num2);
			info.Point = Vector2.zero;
			info.Parameter = 0f;
			if (info.IntersectionType == IntersectionTypes.Point)
			{
				if (num >= -Intersection._intervalThreshold && num2 >= -Intersection._intervalThreshold)
				{
					if (num < 0f)
					{
						num = 0f;
					}
					info.Point = ray0.Center + num * ray0.Direction;
					info.Parameter = num;
				}
				else
				{
					info.IntersectionType = IntersectionTypes.Empty;
				}
			}
			else if (info.IntersectionType == IntersectionTypes.Ray)
			{
				if (Mathf.Abs(num) == 0f)
				{
					float num3 = Vector2.Dot(ray0.Direction, ray1.Direction);
					if (num3 < 0f)
					{
						info.IntersectionType = IntersectionTypes.Point;
					}
					info.Point = ray1.Center;
				}
				else if (num < 0f)
				{
					float num4 = Vector2.Dot(ray0.Direction, ray1.Direction);
					if (num4 < 0f)
					{
						info.IntersectionType = IntersectionTypes.Empty;
					}
					else
					{
						info.Point = ray1.Center;
						info.Parameter = num;
					}
				}
				else
				{
					info.Point = ray1.Center;
					info.Parameter = num;
				}
			}
			return info.IntersectionType != IntersectionTypes.Empty;
		}

		private static IntersectionTypes Classify(ref Ray2 ray, ref Segment2 segment, out float s0, out float s1)
		{
			Vector2 vector = segment.Center - ray.Center;
			s0 = (s1 = 0f);
			float num = ray.Direction.DotPerp(segment.Direction);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float num2 = 1f / num;
				float num3 = vector.DotPerp(segment.Direction);
				s0 = num3 * num2;
				float num4 = vector.DotPerp(ray.Direction);
				s1 = num4 * num2;
				return IntersectionTypes.Point;
			}
			vector.Normalize();
			float f = vector.DotPerp(segment.Direction);
			if (Mathf.Abs(f) <= Intersection._dotThreshold)
			{
				s0 = Vector2.Dot(segment.P0 - ray.Center, ray.Direction);
				s1 = Vector2.Dot(segment.P1 - ray.Center, ray.Direction);
				if (s0 > s1)
				{
					float num5 = s0;
					s0 = s1;
					s1 = num5;
				}
				return IntersectionTypes.Segment;
			}
			return IntersectionTypes.Empty;
		}

		public static bool TestRay2Segment2(ref Ray2 ray, ref Segment2 segment, out IntersectionTypes intersectionType)
		{
			float num;
			float num2;
			intersectionType = Intersection.Classify(ref ray, ref segment, out num, out num2);
			if (intersectionType == IntersectionTypes.Point)
			{
				if (num < -Intersection._intervalThreshold || Mathf.Abs(num2) > segment.Extent + Intersection._intervalThreshold)
				{
					intersectionType = IntersectionTypes.Empty;
				}
			}
			else if (intersectionType == IntersectionTypes.Segment)
			{
				float num4;
				float num5;
				int num3 = Intersection.FindSegment1Segment1(0f, float.PositiveInfinity, num, num2, out num4, out num5);
				if (num3 == 1)
				{
					intersectionType = IntersectionTypes.Point;
				}
				else if (num3 == 0)
				{
					intersectionType = IntersectionTypes.Empty;
				}
			}
			return intersectionType != IntersectionTypes.Empty;
		}

		public static bool TestRay2Segment2(ref Ray2 ray, ref Segment2 segment)
		{
			IntersectionTypes intersectionTypes;
			return Intersection.TestRay2Segment2(ref ray, ref segment, out intersectionTypes);
		}

		public static bool FindRay2Segment2(ref Ray2 ray, ref Segment2 segment, out Ray2Segment2Intr info)
		{
			float num;
			float num2;
			info.IntersectionType = Intersection.Classify(ref ray, ref segment, out num, out num2);
			info.Point0 = (info.Point1 = Vector2.zero);
			info.Parameter0 = (info.Parameter1 = 0f);
			if (info.IntersectionType == IntersectionTypes.Point)
			{
				if (num >= -Intersection._intervalThreshold && Mathf.Abs(num2) <= segment.Extent)
				{
					info.Point0 = ray.Center + num * ray.Direction;
					info.Parameter0 = num;
				}
				else
				{
					info.IntersectionType = IntersectionTypes.Empty;
				}
			}
			else if (info.IntersectionType == IntersectionTypes.Segment)
			{
				float num4;
				float num5;
				int num3 = Intersection.FindSegment1Segment1(0f, float.PositiveInfinity, num, num2, out num4, out num5);
				if (num3 == 2)
				{
					info.Point0 = ray.Center + num4 * ray.Direction;
					info.Point1 = ray.Center + num5 * ray.Direction;
					info.Parameter0 = num4;
					info.Parameter1 = num5;
				}
				else if (num3 == 1)
				{
					info.IntersectionType = IntersectionTypes.Point;
					info.Point0 = ray.Center + num4 * ray.Direction;
					info.Parameter0 = num4;
				}
				else
				{
					info.IntersectionType = IntersectionTypes.Empty;
				}
			}
			return info.IntersectionType != IntersectionTypes.Empty;
		}

		public static bool TestRay2Triangle2(ref Ray2 ray, ref Triangle2 triangle, out IntersectionTypes intersectionType)
		{
			float dist;
			float dist2;
			float dist3;
			int sign;
			int sign2;
			int sign3;
			int num;
			int num2;
			int num3;
			Intersection.TriangleLineRelations(ref ray.Center, ref ray.Direction, ref triangle, out dist, out dist2, out dist3, out sign, out sign2, out sign3, out num, out num2, out num3);
			if (num == 3 || num2 == 3)
			{
				intersectionType = IntersectionTypes.Empty;
			}
			else
			{
				float seg0Start;
				float seg0End;
				bool interval = Intersection.GetInterval(ref ray.Center, ref ray.Direction, ref triangle, dist, dist2, dist3, sign, sign2, sign3, out seg0Start, out seg0End);
				if (interval)
				{
					intersectionType = IntersectionTypes.Empty;
				}
				else
				{
					float num5;
					float num6;
					int num4 = Intersection.FindSegment1Segment1(seg0Start, seg0End, 0f, float.PositiveInfinity, out num5, out num6);
					if (num4 == 2)
					{
						intersectionType = IntersectionTypes.Segment;
					}
					else if (num4 == 1)
					{
						intersectionType = IntersectionTypes.Point;
					}
					else
					{
						intersectionType = IntersectionTypes.Empty;
					}
				}
			}
			return intersectionType != IntersectionTypes.Empty;
		}

		public static bool TestRay2Triangle2(ref Ray2 ray, ref Triangle2 triangle)
		{
			IntersectionTypes intersectionTypes;
			return Intersection.TestRay2Triangle2(ref ray, ref triangle, out intersectionTypes);
		}

		public static bool FindRay2Triangle2(ref Ray2 ray, ref Triangle2 triangle, out Ray2Triangle2Intr info)
		{
			float dist;
			float dist2;
			float dist3;
			int sign;
			int sign2;
			int sign3;
			int num;
			int num2;
			int num3;
			Intersection.TriangleLineRelations(ref ray.Center, ref ray.Direction, ref triangle, out dist, out dist2, out dist3, out sign, out sign2, out sign3, out num, out num2, out num3);
			if (num == 3 || num2 == 3)
			{
				info.IntersectionType = IntersectionTypes.Empty;
				info.Quantity = 0;
				info.Point0 = Vector2.zero;
				info.Point1 = Vector2.zero;
			}
			else
			{
				float seg0Start;
				float seg0End;
				bool interval = Intersection.GetInterval(ref ray.Center, ref ray.Direction, ref triangle, dist, dist2, dist3, sign, sign2, sign3, out seg0Start, out seg0End);
				if (interval)
				{
					info.IntersectionType = IntersectionTypes.Empty;
					info.Quantity = 0;
					info.Point0 = Vector2.zero;
					info.Point1 = Vector2.zero;
				}
				else
				{
					float d;
					float d2;
					info.Quantity = Intersection.FindSegment1Segment1(seg0Start, seg0End, 0f, float.PositiveInfinity, out d, out d2);
					if (info.Quantity == 2)
					{
						info.IntersectionType = IntersectionTypes.Segment;
						info.Point0 = ray.Center + d * ray.Direction;
						info.Point1 = ray.Center + d2 * ray.Direction;
					}
					else if (info.Quantity == 1)
					{
						info.IntersectionType = IntersectionTypes.Point;
						info.Point0 = ray.Center + d * ray.Direction;
						info.Point1 = Vector2.zero;
					}
					else
					{
						info.IntersectionType = IntersectionTypes.Empty;
						info.Point0 = Vector2.zero;
						info.Point1 = Vector2.zero;
					}
				}
			}
			return info.IntersectionType != IntersectionTypes.Empty;
		}

		public static bool TestSegment2AAB2(ref Segment2 segment, ref AAB2 box)
		{
			Vector2 b;
			Vector2 vector;
			box.CalcCenterExtents(out b, out vector);
			Vector2 value = segment.Center - b;
			float num = Mathf.Abs(segment.Direction.x);
			float num2 = Mathf.Abs(value.x);
			float num3 = vector.x + segment.Extent * num;
			if (num2 > num3)
			{
				return false;
			}
			float num4 = Mathf.Abs(segment.Direction.y);
			float num5 = Mathf.Abs(value.y);
			num3 = vector.y + segment.Extent * num4;
			if (num5 > num3)
			{
				return false;
			}
			Vector2 vector2 = segment.Direction.Perp();
			float num6 = Mathf.Abs(vector2.Dot(value));
			float num7 = Mathf.Abs(vector2.x);
			float num8 = Mathf.Abs(vector2.y);
			num3 = vector.x * num7 + vector.y * num8;
			return num6 <= num3;
		}

		public static bool FindSegment2AAB2(ref Segment2 segment, ref AAB2 box, out Segment2AAB2Intr info)
		{
			return Intersection.DoClipping(-segment.Extent, segment.Extent, ref segment.Center, ref segment.Direction, ref box, true, out info.Quantity, out info.Point0, out info.Point1, out info.IntersectionType);
		}

		public static bool TestSegment2Box2(ref Segment2 segment, ref Box2 box)
		{
			Vector2 vector = segment.Center - box.Center;
			float num = Mathf.Abs(segment.Direction.Dot(box.Axis0));
			float num2 = Mathf.Abs(vector.Dot(box.Axis0));
			float num3 = box.Extents.x + segment.Extent * num;
			if (num2 > num3)
			{
				return false;
			}
			float num4 = Mathf.Abs(segment.Direction.Dot(box.Axis1));
			float num5 = Mathf.Abs(vector.Dot(box.Axis1));
			num3 = box.Extents.y + segment.Extent * num4;
			if (num5 > num3)
			{
				return false;
			}
			Vector2 vector2 = segment.Direction.Perp();
			float num6 = Mathf.Abs(vector2.Dot(vector));
			float num7 = Mathf.Abs(vector2.Dot(box.Axis0));
			float num8 = Mathf.Abs(vector2.Dot(box.Axis1));
			num3 = box.Extents.x * num7 + box.Extents.y * num8;
			return num6 <= num3;
		}

		public static bool FindSegment2Box2(ref Segment2 segment, ref Box2 box, out Segment2Box2Intr info)
		{
			return Intersection.DoClipping(-segment.Extent, segment.Extent, ref segment.Center, ref segment.Direction, ref box, true, out info.Quantity, out info.Point0, out info.Point1, out info.IntersectionType);
		}

		public static bool TestSegment2Circle2(ref Segment2 segment, ref Circle2 circle)
		{
			Vector2 rhs = segment.Center - circle.Center;
			float num = rhs.sqrMagnitude - circle.Radius * circle.Radius;
			if (num <= 1E-05f)
			{
				return true;
			}
			float num2 = Vector2.Dot(segment.Direction, rhs);
			float num3 = num2 * num2 - num;
			if (num3 < -1E-05f)
			{
				return false;
			}
			float num4 = Mathf.Abs(num2);
			float num5 = segment.Extent * (segment.Extent - 2f * num4) + num;
			return num5 <= 1E-05f || num4 <= segment.Extent;
		}

		public static bool FindSegment2Circle2(ref Segment2 segment, ref Circle2 circle, out Segment2Circle2Intr info)
		{
			int num;
			float num2;
			float num3;
			bool flag = Intersection.Find(ref segment.Center, ref segment.Direction, ref circle.Center, circle.Radius, out num, out num2, out num3);
			info.Point0 = (info.Point1 = Vector2.zero);
			if (flag)
			{
				if (num == 1)
				{
					if (Mathf.Abs(num2) > segment.Extent + 1E-05f)
					{
						info.IntersectionType = IntersectionTypes.Empty;
					}
					else
					{
						info.IntersectionType = IntersectionTypes.Point;
						info.Point0 = segment.Center + num2 * segment.Direction;
					}
				}
				else
				{
					float num4 = segment.Extent + 1E-05f;
					if (num3 >= -num4 && num2 <= num4)
					{
						if (num3 <= num4)
						{
							if (num2 < -num4)
							{
								num = 1;
								num2 = num3;
							}
						}
						else
						{
							num = ((num2 >= -num4) ? 1 : 0);
						}
						switch (num)
						{
						case 0:
							IL_E4:
							info.IntersectionType = IntersectionTypes.Empty;
							goto IL_15D;
						case 1:
							info.IntersectionType = IntersectionTypes.Point;
							info.Point0 = segment.Center + num2 * segment.Direction;
							goto IL_15D;
						case 2:
							info.IntersectionType = IntersectionTypes.Segment;
							info.Point0 = segment.Center + num2 * segment.Direction;
							info.Point1 = segment.Center + num3 * segment.Direction;
							goto IL_15D;
						}
						goto IL_E4;
					}
					info.IntersectionType = IntersectionTypes.Empty;
				}
			}
			else
			{
				info.IntersectionType = IntersectionTypes.Empty;
			}
			IL_15D:
			return info.IntersectionType != IntersectionTypes.Empty;
		}

		public static bool TestSegment2ConvexPolygon2(ref Segment2 segment, Polygon2 convexPolygon)
		{
			Segment2ConvexPolygon2Intr segment2ConvexPolygon2Intr;
			return Intersection.FindSegment2ConvexPolygon2(ref segment, convexPolygon, out segment2ConvexPolygon2Intr);
		}

		public static bool FindSegment2ConvexPolygon2(ref Segment2 segment, Polygon2 convexPolygon, out Segment2ConvexPolygon2Intr info)
		{
			Edge2[] edges = convexPolygon.Edges;
			int num = edges.Length;
			float num2 = 0f;
			float num3 = 1f;
			Vector2 vector = segment.P1 - segment.P0;
			for (int i = 0; i < num; i++)
			{
				Vector2 vector2 = edges[i].Point1 - edges[i].Point0;
				Vector2 vector3 = new Vector2(vector2.y, -vector2.x);
				float num4 = vector3.Dot(edges[i].Point0 - segment.P0);
				float num5 = vector3.Dot(vector);
				if (Mathf.Abs(num5) < 1E-05f)
				{
					if (num4 < 0f)
					{
						info = default(Segment2ConvexPolygon2Intr);
						return false;
					}
				}
				else
				{
					float num6 = num4 / num5;
					if (num5 < 0f)
					{
						if (num6 > num2)
						{
							num2 = num6;
							if (num2 > num3)
							{
								info = default(Segment2ConvexPolygon2Intr);
								return false;
							}
						}
					}
					else if (num6 < num3)
					{
						num3 = num6;
						if (num3 < num2)
						{
							info = default(Segment2ConvexPolygon2Intr);
							return false;
						}
					}
				}
			}
			if (num3 - num2 > 1E-05f)
			{
				info.IntersectionType = IntersectionTypes.Segment;
				info.Quantity = 2;
				info.Point0 = segment.P0 + num2 * vector;
				info.Point1 = segment.P0 + num3 * vector;
				info.Parameter0 = num2;
				info.Parameter1 = num3;
			}
			else
			{
				info.IntersectionType = IntersectionTypes.Point;
				info.Quantity = 1;
				info.Point0 = segment.P0 + num2 * vector;
				info.Point1 = Vector2ex.Zero;
				info.Parameter0 = num2;
				info.Parameter1 = 0f;
			}
			return true;
		}

		private static IntersectionTypes Classify(ref Segment2 segment0, ref Segment2 segment1, out float s0, out float s1)
		{
			Vector2 vector = segment1.Center - segment0.Center;
			s0 = (s1 = 0f);
			float num = segment0.Direction.DotPerp(segment1.Direction);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float num2 = 1f / num;
				float num3 = vector.DotPerp(segment1.Direction);
				s0 = num3 * num2;
				float num4 = vector.DotPerp(segment0.Direction);
				s1 = num4 * num2;
				return IntersectionTypes.Point;
			}
			vector.Normalize();
			float f = vector.DotPerp(segment1.Direction);
			if (Mathf.Abs(f) <= Intersection._dotThreshold)
			{
				s0 = Vector2.Dot(segment1.P0 - segment0.Center, segment0.Direction);
				s1 = Vector2.Dot(segment1.P1 - segment0.Center, segment0.Direction);
				if (s0 > s1)
				{
					float num5 = s0;
					s0 = s1;
					s1 = num5;
				}
				return IntersectionTypes.Segment;
			}
			return IntersectionTypes.Empty;
		}

		public static bool TestSegment2Segment2(ref Segment2 segment0, ref Segment2 segment1, out IntersectionTypes intersectionType)
		{
			float num;
			float num2;
			intersectionType = Intersection.Classify(ref segment0, ref segment1, out num, out num2);
			if (intersectionType == IntersectionTypes.Point)
			{
				if (Mathf.Abs(num) > segment0.Extent + Intersection._intervalThreshold || Mathf.Abs(num2) > segment1.Extent + Intersection._intervalThreshold)
				{
					intersectionType = IntersectionTypes.Empty;
				}
			}
			else if (intersectionType == IntersectionTypes.Segment)
			{
				float num4;
				float num5;
				int num3 = Intersection.FindSegment1Segment1(-segment0.Extent, segment0.Extent, num, num2, out num4, out num5);
				if (num3 == 1)
				{
					intersectionType = IntersectionTypes.Point;
				}
				else if (num3 == 0)
				{
					intersectionType = IntersectionTypes.Empty;
				}
			}
			return intersectionType != IntersectionTypes.Empty;
		}

		public static bool TestSegment2Segment2(ref Segment2 segment0, ref Segment2 segment1)
		{
			IntersectionTypes intersectionTypes;
			return Intersection.TestSegment2Segment2(ref segment0, ref segment1, out intersectionTypes);
		}

		public static bool FindSegment2Segment2(ref Segment2 segment0, ref Segment2 segment1, out Segment2Segment2Intr info)
		{
			float num;
			float num2;
			info.IntersectionType = Intersection.Classify(ref segment0, ref segment1, out num, out num2);
			info.Point0 = (info.Point1 = Vector2.zero);
			info.Parameter0 = (info.Parameter1 = 0f);
			if (info.IntersectionType == IntersectionTypes.Point)
			{
				if (Mathf.Abs(num) <= segment0.Extent + Intersection._intervalThreshold && Mathf.Abs(num2) <= segment1.Extent + Intersection._intervalThreshold)
				{
					info.Point0 = segment0.Center + num * segment0.Direction;
					info.Parameter0 = num / (segment0.Extent * 2f) + 0.5f;
				}
				else
				{
					info.IntersectionType = IntersectionTypes.Empty;
				}
			}
			else if (info.IntersectionType == IntersectionTypes.Segment)
			{
				float num4;
				float num5;
				int num3 = Intersection.FindSegment1Segment1(-segment0.Extent, segment0.Extent, num, num2, out num4, out num5);
				if (num3 == 2)
				{
					info.Point0 = segment0.Center + num4 * segment0.Direction;
					info.Point1 = segment0.Center + num5 * segment0.Direction;
					float num6 = segment0.Extent * 2f;
					info.Parameter0 = num4 / num6 + 0.5f;
					info.Parameter1 = num5 / num6 + 0.5f;
				}
				else if (num3 == 1)
				{
					info.IntersectionType = IntersectionTypes.Point;
					info.Point0 = segment0.Center + num4 * segment0.Direction;
					float num7 = segment0.Extent * 2f;
					info.Parameter0 = num4 / num7 + 0.5f;
				}
				else
				{
					info.IntersectionType = IntersectionTypes.Empty;
				}
			}
			return info.IntersectionType != IntersectionTypes.Empty;
		}

		public static bool TestSegment2Triangle2(ref Segment2 segment, ref Triangle2 triangle, out IntersectionTypes intersectionType)
		{
			float dist;
			float dist2;
			float dist3;
			int sign;
			int sign2;
			int sign3;
			int num;
			int num2;
			int num3;
			Intersection.TriangleLineRelations(ref segment.Center, ref segment.Direction, ref triangle, out dist, out dist2, out dist3, out sign, out sign2, out sign3, out num, out num2, out num3);
			if (num == 3 || num2 == 3)
			{
				intersectionType = IntersectionTypes.Empty;
			}
			else
			{
				float seg0Start;
				float seg0End;
				bool interval = Intersection.GetInterval(ref segment.Center, ref segment.Direction, ref triangle, dist, dist2, dist3, sign, sign2, sign3, out seg0Start, out seg0End);
				if (interval)
				{
					intersectionType = IntersectionTypes.Empty;
				}
				else
				{
					float num5;
					float num6;
					int num4 = Intersection.FindSegment1Segment1(seg0Start, seg0End, -segment.Extent, segment.Extent, out num5, out num6);
					if (num4 == 2)
					{
						intersectionType = IntersectionTypes.Segment;
					}
					else if (num4 == 1)
					{
						intersectionType = IntersectionTypes.Point;
					}
					else
					{
						intersectionType = IntersectionTypes.Empty;
					}
				}
			}
			return intersectionType != IntersectionTypes.Empty;
		}

		public static bool TestSegment2Triangle2(ref Segment2 segment, ref Triangle2 triangle)
		{
			IntersectionTypes intersectionTypes;
			return Intersection.TestSegment2Triangle2(ref segment, ref triangle, out intersectionTypes);
		}

		public static bool FindSegment2Triangle2(ref Segment2 segment, ref Triangle2 triangle, out Segment2Triangle2Intr info)
		{
			float dist;
			float dist2;
			float dist3;
			int sign;
			int sign2;
			int sign3;
			int num;
			int num2;
			int num3;
			Intersection.TriangleLineRelations(ref segment.Center, ref segment.Direction, ref triangle, out dist, out dist2, out dist3, out sign, out sign2, out sign3, out num, out num2, out num3);
			if (num == 3 || num2 == 3)
			{
				info.IntersectionType = IntersectionTypes.Empty;
				info.Quantity = 0;
				info.Point0 = Vector2.zero;
				info.Point1 = Vector2.zero;
			}
			else
			{
				float seg0Start;
				float seg0End;
				bool interval = Intersection.GetInterval(ref segment.Center, ref segment.Direction, ref triangle, dist, dist2, dist3, sign, sign2, sign3, out seg0Start, out seg0End);
				if (interval)
				{
					info.IntersectionType = IntersectionTypes.Empty;
					info.Quantity = 0;
					info.Point0 = Vector2.zero;
					info.Point1 = Vector2.zero;
				}
				else
				{
					float d;
					float d2;
					info.Quantity = Intersection.FindSegment1Segment1(seg0Start, seg0End, -segment.Extent, segment.Extent, out d, out d2);
					if (info.Quantity == 2)
					{
						info.IntersectionType = IntersectionTypes.Segment;
						info.Point0 = segment.Center + d * segment.Direction;
						info.Point1 = segment.Center + d2 * segment.Direction;
					}
					else if (info.Quantity == 1)
					{
						info.IntersectionType = IntersectionTypes.Point;
						info.Point0 = segment.Center + d * segment.Direction;
						info.Point1 = Vector3.zero;
					}
					else
					{
						info.IntersectionType = IntersectionTypes.Empty;
						info.Point0 = Vector2.zero;
						info.Point1 = Vector2.zero;
					}
				}
			}
			return info.IntersectionType != IntersectionTypes.Empty;
		}

		private static int WhichSide(ref Triangle2 triangle, ref Vector2 P, ref Vector2 D)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			float num4 = D.Dot(triangle.V0 - P);
			if (num4 > 1E-05f)
			{
				num++;
			}
			else if (num4 < -1E-05f)
			{
				num2++;
			}
			else
			{
				num3++;
			}
			if (num > 0 && num2 > 0)
			{
				return 0;
			}
			num4 = D.Dot(triangle.V1 - P);
			if (num4 > 1E-05f)
			{
				num++;
			}
			else if (num4 < -1E-05f)
			{
				num2++;
			}
			else
			{
				num3++;
			}
			if (num > 0 && num2 > 0)
			{
				return 0;
			}
			num4 = D.Dot(triangle.V2 - P);
			if (num4 > 1E-05f)
			{
				num++;
			}
			else if (num4 < -1E-05f)
			{
				num2++;
			}
			else
			{
				num3++;
			}
			if (num > 0 && num2 > 0)
			{
				return 0;
			}
			if (num3 != 0)
			{
				return 0;
			}
			if (num <= 0)
			{
				return -1;
			}
			return 1;
		}

		private static void ClipConvexPolygonAgainstLine(ref Vector2 edgeStart, ref Vector2 edgeEnd, ref int quantity, ref Triangle2Triangle2Intr info)
		{
			Vector2 vector = new Vector2(edgeStart.y - edgeEnd.y, edgeEnd.x - edgeStart.x);
			float num = vector.Dot(edgeStart);
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = -1;
			Float6 @float = default(Float6);
			for (int i = 0; i < quantity; i++)
			{
				float num6 = vector.Dot(info[i]) - num;
				if (num6 > 1E-05f)
				{
					num2++;
					if (num5 < 0)
					{
						num5 = i;
					}
				}
				else if (num6 < -1E-05f)
				{
					num3++;
				}
				else
				{
					num6 = 0f;
					num4++;
				}
				@float[i] = num6;
			}
			if (num2 > 0)
			{
				if (num3 > 0)
				{
					Triangle2Triangle2Intr triangle2Triangle2Intr = default(Triangle2Triangle2Intr);
					int num7 = 0;
					if (num5 > 0)
					{
						int j = num5;
						int i2 = j - 1;
						float d = @float[j] / (@float[j] - @float[i2]);
						triangle2Triangle2Intr[num7++] = info[j] + d * (info[i2] - info[j]);
						while (j < quantity && @float[j] > 0f)
						{
							triangle2Triangle2Intr[num7++] = info[j++];
						}
						if (j < quantity)
						{
							i2 = j - 1;
						}
						else
						{
							j = 0;
							i2 = quantity - 1;
						}
						d = @float[j] / (@float[j] - @float[i2]);
						triangle2Triangle2Intr[num7++] = info[j] + d * (info[i2] - info[j]);
					}
					else
					{
						int j = 0;
						while (j < quantity && @float[j] > 0f)
						{
							triangle2Triangle2Intr[num7++] = info[j++];
						}
						int i2 = j - 1;
						float d2 = @float[j] / (@float[j] - @float[i2]);
						triangle2Triangle2Intr[num7++] = info[j] + d2 * (info[i2] - info[j]);
						while (j < quantity && @float[j] <= 0f)
						{
							j++;
						}
						if (j < quantity)
						{
							i2 = j - 1;
							d2 = @float[j] / (@float[j] - @float[i2]);
							triangle2Triangle2Intr[num7++] = info[j] + d2 * (info[i2] - info[j]);
							while (j < quantity)
							{
								if (@float[j] <= 0f)
								{
									break;
								}
								triangle2Triangle2Intr[num7++] = info[j++];
							}
						}
						else
						{
							i2 = quantity - 1;
							d2 = @float[0] / (@float[0] - @float[i2]);
							triangle2Triangle2Intr[num7++] = info[0] + d2 * (info[i2] - info[0]);
						}
					}
					quantity = num7;
					info = triangle2Triangle2Intr;
					return;
				}
			}
			else
			{
				if (num4 == 0)
				{
					quantity = 0;
					return;
				}
				int num8 = (Mathf.Abs(vector.y) > Mathf.Abs(vector.x)) ? 1 : 0;
				float num9 = float.PositiveInfinity;
				float num10 = float.NegativeInfinity;
				float seg1Start;
				float seg1End;
				if (num8 == 0)
				{
					for (int k = 0; k < quantity; k++)
					{
						if (@float[k] == 0f)
						{
							float y = info[k].y;
							if (y > num10)
							{
								num10 = y;
							}
							if (y < num9)
							{
								num9 = y;
							}
						}
					}
					if (edgeStart.y < edgeEnd.y)
					{
						seg1Start = edgeStart.y;
						seg1End = edgeEnd.y;
					}
					else
					{
						seg1Start = edgeEnd.y;
						seg1End = edgeStart.y;
					}
				}
				else
				{
					for (int l = 0; l < quantity; l++)
					{
						if (@float[l] == 0f)
						{
							float x = info[l].x;
							if (x > num10)
							{
								num10 = x;
							}
							if (x < num9)
							{
								num9 = x;
							}
						}
					}
					if (edgeStart.x < edgeEnd.x)
					{
						seg1Start = edgeStart.x;
						seg1End = edgeEnd.x;
					}
					else
					{
						seg1Start = edgeEnd.x;
						seg1End = edgeStart.x;
					}
				}
				float num12;
				float num13;
				int num11 = Intersection.FindSegment1Segment1(num9, num10, seg1Start, seg1End, out num12, out num13);
				if (num11 > 0)
				{
					if (num8 == 0)
					{
						info.Point0 = new Vector2((num - vector.y * num12) / vector.x, num12);
						if (num11 == 2)
						{
							info.Point1 = new Vector2((num - vector.y * num13) / vector.x, num13);
						}
					}
					else
					{
						info.Point0 = new Vector2(num12, (num - vector.x * num12) / vector.y);
						if (num11 == 2)
						{
							info.Point1 = new Vector2(num13, (num - vector.x * num13) / vector.y);
						}
					}
					info.IntersectionType = ((num11 == 1) ? IntersectionTypes.Point : IntersectionTypes.Segment);
					info.Quantity = num11;
					quantity = -1;
					return;
				}
				quantity = 0;
			}
		}

		public static bool TestTriangle2Triangle2(ref Triangle2 triangle0, ref Triangle2 triangle1)
		{
			Vector2 vector = triangle0.V0;
			Vector2 vector2 = triangle0.V1;
			Vector2 vector3;
			vector3.x = vector2.y - vector.y;
			vector3.y = vector.x - vector2.x;
			if (Intersection.WhichSide(ref triangle1, ref vector, ref vector3) > 0)
			{
				return false;
			}
			vector = triangle0.V1;
			vector2 = triangle0.V2;
			vector3.x = vector2.y - vector.y;
			vector3.y = vector.x - vector2.x;
			if (Intersection.WhichSide(ref triangle1, ref vector, ref vector3) > 0)
			{
				return false;
			}
			vector = triangle0.V2;
			vector2 = triangle0.V0;
			vector3.x = vector2.y - vector.y;
			vector3.y = vector.x - vector2.x;
			if (Intersection.WhichSide(ref triangle1, ref vector, ref vector3) > 0)
			{
				return false;
			}
			vector = triangle1.V0;
			vector2 = triangle1.V1;
			vector3.x = vector2.y - vector.y;
			vector3.y = vector.x - vector2.x;
			if (Intersection.WhichSide(ref triangle0, ref vector, ref vector3) > 0)
			{
				return false;
			}
			vector = triangle1.V1;
			vector2 = triangle1.V2;
			vector3.x = vector2.y - vector.y;
			vector3.y = vector.x - vector2.x;
			if (Intersection.WhichSide(ref triangle0, ref vector, ref vector3) > 0)
			{
				return false;
			}
			vector = triangle1.V2;
			vector2 = triangle1.V0;
			vector3.x = vector2.y - vector.y;
			vector3.y = vector.x - vector2.x;
			return Intersection.WhichSide(ref triangle0, ref vector, ref vector3) <= 0;
		}

		public static bool FindTriangle2Triangle2(ref Triangle2 triangle0, ref Triangle2 triangle1, out Triangle2Triangle2Intr info)
		{
			info = default(Triangle2Triangle2Intr);
			info.Point0 = triangle1.V0;
			info.Point1 = triangle1.V1;
			info.Point2 = triangle1.V2;
			int num = 3;
			Intersection.ClipConvexPolygonAgainstLine(ref triangle0.V2, ref triangle0.V0, ref num, ref info);
			if (num == 0)
			{
				return false;
			}
			if (num < 0)
			{
				return true;
			}
			Intersection.ClipConvexPolygonAgainstLine(ref triangle0.V0, ref triangle0.V1, ref num, ref info);
			if (num == 0)
			{
				return false;
			}
			if (num < 0)
			{
				return true;
			}
			Intersection.ClipConvexPolygonAgainstLine(ref triangle0.V1, ref triangle0.V2, ref num, ref info);
			if (num == 0)
			{
				return false;
			}
			if (num < 0)
			{
				return true;
			}
			info.IntersectionType = IntersectionTypes.Polygon;
			info.Quantity = num;
			return true;
		}

		public static bool TestAAB3AAB3(ref AAB3 box0, ref AAB3 box1)
		{
			return box0.Max.x >= box1.Min.x && box0.Min.x <= box1.Max.x && box0.Max.y >= box1.Min.y && box0.Min.y <= box1.Max.y && box0.Max.z >= box1.Min.z && box0.Min.z <= box1.Max.z;
		}

		public static bool FindAAB3AAB3(ref AAB3 box0, ref AAB3 box1, out AAB3 intersection)
		{
			if (box0.Max.x < box1.Min.x || box0.Min.x > box1.Max.x)
			{
				intersection = default(AAB3);
				return false;
			}
			if (box0.Max.y < box1.Min.y || box0.Min.y > box1.Max.y)
			{
				intersection = default(AAB3);
				return false;
			}
			if (box0.Max.z < box1.Min.z || box0.Min.z > box1.Max.z)
			{
				intersection = default(AAB3);
				return false;
			}
			intersection.Max.x = ((box0.Max.x <= box1.Max.x) ? box0.Max.x : box1.Max.x);
			intersection.Min.x = ((box0.Min.x <= box1.Min.x) ? box1.Min.x : box0.Min.x);
			intersection.Max.y = ((box0.Max.y <= box1.Max.y) ? box0.Max.y : box1.Max.y);
			intersection.Min.y = ((box0.Min.y <= box1.Min.y) ? box1.Min.y : box0.Min.y);
			intersection.Max.z = ((box0.Max.z <= box1.Max.z) ? box0.Max.z : box1.Max.z);
			intersection.Min.z = ((box0.Min.z <= box1.Min.z) ? box1.Min.z : box0.Min.z);
			return true;
		}

		public static bool TestAAB3AAB3OverlapX(ref AAB3 box0, ref AAB3 box1)
		{
			return box0.Max.x >= box1.Min.x && box0.Min.x <= box1.Max.x;
		}

		public static bool TestAAB3AAB3OverlapY(ref AAB3 box0, ref AAB3 box1)
		{
			return box0.Max.y >= box1.Min.y && box0.Min.y <= box1.Max.y;
		}

		public static bool TestAAB3AAB3OverlapZ(ref AAB3 box0, ref AAB3 box1)
		{
			return box0.Max.z >= box1.Min.z && box0.Min.z <= box1.Max.z;
		}

		public static bool TestAAB3Sphere3(ref AAB3 box, ref Sphere3 sphere)
		{
			float num = 0f;
			float num2 = sphere.Center.x;
			if (num2 < box.Min.x)
			{
				float num3 = num2 - box.Min.x;
				num += num3 * num3;
			}
			else if (num2 > box.Max.x)
			{
				float num3 = num2 - box.Max.x;
				num += num3 * num3;
			}
			num2 = sphere.Center.y;
			if (num2 < box.Min.y)
			{
				float num3 = num2 - box.Min.y;
				num += num3 * num3;
			}
			else if (num2 > box.Max.y)
			{
				float num3 = num2 - box.Max.y;
				num += num3 * num3;
			}
			num2 = sphere.Center.z;
			if (num2 < box.Min.z)
			{
				float num3 = num2 - box.Min.z;
				num += num3 * num3;
			}
			else if (num2 > box.Max.z)
			{
				float num3 = num2 - box.Max.z;
				num += num3 * num3;
			}
			return num <= sphere.Radius * sphere.Radius;
		}

		public static bool TestBox3Box3(ref Box3 box0, ref Box3 box1)
		{
			float num = 0.99999f;
			bool flag = false;
			Vector3 axis = box0.Axis0;
			Vector3 axis2 = box0.Axis1;
			Vector3 axis3 = box0.Axis2;
			Vector3 axis4 = box1.Axis0;
			Vector3 axis5 = box1.Axis1;
			Vector3 axis6 = box1.Axis2;
			float x = box0.Extents.x;
			float y = box0.Extents.y;
			float z = box0.Extents.z;
			float x2 = box1.Extents.x;
			float y2 = box1.Extents.y;
			float z2 = box1.Extents.z;
			Vector3 value = box1.Center - box0.Center;
			float num2 = axis.Dot(axis4);
			float num3 = Mathf.Abs(num2);
			if (num3 > num)
			{
				flag = true;
			}
			float num4 = axis.Dot(axis5);
			float num5 = Mathf.Abs(num4);
			if (num5 > num)
			{
				flag = true;
			}
			float num6 = axis.Dot(axis6);
			float num7 = Mathf.Abs(num6);
			if (num7 > num)
			{
				flag = true;
			}
			float num8 = axis.Dot(value);
			float num9 = Mathf.Abs(num8);
			float num10 = x2 * num3 + y2 * num5 + z2 * num7;
			float num11 = x + num10;
			if (num9 > num11)
			{
				return false;
			}
			float num12 = axis2.Dot(axis4);
			float num13 = Mathf.Abs(num12);
			if (num13 > num)
			{
				flag = true;
			}
			float num14 = axis2.Dot(axis5);
			float num15 = Mathf.Abs(num14);
			if (num15 > num)
			{
				flag = true;
			}
			float num16 = axis2.Dot(axis6);
			float num17 = Mathf.Abs(num16);
			if (num17 > num)
			{
				flag = true;
			}
			float num18 = axis2.Dot(value);
			num9 = Mathf.Abs(num18);
			num10 = x2 * num13 + y2 * num15 + z2 * num17;
			num11 = y + num10;
			if (num9 > num11)
			{
				return false;
			}
			float num19 = axis3.Dot(axis4);
			float num20 = Mathf.Abs(num19);
			if (num20 > num)
			{
				flag = true;
			}
			float num21 = axis3.Dot(axis5);
			float num22 = Mathf.Abs(num21);
			if (num22 > num)
			{
				flag = true;
			}
			float num23 = axis3.Dot(axis6);
			float num24 = Mathf.Abs(num23);
			if (num24 > num)
			{
				flag = true;
			}
			float num25 = axis3.Dot(value);
			num9 = Mathf.Abs(num25);
			num10 = x2 * num20 + y2 * num22 + z2 * num24;
			num11 = z + num10;
			if (num9 > num11)
			{
				return false;
			}
			num9 = Mathf.Abs(axis4.Dot(value));
			float num26 = x * num3 + y * num13 + z * num20;
			num11 = num26 + x2;
			if (num9 > num11)
			{
				return false;
			}
			num9 = Mathf.Abs(axis5.Dot(value));
			num26 = x * num5 + y * num15 + z * num22;
			num11 = num26 + y2;
			if (num9 > num11)
			{
				return false;
			}
			num9 = Mathf.Abs(axis6.Dot(value));
			num26 = x * num7 + y * num17 + z * num24;
			num11 = num26 + z2;
			if (num9 > num11)
			{
				return false;
			}
			if (flag)
			{
				return true;
			}
			num9 = Mathf.Abs(num25 * num12 - num18 * num19);
			num26 = y * num20 + z * num13;
			num10 = y2 * num7 + z2 * num5;
			num11 = num26 + num10;
			if (num9 > num11)
			{
				return false;
			}
			num9 = Mathf.Abs(num25 * num14 - num18 * num21);
			num26 = y * num22 + z * num15;
			num10 = x2 * num7 + z2 * num3;
			num11 = num26 + num10;
			if (num9 > num11)
			{
				return false;
			}
			num9 = Mathf.Abs(num25 * num16 - num18 * num23);
			num26 = y * num24 + z * num17;
			num10 = x2 * num5 + y2 * num3;
			num11 = num26 + num10;
			if (num9 > num11)
			{
				return false;
			}
			num9 = Mathf.Abs(num8 * num19 - num25 * num2);
			num26 = x * num20 + z * num3;
			num10 = y2 * num17 + z2 * num15;
			num11 = num26 + num10;
			if (num9 > num11)
			{
				return false;
			}
			num9 = Mathf.Abs(num8 * num21 - num25 * num4);
			num26 = x * num22 + z * num5;
			num10 = x2 * num17 + z2 * num13;
			num11 = num26 + num10;
			if (num9 > num11)
			{
				return false;
			}
			num9 = Mathf.Abs(num8 * num23 - num25 * num6);
			num26 = x * num24 + z * num7;
			num10 = x2 * num15 + y2 * num13;
			num11 = num26 + num10;
			if (num9 > num11)
			{
				return false;
			}
			num9 = Mathf.Abs(num18 * num2 - num8 * num12);
			num26 = x * num13 + y * num3;
			num10 = y2 * num24 + z2 * num22;
			num11 = num26 + num10;
			if (num9 > num11)
			{
				return false;
			}
			num9 = Mathf.Abs(num18 * num4 - num8 * num14);
			num26 = x * num15 + y * num5;
			num10 = x2 * num24 + z2 * num20;
			num11 = num26 + num10;
			if (num9 > num11)
			{
				return false;
			}
			num9 = Mathf.Abs(num18 * num6 - num8 * num16);
			num26 = x * num17 + y * num7;
			num10 = x2 * num22 + y2 * num20;
			num11 = num26 + num10;
			return num9 <= num11;
		}

		public static bool TestBox3Capsule3(ref Box3 box, ref Capsule3 capsule)
		{
			float num = Distance.Segment3Box3(ref capsule.Segment, ref box);
			return num <= capsule.Radius;
		}

		public static bool TestBox3Sphere3(ref Box3 box, ref Sphere3 sphere)
		{
			float num = 0f;
			Vector3 vector = sphere.Center - box.Center;
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
			num2 = vector.Dot(box.Axis1);
			num3 = box.Extents.y;
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
			num2 = vector.Dot(box.Axis2);
			num3 = box.Extents.z;
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
			return num <= sphere.Radius * sphere.Radius;
		}

		private static bool DoClipping(float t0, float t1, ref Vector3 origin, ref Vector3 direction, ref AAB3 box, bool solid, out int quantity, out Vector3 point0, out Vector3 point1, out IntersectionTypes intrType)
		{
			Vector3 vector;
			Vector3 vector2;
			box.CalcCenterExtents(out vector, out vector2);
			Vector3 vector3 = new Vector3(origin.x - vector.x, origin.y - vector.y, origin.z - vector.z);
			float num = t0;
			float num2 = t1;
			bool flag = Intersection.Clip(direction.x, -vector3.x - vector2.x, ref t0, ref t1) && Intersection.Clip(-direction.x, vector3.x - vector2.x, ref t0, ref t1) && Intersection.Clip(direction.y, -vector3.y - vector2.y, ref t0, ref t1) && Intersection.Clip(-direction.y, vector3.y - vector2.y, ref t0, ref t1) && Intersection.Clip(direction.z, -vector3.z - vector2.z, ref t0, ref t1) && Intersection.Clip(-direction.z, vector3.z - vector2.z, ref t0, ref t1);
			if (flag && (solid || t0 != num || t1 != num2))
			{
				if (t1 > t0)
				{
					intrType = IntersectionTypes.Segment;
					quantity = 2;
					point0 = origin + t0 * direction;
					point1 = origin + t1 * direction;
				}
				else
				{
					intrType = IntersectionTypes.Point;
					quantity = 1;
					point0 = origin + t0 * direction;
					point1 = Vector3ex.Zero;
				}
			}
			else
			{
				intrType = IntersectionTypes.Empty;
				quantity = 0;
				point0 = Vector3ex.Zero;
				point1 = Vector3ex.Zero;
			}
			return intrType != IntersectionTypes.Empty;
		}

		public static bool TestLine3AAB3(ref Line3 line, ref AAB3 box)
		{
			Vector3 b;
			Vector3 vector;
			box.CalcCenterExtents(out b, out vector);
			Vector3 value = line.Center - b;
			Vector3 vector2 = line.Direction.Cross(value);
			float num = Mathf.Abs(line.Direction.y);
			float num2 = Mathf.Abs(line.Direction.z);
			float num3 = Mathf.Abs(vector2.x);
			float num4 = vector.y * num2 + vector.z * num;
			if (num3 > num4)
			{
				return false;
			}
			float num5 = Mathf.Abs(line.Direction.x);
			float num6 = Mathf.Abs(vector2.y);
			num4 = vector.x * num2 + vector.z * num5;
			if (num6 > num4)
			{
				return false;
			}
			float num7 = Mathf.Abs(vector2.z);
			num4 = vector.x * num + vector.y * num5;
			return num7 <= num4;
		}

		public static bool FindLine3AAB3(ref Line3 line, ref AAB3 box, out Line3AAB3Intr info)
		{
			return Intersection.DoClipping(float.NegativeInfinity, float.PositiveInfinity, ref line.Center, ref line.Direction, ref box, true, out info.Quantity, out info.Point0, out info.Point1, out info.IntersectionType);
		}

		private static bool DoClipping(float t0, float t1, ref Vector3 origin, ref Vector3 direction, ref Box3 box, bool solid, out int quantity, out Vector3 point0, out Vector3 point1, out IntersectionTypes intrType)
		{
			Vector3 vector = origin - box.Center;
			Vector3 vector2 = new Vector3(vector.Dot(box.Axis0), vector.Dot(box.Axis1), vector.Dot(box.Axis2));
			Vector3 vector3 = new Vector3(direction.Dot(box.Axis0), direction.Dot(box.Axis1), direction.Dot(box.Axis2));
			float num = t0;
			float num2 = t1;
			bool flag = Intersection.Clip(vector3.x, -vector2.x - box.Extents.x, ref t0, ref t1) && Intersection.Clip(-vector3.x, vector2.x - box.Extents.x, ref t0, ref t1) && Intersection.Clip(vector3.y, -vector2.y - box.Extents.y, ref t0, ref t1) && Intersection.Clip(-vector3.y, vector2.y - box.Extents.y, ref t0, ref t1) && Intersection.Clip(vector3.z, -vector2.z - box.Extents.z, ref t0, ref t1) && Intersection.Clip(-vector3.z, vector2.z - box.Extents.z, ref t0, ref t1);
			if (flag && (solid || t0 != num || t1 != num2))
			{
				if (t1 > t0)
				{
					intrType = IntersectionTypes.Segment;
					quantity = 2;
					point0 = origin + t0 * direction;
					point1 = origin + t1 * direction;
				}
				else
				{
					intrType = IntersectionTypes.Point;
					quantity = 1;
					point0 = origin + t0 * direction;
					point1 = Vector3ex.Zero;
				}
			}
			else
			{
				intrType = IntersectionTypes.Empty;
				quantity = 0;
				point0 = Vector3ex.Zero;
				point1 = Vector3ex.Zero;
			}
			return intrType != IntersectionTypes.Empty;
		}

		public static bool TestLine3Box3(ref Line3 line, ref Box3 box)
		{
			Vector3 value = line.Center - box.Center;
			Vector3 vector = line.Direction.Cross(value);
			float num = Mathf.Abs(line.Direction.Dot(box.Axis1));
			float num2 = Mathf.Abs(line.Direction.Dot(box.Axis2));
			float num3 = Mathf.Abs(vector.Dot(box.Axis0));
			float num4 = box.Extents.y * num2 + box.Extents.z * num;
			if (num3 > num4)
			{
				return false;
			}
			float num5 = Mathf.Abs(line.Direction.Dot(box.Axis0));
			float num6 = Mathf.Abs(vector.Dot(box.Axis1));
			num4 = box.Extents.x * num2 + box.Extents.z * num5;
			if (num6 > num4)
			{
				return false;
			}
			float num7 = Mathf.Abs(vector.Dot(box.Axis2));
			num4 = box.Extents.x * num + box.Extents.y * num5;
			return num7 <= num4;
		}

		public static bool FindLine3Box3(ref Line3 line, ref Box3 box, out Line3Box3Intr info)
		{
			return Intersection.DoClipping(float.NegativeInfinity, float.PositiveInfinity, ref line.Center, ref line.Direction, ref box, true, out info.Quantity, out info.Point0, out info.Point1, out info.IntersectionType);
		}

		public static bool TestLine3Circle3(ref Line3 line, ref Circle3 circle)
		{
			Line3Circle3Intr line3Circle3Intr;
			return Intersection.FindLine3Circle3(ref line, ref circle, out line3Circle3Intr);
		}

		public static bool FindLine3Circle3(ref Line3 line, ref Circle3 circle, out Line3Circle3Intr info)
		{
			float num = line.Direction.Dot(circle.Normal);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float num2 = circle.Normal.Dot(line.Center - circle.Center);
				float t = -num2 / num;
				Vector3 vector = line.Eval(t);
				if ((vector - circle.Center).sqrMagnitude <= circle.Radius * circle.Radius)
				{
					info.IntersectionType = IntersectionTypes.Point;
					info.Point = vector;
					return true;
				}
			}
			info.IntersectionType = IntersectionTypes.Empty;
			info.Point = Vector3.zero;
			return false;
		}

		public static bool TestLine3Plane3(ref Line3 line, ref Plane3 plane, out IntersectionTypes intersectionType)
		{
			float f = line.Direction.Dot(plane.Normal);
			if (Mathf.Abs(f) > Intersection._dotThreshold)
			{
				intersectionType = IntersectionTypes.Point;
				return true;
			}
			float f2 = plane.SignedDistanceTo(ref line.Center);
			if (Mathf.Abs(f2) <= Intersection._distanceThreshold)
			{
				intersectionType = IntersectionTypes.Line;
				return true;
			}
			intersectionType = IntersectionTypes.Empty;
			return false;
		}

		public static bool TestLine3Plane3(ref Line3 line, ref Plane3 plane)
		{
			IntersectionTypes intersectionTypes;
			return Intersection.TestLine3Plane3(ref line, ref plane, out intersectionTypes);
		}

		public static bool FindLine3Plane3(ref Line3 line, ref Plane3 plane, out Line3Plane3Intr info)
		{
			float num = line.Direction.Dot(plane.Normal);
			float num2 = plane.SignedDistanceTo(ref line.Center);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				info.LineParameter = -num2 / num;
				info.IntersectionType = IntersectionTypes.Point;
				info.Point = line.Eval(info.LineParameter);
				return true;
			}
			if (Mathf.Abs(num2) <= Intersection._distanceThreshold)
			{
				info.LineParameter = 0f;
				info.IntersectionType = IntersectionTypes.Line;
				info.Point = Vector3.zero;
				return true;
			}
			info.IntersectionType = IntersectionTypes.Empty;
			info.LineParameter = 0f;
			info.Point = Vector3.zero;
			return false;
		}

		public static bool TestLine3Polygon3(ref Line3 line, Polygon3 polygon)
		{
			Line3Polygon3Intr line3Polygon3Intr;
			return Intersection.FindLine3Polygon3(ref line, polygon, out line3Polygon3Intr);
		}

		public static bool FindLine3Polygon3(ref Line3 line, Polygon3 polygon, out Line3Polygon3Intr info)
		{
			Plane3 plane = polygon.Plane;
			float num = line.Direction.Dot(plane.Normal);
			float num2 = plane.SignedDistanceTo(ref line.Center);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float t = -num2 / num;
				Vector3 vector = line.Eval(t);
				ProjectionPlanes projectionPlane = plane.Normal.GetProjectionPlane();
				Polygon2 polygon2 = Polygon2.CreateProjected(polygon, projectionPlane);
				Vector2 point = vector.ToVector2(projectionPlane);
				bool flag = polygon2.ContainsSimple(point);
				if (flag)
				{
					info.IntersectionType = IntersectionTypes.Point;
					info.Point = vector;
					return true;
				}
			}
			info.IntersectionType = IntersectionTypes.Empty;
			info.Point = Vector3.zero;
			return false;
		}

		private static bool Point3InsideRectangle3(ref Vector3 point, ref Rectangle3 rectangle)
		{
			Vector3 vector = point - rectangle.Center;
			float num = vector.Dot(rectangle.Axis0);
			float num2 = vector.Dot(rectangle.Axis1);
			float num3 = rectangle.Extents.x;
			if (num < -num3)
			{
				return false;
			}
			if (num > num3)
			{
				return false;
			}
			num3 = rectangle.Extents.y;
			return num2 >= -num3 && num2 <= num3;
		}

		public static bool TestLine3Rectangle3(ref Line3 line, ref Rectangle3 rectangle)
		{
			Line3Rectangle3Intr line3Rectangle3Intr;
			return Intersection.FindLine3Rectangle3(ref line, ref rectangle, out line3Rectangle3Intr);
		}

		public static bool FindLine3Rectangle3(ref Line3 line, ref Rectangle3 rectangle, out Line3Rectangle3Intr info)
		{
			float num = line.Direction.Dot(rectangle.Normal);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float num2 = rectangle.Normal.Dot(line.Center - rectangle.Center);
				float t = -num2 / num;
				Vector3 point = line.Eval(t);
				bool flag = Intersection.Point3InsideRectangle3(ref point, ref rectangle);
				if (flag)
				{
					info.IntersectionType = IntersectionTypes.Point;
					info.Point = point;
					return true;
				}
			}
			info.IntersectionType = IntersectionTypes.Empty;
			info.Point = Vector3.zero;
			return false;
		}

		public static bool TestLine3Sphere3(ref Line3 line, ref Sphere3 sphere)
		{
			Vector3 value = line.Center - sphere.Center;
			float num = value.sqrMagnitude - sphere.Radius * sphere.Radius;
			float num2 = line.Direction.Dot(value);
			float num3 = num2 * num2 - num;
			return num3 >= -1E-05f;
		}

		public static bool FindLine3Sphere3(ref Line3 line, ref Sphere3 sphere, out Line3Sphere3Intr info)
		{
			Vector3 vector = line.Center - sphere.Center;
			float num = vector.Dot(vector) - sphere.Radius * sphere.Radius;
			float num2 = line.Direction.Dot(vector);
			float num3 = num2 * num2 - num;
			if (num3 < -1E-05f)
			{
				info = default(Line3Sphere3Intr);
			}
			else if (num3 > 1E-05f)
			{
				float num4 = Mathf.Sqrt(num3);
				info.LineParameter0 = -num2 - num4;
				info.LineParameter1 = -num2 + num4;
				info.Point0 = line.Center + info.LineParameter0 * line.Direction;
				info.Point1 = line.Center + info.LineParameter1 * line.Direction;
				info.IntersectionType = IntersectionTypes.Segment;
				info.Quantity = 2;
			}
			else
			{
				info.LineParameter0 = -num2;
				info.LineParameter1 = 0f;
				info.Point0 = line.Center + info.LineParameter0 * line.Direction;
				info.Point1 = Vector3.zero;
				info.IntersectionType = IntersectionTypes.Point;
				info.Quantity = 1;
			}
			return (float)info.Quantity > 0f;
		}

		public static bool TestLine3Triangle3(ref Line3 line, ref Triangle3 triangle, out IntersectionTypes intersectionType)
		{
			Vector3 vector = line.Center - triangle.V0;
			Vector3 vector2 = triangle.V1 - triangle.V0;
			Vector3 value = triangle.V2 - triangle.V0;
			Vector3 value2 = vector2.Cross(value);
			float num = line.Direction.Dot(value2);
			float num2;
			if (num > Intersection._dotThreshold)
			{
				num2 = 1f;
			}
			else
			{
				if (num >= -Intersection._dotThreshold)
				{
					intersectionType = IntersectionTypes.Empty;
					return false;
				}
				num2 = -1f;
				num = -num;
			}
			float num3 = num2 * line.Direction.Dot(vector.Cross(value));
			if (num3 >= -1E-05f)
			{
				float num4 = num2 * line.Direction.Dot(vector2.Cross(vector));
				if (num4 >= -1E-05f && num3 + num4 <= num + 1E-05f)
				{
					intersectionType = IntersectionTypes.Point;
					return true;
				}
			}
			intersectionType = IntersectionTypes.Empty;
			return false;
		}

		public static bool TestLine3Triangle3(ref Line3 line, ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, out IntersectionTypes intersectionType)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			return Intersection.TestLine3Triangle3(ref line, ref triangle, out intersectionType);
		}

		public static bool TestLine3Triangle3(ref Line3 line, Vector3 v0, Vector3 v1, Vector3 v2, out IntersectionTypes intersectionType)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			return Intersection.TestLine3Triangle3(ref line, ref triangle, out intersectionType);
		}

		public static bool TestLine3Triangle3(ref Line3 line, ref Triangle3 triangle)
		{
			IntersectionTypes intersectionTypes;
			return Intersection.TestLine3Triangle3(ref line, ref triangle, out intersectionTypes);
		}

		public static bool TestLine3Triangle3(ref Line3 line, ref Vector3 v0, ref Vector3 v1, ref Vector3 v2)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			IntersectionTypes intersectionTypes;
			return Intersection.TestLine3Triangle3(ref line, ref triangle, out intersectionTypes);
		}

		public static bool TestLine3Triangle3(ref Line3 line, Vector3 v0, Vector3 v1, Vector3 v2)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			IntersectionTypes intersectionTypes;
			return Intersection.TestLine3Triangle3(ref line, ref triangle, out intersectionTypes);
		}

		public static bool FindLine3Triangle3(ref Line3 line, ref Triangle3 triangle, out Line3Triangle3Intr info)
		{
			Vector3 vector = line.Center - triangle.V0;
			Vector3 vector2 = triangle.V1 - triangle.V0;
			Vector3 value = triangle.V2 - triangle.V0;
			Vector3 value2 = vector2.Cross(value);
			float num = line.Direction.Dot(value2);
			float num2;
			if (num > Intersection._dotThreshold)
			{
				num2 = 1f;
			}
			else
			{
				if (num >= -Intersection._dotThreshold)
				{
					info = default(Line3Triangle3Intr);
					return false;
				}
				num2 = -1f;
				num = -num;
			}
			float num3 = num2 * line.Direction.Dot(vector.Cross(value));
			if (num3 >= -1E-05f)
			{
				float num4 = num2 * line.Direction.Dot(vector2.Cross(vector));
				if (num4 >= -1E-05f && num3 + num4 <= num + 1E-05f)
				{
					float num5 = -num2 * vector.Dot(value2);
					float num6 = 1f / num;
					info.IntersectionType = IntersectionTypes.Point;
					info.LineParameter = num5 * num6;
					info.Point = line.Eval(info.LineParameter);
					info.TriBary1 = num3 * num6;
					info.TriBary2 = num4 * num6;
					info.TriBary0 = 1f - info.TriBary1 - info.TriBary2;
					return true;
				}
			}
			info = default(Line3Triangle3Intr);
			return false;
		}

		public static bool FindLine3Triangle3(ref Line3 line, ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, out Line3Triangle3Intr info)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			return Intersection.FindLine3Triangle3(ref line, ref triangle, out info);
		}

		public static bool FindLine3Triangle3(ref Line3 line, Vector3 v0, Vector3 v1, Vector3 v2, out Line3Triangle3Intr info)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			return Intersection.FindLine3Triangle3(ref line, ref triangle, out info);
		}

		public static bool TestPlane3AAB3(ref Plane3 plane, ref AAB3 box)
		{
			Vector3 vector;
			Vector3 vector2;
			if (plane.Normal.x >= 0f)
			{
				vector.x = box.Min.x;
				vector2.x = box.Max.x;
			}
			else
			{
				vector.x = box.Max.x;
				vector2.x = box.Min.x;
			}
			if (plane.Normal.y >= 0f)
			{
				vector.y = box.Min.y;
				vector2.y = box.Max.y;
			}
			else
			{
				vector.y = box.Max.y;
				vector2.y = box.Min.y;
			}
			if (plane.Normal.z >= 0f)
			{
				vector.z = box.Min.z;
				vector2.z = box.Max.z;
			}
			else
			{
				vector.z = box.Max.z;
				vector2.z = box.Min.z;
			}
			return plane.SignedDistanceTo(ref vector) < 0f && plane.SignedDistanceTo(ref vector2) > 0f;
		}

		public static bool TestPlane3Box3(ref Plane3 plane, ref Box3 box)
		{
			float f = box.Extents.x * plane.Normal.Dot(box.Axis0);
			float f2 = box.Extents.y * plane.Normal.Dot(box.Axis1);
			float f3 = box.Extents.z * plane.Normal.Dot(box.Axis2);
			float num = Mathf.Abs(f) + Mathf.Abs(f2) + Mathf.Abs(f3);
			float f4 = plane.SignedDistanceTo(ref box.Center);
			return Mathf.Abs(f4) <= num;
		}

		public static bool TestPlane3Plane3(ref Plane3 plane0, ref Plane3 plane1)
		{
			float f = plane0.Normal.Dot(plane1.Normal);
			return Mathf.Abs(f) < 0.99999f;
		}

		public static bool FindPlane3Plane3(ref Plane3 plane0, ref Plane3 plane1, out Plane3Plane3Intr info)
		{
			float num = plane0.Normal.Dot(plane1.Normal);
			if (Mathf.Abs(num) < 0.99999f)
			{
				float num2 = 1f / (1f - num * num);
				float d = (plane0.Constant - num * plane1.Constant) * num2;
				float d2 = (plane1.Constant - num * plane0.Constant) * num2;
				info.IntersectionType = IntersectionTypes.Line;
				info.Line.Center = d * plane0.Normal + d2 * plane1.Normal;
				info.Line.Direction = plane0.Normal.UnitCross(plane1.Normal);
				return true;
			}
			float f;
			if (num >= 0f)
			{
				f = plane0.Constant - plane1.Constant;
			}
			else
			{
				f = plane0.Constant + plane1.Constant;
			}
			if (Mathf.Abs(f) < 1E-05f)
			{
				info.IntersectionType = IntersectionTypes.Plane;
				info.Line = default(Line3);
				return true;
			}
			info.IntersectionType = IntersectionTypes.Empty;
			info.Line = default(Line3);
			return false;
		}

		public static bool TestPlane3Sphere3(ref Plane3 plane, ref Sphere3 sphere)
		{
			float f = plane.SignedDistanceTo(ref sphere.Center);
			return Mathf.Abs(f) <= sphere.Radius + 1E-05f;
		}

		public static bool FindPlane3Sphere3(ref Plane3 plane, ref Sphere3 sphere, out Plane3Sphere3Intr info)
		{
			float num = plane.SignedDistanceTo(ref sphere.Center);
			float num2 = Mathf.Abs(num);
			if (num2 <= sphere.Radius + 1E-05f)
			{
				if (num2 >= sphere.Radius - 1E-05f)
				{
					info.IntersectionType = IntersectionTypes.Point;
					info.Circle = default(Circle3);
				}
				else
				{
					Vector3 vector = sphere.Center - num * plane.Normal;
					float radius = Mathf.Sqrt(Mathf.Abs(sphere.Radius * sphere.Radius - num2 * num2));
					info.IntersectionType = IntersectionTypes.Other;
					info.Circle = new Circle3(ref vector, ref plane.Normal, radius);
				}
				return true;
			}
			info.IntersectionType = IntersectionTypes.Empty;
			info.Circle = default(Circle3);
			return false;
		}

		public static bool TestPlane3Triangle3(ref Plane3 plane, ref Triangle3 triangle)
		{
			float num = 0f;
			float num2 = plane.SignedDistanceTo(ref triangle.V0);
			if (Mathf.Abs(num2) <= Intersection._distanceThreshold)
			{
				num2 = num;
			}
			float num3 = plane.SignedDistanceTo(ref triangle.V1);
			if (Mathf.Abs(num3) <= Intersection._distanceThreshold)
			{
				num3 = num;
			}
			float num4 = plane.SignedDistanceTo(ref triangle.V2);
			if (Mathf.Abs(num4) <= Intersection._distanceThreshold)
			{
				num4 = num;
			}
			return (num2 <= num || num3 <= num || num4 <= num) && (num2 >= num || num3 >= num || num4 >= num);
		}

		public static bool FindPlane3Triangle3(ref Plane3 plane, ref Triangle3 triangle, out Plane3Triangle3Intr info)
		{
			float num = 0f;
			float num2 = plane.SignedDistanceTo(ref triangle.V0);
			if (Mathf.Abs(num2) <= Intersection._distanceThreshold)
			{
				num2 = num;
			}
			float num3 = plane.SignedDistanceTo(ref triangle.V1);
			if (Mathf.Abs(num3) <= Intersection._distanceThreshold)
			{
				num3 = num;
			}
			float num4 = plane.SignedDistanceTo(ref triangle.V2);
			if (Mathf.Abs(num4) <= Intersection._distanceThreshold)
			{
				num4 = num;
			}
			Vector3 v = triangle.V0;
			Vector3 v2 = triangle.V1;
			Vector3 v3 = triangle.V2;
			info.Point0 = (info.Point1 = (info.Point2 = Vector3.zero));
			if (num2 > num)
			{
				if (num3 > num)
				{
					if (num4 > num)
					{
						info.IntersectionType = IntersectionTypes.Empty;
						info.Quantity = 0;
					}
					else if (num4 < num)
					{
						info.Quantity = 2;
						info.Point0 = v + num2 / (num2 - num4) * (v3 - v);
						info.Point1 = v2 + num3 / (num3 - num4) * (v3 - v2);
						info.IntersectionType = IntersectionTypes.Segment;
					}
					else
					{
						info.Quantity = 1;
						info.Point0 = v3;
						info.IntersectionType = IntersectionTypes.Point;
					}
				}
				else if (num3 < num)
				{
					if (num4 > num)
					{
						info.Quantity = 2;
						info.Point0 = v + num2 / (num2 - num3) * (v2 - v);
						info.Point1 = v2 + num3 / (num3 - num4) * (v3 - v2);
						info.IntersectionType = IntersectionTypes.Segment;
					}
					else if (num4 < num)
					{
						info.Quantity = 2;
						info.Point0 = v + num2 / (num2 - num3) * (v2 - v);
						info.Point1 = v + num2 / (num2 - num4) * (v3 - v);
						info.IntersectionType = IntersectionTypes.Segment;
					}
					else
					{
						info.Quantity = 2;
						info.Point0 = v + num2 / (num2 - num3) * (v2 - v);
						info.Point1 = v3;
						info.IntersectionType = IntersectionTypes.Segment;
					}
				}
				else if (num4 > num)
				{
					info.Quantity = 1;
					info.Point0 = v2;
					info.IntersectionType = IntersectionTypes.Point;
				}
				else if (num4 < num)
				{
					info.Quantity = 2;
					info.Point0 = v + num2 / (num2 - num4) * (v3 - v);
					info.Point1 = v2;
					info.IntersectionType = IntersectionTypes.Segment;
				}
				else
				{
					info.Quantity = 2;
					info.Point0 = v2;
					info.Point1 = v3;
					info.IntersectionType = IntersectionTypes.Segment;
				}
			}
			else if (num2 < num)
			{
				if (num3 > num)
				{
					if (num4 > num)
					{
						info.Quantity = 2;
						info.Point0 = v + num2 / (num2 - num3) * (v2 - v);
						info.Point1 = v + num2 / (num2 - num4) * (v3 - v);
						info.IntersectionType = IntersectionTypes.Segment;
					}
					else if (num4 < num)
					{
						info.Quantity = 2;
						info.Point0 = v + num2 / (num2 - num3) * (v2 - v);
						info.Point1 = v2 + num3 / (num3 - num4) * (v3 - v2);
						info.IntersectionType = IntersectionTypes.Segment;
					}
					else
					{
						info.Quantity = 2;
						info.Point0 = v + num2 / (num2 - num3) * (v2 - v);
						info.Point1 = v3;
						info.IntersectionType = IntersectionTypes.Segment;
					}
				}
				else if (num3 < num)
				{
					if (num4 > num)
					{
						info.Quantity = 2;
						info.Point0 = v + num2 / (num2 - num4) * (v3 - v);
						info.Point1 = v2 + num3 / (num3 - num4) * (v3 - v2);
						info.IntersectionType = IntersectionTypes.Segment;
					}
					else if (num4 < num)
					{
						info.Quantity = 0;
						info.IntersectionType = IntersectionTypes.Empty;
					}
					else
					{
						info.Quantity = 1;
						info.Point0 = v3;
						info.IntersectionType = IntersectionTypes.Point;
					}
				}
				else if (num4 > num)
				{
					info.Quantity = 2;
					info.Point0 = v + num2 / (num2 - num4) * (v3 - v);
					info.Point1 = v2;
					info.IntersectionType = IntersectionTypes.Segment;
				}
				else if (num4 < num)
				{
					info.Quantity = 1;
					info.Point0 = v2;
					info.IntersectionType = IntersectionTypes.Point;
				}
				else
				{
					info.Quantity = 2;
					info.Point0 = v2;
					info.Point1 = v3;
					info.IntersectionType = IntersectionTypes.Segment;
				}
			}
			else if (num3 > num)
			{
				if (num4 > num)
				{
					info.Quantity = 1;
					info.Point0 = v;
					info.IntersectionType = IntersectionTypes.Point;
				}
				else if (num4 < num)
				{
					info.Quantity = 2;
					info.Point0 = v2 + num3 / (num3 - num4) * (v3 - v2);
					info.Point1 = v;
					info.IntersectionType = IntersectionTypes.Segment;
				}
				else
				{
					info.Quantity = 2;
					info.Point0 = v;
					info.Point1 = v3;
					info.IntersectionType = IntersectionTypes.Segment;
				}
			}
			else if (num3 < num)
			{
				if (num4 > num)
				{
					info.Quantity = 2;
					info.Point0 = v2 + num3 / (num3 - num4) * (v3 - v2);
					info.Point1 = v;
					info.IntersectionType = IntersectionTypes.Segment;
				}
				else if (num4 < num)
				{
					info.Quantity = 1;
					info.Point0 = v;
					info.IntersectionType = IntersectionTypes.Point;
				}
				else
				{
					info.Quantity = 2;
					info.Point0 = v;
					info.Point1 = v3;
					info.IntersectionType = IntersectionTypes.Segment;
				}
			}
			else if (num4 > num)
			{
				info.Quantity = 2;
				info.Point0 = v;
				info.Point1 = v2;
				info.IntersectionType = IntersectionTypes.Segment;
			}
			else if (num4 < num)
			{
				info.Quantity = 2;
				info.Point0 = v;
				info.Point1 = v2;
				info.IntersectionType = IntersectionTypes.Segment;
			}
			else
			{
				info.Quantity = 3;
				info.Point0 = v;
				info.Point1 = v2;
				info.Point2 = v3;
				info.IntersectionType = IntersectionTypes.Polygon;
			}
			return info.IntersectionType != IntersectionTypes.Empty;
		}

		public static bool TestRay3AAB3(ref Ray3 ray, ref AAB3 box)
		{
			Vector3 b;
			Vector3 vector;
			box.CalcCenterExtents(out b, out vector);
			Vector3 value = ray.Center - b;
			float x = ray.Direction.x;
			float num = Mathf.Abs(x);
			float x2 = value.x;
			float num2 = Mathf.Abs(x2);
			if (num2 > vector.x && x2 * x >= 0f)
			{
				return false;
			}
			float y = ray.Direction.y;
			float num3 = Mathf.Abs(y);
			float y2 = value.y;
			float num4 = Mathf.Abs(y2);
			if (num4 > vector.y && y2 * y >= 0f)
			{
				return false;
			}
			float z = ray.Direction.z;
			float num5 = Mathf.Abs(z);
			float z2 = value.z;
			float num6 = Mathf.Abs(z2);
			if (num6 > vector.z && z2 * z >= 0f)
			{
				return false;
			}
			Vector3 vector2 = ray.Direction.Cross(value);
			float num7 = Mathf.Abs(vector2.x);
			float num8 = vector.y * num5 + vector.z * num3;
			if (num7 > num8)
			{
				return false;
			}
			float num9 = Mathf.Abs(vector2.y);
			num8 = vector.x * num5 + vector.z * num;
			if (num9 > num8)
			{
				return false;
			}
			float num10 = Mathf.Abs(vector2.z);
			num8 = vector.x * num3 + vector.y * num;
			return num10 <= num8;
		}

		public static bool FindRay3AAB3(ref Ray3 ray, ref AAB3 box, out Ray3AAB3Intr info)
		{
			return Intersection.DoClipping(0f, float.PositiveInfinity, ref ray.Center, ref ray.Direction, ref box, true, out info.Quantity, out info.Point0, out info.Point1, out info.IntersectionType);
		}

		public static bool TestRay3Box3(ref Ray3 ray, ref Box3 box)
		{
			Vector3 vector = ray.Center - box.Center;
			float num = ray.Direction.Dot(box.Axis0);
			float num2 = Mathf.Abs(num);
			float num3 = vector.Dot(box.Axis0);
			float num4 = Mathf.Abs(num3);
			if (num4 > box.Extents.x && num3 * num >= 0f)
			{
				return false;
			}
			float num5 = ray.Direction.Dot(box.Axis1);
			float num6 = Mathf.Abs(num5);
			float num7 = vector.Dot(box.Axis1);
			float num8 = Mathf.Abs(num7);
			if (num8 > box.Extents.y && num7 * num5 >= 0f)
			{
				return false;
			}
			float num9 = ray.Direction.Dot(box.Axis2);
			float num10 = Mathf.Abs(num9);
			float num11 = vector.Dot(box.Axis2);
			float num12 = Mathf.Abs(num11);
			if (num12 > box.Extents.z && num11 * num9 >= 0f)
			{
				return false;
			}
			Vector3 vector2 = ray.Direction.Cross(vector);
			float num13 = Mathf.Abs(vector2.Dot(box.Axis0));
			float num14 = box.Extents.y * num10 + box.Extents.z * num6;
			if (num13 > num14)
			{
				return false;
			}
			float num15 = Mathf.Abs(vector2.Dot(box.Axis1));
			num14 = box.Extents.x * num10 + box.Extents.z * num2;
			if (num15 > num14)
			{
				return false;
			}
			float num16 = Mathf.Abs(vector2.Dot(box.Axis2));
			num14 = box.Extents.x * num6 + box.Extents.y * num2;
			return num16 <= num14;
		}

		public static bool FindRay3Box3(ref Ray3 ray, ref Box3 box, out Ray3Box3Intr info)
		{
			return Intersection.DoClipping(0f, float.PositiveInfinity, ref ray.Center, ref ray.Direction, ref box, true, out info.Quantity, out info.Point0, out info.Point1, out info.IntersectionType);
		}

		public static bool TestRay3Circle3(ref Ray3 ray, ref Circle3 circle)
		{
			Ray3Circle3Intr ray3Circle3Intr;
			return Intersection.FindRay3Circle3(ref ray, ref circle, out ray3Circle3Intr);
		}

		public static bool FindRay3Circle3(ref Ray3 ray, ref Circle3 circle, out Ray3Circle3Intr info)
		{
			float num = ray.Direction.Dot(circle.Normal);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float num2 = circle.Normal.Dot(ray.Center - circle.Center);
				float num3 = -num2 / num;
				if (num3 >= -Intersection._intervalThreshold)
				{
					Vector3 vector = ray.Center + num3 * ray.Direction;
					if ((vector - circle.Center).sqrMagnitude <= circle.Radius * circle.Radius)
					{
						info.IntersectionType = IntersectionTypes.Point;
						info.Point = vector;
						return true;
					}
				}
			}
			info.IntersectionType = IntersectionTypes.Empty;
			info.Point = Vector3.zero;
			return false;
		}

		public static bool TestRay3Plane3(ref Ray3 ray, ref Plane3 plane, out IntersectionTypes intersectionType)
		{
			Ray3Plane3Intr ray3Plane3Intr;
			bool result = Intersection.FindRay3Plane3(ref ray, ref plane, out ray3Plane3Intr);
			intersectionType = ray3Plane3Intr.IntersectionType;
			return result;
		}

		public static bool TestRay3Plane3(ref Ray3 ray, ref Plane3 plane)
		{
			Ray3Plane3Intr ray3Plane3Intr;
			return Intersection.FindRay3Plane3(ref ray, ref plane, out ray3Plane3Intr);
		}

		public static bool FindRay3Plane3(ref Ray3 ray, ref Plane3 plane, out Ray3Plane3Intr info)
		{
			float num = ray.Direction.Dot(plane.Normal);
			float num2 = plane.SignedDistanceTo(ref ray.Center);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float num3 = -num2 / num;
				if (num3 >= -Intersection._intervalThreshold)
				{
					info.RayParameter = num3;
					info.IntersectionType = IntersectionTypes.Point;
					info.Point = ray.Center + num3 * ray.Direction;
					return true;
				}
				info.IntersectionType = IntersectionTypes.Empty;
				info.RayParameter = 0f;
				info.Point = Vector3.zero;
				return false;
			}
			else
			{
				if (Mathf.Abs(num2) <= Intersection._distanceThreshold)
				{
					info.RayParameter = 0f;
					info.IntersectionType = IntersectionTypes.Ray;
					info.Point = Vector3.zero;
					return true;
				}
				info.IntersectionType = IntersectionTypes.Empty;
				info.RayParameter = 0f;
				info.Point = Vector3.zero;
				return false;
			}
		}

		public static bool TestRay3Polygon3(ref Ray3 ray, Polygon3 polygon)
		{
			Ray3Polygon3Intr ray3Polygon3Intr;
			return Intersection.FindRay3Polygon3(ref ray, polygon, out ray3Polygon3Intr);
		}

		public static bool FindRay3Polygon3(ref Ray3 ray, Polygon3 polygon, out Ray3Polygon3Intr info)
		{
			Plane3 plane = polygon.Plane;
			float num = ray.Direction.Dot(plane.Normal);
			float num2 = plane.SignedDistanceTo(ref ray.Center);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float num3 = -num2 / num;
				if (num3 < -Intersection._intervalThreshold)
				{
					info.IntersectionType = IntersectionTypes.Empty;
					info.Point = Vector3.zero;
					return false;
				}
				Vector3 vector = ray.Center + num3 * ray.Direction;
				ProjectionPlanes projectionPlane = plane.Normal.GetProjectionPlane();
				Polygon2 polygon2 = Polygon2.CreateProjected(polygon, projectionPlane);
				Vector2 point = vector.ToVector2(projectionPlane);
				bool flag = polygon2.ContainsSimple(point);
				if (flag)
				{
					info.IntersectionType = IntersectionTypes.Point;
					info.Point = vector;
					return true;
				}
			}
			info.IntersectionType = IntersectionTypes.Empty;
			info.Point = Vector3.zero;
			return false;
		}

		public static bool TestRay3Rectangle3(ref Ray3 ray, ref Rectangle3 rectangle)
		{
			Ray3Rectangle3Intr ray3Rectangle3Intr;
			return Intersection.FindRay3Rectangle3(ref ray, ref rectangle, out ray3Rectangle3Intr);
		}

		public static bool FindRay3Rectangle3(ref Ray3 ray, ref Rectangle3 rectangle, out Ray3Rectangle3Intr info)
		{
			float num = ray.Direction.Dot(rectangle.Normal);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float num2 = rectangle.Normal.Dot(ray.Center - rectangle.Center);
				float num3 = -num2 / num;
				if (num3 >= -Intersection._intervalThreshold)
				{
					Vector3 point = ray.Center + num3 * ray.Direction;
					bool flag = Intersection.Point3InsideRectangle3(ref point, ref rectangle);
					if (flag)
					{
						info.IntersectionType = IntersectionTypes.Point;
						info.Point = point;
						return true;
					}
				}
			}
			info.IntersectionType = IntersectionTypes.Empty;
			info.Point = Vector3.zero;
			return false;
		}

		public static bool TestRay3Sphere3(ref Ray3 ray, ref Sphere3 sphere)
		{
			Vector3 vector = ray.Center - sphere.Center;
			float num = vector.Dot(vector) - sphere.Radius * sphere.Radius;
			if (num <= 0f)
			{
				return true;
			}
			float num2 = ray.Direction.Dot(vector);
			return num2 < 0f && num2 * num2 >= num;
		}

		public static bool FindRay3Sphere3(ref Ray3 ray, ref Sphere3 sphere, out Ray3Sphere3Intr info)
		{
			Vector3 vector = ray.Center - sphere.Center;
			float num = vector.Dot(vector) - sphere.Radius * sphere.Radius;
			float num2;
			float num3;
			if (num <= 0f)
			{
				num2 = ray.Direction.Dot(vector);
				num3 = num2 * num2 - num;
				float num4 = Mathf.Sqrt(num3);
				info.RayParameter0 = -num2 + num4;
				info.RayParameter1 = 0f;
				info.Point0 = ray.Center + info.RayParameter0 * ray.Direction;
				info.Point1 = Vector3.zero;
				info.Quantity = 1;
				info.IntersectionType = IntersectionTypes.Point;
				return true;
			}
			num2 = ray.Direction.Dot(vector);
			if (num2 >= 0f)
			{
				info = default(Ray3Sphere3Intr);
				return false;
			}
			num3 = num2 * num2 - num;
			if (num3 < 0f)
			{
				info = default(Ray3Sphere3Intr);
			}
			else if (num3 >= 1E-05f)
			{
				float num4 = Mathf.Sqrt(num3);
				info.RayParameter0 = -num2 - num4;
				info.RayParameter1 = -num2 + num4;
				info.Point0 = ray.Center + info.RayParameter0 * ray.Direction;
				info.Point1 = ray.Center + info.RayParameter1 * ray.Direction;
				info.Quantity = 2;
				info.IntersectionType = IntersectionTypes.Segment;
			}
			else
			{
				info.RayParameter0 = -num2;
				info.RayParameter1 = 0f;
				info.Point0 = ray.Center + info.RayParameter0 * ray.Direction;
				info.Point1 = Vector3.zero;
				info.Quantity = 1;
				info.IntersectionType = IntersectionTypes.Point;
			}
			return info.Quantity > 0;
		}

		public static bool TestRay3Triangle3(ref Ray3 ray, ref Triangle3 triangle, out IntersectionTypes intersectionType)
		{
			Vector3 vector = ray.Center - triangle.V0;
			Vector3 vector2 = triangle.V1 - triangle.V0;
			Vector3 value = triangle.V2 - triangle.V0;
			Vector3 value2 = vector2.Cross(value);
			float num = ray.Direction.Dot(value2);
			float num2;
			if (num > Intersection._dotThreshold)
			{
				num2 = 1f;
			}
			else
			{
				if (num >= -Intersection._dotThreshold)
				{
					intersectionType = IntersectionTypes.Empty;
					return false;
				}
				num2 = -1f;
				num = -num;
			}
			float num3 = num2 * ray.Direction.Dot(vector.Cross(value));
			if (num3 >= -1E-05f)
			{
				float num4 = num2 * ray.Direction.Dot(vector2.Cross(vector));
				if (num4 >= -1E-05f && num3 + num4 <= num + 1E-05f)
				{
					float num5 = -num2 * vector.Dot(value2);
					if (num5 >= -Intersection._intervalThreshold)
					{
						intersectionType = IntersectionTypes.Point;
						return true;
					}
				}
			}
			intersectionType = IntersectionTypes.Empty;
			return false;
		}

		public static bool TestRay3Triangle3(ref Ray3 ray, ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, out IntersectionTypes intersectionType)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			return Intersection.TestRay3Triangle3(ref ray, ref triangle, out intersectionType);
		}

		public static bool TestRay3Triangle3(ref Ray3 ray, Vector3 v0, Vector3 v1, Vector3 v2, out IntersectionTypes intersectionType)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			return Intersection.TestRay3Triangle3(ref ray, ref triangle, out intersectionType);
		}

		public static bool TestRay3Triangle3(ref Ray3 ray, ref Triangle3 triangle)
		{
			IntersectionTypes intersectionTypes;
			return Intersection.TestRay3Triangle3(ref ray, ref triangle, out intersectionTypes);
		}

		public static bool TestRay3Triangle3(ref Ray3 ray, ref Vector3 v0, ref Vector3 v1, ref Vector3 v2)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			IntersectionTypes intersectionTypes;
			return Intersection.TestRay3Triangle3(ref ray, ref triangle, out intersectionTypes);
		}

		public static bool TestRay3Triangle3(ref Ray3 ray, Vector3 v0, Vector3 v1, Vector3 v2)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			IntersectionTypes intersectionTypes;
			return Intersection.TestRay3Triangle3(ref ray, ref triangle, out intersectionTypes);
		}

		public static bool FindRay3Triangle3(ref Ray3 ray, ref Triangle3 triangle, out Ray3Triangle3Intr info)
		{
			Vector3 vector = ray.Center - triangle.V0;
			Vector3 vector2 = triangle.V1 - triangle.V0;
			Vector3 value = triangle.V2 - triangle.V0;
			Vector3 value2 = vector2.Cross(value);
			float num = ray.Direction.Dot(value2);
			float num2;
			if (num > Intersection._dotThreshold)
			{
				num2 = 1f;
			}
			else
			{
				if (num >= -Intersection._dotThreshold)
				{
					info = default(Ray3Triangle3Intr);
					return false;
				}
				num2 = -1f;
				num = -num;
			}
			float num3 = num2 * ray.Direction.Dot(vector.Cross(value));
			if (num3 >= -1E-05f)
			{
				float num4 = num2 * ray.Direction.Dot(vector2.Cross(vector));
				if (num4 >= -1E-05f && num3 + num4 <= num + 1E-05f)
				{
					float num5 = -num2 * vector.Dot(value2);
					if (num5 >= -Intersection._intervalThreshold)
					{
						float num6 = 1f / num;
						info.IntersectionType = IntersectionTypes.Point;
						info.RayParameter = num5 * num6;
						info.Point = ray.Eval(info.RayParameter);
						info.TriBary1 = num3 * num6;
						info.TriBary2 = num4 * num6;
						info.TriBary0 = 1f - info.TriBary1 - info.TriBary2;
						return true;
					}
				}
			}
			info = default(Ray3Triangle3Intr);
			return false;
		}

		public static bool FindRay3Triangle3(ref Ray3 ray, ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, out Ray3Triangle3Intr info)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			return Intersection.FindRay3Triangle3(ref ray, ref triangle, out info);
		}

		public static bool FindRay3Triangle3(ref Ray3 ray, Vector3 v0, Vector3 v1, Vector3 v2, out Ray3Triangle3Intr info)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			return Intersection.FindRay3Triangle3(ref ray, ref triangle, out info);
		}

		public static bool TestSegment3AAB3(ref Segment3 segment, ref AAB3 box)
		{
			Vector3 b;
			Vector3 vector;
			box.CalcCenterExtents(out b, out vector);
			Vector3 value = segment.Center - b;
			float num = Mathf.Abs(segment.Direction.x);
			float num2 = Mathf.Abs(value.x);
			float num3 = vector.x + segment.Extent * num;
			if (num2 > num3)
			{
				return false;
			}
			float num4 = Mathf.Abs(segment.Direction.y);
			float num5 = Mathf.Abs(value.y);
			num3 = vector.y + segment.Extent * num4;
			if (num5 > num3)
			{
				return false;
			}
			float num6 = Mathf.Abs(segment.Direction.z);
			float num7 = Mathf.Abs(value.z);
			num3 = vector.z + segment.Extent * num6;
			if (num7 > num3)
			{
				return false;
			}
			Vector3 vector2 = segment.Direction.Cross(value);
			float num8 = Mathf.Abs(vector2.x);
			num3 = vector.y * num6 + vector.z * num4;
			if (num8 > num3)
			{
				return false;
			}
			float num9 = Mathf.Abs(vector2.y);
			num3 = vector.x * num6 + vector.z * num;
			if (num9 > num3)
			{
				return false;
			}
			float num10 = Mathf.Abs(vector2.z);
			num3 = vector.x * num4 + vector.y * num;
			return num10 <= num3;
		}

		public static bool FindSegment3AAB3(ref Segment3 segment, ref AAB3 box, out Segment3AAB3Intr info)
		{
			return Intersection.DoClipping(-segment.Extent, segment.Extent, ref segment.Center, ref segment.Direction, ref box, true, out info.Quantity, out info.Point0, out info.Point1, out info.IntersectionType);
		}

		public static bool TestSegment3Box3(ref Segment3 segment, ref Box3 box)
		{
			Vector3 vector = segment.Center - box.Center;
			float num = Mathf.Abs(segment.Direction.Dot(box.Axis0));
			float num2 = Mathf.Abs(vector.Dot(box.Axis0));
			float num3 = box.Extents.x + segment.Extent * num;
			if (num2 > num3)
			{
				return false;
			}
			float num4 = Mathf.Abs(segment.Direction.Dot(box.Axis1));
			float num5 = Mathf.Abs(vector.Dot(box.Axis1));
			num3 = box.Extents.y + segment.Extent * num4;
			if (num5 > num3)
			{
				return false;
			}
			float num6 = Mathf.Abs(segment.Direction.Dot(box.Axis2));
			float num7 = Mathf.Abs(vector.Dot(box.Axis2));
			num3 = box.Extents.z + segment.Extent * num6;
			if (num7 > num3)
			{
				return false;
			}
			Vector3 vector2 = segment.Direction.Cross(vector);
			float num8 = Mathf.Abs(vector2.Dot(box.Axis0));
			num3 = box.Extents.y * num6 + box.Extents.z * num4;
			if (num8 > num3)
			{
				return false;
			}
			float num9 = Mathf.Abs(vector2.Dot(box.Axis1));
			num3 = box.Extents.x * num6 + box.Extents.z * num;
			if (num9 > num3)
			{
				return false;
			}
			float num10 = Mathf.Abs(vector2.Dot(box.Axis2));
			num3 = box.Extents.x * num4 + box.Extents.y * num;
			return num10 <= num3;
		}

		public static bool FindSegment3Box3(ref Segment3 segment, ref Box3 box, out Segment3Box3Intr info)
		{
			return Intersection.DoClipping(-segment.Extent, segment.Extent, ref segment.Center, ref segment.Direction, ref box, true, out info.Quantity, out info.Point0, out info.Point1, out info.IntersectionType);
		}

		public static bool TestSegment3Circle3(ref Segment3 segment, ref Circle3 circle)
		{
			Segment3Circle3Intr segment3Circle3Intr;
			return Intersection.FindSegment3Circle3(ref segment, ref circle, out segment3Circle3Intr);
		}

		public static bool FindSegment3Circle3(ref Segment3 segment, ref Circle3 circle, out Segment3Circle3Intr info)
		{
			float num = segment.Direction.Dot(circle.Normal);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float num2 = circle.Normal.Dot(segment.Center - circle.Center);
				float num3 = -num2 / num;
				if (Mathf.Abs(num3) <= segment.Extent + Intersection._intervalThreshold)
				{
					Vector3 vector = segment.Center + num3 * segment.Direction;
					if ((vector - circle.Center).sqrMagnitude <= circle.Radius * circle.Radius)
					{
						info.IntersectionType = IntersectionTypes.Point;
						info.Point = vector;
						return true;
					}
				}
			}
			info.IntersectionType = IntersectionTypes.Empty;
			info.Point = Vector3.zero;
			return false;
		}

		public static bool TestSegment3Plane3(ref Segment3 segment, ref Plane3 plane, out IntersectionTypes intersectionType)
		{
			Vector3 p = segment.P0;
			float num = plane.SignedDistanceTo(ref p);
			if (Mathf.Abs(num) <= Intersection._distanceThreshold)
			{
				num = 0f;
			}
			Vector3 p2 = segment.P1;
			float num2 = plane.SignedDistanceTo(ref p2);
			if (Mathf.Abs(num2) <= Intersection._distanceThreshold)
			{
				num2 = 0f;
			}
			float num3 = num * num2;
			if (num3 < 0f)
			{
				intersectionType = IntersectionTypes.Point;
				return true;
			}
			if (num3 > 0f)
			{
				intersectionType = IntersectionTypes.Empty;
				return false;
			}
			if (num != 0f || num2 != 0f)
			{
				intersectionType = IntersectionTypes.Point;
				return true;
			}
			intersectionType = IntersectionTypes.Segment;
			return true;
		}

		public static bool TestSegment3Plane3(ref Segment3 segment, ref Plane3 plane)
		{
			IntersectionTypes intersectionTypes;
			return Intersection.TestSegment3Plane3(ref segment, ref plane, out intersectionTypes);
		}

		public static bool FindSegment3Plane3(ref Segment3 segment, ref Plane3 plane, out Segment3Plane3Intr info)
		{
			float num = segment.Direction.Dot(plane.Normal);
			float num2 = plane.SignedDistanceTo(ref segment.Center);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float num3 = -num2 / num;
				if (Mathf.Abs(num3) <= segment.Extent + Intersection._intervalThreshold)
				{
					info.IntersectionType = IntersectionTypes.Point;
					info.Point = segment.Center + num3 * segment.Direction;
					info.SegmentParameter = (num3 + segment.Extent) / (segment.Extent * 2f);
					return true;
				}
				info.IntersectionType = IntersectionTypes.Empty;
				info.Point = Vector3.zero;
				info.SegmentParameter = 0f;
				return false;
			}
			else
			{
				if (Mathf.Abs(num2) <= Intersection._distanceThreshold)
				{
					info.IntersectionType = IntersectionTypes.Segment;
					info.Point = Vector3.zero;
					info.SegmentParameter = 0f;
					return true;
				}
				info.IntersectionType = IntersectionTypes.Empty;
				info.Point = Vector3.zero;
				info.SegmentParameter = 0f;
				return false;
			}
		}

		public static bool TestSegment3Polygon3(ref Segment3 segment, Polygon3 polygon)
		{
			Segment3Polygon3Intr segment3Polygon3Intr;
			return Intersection.FindSegment3Polygon3(ref segment, polygon, out segment3Polygon3Intr);
		}

		public static bool FindSegment3Polygon3(ref Segment3 segment, Polygon3 polygon, out Segment3Polygon3Intr info)
		{
			Plane3 plane = polygon.Plane;
			float num = segment.Direction.Dot(plane.Normal);
			float num2 = plane.SignedDistanceTo(ref segment.Center);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float num3 = -num2 / num;
				if (Mathf.Abs(num3) > segment.Extent + Intersection._intervalThreshold)
				{
					info.IntersectionType = IntersectionTypes.Empty;
					info.Point = Vector3.zero;
					return false;
				}
				Vector3 vector = segment.Center + num3 * segment.Direction;
				ProjectionPlanes projectionPlane = plane.Normal.GetProjectionPlane();
				Polygon2 polygon2 = Polygon2.CreateProjected(polygon, projectionPlane);
				Vector2 point = vector.ToVector2(projectionPlane);
				bool flag = polygon2.ContainsSimple(point);
				if (flag)
				{
					info.IntersectionType = IntersectionTypes.Point;
					info.Point = vector;
					return true;
				}
			}
			info.IntersectionType = IntersectionTypes.Empty;
			info.Point = Vector3.zero;
			return false;
		}

		public static bool TestSegment3Rectangle3(ref Segment3 segment, ref Rectangle3 rectangle)
		{
			Segment3Rectangle3Intr segment3Rectangle3Intr;
			return Intersection.FindSegment3Rectangle3(ref segment, ref rectangle, out segment3Rectangle3Intr);
		}

		public static bool FindSegment3Rectangle3(ref Segment3 segment, ref Rectangle3 rectangle, out Segment3Rectangle3Intr info)
		{
			float num = segment.Direction.Dot(rectangle.Normal);
			if (Mathf.Abs(num) > Intersection._dotThreshold)
			{
				float num2 = rectangle.Normal.Dot(segment.Center - rectangle.Center);
				float num3 = -num2 / num;
				if (Mathf.Abs(num3) <= segment.Extent + Intersection._intervalThreshold)
				{
					Vector3 point = segment.Center + num3 * segment.Direction;
					bool flag = Intersection.Point3InsideRectangle3(ref point, ref rectangle);
					if (flag)
					{
						info.IntersectionType = IntersectionTypes.Point;
						info.Point = point;
						return true;
					}
				}
			}
			info.IntersectionType = IntersectionTypes.Empty;
			info.Point = Vector3.zero;
			return false;
		}

		public static bool TestSegment3Sphere3(ref Segment3 segment, ref Sphere3 sphere)
		{
			Vector3 vector = segment.Center - sphere.Center;
			float num = vector.Dot(vector) - sphere.Radius * sphere.Radius;
			float num2 = segment.Direction.Dot(vector);
			float num3 = num2 * num2 - num;
			if (num3 < 0f)
			{
				return false;
			}
			float num4 = segment.Extent * segment.Extent + num;
			float num5 = 2f * num2 * segment.Extent;
			float num6 = num4 - num5;
			float num7 = num4 + num5;
			return num6 * num7 <= 0f || (num6 > 0f && Mathf.Abs(num2) < segment.Extent);
		}

		public static bool FindSegment3Sphere3(ref Segment3 segment, ref Sphere3 sphere, out Segment3Sphere3Intr info)
		{
			Vector3 vector = segment.Center - sphere.Center;
			float num = vector.Dot(vector) - sphere.Radius * sphere.Radius;
			float num2 = segment.Direction.Dot(vector);
			float num3 = num2 * num2 - num;
			if (num3 < 0f)
			{
				info = default(Segment3Sphere3Intr);
				return false;
			}
			float num4 = segment.Extent * segment.Extent + num;
			float num5 = 2f * num2 * segment.Extent;
			float num6 = num4 - num5;
			float num7 = num4 + num5;
			if (num6 * num7 <= 0f)
			{
				float num8 = Mathf.Sqrt(num3);
				info.SegmentParameter0 = ((num6 > 0f) ? (-num2 - num8) : (-num2 + num8));
				info.SegmentParameter1 = 0f;
				info.Point0 = segment.Center + info.SegmentParameter0 * segment.Direction;
				info.Point1 = Vector3.zero;
				info.SegmentParameter0 = (info.SegmentParameter0 + segment.Extent) / (2f * segment.Extent);
				info.Quantity = 1;
				info.IntersectionType = IntersectionTypes.Point;
				return true;
			}
			if (num6 > 0f && Mathf.Abs(num2) < segment.Extent)
			{
				if (num3 >= 1E-05f)
				{
					float num8 = Mathf.Sqrt(num3);
					info.SegmentParameter0 = -num2 - num8;
					info.SegmentParameter1 = -num2 + num8;
					info.Point0 = segment.Center + info.SegmentParameter0 * segment.Direction;
					info.Point1 = segment.Center + info.SegmentParameter1 * segment.Direction;
					info.SegmentParameter0 = (info.SegmentParameter0 + segment.Extent) / (2f * segment.Extent);
					info.SegmentParameter1 = (info.SegmentParameter1 + segment.Extent) / (2f * segment.Extent);
					info.Quantity = 2;
					info.IntersectionType = IntersectionTypes.Segment;
				}
				else
				{
					info.SegmentParameter0 = -num2;
					info.SegmentParameter1 = 0f;
					info.Point0 = segment.Center + info.SegmentParameter0 * segment.Direction;
					info.Point1 = Vector3.zero;
					info.SegmentParameter0 = (info.SegmentParameter0 + segment.Extent) / (2f * segment.Extent);
					info.Quantity = 1;
					info.IntersectionType = IntersectionTypes.Point;
				}
			}
			else
			{
				info = default(Segment3Sphere3Intr);
			}
			return info.Quantity > 0;
		}

		public static bool TestSegment3Triangle3(ref Segment3 segment, ref Triangle3 triangle, out IntersectionTypes intersectionType)
		{
			Vector3 vector = segment.Center - triangle.V0;
			Vector3 vector2 = triangle.V1 - triangle.V0;
			Vector3 value = triangle.V2 - triangle.V0;
			Vector3 value2 = vector2.Cross(value);
			float num = segment.Direction.Dot(value2);
			float num2;
			if (num > Intersection._dotThreshold)
			{
				num2 = 1f;
			}
			else
			{
				if (num >= -Intersection._dotThreshold)
				{
					intersectionType = IntersectionTypes.Empty;
					return false;
				}
				num2 = -1f;
				num = -num;
			}
			float num3 = num2 * segment.Direction.Dot(vector.Cross(value));
			if (num3 >= -1E-05f)
			{
				float num4 = num2 * segment.Direction.Dot(vector2.Cross(vector));
				if (num4 >= -1E-05f && num3 + num4 <= num + 1E-05f)
				{
					float num5 = -num2 * vector.Dot(value2);
					float num6 = segment.Extent * num;
					if (-num6 - 1E-05f <= num5 && num5 <= num6 + 1E-05f)
					{
						intersectionType = IntersectionTypes.Point;
						return true;
					}
				}
			}
			intersectionType = IntersectionTypes.Empty;
			return false;
		}

		public static bool TestSegment3Triangle3(ref Segment3 segment, ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, out IntersectionTypes intersectionType)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			return Intersection.TestSegment3Triangle3(ref segment, ref triangle, out intersectionType);
		}

		public static bool TestSegment3Triangle3(ref Segment3 segment, Vector3 v0, Vector3 v1, Vector3 v2, out IntersectionTypes intersectionType)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			return Intersection.TestSegment3Triangle3(ref segment, ref triangle, out intersectionType);
		}

		public static bool TestSegment3Triangle3(ref Segment3 segment, ref Triangle3 triangle)
		{
			IntersectionTypes intersectionTypes;
			return Intersection.TestSegment3Triangle3(ref segment, ref triangle, out intersectionTypes);
		}

		public static bool TestSegment3Triangle3(ref Segment3 segment, ref Vector3 v0, ref Vector3 v1, ref Vector3 v2)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			IntersectionTypes intersectionTypes;
			return Intersection.TestSegment3Triangle3(ref segment, ref triangle, out intersectionTypes);
		}

		public static bool TestSegment3Triangle3(ref Segment3 segment, Vector3 v0, Vector3 v1, Vector3 v2)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			IntersectionTypes intersectionTypes;
			return Intersection.TestSegment3Triangle3(ref segment, ref triangle, out intersectionTypes);
		}

		public static bool FindSegment3Triangle3(ref Segment3 segment, ref Triangle3 triangle, out Segment3Triangle3Intr info)
		{
			Vector3 vector = segment.Center - triangle.V0;
			Vector3 vector2 = triangle.V1 - triangle.V0;
			Vector3 value = triangle.V2 - triangle.V0;
			Vector3 value2 = vector2.Cross(value);
			float num = segment.Direction.Dot(value2);
			float num2;
			if (num > Intersection._dotThreshold)
			{
				num2 = 1f;
			}
			else
			{
				if (num >= -Intersection._dotThreshold)
				{
					info = default(Segment3Triangle3Intr);
					return false;
				}
				num2 = -1f;
				num = -num;
			}
			float num3 = num2 * segment.Direction.Dot(vector.Cross(value));
			if (num3 >= -1E-05f)
			{
				float num4 = num2 * segment.Direction.Dot(vector2.Cross(vector));
				if (num4 >= -1E-05f && num3 + num4 <= num + 1E-05f)
				{
					float num5 = -num2 * vector.Dot(value2);
					float num6 = segment.Extent * num;
					if (-num6 - 1E-05f <= num5 && num5 <= num6 + 1E-05f)
					{
						float num7 = 1f / num;
						info.IntersectionType = IntersectionTypes.Point;
						info.SegmentParameter = num5 * num7;
						info.Point = segment.Center + info.SegmentParameter * segment.Direction;
						info.SegmentParameter = (info.SegmentParameter + segment.Extent) / (2f * segment.Extent);
						info.TriBary1 = num3 * num7;
						info.TriBary2 = num4 * num7;
						info.TriBary0 = 1f - info.TriBary1 - info.TriBary2;
						return true;
					}
				}
			}
			info = default(Segment3Triangle3Intr);
			return false;
		}

		public static bool FindSegment3Triangle3(ref Segment3 segment, ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, out Segment3Triangle3Intr info)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			return Intersection.FindSegment3Triangle3(ref segment, ref triangle, out info);
		}

		public static bool FindSegment3Triangle3(ref Segment3 segment, Vector3 v0, Vector3 v1, Vector3 v2, out Segment3Triangle3Intr info)
		{
			Triangle3 triangle = new Triangle3
			{
				V0 = v0,
				V1 = v1,
				V2 = v2
			};
			return Intersection.FindSegment3Triangle3(ref segment, ref triangle, out info);
		}

		public static bool TestSphere3Sphere3(ref Sphere3 sphere0, ref Sphere3 sphere1)
		{
			Vector3 vector = sphere1.Center - sphere0.Center;
			float num = sphere0.Radius + sphere1.Radius;
			return vector.sqrMagnitude <= num * num;
		}

		public static bool FindSphere3Sphere3(ref Sphere3 sphere0, ref Sphere3 sphere1, out Sphere3Sphere3Intr info)
		{
			Vector3 a = sphere1.Center - sphere0.Center;
			float sqrMagnitude = a.sqrMagnitude;
			float radius = sphere0.Radius;
			float radius2 = sphere1.Radius;
			float num = radius - radius2;
			if (a.sqrMagnitude < 9.99999944E-11f && Mathf.Abs(num) < 1E-05f)
			{
				info.IntersectionType = Sphere3Sphere3IntrTypes.Same;
				info.ContactPoint = Vector3ex.Zero;
				info.Circle = default(Circle3);
				return true;
			}
			float num2 = radius + radius2;
			float num3 = num2 * num2;
			if (sqrMagnitude > num3)
			{
				info = default(Sphere3Sphere3Intr);
				return false;
			}
			if (sqrMagnitude == num3)
			{
				a.Normalize();
				info.ContactPoint = sphere0.Center + radius * a;
				info.Circle = default(Circle3);
				info.IntersectionType = Sphere3Sphere3IntrTypes.Point;
				return true;
			}
			float num4 = num * num;
			if (sqrMagnitude < num4)
			{
				a.Normalize();
				info.ContactPoint = 0.5f * (sphere0.Center + sphere1.Center);
				info.Circle = default(Circle3);
				info.IntersectionType = ((num <= 0f) ? Sphere3Sphere3IntrTypes.Sphere0 : Sphere3Sphere3IntrTypes.Sphere1);
				return true;
			}
			if (sqrMagnitude == num4)
			{
				a.Normalize();
				if (num <= 0f)
				{
					info.IntersectionType = Sphere3Sphere3IntrTypes.Sphere0Point;
					info.ContactPoint = sphere1.Center + radius2 * a;
				}
				else
				{
					info.IntersectionType = Sphere3Sphere3IntrTypes.Sphere1Point;
					info.ContactPoint = sphere0.Center + radius * a;
				}
				info.Circle = default(Circle3);
				return true;
			}
			float num5 = 0.5f * (1f + num * num2 / sqrMagnitude);
			Vector3 vector = sphere0.Center + num5 * a;
			float radius3 = Mathf.Sqrt(Mathf.Abs(radius * radius - num5 * num5 * sqrMagnitude));
			a.Normalize();
			info.Circle = new Circle3(ref vector, ref a, radius3);
			info.IntersectionType = Sphere3Sphere3IntrTypes.Circle;
			info.ContactPoint = Vector3ex.Zero;
			return true;
		}

		private static void ProjectOntoAxis(ref Triangle3 triangle, ref Vector3 axis, out float fmin, out float fmax)
		{
			float num = axis.Dot(triangle.V0);
			float num2 = axis.Dot(triangle.V1);
			float num3 = axis.Dot(triangle.V2);
			fmin = num;
			fmax = fmin;
			if (num2 < fmin)
			{
				fmin = num2;
			}
			else if (num2 > fmax)
			{
				fmax = num2;
			}
			if (num3 < fmin)
			{
				fmin = num3;
				return;
			}
			if (num3 > fmax)
			{
				fmax = num3;
			}
		}

		private static void TrianglePlaneRelations(ref Triangle3 triangle, ref Plane3 plane, out float dist0, out float dist1, out float dist2, out int sign0, out int sign1, out int sign2, out int positive, out int negative, out int zero)
		{
			positive = 0;
			negative = 0;
			zero = 0;
			dist0 = plane.SignedDistanceTo(ref triangle.V0);
			if (dist0 > 1E-05f)
			{
				sign0 = 1;
				positive++;
			}
			else if (dist0 < -1E-05f)
			{
				sign0 = -1;
				negative++;
			}
			else
			{
				dist0 = 0f;
				sign0 = 0;
				zero++;
			}
			dist1 = plane.SignedDistanceTo(ref triangle.V1);
			if (dist1 > 1E-05f)
			{
				sign1 = 1;
				positive++;
			}
			else if (dist1 < -1E-05f)
			{
				sign1 = -1;
				negative++;
			}
			else
			{
				dist1 = 0f;
				sign1 = 0;
				zero++;
			}
			dist2 = plane.SignedDistanceTo(ref triangle.V2);
			if (dist2 > 1E-05f)
			{
				sign2 = 1;
				positive++;
				return;
			}
			if (dist2 < -1E-05f)
			{
				sign2 = -1;
				negative++;
				return;
			}
			dist2 = 0f;
			sign2 = 0;
			zero++;
		}

		private static bool TrianglePlaneRelationsQuick(ref Triangle3 triangle, ref Plane3 plane)
		{
			float num = plane.SignedDistanceTo(ref triangle.V0);
			int num2;
			if (num > 1E-05f)
			{
				num2 = 1;
			}
			else
			{
				if (num >= -1E-05f)
				{
					return false;
				}
				num2 = -1;
			}
			num = plane.SignedDistanceTo(ref triangle.V1);
			if (num > 1E-05f)
			{
				if (num2 == -1)
				{
					return false;
				}
			}
			else
			{
				if (num >= -1E-05f)
				{
					return false;
				}
				if (num2 == 1)
				{
					return false;
				}
			}
			num = plane.SignedDistanceTo(ref triangle.V2);
			if (num > 1E-05f)
			{
				if (num2 == -1)
				{
					return false;
				}
			}
			else
			{
				if (num >= -1E-05f)
				{
					return false;
				}
				if (num2 == 1)
				{
					return false;
				}
			}
			return true;
		}

		private static bool IntersectsSegment(ref Plane3 plane, ref Triangle3 triangle, ref Vector3 end0, ref Vector3 end1, bool grazing, out Triangle3Triangle3Intr info)
		{
			int num = 0;
			float num2 = Mathf.Abs(plane.Normal.x);
			float num3 = Mathf.Abs(plane.Normal.y);
			if (num3 > num2)
			{
				num = 1;
				num2 = num3;
			}
			num3 = Mathf.Abs(plane.Normal.z);
			if (num3 > num2)
			{
				num = 2;
			}
			Triangle2 triangle2;
			Vector2 vector;
			Vector2 vector2;
			if (num == 0)
			{
				triangle2.V0.x = triangle.V0.y;
				triangle2.V0.y = triangle.V0.z;
				triangle2.V1.x = triangle.V1.y;
				triangle2.V1.y = triangle.V1.z;
				triangle2.V2.x = triangle.V2.y;
				triangle2.V2.y = triangle.V2.z;
				vector.x = end0.y;
				vector.y = end0.z;
				vector2.x = end1.y;
				vector2.y = end1.z;
			}
			else if (num == 1)
			{
				triangle2.V0.x = triangle.V0.x;
				triangle2.V0.y = triangle.V0.z;
				triangle2.V1.x = triangle.V1.x;
				triangle2.V1.y = triangle.V1.z;
				triangle2.V2.x = triangle.V2.x;
				triangle2.V2.y = triangle.V2.z;
				vector.x = end0.x;
				vector.y = end0.z;
				vector2.x = end1.x;
				vector2.y = end1.z;
			}
			else
			{
				triangle2.V0.x = triangle.V0.x;
				triangle2.V0.y = triangle.V0.y;
				triangle2.V1.x = triangle.V1.x;
				triangle2.V1.y = triangle.V1.y;
				triangle2.V2.x = triangle.V2.x;
				triangle2.V2.y = triangle.V2.y;
				vector.x = end0.x;
				vector.y = end0.y;
				vector2.x = end1.x;
				vector2.y = end1.y;
			}
			Segment2 segment = new Segment2(ref vector, ref vector2);
			Segment2Triangle2Intr segment2Triangle2Intr;
			if (!Intersection.FindSegment2Triangle2(ref segment, ref triangle2, out segment2Triangle2Intr))
			{
				info = default(Triangle3Triangle3Intr);
				return false;
			}
			Vector2 point;
			Vector2 vector3;
			if (segment2Triangle2Intr.IntersectionType == IntersectionTypes.Segment)
			{
				info.IntersectionType = IntersectionTypes.Segment;
				info.Touching = grazing;
				info.Quantity = 2;
				point = segment2Triangle2Intr.Point0;
				vector3 = segment2Triangle2Intr.Point1;
			}
			else
			{
				info.IntersectionType = IntersectionTypes.Point;
				info.Touching = true;
				info.Quantity = 1;
				point = segment2Triangle2Intr.Point0;
				vector3 = Vector2.zero;
			}
			if (num == 0)
			{
				float num4 = 1f / plane.Normal.x;
				info.Point0 = new Vector3(num4 * (plane.Constant - plane.Normal.y * point.x - plane.Normal.z * point.y), point.x, point.y);
				info.Point1 = ((info.Quantity == 2) ? new Vector3(num4 * (plane.Constant - plane.Normal.y * vector3.x - plane.Normal.z * vector3.y), vector3.x, vector3.y) : Vector3.zero);
			}
			else if (num == 1)
			{
				float num5 = 1f / plane.Normal.y;
				info.Point0 = new Vector3(point.x, num5 * (plane.Constant - plane.Normal.x * point.x - plane.Normal.z * point.y), point.y);
				info.Point1 = ((info.Quantity == 2) ? new Vector3(vector3.x, num5 * (plane.Constant - plane.Normal.x * vector3.x - plane.Normal.z * vector3.y), vector3.y) : Vector3.zero);
			}
			else
			{
				float num6 = 1f / plane.Normal.z;
				info.Point0 = new Vector3(point.x, point.y, num6 * (plane.Constant - plane.Normal.x * point.x - plane.Normal.y * point.y));
				info.Point1 = ((info.Quantity == 2) ? new Vector3(vector3.x, vector3.y, num6 * (plane.Constant - plane.Normal.x * vector3.x - plane.Normal.y * vector3.y)) : Vector3.zero);
			}
			info.CoplanarIntersectionType = IntersectionTypes.Empty;
			info.Point2 = Vector3.zero;
			info.Point3 = Vector3.zero;
			info.Point4 = Vector3.zero;
			info.Point5 = Vector3.zero;
			return true;
		}

		private static int QueryToLine(ref Vector2 test, ref Vector2 vec0, ref Vector2 vec1)
		{
			float num = test.x - vec0.x;
			float num2 = test.y - vec0.y;
			float num3 = vec1.x - vec0.x;
			float num4 = vec1.y - vec0.y;
			float num5 = num * num4 - num3 * num2;
			if (num5 > 1E-05f)
			{
				return 1;
			}
			if (num5 >= -1E-05f)
			{
				return 0;
			}
			return -1;
		}

		private static int QueryToTriangle(ref Vector2 test, ref Vector2 v0, ref Vector2 v1, ref Vector2 v2)
		{
			int num = Intersection.QueryToLine(ref test, ref v1, ref v2);
			if (num > 0)
			{
				return 1;
			}
			int num2 = Intersection.QueryToLine(ref test, ref v0, ref v2);
			if (num2 < 0)
			{
				return 1;
			}
			int num3 = Intersection.QueryToLine(ref test, ref v0, ref v1);
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

		private static bool ContainsPoint(ref Triangle3 triangle, ref Plane3 plane, ref Vector3 point, out Triangle3Triangle3Intr info)
		{
			Vector3 vector;
			Vector3 vector2;
			Vector3ex.CreateOrthonormalBasis(out vector, out vector2, ref plane.Normal);
			Vector3 value = point - triangle.V0;
			Vector3 value2 = triangle.V1 - triangle.V0;
			Vector3 value3 = triangle.V2 - triangle.V0;
			Vector2 vector3 = new Vector2(vector.Dot(value), vector2.Dot(value));
			Vector2 zero = Vector2.zero;
			Vector2 vector4 = new Vector2(vector.Dot(value2), vector2.Dot(value2));
			Vector2 vector5 = new Vector2(vector.Dot(value3), vector2.Dot(value3));
			int num = Intersection.QueryToTriangle(ref vector3, ref zero, ref vector4, ref vector5);
			if (num <= 0)
			{
				info.IntersectionType = IntersectionTypes.Point;
				info.CoplanarIntersectionType = IntersectionTypes.Empty;
				info.Touching = true;
				info.Quantity = 1;
				info.Point0 = point;
				info.Point1 = Vector3.zero;
				info.Point2 = Vector3.zero;
				info.Point3 = Vector3.zero;
				info.Point4 = Vector3.zero;
				info.Point5 = Vector3.zero;
				return true;
			}
			info = default(Triangle3Triangle3Intr);
			return false;
		}

		private static bool GetCoplanarIntersection(ref Plane3 plane, ref Triangle3 tri0, ref Triangle3 tri1, out Triangle3Triangle3Intr info)
		{
			int num = 0;
			float num2 = Mathf.Abs(plane.Normal.x);
			float num3 = Mathf.Abs(plane.Normal.y);
			if (num3 > num2)
			{
				num = 1;
				num2 = num3;
			}
			num3 = Mathf.Abs(plane.Normal.z);
			if (num3 > num2)
			{
				num = 2;
			}
			Triangle2 triangle;
			Triangle2 triangle2;
			if (num == 0)
			{
				triangle.V0.x = tri0.V0.y;
				triangle.V0.y = tri0.V0.z;
				triangle2.V0.x = tri1.V0.y;
				triangle2.V0.y = tri1.V0.z;
				triangle.V1.x = tri0.V1.y;
				triangle.V1.y = tri0.V1.z;
				triangle2.V1.x = tri1.V1.y;
				triangle2.V1.y = tri1.V1.z;
				triangle.V2.x = tri0.V2.y;
				triangle.V2.y = tri0.V2.z;
				triangle2.V2.x = tri1.V2.y;
				triangle2.V2.y = tri1.V2.z;
			}
			else if (num == 1)
			{
				triangle.V0.x = tri0.V0.x;
				triangle.V0.y = tri0.V0.z;
				triangle2.V0.x = tri1.V0.x;
				triangle2.V0.y = tri1.V0.z;
				triangle.V1.x = tri0.V1.x;
				triangle.V1.y = tri0.V1.z;
				triangle2.V1.x = tri1.V1.x;
				triangle2.V1.y = tri1.V1.z;
				triangle.V2.x = tri0.V2.x;
				triangle.V2.y = tri0.V2.z;
				triangle2.V2.x = tri1.V2.x;
				triangle2.V2.y = tri1.V2.z;
			}
			else
			{
				triangle.V0.x = tri0.V0.x;
				triangle.V0.y = tri0.V0.y;
				triangle2.V0.x = tri1.V0.x;
				triangle2.V0.y = tri1.V0.y;
				triangle.V1.x = tri0.V1.x;
				triangle.V1.y = tri0.V1.y;
				triangle2.V1.x = tri1.V1.x;
				triangle2.V1.y = tri1.V1.y;
				triangle.V2.x = tri0.V2.x;
				triangle.V2.y = tri0.V2.y;
				triangle2.V2.x = tri1.V2.x;
				triangle2.V2.y = tri1.V2.y;
			}
			Vector2 vector = triangle.V1 - triangle.V0;
			Vector2 value = triangle.V2 - triangle.V0;
			if (vector.DotPerp(value) < 0f)
			{
				Vector2 v = triangle.V1;
				triangle.V1 = triangle.V2;
				triangle.V2 = v;
			}
			vector = triangle2.V1 - triangle2.V0;
			value = triangle2.V2 - triangle2.V0;
			if (vector.DotPerp(value) < 0f)
			{
				Vector2 v = triangle2.V1;
				triangle2.V1 = triangle2.V2;
				triangle2.V2 = v;
			}
			Triangle2Triangle2Intr triangle2Triangle2Intr;
			bool flag = Intersection.FindTriangle2Triangle2(ref triangle, ref triangle2, out triangle2Triangle2Intr);
			info = default(Triangle3Triangle3Intr);
			if (!flag)
			{
				return false;
			}
			int quantity = triangle2Triangle2Intr.Quantity;
			info.Quantity = quantity;
			if (num == 0)
			{
				float num4 = 1f / plane.Normal.x;
				for (int i = 0; i < quantity; i++)
				{
					info[i] = new Vector3(num4 * (plane.Constant - plane.Normal.y * triangle2Triangle2Intr[i].x - plane.Normal.z * triangle2Triangle2Intr[i].y), triangle2Triangle2Intr[i].x, triangle2Triangle2Intr[i].y);
				}
			}
			else if (num == 1)
			{
				float num5 = 1f / plane.Normal.y;
				for (int j = 0; j < quantity; j++)
				{
					info[j] = new Vector3(triangle2Triangle2Intr[j].x, num5 * (plane.Constant - plane.Normal.x * triangle2Triangle2Intr[j].x - plane.Normal.z * triangle2Triangle2Intr[j].y), triangle2Triangle2Intr[j].y);
				}
			}
			else
			{
				float num6 = 1f / plane.Normal.z;
				for (int k = 0; k < quantity; k++)
				{
					info[k] = new Vector3(triangle2Triangle2Intr[k].x, triangle2Triangle2Intr[k].y, num6 * (plane.Constant - plane.Normal.x * triangle2Triangle2Intr[k].x - plane.Normal.y * triangle2Triangle2Intr[k].y));
				}
			}
			info.IntersectionType = IntersectionTypes.Plane;
			info.CoplanarIntersectionType = triangle2Triangle2Intr.IntersectionType;
			return true;
		}

		public static bool TestTriangle3Triangle3(ref Triangle3 triangle0, ref Triangle3 triangle1, out IntersectionTypes intersectionType)
		{
			Vector3 vector = triangle0.V1 - triangle0.V0;
			Vector3 vector2 = triangle0.V2 - triangle0.V1;
			Vector3 vector3 = triangle0.V0 - triangle0.V2;
			Vector3 vector4 = vector.UnitCross(vector2);
			float num = vector4.Dot(triangle0.V0);
			float num2;
			float num3;
			Intersection.ProjectOntoAxis(ref triangle1, ref vector4, out num2, out num3);
			if (num < num2 || num > num3)
			{
				intersectionType = IntersectionTypes.Empty;
				return false;
			}
			Vector3 vector5 = triangle1.V1 - triangle1.V0;
			Vector3 value = triangle1.V2 - triangle1.V1;
			Vector3 value2 = triangle1.V0 - triangle1.V2;
			Vector3 vector6 = vector5.UnitCross(value);
			intersectionType = IntersectionTypes.Empty;
			Vector3 vector7 = vector4.UnitCross(vector6);
			if (vector7.Dot(vector7) >= 1E-05f)
			{
				float num4 = vector6.Dot(triangle1.V0);
				float num5;
				float num6;
				Intersection.ProjectOntoAxis(ref triangle0, ref vector6, out num5, out num6);
				if (num4 < num5 || num4 > num6)
				{
					return false;
				}
				Vector3 vector8 = vector.UnitCross(vector5);
				Intersection.ProjectOntoAxis(ref triangle0, ref vector8, out num5, out num6);
				Intersection.ProjectOntoAxis(ref triangle1, ref vector8, out num2, out num3);
				if (num6 < num2 || num3 < num5)
				{
					return false;
				}
				vector8 = vector.UnitCross(value);
				Intersection.ProjectOntoAxis(ref triangle0, ref vector8, out num5, out num6);
				Intersection.ProjectOntoAxis(ref triangle1, ref vector8, out num2, out num3);
				if (num6 < num2 || num3 < num5)
				{
					return false;
				}
				vector8 = vector.UnitCross(value2);
				Intersection.ProjectOntoAxis(ref triangle0, ref vector8, out num5, out num6);
				Intersection.ProjectOntoAxis(ref triangle1, ref vector8, out num2, out num3);
				if (num6 < num2 || num3 < num5)
				{
					return false;
				}
				vector8 = vector2.UnitCross(vector5);
				Intersection.ProjectOntoAxis(ref triangle0, ref vector8, out num5, out num6);
				Intersection.ProjectOntoAxis(ref triangle1, ref vector8, out num2, out num3);
				if (num6 < num2 || num3 < num5)
				{
					return false;
				}
				vector8 = vector2.UnitCross(value);
				Intersection.ProjectOntoAxis(ref triangle0, ref vector8, out num5, out num6);
				Intersection.ProjectOntoAxis(ref triangle1, ref vector8, out num2, out num3);
				if (num6 < num2 || num3 < num5)
				{
					return false;
				}
				vector8 = vector2.UnitCross(value2);
				Intersection.ProjectOntoAxis(ref triangle0, ref vector8, out num5, out num6);
				Intersection.ProjectOntoAxis(ref triangle1, ref vector8, out num2, out num3);
				if (num6 < num2 || num3 < num5)
				{
					return false;
				}
				vector8 = vector3.UnitCross(vector5);
				Intersection.ProjectOntoAxis(ref triangle0, ref vector8, out num5, out num6);
				Intersection.ProjectOntoAxis(ref triangle1, ref vector8, out num2, out num3);
				if (num6 < num2 || num3 < num5)
				{
					return false;
				}
				vector8 = vector3.UnitCross(value);
				Intersection.ProjectOntoAxis(ref triangle0, ref vector8, out num5, out num6);
				Intersection.ProjectOntoAxis(ref triangle1, ref vector8, out num2, out num3);
				if (num6 < num2 || num3 < num5)
				{
					return false;
				}
				vector8 = vector3.UnitCross(value2);
				Intersection.ProjectOntoAxis(ref triangle0, ref vector8, out num5, out num6);
				Intersection.ProjectOntoAxis(ref triangle1, ref vector8, out num2, out num3);
				if (num6 < num2 || num3 < num5)
				{
					return false;
				}
				intersectionType = IntersectionTypes.Other;
			}
			else
			{
				Vector3 vector8 = vector4.UnitCross(vector);
				float num5;
				float num6;
				Intersection.ProjectOntoAxis(ref triangle0, ref vector8, out num5, out num6);
				Intersection.ProjectOntoAxis(ref triangle1, ref vector8, out num2, out num3);
				if (num6 < num2 || num3 < num5)
				{
					return false;
				}
				vector8 = vector4.UnitCross(vector2);
				Intersection.ProjectOntoAxis(ref triangle0, ref vector8, out num5, out num6);
				Intersection.ProjectOntoAxis(ref triangle1, ref vector8, out num2, out num3);
				if (num6 < num2 || num3 < num5)
				{
					return false;
				}
				vector8 = vector4.UnitCross(vector3);
				Intersection.ProjectOntoAxis(ref triangle0, ref vector8, out num5, out num6);
				Intersection.ProjectOntoAxis(ref triangle1, ref vector8, out num2, out num3);
				if (num6 < num2 || num3 < num5)
				{
					return false;
				}
				vector8 = vector6.UnitCross(vector5);
				Intersection.ProjectOntoAxis(ref triangle0, ref vector8, out num5, out num6);
				Intersection.ProjectOntoAxis(ref triangle1, ref vector8, out num2, out num3);
				if (num6 < num2 || num3 < num5)
				{
					return false;
				}
				vector8 = vector6.UnitCross(value);
				Intersection.ProjectOntoAxis(ref triangle0, ref vector8, out num5, out num6);
				Intersection.ProjectOntoAxis(ref triangle1, ref vector8, out num2, out num3);
				if (num6 < num2 || num3 < num5)
				{
					return false;
				}
				vector8 = vector6.UnitCross(value2);
				Intersection.ProjectOntoAxis(ref triangle0, ref vector8, out num5, out num6);
				Intersection.ProjectOntoAxis(ref triangle1, ref vector8, out num2, out num3);
				if (num6 < num2 || num3 < num5)
				{
					return false;
				}
				intersectionType = IntersectionTypes.Plane;
			}
			return true;
		}

		public static bool TestTriangle3Triangle3(ref Triangle3 triangle0, ref Triangle3 triangle1)
		{
			IntersectionTypes intersectionTypes;
			return Intersection.TestTriangle3Triangle3(ref triangle0, ref triangle1, out intersectionTypes);
		}

		public static bool FindTriangle3Triangle3(ref Triangle3 triangle0, ref Triangle3 triangle1, out Triangle3Triangle3Intr info, bool reportCoplanarIntersections = false)
		{
			Plane3 plane = new Plane3(ref triangle0.V0, ref triangle0.V1, ref triangle0.V2);
			float num;
			float num2;
			float num3;
			int num4;
			int num5;
			int num6;
			int num7;
			int num8;
			int num9;
			Intersection.TrianglePlaneRelations(ref triangle1, ref plane, out num, out num2, out num3, out num4, out num5, out num6, out num7, out num8, out num9);
			if (num7 == 3 || num8 == 3)
			{
				info = default(Triangle3Triangle3Intr);
				return false;
			}
			if (num9 == 3)
			{
				if (reportCoplanarIntersections)
				{
					return Intersection.GetCoplanarIntersection(ref plane, ref triangle0, ref triangle1, out info);
				}
				info = default(Triangle3Triangle3Intr);
				return false;
			}
			else
			{
				if (num7 == 0 || num8 == 0)
				{
					if (num9 == 2)
					{
						if (num4 != 0)
						{
							return Intersection.IntersectsSegment(ref plane, ref triangle0, ref triangle1.V2, ref triangle1.V1, true, out info);
						}
						if (num5 != 0)
						{
							return Intersection.IntersectsSegment(ref plane, ref triangle0, ref triangle1.V0, ref triangle1.V2, true, out info);
						}
						if (num6 != 0)
						{
							return Intersection.IntersectsSegment(ref plane, ref triangle0, ref triangle1.V1, ref triangle1.V0, true, out info);
						}
					}
					else
					{
						if (num4 == 0)
						{
							return Intersection.ContainsPoint(ref triangle0, ref plane, ref triangle1.V0, out info);
						}
						if (num5 == 0)
						{
							return Intersection.ContainsPoint(ref triangle0, ref plane, ref triangle1.V1, out info);
						}
						if (num6 == 0)
						{
							return Intersection.ContainsPoint(ref triangle0, ref plane, ref triangle1.V2, out info);
						}
					}
				}
				Plane3 plane2 = new Plane3(ref triangle1.V0, ref triangle1.V1, ref triangle1.V2);
				if (Intersection.TrianglePlaneRelationsQuick(ref triangle0, ref plane2))
				{
					info = default(Triangle3Triangle3Intr);
					return false;
				}
				if (num9 == 0)
				{
					int num10 = (num7 == 1) ? 1 : -1;
					if (num4 == num10)
					{
						float d = num / (num - num3);
						Vector3 vector = triangle1.V0 + d * (triangle1.V2 - triangle1.V0);
						d = num / (num - num2);
						Vector3 vector2 = triangle1.V0 + d * (triangle1.V1 - triangle1.V0);
						return Intersection.IntersectsSegment(ref plane, ref triangle0, ref vector, ref vector2, false, out info);
					}
					if (num5 == num10)
					{
						float d = num2 / (num2 - num);
						Vector3 vector = triangle1.V1 + d * (triangle1.V0 - triangle1.V1);
						d = num2 / (num2 - num3);
						Vector3 vector2 = triangle1.V1 + d * (triangle1.V2 - triangle1.V1);
						return Intersection.IntersectsSegment(ref plane, ref triangle0, ref vector, ref vector2, false, out info);
					}
					if (num6 == num10)
					{
						float d = num3 / (num3 - num2);
						Vector3 vector = triangle1.V2 + d * (triangle1.V1 - triangle1.V2);
						d = num3 / (num3 - num);
						Vector3 vector2 = triangle1.V2 + d * (triangle1.V0 - triangle1.V2);
						return Intersection.IntersectsSegment(ref plane, ref triangle0, ref vector, ref vector2, false, out info);
					}
				}
				else
				{
					if (num4 == 0)
					{
						float d = num3 / (num3 - num2);
						Vector3 vector = triangle1.V2 + d * (triangle1.V1 - triangle1.V2);
						return Intersection.IntersectsSegment(ref plane, ref triangle0, ref triangle1.V0, ref vector, false, out info);
					}
					if (num5 == 0)
					{
						float d = num / (num - num3);
						Vector3 vector = triangle1.V0 + d * (triangle1.V2 - triangle1.V0);
						return Intersection.IntersectsSegment(ref plane, ref triangle0, ref triangle1.V1, ref vector, false, out info);
					}
					if (num6 == 0)
					{
						float d = num2 / (num2 - num);
						Vector3 vector = triangle1.V1 + d * (triangle1.V0 - triangle1.V1);
						return Intersection.IntersectsSegment(ref plane, ref triangle0, ref triangle1.V2, ref vector, false, out info);
					}
				}
				info = default(Triangle3Triangle3Intr);
				return false;
			}
		}

		static Intersection()
		{
			Intersection._intervalThreshold = (Intersection._dotThreshold = (Intersection._distanceThreshold = 1E-05f));
		}

		public static int FindSegment1Segment1(float seg0Start, float seg0End, float seg1Start, float seg1End, out float w0, out float w1)
		{
			w0 = (w1 = 0f);
			float distanceThreshold = Intersection._distanceThreshold;
			if (seg0End < seg1Start - distanceThreshold || seg0Start > seg1End + distanceThreshold)
			{
				return 0;
			}
			if (seg0End <= seg1Start + distanceThreshold)
			{
				w0 = seg0End;
				return 1;
			}
			if (seg0Start >= seg1End - distanceThreshold)
			{
				w0 = seg0Start;
				return 1;
			}
			if (seg0Start < seg1Start)
			{
				w0 = seg1Start;
			}
			else
			{
				w0 = seg0Start;
			}
			if (seg0End > seg1End)
			{
				w1 = seg1End;
			}
			else
			{
				w1 = seg0End;
			}
			if (w1 - w0 <= distanceThreshold)
			{
				return 1;
			}
			return 2;
		}
	}
}

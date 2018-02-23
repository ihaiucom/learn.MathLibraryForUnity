using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dest.Math
{
	public struct Box2
	{
		public Vector2 Center;

		public Vector2 Axis0;

		public Vector2 Axis1;

		public Vector2 Extents;

		public Box2(ref Vector2 center, ref Vector2 axis0, ref Vector2 axis1, ref Vector2 extents)
		{
			this.Center = center;
			this.Axis0 = axis0;
			this.Axis1 = axis1;
			this.Extents = extents;
		}

		public Box2(Vector2 center, Vector2 axis0, Vector2 axis1, Vector2 extents)
		{
			this.Center = center;
			this.Axis0 = axis0;
			this.Axis1 = axis1;
			this.Extents = extents;
		}

		public Box2(ref AAB2 box)
		{
			box.CalcCenterExtents(out this.Center, out this.Extents);
			this.Axis0 = Vector2ex.UnitX;
			this.Axis1 = Vector2ex.UnitY;
		}

		public Box2(AAB2 box)
		{
			box.CalcCenterExtents(out this.Center, out this.Extents);
			this.Axis0 = Vector2ex.UnitX;
			this.Axis1 = Vector2ex.UnitY;
		}

		public static Box2 CreateFromPoints(IList<Vector2> points)
		{
			int count = points.Count;
			if (count == 0)
			{
				return default(Box2);
			}
			Box2 result = Approximation.GaussPointsFit2(points);
			Vector2 vector = points[0] - result.Center;
			Vector2 vector2 = new Vector2(vector.Dot(result.Axis0), vector.Dot(result.Axis1));
			Vector2 vector3 = vector2;
			for (int i = 1; i < count; i++)
			{
				vector = points[i] - result.Center;
				for (int j = 0; j < 2; j++)
				{
					float num = vector.Dot(result.GetAxis(j));
					if (num < vector2[j])
					{
						vector2[j] = num;
					}
					else if (num > vector3[j])
					{
						vector3[j] = num;
					}
				}
			}
			result.Center += 0.5f * (vector2[0] + vector3[0]) * result.Axis0 + 0.5f * (vector2[1] + vector3[1]) * result.Axis1;
			result.Extents.x = 0.5f * (vector3[0] - vector2[0]);
			result.Extents.y = 0.5f * (vector3[1] - vector2[1]);
			return result;
		}

		public Vector2 GetAxis(int index)
		{
			if (index == 0)
			{
				return this.Axis0;
			}
			if (index == 1)
			{
				return this.Axis1;
			}
			return Vector2ex.Zero;
		}

		public void CalcVertices(out Vector2 vertex0, out Vector2 vertex1, out Vector2 vertex2, out Vector2 vertex3)
		{
			Vector2 b = this.Axis0 * this.Extents.x;
			Vector2 b2 = this.Axis1 * this.Extents.y;
			vertex0 = this.Center - b - b2;
			vertex1 = this.Center + b - b2;
			vertex2 = this.Center + b + b2;
			vertex3 = this.Center - b + b2;
		}

		public Vector2[] CalcVertices()
		{
			Vector2 b = this.Axis0 * this.Extents.x;
			Vector2 b2 = this.Axis1 * this.Extents.y;
			return new Vector2[]
			{
				this.Center - b - b2,
				this.Center + b - b2,
				this.Center + b + b2,
				this.Center - b + b2
			};
		}

		public void CalcVertices(Vector2[] array)
		{
			Vector2 b = this.Axis0 * this.Extents.x;
			Vector2 b2 = this.Axis1 * this.Extents.y;
			array[0] = this.Center - b - b2;
			array[1] = this.Center + b - b2;
			array[2] = this.Center + b + b2;
			array[3] = this.Center - b + b2;
		}

		public float CalcArea()
		{
			return 4f * this.Extents.x * this.Extents.y;
		}

		public float DistanceTo(Vector2 point)
		{
			return Distance.Point2Box2(ref point, ref this);
		}

		public Vector2 Project(Vector2 point)
		{
			Vector2 result;
			Distance.SqrPoint2Box2(ref point, ref this, out result);
			return result;
		}

		public bool Contains(ref Vector2 point)
		{
			Vector2 vector;
			vector.x = point.x - this.Center.x;
			vector.y = point.y - this.Center.y;
			float num = vector.Dot(this.Axis0);
			if (num < -this.Extents.x)
			{
				return false;
			}
			if (num > this.Extents.x)
			{
				return false;
			}
			num = vector.Dot(this.Axis1);
			return num >= -this.Extents.y && num <= this.Extents.y;
		}

		public bool Contains(Vector2 point)
		{
			Vector2 vector;
			vector.x = point.x - this.Center.x;
			vector.y = point.y - this.Center.y;
			float num = vector.Dot(this.Axis0);
			if (num < -this.Extents.x)
			{
				return false;
			}
			if (num > this.Extents.x)
			{
				return false;
			}
			num = vector.Dot(this.Axis1);
			return num >= -this.Extents.y && num <= this.Extents.y;
		}

		public void Include(ref Box2 box)
		{
			Box2 box2 = default(Box2);
			box2.Center = 0.5f * (this.Center + box.Center);
			if (this.Axis0.Dot(box.Axis0) >= 0f)
			{
				box2.Axis0 = 0.5f * (this.Axis0 + box.Axis0);
				box2.Axis0.Normalize();
			}
			else
			{
				box2.Axis0 = 0.5f * (this.Axis0 - box.Axis0);
				box2.Axis0.Normalize();
			}
			box2.Axis1 = -box2.Axis0.Perp();
			Vector2 zero = Vector2ex.Zero;
			Vector2 zero2 = Vector2ex.Zero;
			Vector2[] array = this.CalcVertices();
			for (int i = 0; i < 4; i++)
			{
				Vector2 vector = array[i] - box2.Center;
				for (int j = 0; j < 2; j++)
				{
					float num = vector.Dot(box2.GetAxis(j));
					if (num > zero2[j])
					{
						zero2[j] = num;
					}
					else if (num < zero[j])
					{
						zero[j] = num;
					}
				}
			}
			box.CalcVertices(out array[0], out array[1], out array[2], out array[3]);
			for (int i = 0; i < 4; i++)
			{
				Vector2 vector = array[i] - box2.Center;
				for (int j = 0; j < 2; j++)
				{
					float num = vector.Dot(box2.GetAxis(j));
					if (num > zero2[j])
					{
						zero2[j] = num;
					}
					else if (num < zero[j])
					{
						zero[j] = num;
					}
				}
			}
			for (int j = 0; j < 2; j++)
			{
				box2.Center += box2.GetAxis(j) * (0.5f * (zero2[j] + zero[j]));
				box2.Extents[j] = 0.5f * (zero2[j] - zero[j]);
			}
			this = box2;
		}

		public void Include(Box2 box)
		{
			this.Include(ref box);
		}

		public override string ToString()
		{
			return string.Format("[Center: {0} Axis0: {1} Axis1: {2} Extents: {3}]", new object[]
			{
				this.Center.ToStringEx(),
				this.Axis0.ToStringEx(),
				this.Axis1.ToStringEx(),
				this.Extents.ToStringEx()
			});
		}
	}
}

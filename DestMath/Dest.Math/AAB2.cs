using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dest.Math
{
	public struct AAB2
	{
		public Vector2 Min;

		public Vector2 Max;

		public AAB2(ref Vector2 min, ref Vector2 max)
		{
			this.Min = min;
			this.Max = max;
		}

		public AAB2(Vector2 min, Vector2 max)
		{
			this.Min = min;
			this.Max = max;
		}

		public AAB2(float xMin, float xMax, float yMin, float yMax)
		{
			this.Min.x = xMin;
			this.Min.y = yMin;
			this.Max.x = xMax;
			this.Max.y = yMax;
		}

		public static implicit operator Rect(AAB2 value)
		{
			return Rect.MinMaxRect(value.Min.x, value.Min.y, value.Max.x, value.Max.y);
		}

		public static implicit operator AAB2(Rect value)
		{
			return new AAB2
			{
				Min = new Vector2(value.xMin, value.yMin),
				Max = new Vector2(value.xMax, value.yMax)
			};
		}

		public static AAB2 CreateFromPoint(ref Vector2 point)
		{
			AAB2 result;
			result.Min = point;
			result.Max = point;
			return result;
		}

		public static AAB2 CreateFromPoint(Vector2 point)
		{
			AAB2 result;
			result.Min = point;
			result.Max = point;
			return result;
		}

		public static AAB2 CreateFromTwoPoints(ref Vector2 point0, ref Vector2 point1)
		{
			AAB2 result;
			if (point0.x < point1.x)
			{
				result.Min.x = point0.x;
				result.Max.x = point1.x;
			}
			else
			{
				result.Min.x = point1.x;
				result.Max.x = point0.x;
			}
			if (point0.y < point1.y)
			{
				result.Min.y = point0.y;
				result.Max.y = point1.y;
			}
			else
			{
				result.Min.y = point1.y;
				result.Max.y = point0.y;
			}
			return result;
		}

		public static AAB2 CreateFromTwoPoints(Vector2 point0, Vector2 point1)
		{
			return AAB2.CreateFromTwoPoints(ref point0, ref point1);
		}

		public static AAB2 CreateFromPoints(IEnumerable<Vector2> points)
		{
			IEnumerator<Vector2> enumerator = points.GetEnumerator();
			enumerator.Reset();
			if (!enumerator.MoveNext())
			{
				return default(AAB2);
			}
			AAB2 result = AAB2.CreateFromPoint(enumerator.Current);
			while (enumerator.MoveNext())
			{
				result.Include(enumerator.Current);
			}
			return result;
		}

		public static AAB2 CreateFromPoints(IList<Vector2> points)
		{
			int count = points.Count;
			if (count > 0)
			{
				AAB2 result = AAB2.CreateFromPoint(points[0]);
				for (int i = 1; i < count; i++)
				{
					result.Include(points[i]);
				}
				return result;
			}
			return default(AAB2);
		}

		public static AAB2 CreateFromPoints(Vector2[] points)
		{
			int num = points.Length;
			if (num > 0)
			{
				AAB2 result = AAB2.CreateFromPoint(ref points[0]);
				for (int i = 1; i < num; i++)
				{
					result.Include(ref points[i]);
				}
				return result;
			}
			return default(AAB2);
		}

		public void CalcCenterExtents(out Vector2 center, out Vector2 extents)
		{
			center.x = 0.5f * (this.Max.x + this.Min.x);
			center.y = 0.5f * (this.Max.y + this.Min.y);
			extents.x = 0.5f * (this.Max.x - this.Min.x);
			extents.y = 0.5f * (this.Max.y - this.Min.y);
		}

		public void CalcVertices(out Vector2 vertex0, out Vector2 vertex1, out Vector2 vertex2, out Vector2 vertex3)
		{
			vertex0 = this.Min;
			vertex1 = new Vector2(this.Max.x, this.Min.y);
			vertex2 = this.Max;
			vertex3 = new Vector2(this.Min.x, this.Max.y);
		}

		public Vector2[] CalcVertices()
		{
			return new Vector2[]
			{
				this.Min,
				new Vector2(this.Max.x, this.Min.y),
				this.Max,
				new Vector2(this.Min.x, this.Max.y)
			};
		}

		public void CalcVertices(Vector2[] array)
		{
			array[0] = this.Min;
			array[1] = new Vector2(this.Max.x, this.Min.y);
			array[2] = this.Max;
			array[3] = new Vector2(this.Min.x, this.Max.y);
		}

		public float CalcArea()
		{
			return (this.Max.x - this.Min.x) * (this.Max.y - this.Min.y);
		}

		public float DistanceTo(Vector2 point)
		{
			return Distance.Point2AAB2(ref point, ref this);
		}

		public Vector2 Project(Vector2 point)
		{
			Vector2 result;
			Distance.SqrPoint2AAB2(ref point, ref this, out result);
			return result;
		}

		public bool Contains(ref Vector2 point)
		{
			return point.x >= this.Min.x && point.x <= this.Max.x && point.y >= this.Min.y && point.y <= this.Max.y;
		}

		public bool Contains(Vector2 point)
		{
			return point.x >= this.Min.x && point.x <= this.Max.x && point.y >= this.Min.y && point.y <= this.Max.y;
		}

		public void Include(ref Vector2 point)
		{
			if (point.x < this.Min.x)
			{
				this.Min.x = point.x;
			}
			else if (point.x > this.Max.x)
			{
				this.Max.x = point.x;
			}
			if (point.y < this.Min.y)
			{
				this.Min.y = point.y;
				return;
			}
			if (point.y > this.Max.y)
			{
				this.Max.y = point.y;
			}
		}

		public void Include(Vector2 point)
		{
			if (point.x < this.Min.x)
			{
				this.Min.x = point.x;
			}
			else if (point.x > this.Max.x)
			{
				this.Max.x = point.x;
			}
			if (point.y < this.Min.y)
			{
				this.Min.y = point.y;
				return;
			}
			if (point.y > this.Max.y)
			{
				this.Max.y = point.y;
			}
		}

		public void Include(ref AAB2 box)
		{
			this.Include(ref box.Min);
			this.Include(ref box.Max);
		}

		public void Include(AAB2 box)
		{
			this.Include(ref box.Min);
			this.Include(ref box.Max);
		}

		public override string ToString()
		{
			return string.Format("[Min: {0} Max: {1}]", this.Min.ToStringEx(), this.Max.ToStringEx());
		}
	}
}

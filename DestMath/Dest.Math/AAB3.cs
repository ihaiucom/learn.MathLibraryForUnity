using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dest.Math
{
	public struct AAB3
	{
		public Vector3 Min;

		public Vector3 Max;

		public AAB3(ref Vector3 min, ref Vector3 max)
		{
			this.Min = min;
			this.Max = max;
		}

		public AAB3(Vector3 min, Vector3 max)
		{
			this.Min = min;
			this.Max = max;
		}

		public AAB3(float xMin, float xMax, float yMin, float yMax, float zMin, float zMax)
		{
			this.Min.x = xMin;
			this.Min.y = yMin;
			this.Min.z = zMin;
			this.Max.x = xMax;
			this.Max.y = yMax;
			this.Max.z = zMax;
		}

		public static implicit operator Bounds(AAB3 value)
		{
			Vector3 center;
			Vector3 extents;
			value.CalcCenterExtents(out center, out extents);
			return new Bounds
			{
				center = center,
				extents = extents
			};
		}

		public static implicit operator AAB3(Bounds value)
		{
			return new AAB3
			{
				Min = value.min,
				Max = value.max
			};
		}

		public static AAB3 CreateFromPoint(ref Vector3 point)
		{
			AAB3 result;
			result.Min = point;
			result.Max = point;
			return result;
		}

		public static AAB3 CreateFromPoint(Vector3 point)
		{
			AAB3 result;
			result.Min = point;
			result.Max = point;
			return result;
		}

		public static AAB3 CreateFromTwoPoints(ref Vector3 point0, ref Vector3 point1)
		{
			AAB3 result;
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
			if (point0.z < point1.z)
			{
				result.Min.z = point0.z;
				result.Max.z = point1.z;
			}
			else
			{
				result.Min.z = point1.z;
				result.Max.z = point0.z;
			}
			return result;
		}

		public static AAB3 CreateFromTwoPoints(Vector3 point0, Vector3 point1)
		{
			return AAB3.CreateFromTwoPoints(ref point0, ref point1);
		}

		public static AAB3 CreateFromPoints(IEnumerable<Vector3> points)
		{
			IEnumerator<Vector3> enumerator = points.GetEnumerator();
			enumerator.Reset();
			if (!enumerator.MoveNext())
			{
				return default(AAB3);
			}
			AAB3 result = AAB3.CreateFromPoint(enumerator.Current);
			while (enumerator.MoveNext())
			{
				result.Include(enumerator.Current);
			}
			return result;
		}

		public static AAB3 CreateFromPoints(IList<Vector3> points)
		{
			int count = points.Count;
			if (count > 0)
			{
				AAB3 result = AAB3.CreateFromPoint(points[0]);
				for (int i = 1; i < count; i++)
				{
					result.Include(points[i]);
				}
				return result;
			}
			return default(AAB3);
		}

		public static AAB3 CreateFromPoints(Vector3[] points)
		{
			int num = points.Length;
			if (num > 0)
			{
				AAB3 result = AAB3.CreateFromPoint(ref points[0]);
				for (int i = 1; i < num; i++)
				{
					result.Include(ref points[i]);
				}
				return result;
			}
			return default(AAB3);
		}

		public void CalcCenterExtents(out Vector3 center, out Vector3 extents)
		{
			center.x = 0.5f * (this.Max.x + this.Min.x);
			center.y = 0.5f * (this.Max.y + this.Min.y);
			center.z = 0.5f * (this.Max.z + this.Min.z);
			extents.x = 0.5f * (this.Max.x - this.Min.x);
			extents.y = 0.5f * (this.Max.y - this.Min.y);
			extents.z = 0.5f * (this.Max.z - this.Min.z);
		}

		public void CalcVertices(out Vector3 vertex0, out Vector3 vertex1, out Vector3 vertex2, out Vector3 vertex3, out Vector3 vertex4, out Vector3 vertex5, out Vector3 vertex6, out Vector3 vertex7)
		{
			vertex0 = this.Min;
			vertex1 = new Vector3(this.Max.x, this.Min.y, this.Min.z);
			vertex2 = new Vector3(this.Max.x, this.Max.y, this.Min.z);
			vertex3 = new Vector3(this.Min.x, this.Max.y, this.Min.z);
			vertex4 = new Vector3(this.Min.x, this.Min.y, this.Max.z);
			vertex5 = new Vector3(this.Max.x, this.Min.y, this.Max.z);
			vertex6 = this.Max;
			vertex7 = new Vector3(this.Min.x, this.Max.y, this.Max.z);
		}

		public Vector3[] CalcVertices()
		{
			return new Vector3[]
			{
				this.Min,
				new Vector3(this.Max.x, this.Min.y, this.Min.z),
				new Vector3(this.Max.x, this.Max.y, this.Min.z),
				new Vector3(this.Min.x, this.Max.y, this.Min.z),
				new Vector3(this.Min.x, this.Min.y, this.Max.z),
				new Vector3(this.Max.x, this.Min.y, this.Max.z),
				this.Max,
				new Vector3(this.Min.x, this.Max.y, this.Max.z)
			};
		}

		public void CalcVertices(Vector3[] array)
		{
			array[0] = this.Min;
			array[1] = new Vector3(this.Max.x, this.Min.y, this.Min.z);
			array[2] = new Vector3(this.Max.x, this.Max.y, this.Min.z);
			array[3] = new Vector3(this.Min.x, this.Max.y, this.Min.z);
			array[4] = new Vector3(this.Min.x, this.Min.y, this.Max.z);
			array[5] = new Vector3(this.Max.x, this.Min.y, this.Max.z);
			array[6] = this.Max;
			array[7] = new Vector3(this.Min.x, this.Max.y, this.Max.z);
		}

		public float CalcVolume()
		{
			return (this.Max.x - this.Min.x) * (this.Max.y - this.Min.y) * (this.Max.z - this.Min.z);
		}

		public float DistanceTo(Vector3 point)
		{
			return Distance.Point3AAB3(ref point, ref this);
		}

		public Vector3 Project(Vector3 point)
		{
			Vector3 result;
			Distance.SqrPoint3AAB3(ref point, ref this, out result);
			return result;
		}

		public bool Contains(ref Vector3 point)
		{
			return point.x >= this.Min.x && point.x <= this.Max.x && point.y >= this.Min.y && point.y <= this.Max.y && point.z >= this.Min.z && point.z <= this.Max.z;
		}

		public bool Contains(Vector3 point)
		{
			return point.x >= this.Min.x && point.x <= this.Max.x && point.y >= this.Min.y && point.y <= this.Max.y && point.z >= this.Min.z && point.z <= this.Max.z;
		}

		public void Include(ref Vector3 point)
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
			}
			else if (point.y > this.Max.y)
			{
				this.Max.y = point.y;
			}
			if (point.z < this.Min.z)
			{
				this.Min.z = point.z;
				return;
			}
			if (point.z > this.Max.z)
			{
				this.Max.z = point.z;
			}
		}

		public void Include(Vector3 point)
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
			}
			else if (point.y > this.Max.y)
			{
				this.Max.y = point.y;
			}
			if (point.z < this.Min.z)
			{
				this.Min.z = point.z;
				return;
			}
			if (point.z > this.Max.z)
			{
				this.Max.z = point.z;
			}
		}

		public void Include(ref AAB3 box)
		{
			this.Include(ref box.Min);
			this.Include(ref box.Max);
		}

		public void Include(AAB3 box)
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

using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Rectangle3
	{
		public Vector3 Center;

		public Vector3 Axis0;

		public Vector3 Axis1;

		public Vector3 Normal;

		public Vector2 Extents;

		public Rectangle3(ref Vector3 center, ref Vector3 axis0, ref Vector3 axis1, ref Vector2 extents)
		{
			this.Center = center;
			this.Axis0 = axis0;
			this.Axis1 = axis1;
			this.Normal = axis0.Cross(axis1);
			this.Extents = extents;
		}

		public Rectangle3(Vector3 center, Vector3 axis0, Vector3 axis1, Vector2 extents)
		{
			this.Center = center;
			this.Axis0 = axis0;
			this.Axis1 = axis1;
			this.Normal = axis0.Cross(axis1);
			this.Extents = extents;
		}

		public static Rectangle3 CreateFromCCWPoints(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
		{
			Vector3 vector = p1 - p0;
			Vector3 vector2 = p2 - p1;
			Rectangle3 result;
			result.Center = (p0 + p2) * 0.5f;
			result.Extents.x = Vector3ex.Normalize(ref vector, 1E-05f) * 0.5f;
			result.Extents.y = Vector3ex.Normalize(ref vector2, 1E-05f) * 0.5f;
			result.Axis0 = vector;
			result.Axis1 = vector2;
			result.Normal = vector.Cross(vector2);
			return result;
		}

		public static Rectangle3 CreateFromCWPoints(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
		{
			Vector3 vector = p2 - p1;
			Vector3 vector2 = p1 - p0;
			Rectangle3 result;
			result.Center = (p0 + p2) * 0.5f;
			result.Extents.x = Vector3ex.Normalize(ref vector, 1E-05f) * 0.5f;
			result.Extents.y = Vector3ex.Normalize(ref vector2, 1E-05f) * 0.5f;
			result.Axis0 = vector;
			result.Axis1 = vector2;
			result.Normal = vector.Cross(vector2);
			return result;
		}

		public void CalcVertices(out Vector3 vertex0, out Vector3 vertex1, out Vector3 vertex2, out Vector3 vertex3)
		{
			Vector3 b = this.Axis0 * this.Extents.x;
			Vector3 b2 = this.Axis1 * this.Extents.y;
			vertex0 = this.Center - b - b2;
			vertex1 = this.Center + b - b2;
			vertex2 = this.Center + b + b2;
			vertex3 = this.Center - b + b2;
		}

		public Vector3[] CalcVertices()
		{
			Vector3 b = this.Axis0 * this.Extents.x;
			Vector3 b2 = this.Axis1 * this.Extents.y;
			return new Vector3[]
			{
				this.Center - b - b2,
				this.Center + b - b2,
				this.Center + b + b2,
				this.Center - b + b2
			};
		}

		public void CalcVertices(Vector3[] array)
		{
			Vector3 b = this.Axis0 * this.Extents.x;
			Vector3 b2 = this.Axis1 * this.Extents.y;
			array[0] = this.Center - b - b2;
			array[1] = this.Center + b - b2;
			array[2] = this.Center + b + b2;
			array[3] = this.Center - b + b2;
		}

		public float CalcArea()
		{
			return 4f * this.Extents.x * this.Extents.y;
		}

		public float DistanceTo(Vector3 point)
		{
			return Distance.Point3Rectangle3(ref point, ref this);
		}

		public Vector3 Project(Vector3 point)
		{
			Vector3 result;
			Distance.SqrPoint3Rectangle3(ref point, ref this, out result);
			return result;
		}

		public override string ToString()
		{
			return string.Format("[Center: {0}  Axis0: {1} Axis1: {2} Normal: {3} Extents: {4}]", new object[]
			{
				this.Center.ToStringEx(),
				this.Axis0.ToStringEx(),
				this.Axis1.ToStringEx(),
				this.Normal.ToStringEx(),
				this.Extents.ToStringEx()
			});
		}
	}
}

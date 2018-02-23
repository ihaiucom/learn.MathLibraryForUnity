using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Ray3
	{
		public Vector3 Center;

		public Vector3 Direction;

		public Ray3(ref Vector3 center, ref Vector3 direction)
		{
			this.Center = center;
			this.Direction = direction;
		}

		public Ray3(Vector3 center, Vector3 direction)
		{
			this.Center = center;
			this.Direction = direction;
		}

		public static implicit operator Ray(Ray3 value)
		{
			return new Ray(value.Center, value.Direction);
		}

		public static implicit operator Ray3(Ray value)
		{
			return new Ray3(value.origin, value.direction);
		}

		public Vector3 Eval(float t)
		{
			return this.Center + this.Direction * t;
		}

		public float DistanceTo(Vector3 point)
		{
			return Distance.Point3Ray3(ref point, ref this);
		}

		public Vector3 Project(Vector3 point)
		{
			Vector3 result;
			Distance.SqrPoint3Ray3(ref point, ref this, out result);
			return result;
		}

		public override string ToString()
		{
			return string.Format("[Origin: {0} Direction: {1}]", this.Center.ToStringEx(), this.Direction.ToStringEx());
		}
	}
}

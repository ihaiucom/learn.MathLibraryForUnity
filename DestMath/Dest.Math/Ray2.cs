using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Ray2
	{
		public Vector2 Center;

		public Vector2 Direction;

		public Ray2(ref Vector2 center, ref Vector2 direction)
		{
			this.Center = center;
			this.Direction = direction;
		}

		public Ray2(Vector2 center, Vector2 direction)
		{
			this.Center = center;
			this.Direction = direction;
		}

		public Vector2 Eval(float t)
		{
			return this.Center + this.Direction * t;
		}

		public float DistanceTo(Vector2 point)
		{
			return Distance.Point2Ray2(ref point, ref this);
		}

		public Vector2 Project(Vector2 point)
		{
			Vector2 result;
			Distance.SqrPoint2Ray2(ref point, ref this, out result);
			return result;
		}

		public override string ToString()
		{
			return string.Format("[Origin: {0} Direction: {1}]", this.Center.ToStringEx(), this.Direction.ToStringEx());
		}
	}
}

using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Segment2
	{
		public Vector2 P0;

		public Vector2 P1;

		public Vector2 Center;

		public Vector2 Direction;

		public float Extent;

		public Segment2(ref Vector2 p0, ref Vector2 p1)
		{
			this.P0 = p0;
			this.P1 = p1;
			this.Center = (this.Direction = Vector2.zero);
			this.Extent = 0f;
			this.CalcCenterDirectionExtent();
		}

		public Segment2(Vector2 p0, Vector2 p1)
		{
			this.P0 = p0;
			this.P1 = p1;
			this.Center = (this.Direction = Vector2.zero);
			this.Extent = 0f;
			this.CalcCenterDirectionExtent();
		}

		public Segment2(ref Vector2 center, ref Vector2 direction, float extent)
		{
			this.Center = center;
			this.Direction = direction;
			this.Extent = extent;
			this.P0 = (this.P1 = Vector2.zero);
			this.CalcEndPoints();
		}

		public Segment2(Vector2 center, Vector2 direction, float extent)
		{
			this.Center = center;
			this.Direction = direction;
			this.Extent = extent;
			this.P0 = (this.P1 = Vector2.zero);
			this.CalcEndPoints();
		}

		public void SetEndpoints(Vector2 p0, Vector2 p1)
		{
			this.P0 = p0;
			this.P1 = p1;
			this.CalcCenterDirectionExtent();
		}

		public void SetCenterDirectionExtent(Vector2 center, Vector2 direction, float extent)
		{
			this.Center = center;
			this.Direction = direction;
			this.Extent = extent;
			this.CalcEndPoints();
		}

		public void CalcCenterDirectionExtent()
		{
			this.Center = 0.5f * (this.P0 + this.P1);
			this.Direction = this.P1 - this.P0;
			float magnitude = this.Direction.magnitude;
			float d = 1f / magnitude;
			this.Direction *= d;
			this.Extent = 0.5f * magnitude;
		}

		public void CalcEndPoints()
		{
			this.P0 = this.Center - this.Extent * this.Direction;
			this.P1 = this.Center + this.Extent * this.Direction;
		}

		public Vector2 Eval(float s)
		{
			return (1f - s) * this.P0 + s * this.P1;
		}

		public float DistanceTo(Vector2 point)
		{
			return Distance.Point2Segment2(ref point, ref this);
		}

		public Vector2 Project(Vector2 point)
		{
			Vector2 result;
			Distance.SqrPoint2Segment2(ref point, ref this, out result);
			return result;
		}

		public override string ToString()
		{
			return string.Format("[P0: {0} P1: {1} Center: {2} Direction: {3} Extent: {4}]", new object[]
			{
				this.P0.ToStringEx(),
				this.P1.ToStringEx(),
				this.Center.ToStringEx(),
				this.Direction.ToStringEx(),
				this.Extent.ToString()
			});
		}
	}
}

using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Segment3
	{
		public Vector3 P0;

		public Vector3 P1;

		public Vector3 Center;

		public Vector3 Direction;

		public float Extent;

		public Segment3(ref Vector3 p0, ref Vector3 p1)
		{
			this.P0 = p0;
			this.P1 = p1;
			this.Center = (this.Direction = Vector3.zero);
			this.Extent = 0f;
			this.CalcCenterDirectionExtent();
		}

		public Segment3(Vector3 p0, Vector3 p1)
		{
			this.P0 = p0;
			this.P1 = p1;
			this.Center = (this.Direction = Vector3.zero);
			this.Extent = 0f;
			this.CalcCenterDirectionExtent();
		}

		public Segment3(ref Vector3 center, ref Vector3 direction, float extent)
		{
			this.Center = center;
			this.Direction = direction;
			this.Extent = extent;
			this.P0 = (this.P1 = Vector3.zero);
			this.CalcEndPoints();
		}

		public Segment3(Vector3 center, Vector3 direction, float extent)
		{
			this.Center = center;
			this.Direction = direction;
			this.Extent = extent;
			this.P0 = (this.P1 = Vector3.zero);
			this.CalcEndPoints();
		}

		public void SetEndpoints(Vector3 p0, Vector3 p1)
		{
			this.P0 = p0;
			this.P1 = p1;
			this.CalcCenterDirectionExtent();
		}

		public void SetCenterDirectionExtent(Vector3 center, Vector3 direction, float extent)
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

		public Vector3 Eval(float s)
		{
			return (1f - s) * this.P0 + s * this.P1;
		}

		public float DistanceTo(Vector3 point)
		{
			return Distance.Point3Segment3(ref point, ref this);
		}

		public Vector3 Project(Vector3 point)
		{
			Vector3 result;
			Distance.SqrPoint3Segment3(ref point, ref this, out result);
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

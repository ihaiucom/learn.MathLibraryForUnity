using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Line3
	{
		public Vector3 Center;

		public Vector3 Direction;

		public Line3(ref Vector3 center, ref Vector3 direction)
		{
			this.Center = center;
			this.Direction = direction;
		}

		public Line3(Vector3 center, Vector3 direction)
		{
			this.Center = center;
			this.Direction = direction;
		}

		public static Line3 CreateFromTwoPoints(ref Vector3 p0, ref Vector3 p1)
		{
			Line3 result;
			result.Center = p0;
			result.Direction = (p1 - p0).normalized;
			return result;
		}

		public static Line3 CreateFromTwoPoints(Vector3 p0, Vector3 p1)
		{
			Line3 result;
			result.Center = p0;
			result.Direction = (p1 - p0).normalized;
			return result;
		}

		public Vector3 Eval(float t)
		{
			return this.Center + this.Direction * t;
		}

		public float DistanceTo(Vector3 point)
		{
			return Distance.Point3Line3(ref point, ref this);
		}

		public Vector3 Project(Vector3 point)
		{
			Vector3 result;
			Distance.SqrPoint3Line3(ref point, ref this, out result);
			return result;
		}

		public float AngleBetweenTwoLines(Line3 anotherLine, bool acuteAngleDesired = false)
		{
			float num = Mathf.Acos(this.Direction.Dot(anotherLine.Direction));
			if (acuteAngleDesired && num > 1.57079637f)
			{
				return 3.14159274f - num;
			}
			return num;
		}

		public override string ToString()
		{
			return string.Format("[Origin: {0} Direction: {1}]", this.Center.ToStringEx(), this.Direction.ToStringEx());
		}
	}
}

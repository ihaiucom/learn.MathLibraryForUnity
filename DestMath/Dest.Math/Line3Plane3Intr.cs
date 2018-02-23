using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Line3Plane3Intr
	{
		public IntersectionTypes IntersectionType;

		public Vector3 Point;

		public float LineParameter;
	}
}

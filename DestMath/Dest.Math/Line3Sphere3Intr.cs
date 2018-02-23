using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Line3Sphere3Intr
	{
		public IntersectionTypes IntersectionType;

		public int Quantity;

		public Vector3 Point0;

		public Vector3 Point1;

		public float LineParameter0;

		public float LineParameter1;
	}
}

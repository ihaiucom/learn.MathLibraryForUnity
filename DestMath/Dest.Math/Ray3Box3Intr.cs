using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Ray3Box3Intr
	{
		public IntersectionTypes IntersectionType;

		public int Quantity;

		public Vector3 Point0;

		public Vector3 Point1;
	}
}

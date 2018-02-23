using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Line3Triangle3Intr
	{
		public IntersectionTypes IntersectionType;

		public Vector3 Point;

		public float LineParameter;

		public float TriBary0;

		public float TriBary1;

		public float TriBary2;
	}
}

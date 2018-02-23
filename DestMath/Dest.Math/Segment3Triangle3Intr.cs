using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Segment3Triangle3Intr
	{
		public IntersectionTypes IntersectionType;

		public Vector3 Point;

		public float SegmentParameter;

		public float TriBary0;

		public float TriBary1;

		public float TriBary2;
	}
}

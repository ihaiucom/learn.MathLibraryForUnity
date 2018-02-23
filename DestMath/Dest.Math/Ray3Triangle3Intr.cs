using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Ray3Triangle3Intr
	{
		public IntersectionTypes IntersectionType;

		public Vector3 Point;

		public float RayParameter;

		public float TriBary0;

		public float TriBary1;

		public float TriBary2;
	}
}

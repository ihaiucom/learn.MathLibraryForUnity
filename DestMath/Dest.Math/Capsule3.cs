using System;

namespace Dest.Math
{
	public struct Capsule3
	{
		public Segment3 Segment;

		public float Radius;

		public Capsule3(ref Segment3 segment, float radius)
		{
			this.Segment = segment;
			this.Radius = radius;
		}

		public Capsule3(Segment3 segment, float radius)
		{
			this.Segment = segment;
			this.Radius = radius;
		}
	}
}

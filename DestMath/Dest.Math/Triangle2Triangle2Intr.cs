using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Triangle2Triangle2Intr
	{
		public IntersectionTypes IntersectionType;

		public int Quantity;

		public Vector2 Point0;

		public Vector2 Point1;

		public Vector2 Point2;

		public Vector2 Point3;

		public Vector2 Point4;

		public Vector2 Point5;

		public Vector2 this[int i]
		{
			get
			{
				switch (i)
				{
				case 0:
					return this.Point0;
				case 1:
					return this.Point1;
				case 2:
					return this.Point2;
				case 3:
					return this.Point3;
				case 4:
					return this.Point4;
				case 5:
					return this.Point5;
				default:
					return Vector2.zero;
				}
			}
			internal set
			{
				switch (i)
				{
				case 0:
					this.Point0 = value;
					return;
				case 1:
					this.Point1 = value;
					return;
				case 2:
					this.Point2 = value;
					return;
				case 3:
					this.Point3 = value;
					return;
				case 4:
					this.Point4 = value;
					return;
				case 5:
					this.Point5 = value;
					return;
				default:
					return;
				}
			}
		}
	}
}

using System;

namespace Dest.Math
{
	public struct QuadraticRoots
	{
		public float X0;

		public float X1;

		public int RootCount;

		public float this[int rootIndex]
		{
			get
			{
				switch (rootIndex)
				{
				case 0:
					return this.X0;
				case 1:
					return this.X1;
				default:
					return float.NaN;
				}
			}
		}
	}
}

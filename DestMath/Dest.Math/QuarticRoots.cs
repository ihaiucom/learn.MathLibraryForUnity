using System;

namespace Dest.Math
{
	public struct QuarticRoots
	{
		public float X0;

		public float X1;

		public float X2;

		public float X3;

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
				case 2:
					return this.X2;
				case 3:
					return this.X3;
				default:
					return float.NaN;
				}
			}
		}
	}
}

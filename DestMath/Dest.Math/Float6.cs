using System;

namespace Dest.Math
{
	internal struct Float6
	{
		private float _0;

		private float _1;

		private float _2;

		private float _3;

		private float _4;

		private float _5;

		public float this[int i]
		{
			get
			{
				switch (i)
				{
				case 0:
					return this._0;
				case 1:
					return this._1;
				case 2:
					return this._2;
				case 3:
					return this._3;
				case 4:
					return this._4;
				case 5:
					return this._5;
				default:
					return 0f;
				}
			}
			set
			{
				switch (i)
				{
				case 0:
					this._0 = value;
					return;
				case 1:
					this._1 = value;
					return;
				case 2:
					this._2 = value;
					return;
				case 3:
					this._3 = value;
					return;
				case 4:
					this._4 = value;
					return;
				case 5:
					this._5 = value;
					return;
				default:
					return;
				}
			}
		}
	}
}

using System;

namespace Dest.Math
{
	public class OdeMidpoint : OdeSolver
	{
		private float _halfStep;

		private float[] _yTemp;

		public override float Step
		{
			get
			{
				return base.Step;
			}
			set
			{
				this._step = value;
				this._halfStep = this._step * 0.5f;
			}
		}

		public OdeMidpoint(int dim, float step, OdeFunction function) : base(dim, step, function)
		{
			this._halfStep = this._step * 0.5f;
			this._yTemp = new float[this._dim];
		}

		public override void Update(float tIn, float[] yIn, ref float tOut, float[] yOut)
		{
			this._function(tIn, yIn, this._FValue);
			for (int i = 0; i < this._dim; i++)
			{
				this._yTemp[i] = yIn[i] + this._halfStep * this._FValue[i];
			}
			float t = tIn + this._halfStep;
			this._function(t, this._yTemp, this._FValue);
			for (int i = 0; i < this._dim; i++)
			{
				yOut[i] = yIn[i] + this._step * this._FValue[i];
			}
			tOut = tIn + this._step;
		}
	}
}

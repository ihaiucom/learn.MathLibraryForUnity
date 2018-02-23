using System;

namespace Dest.Math
{
	public class OdeRungeKutta4 : OdeSolver
	{
		private float _halfStep;

		private float _sixthStep;

		private float[] _temp1;

		private float[] _temp2;

		private float[] _temp3;

		private float[] _temp4;

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
				this._sixthStep = this._step / 6f;
			}
		}

		public OdeRungeKutta4(int dim, float step, OdeFunction function) : base(dim, step, function)
		{
			this._halfStep = 0.5f * step;
			this._sixthStep = step / 6f;
			this._temp1 = new float[this._dim];
			this._temp2 = new float[this._dim];
			this._temp3 = new float[this._dim];
			this._temp4 = new float[this._dim];
			this._yTemp = new float[this._dim];
		}

		public override void Update(float tIn, float[] yIn, ref float tOut, float[] yOut)
		{
			this._function(tIn, yIn, this._temp1);
			for (int i = 0; i < this._dim; i++)
			{
				this._yTemp[i] = yIn[i] + this._halfStep * this._temp1[i];
			}
			float t = tIn + this._halfStep;
			this._function(t, this._yTemp, this._temp2);
			for (int i = 0; i < this._dim; i++)
			{
				this._yTemp[i] = yIn[i] + this._halfStep * this._temp2[i];
			}
			this._function(t, this._yTemp, this._temp3);
			for (int i = 0; i < this._dim; i++)
			{
				this._yTemp[i] = yIn[i] + this._step * this._temp3[i];
			}
			tOut = tIn + this._step;
			this._function(tOut, this._yTemp, this._temp4);
			for (int i = 0; i < this._dim; i++)
			{
				yOut[i] = yIn[i] + this._sixthStep * (this._temp1[i] + 2f * (this._temp2[i] + this._temp3[i]) + this._temp4[i]);
			}
		}
	}
}

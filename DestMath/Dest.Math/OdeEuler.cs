using System;

namespace Dest.Math
{
	public class OdeEuler : OdeSolver
	{
		public OdeEuler(int dim, float step, OdeFunction function) : base(dim, step, function)
		{
		}

		public override void Update(float tIn, float[] yIn, ref float tOut, float[] yOut)
		{
			this._function(tIn, yIn, this._FValue);
			for (int i = 0; i < this._dim; i++)
			{
				yOut[i] = yIn[i] + this._step * this._FValue[i];
			}
			tOut = tIn + this._step;
		}
	}
}

using System;

namespace Dest.Math
{
	public abstract class OdeSolver
	{
		protected int _dim;

		protected float _step;

		protected OdeFunction _function;

		protected float[] _FValue;

		public virtual float Step
		{
			get
			{
				return this._step;
			}
			set
			{
				this._step = value;
			}
		}

		public OdeSolver(int dim, float step, OdeFunction function)
		{
			this._dim = dim;
			this._step = step;
			this._function = function;
			this._FValue = new float[this._dim];
		}

		public abstract void Update(float tIn, float[] yIn, ref float tOut, float[] yOut);
	}
}

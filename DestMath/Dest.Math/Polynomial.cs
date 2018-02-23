using System;
using UnityEngine;

namespace Dest.Math
{
	public class Polynomial
	{
		private int _degree;

		private float[] _coeffs;

		public int Degree
		{
			get
			{
				return this._degree;
			}
			set
			{
				this._degree = value;
				this._coeffs = new float[this._degree + 1];
			}
		}

		public float this[int index]
		{
			get
			{
				return this._coeffs[index];
			}
			set
			{
				this._coeffs[index] = value;
			}
		}

		public Polynomial(int degree)
		{
			this.Degree = degree;
		}

		public Polynomial DeepCopy()
		{
			Polynomial polynomial = new Polynomial(this._degree);
			for (int i = 0; i <= this._degree; i++)
			{
				polynomial._coeffs[i] = this._coeffs[i];
			}
			return polynomial;
		}

		public Polynomial CalcDerivative()
		{
			if (this._degree > 0)
			{
				Polynomial polynomial = new Polynomial(this._degree - 1);
				int i = 0;
				int num = 1;
				while (i < this._degree)
				{
					polynomial._coeffs[i] = (float)num * this._coeffs[num];
					i++;
					num++;
				}
				return polynomial;
			}
			Polynomial polynomial2 = new Polynomial(0);
			polynomial2._coeffs[0] = 0f;
			return polynomial2;
		}

		public Polynomial CalcInversion()
		{
			Polynomial polynomial = new Polynomial(this._degree);
			for (int i = 0; i <= this._degree; i++)
			{
				polynomial._coeffs[i] = this._coeffs[this._degree - i];
			}
			return polynomial;
		}

		public void Compress(float epsilon = 1E-05f)
		{
			int num = this._degree;
			int num2 = num;
			while (num2 >= 0 && Mathf.Abs(this._coeffs[num2]) <= epsilon)
			{
				num--;
				num2--;
			}
			if (num >= 0)
			{
				this._degree = num;
				float num3 = 1f / this._coeffs[this._degree];
				this._coeffs[this._degree] = 1f;
				for (int i = 0; i < this._degree; i++)
				{
					this._coeffs[i] *= num3;
				}
			}
		}

		public float Eval(float t)
		{
			float num = this._coeffs[this._degree];
			for (int i = this._degree - 1; i >= 0; i--)
			{
				num *= t;
				num += this._coeffs[i];
			}
			return num;
		}
	}
}

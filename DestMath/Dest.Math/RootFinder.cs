using System;
using UnityEngine;

namespace Dest.Math
{
	public static class RootFinder
	{
		private class PolyRootFinder
		{
			private int _count;

			private int _maxRoot;

			private float[] _roots;

			private float _epsilon;

			public float[] Roots
			{
				get
				{
					return this._roots;
				}
			}

			public PolyRootFinder(float epsilon)
			{
				this._count = 0;
				this._maxRoot = 4;
				this._roots = new float[this._maxRoot];
				this._epsilon = epsilon;
			}

			public bool Bisection(Polynomial poly, float xMin, float xMax, int digits, out float root)
			{
				float num = poly.Eval(xMin);
				if (Mathf.Abs(num) <= 1E-05f)
				{
					root = xMin;
					return true;
				}
				float num2 = poly.Eval(xMax);
				if (Mathf.Abs(num2) <= 1E-05f)
				{
					root = xMax;
					return true;
				}
				root = float.NaN;
				if (num * num2 > 0f)
				{
					return false;
				}
				float num3 = Mathf.Log(xMax - xMin);
				float num4 = (float)digits * Mathf.Log(10f);
				float num5 = (num3 + num4) / Mathf.Log(2f);
				int num6 = (int)(num5 + 0.5f);
				for (int i = 0; i < num6; i++)
				{
					root = 0.5f * (xMin + xMax);
					float num7 = poly.Eval(root);
					float num8 = num7 * num;
					if (num8 < 0f)
					{
						xMax = root;
					}
					else
					{
						if (num8 <= 0f)
						{
							break;
						}
						xMin = root;
						num = num7;
					}
				}
				return true;
			}

			public bool Find(Polynomial poly, float xMin, float xMax, int digits)
			{
				if (poly.Degree > this._maxRoot)
				{
					this._maxRoot = poly.Degree;
					this._roots = new float[this._maxRoot];
				}
				float num2;
				if (poly.Degree != 1)
				{
					Polynomial poly2 = poly.CalcDerivative();
					this.Find(poly2, xMin, xMax, digits);
					int num = 0;
					float[] array = new float[this._count + 1];
					if (this._count > 0)
					{
						if (this.Bisection(poly, xMin, this._roots[0], digits, out num2))
						{
							array[num++] = num2;
						}
						for (int i = 0; i <= this._count - 2; i++)
						{
							if (this.Bisection(poly, this._roots[i], this._roots[i + 1], digits, out num2))
							{
								array[num++] = num2;
							}
						}
						if (this.Bisection(poly, this._roots[this._count - 1], xMax, digits, out num2))
						{
							array[num++] = num2;
						}
					}
					else if (this.Bisection(poly, xMin, xMax, digits, out num2))
					{
						array[num++] = num2;
					}
					if (num > 0)
					{
						this._count = 1;
						this._roots[0] = array[0];
						for (int i = 1; i < num; i++)
						{
							float f = array[i] - array[i - 1];
							if (Mathf.Abs(f) > this._epsilon)
							{
								this._roots[this._count++] = array[i];
							}
						}
					}
					else
					{
						this._count = 0;
					}
					return this._count > 0;
				}
				if (this.Bisection(poly, xMin, xMax, digits, out num2) && num2 != float.NaN)
				{
					this._count = 1;
					this._roots[0] = num2;
					return true;
				}
				this._count = 0;
				return false;
			}
		}

		private const float third = 0.333333343f;

		private const float twentySeventh = 0.0370370373f;

		private static float sqrt3 = Mathf.Sqrt(3f);

		public static bool BrentsMethod(Func<float, float> function, float x0, float x1, out BrentsRoot root, int maxIterations = 128, float negativeTolerance = -1E-05f, float positiveTolerance = 1E-05f, float stepTolerance = 1E-05f, float segmentTolerance = 1E-05f)
		{
			root.Iterations = 0;
			root.ExceededMaxIterations = false;
			if (x1 <= x0)
			{
				root.X = float.NaN;
				return false;
			}
			float num = function(x0);
			if (negativeTolerance <= num && num <= positiveTolerance)
			{
				root.X = x0;
				return true;
			}
			float num2 = function(x1);
			if (negativeTolerance <= num2 && num2 <= positiveTolerance)
			{
				root.X = x1;
				return true;
			}
			if (num * num2 >= 0f)
			{
				root.X = float.NaN;
				return false;
			}
			if (Mathf.Abs(num) < Mathf.Abs(num2))
			{
				float num3 = x0;
				x0 = x1;
				x1 = num3;
				num3 = num;
				num = num2;
				num2 = num3;
			}
			float num4 = x0;
			float num5 = x0;
			float num6 = num;
			bool flag = true;
			int i;
			for (i = 0; i < maxIterations; i++)
			{
				float num7 = num - num2;
				float num8 = num - num6;
				float num9 = num2 - num6;
				float num10 = 1f / num7;
				float num13;
				if (num8 != 0f && num9 != 0f)
				{
					float num11 = 1f / num8;
					float num12 = 1f / num9;
					num13 = x0 * num2 * num6 * num10 * num11 - x1 * num * num6 * num10 * num12 + num4 * num * num2 * num11 * num12;
				}
				else
				{
					num13 = (x1 * num - x0 * num2) * num10;
				}
				float num14 = num13 - 0.75f * x0 - 0.25f * x1;
				float num15 = num13 - x1;
				float num16 = Mathf.Abs(num15);
				float num17 = Mathf.Abs(x1 - num4);
				float num18 = Mathf.Abs(num4 - num5);
				bool flag2;
				if (num14 * num15 > 0f)
				{
					flag2 = true;
				}
				else if (flag)
				{
					flag2 = (num16 >= 0.5f * num17 || num17 <= stepTolerance);
				}
				else
				{
					flag2 = (num16 >= 0.5f * num18 || num18 <= stepTolerance);
				}
				if (flag2)
				{
					num13 = 0.5f * (x0 + x1);
					flag = true;
				}
				else
				{
					flag = false;
				}
				float num19 = function(num13);
				if (negativeTolerance <= num19 && num19 <= positiveTolerance)
				{
					root.X = num13;
					root.Iterations = i;
					return true;
				}
				num5 = num4;
				num4 = x1;
				num6 = num2;
				if (num * num19 < 0f)
				{
					x1 = num13;
					num2 = num19;
				}
				else
				{
					x0 = num13;
					num = num19;
				}
				if (Mathf.Abs(x1 - x0) <= segmentTolerance)
				{
					root.X = x1;
					root.Iterations = i;
					return true;
				}
				if (Mathf.Abs(num) < Mathf.Abs(num2))
				{
					float num20 = x0;
					x0 = x1;
					x1 = num20;
					num20 = num;
					num = num2;
					num2 = num20;
				}
			}
			root.X = x1;
			root.Iterations = i;
			root.ExceededMaxIterations = true;
			return true;
		}

		public static bool Linear(float c0, float c1, out float root, float epsilon = 1E-05f)
		{
			if (Mathf.Abs(c1) >= epsilon)
			{
				root = -c0 / c1;
				return true;
			}
			root = float.NaN;
			return false;
		}

		public static bool Quadratic(float c0, float c1, float c2, out QuadraticRoots roots, float epsilon = 1E-05f)
		{
			if (Mathf.Abs(c2) <= epsilon)
			{
				float x;
				bool flag = RootFinder.Linear(c0, c1, out x, epsilon);
				if (flag)
				{
					roots.X0 = x;
					roots.X1 = float.NaN;
					roots.RootCount = 1;
				}
				else
				{
					roots.X0 = float.NaN;
					roots.X1 = float.NaN;
					roots.RootCount = 0;
				}
				return flag;
			}
			float num = c1 * c1 - 4f * c0 * c2;
			if (Mathf.Abs(num) <= epsilon)
			{
				num = 0f;
			}
			if (num < 0f)
			{
				roots.X0 = float.NaN;
				roots.X1 = float.NaN;
				roots.RootCount = 0;
				return false;
			}
			float num2 = 0.5f / c2;
			if (num > 0f)
			{
				num = Mathf.Sqrt(num);
				roots.X0 = num2 * (-c1 - num);
				roots.X1 = num2 * (-c1 + num);
				roots.RootCount = 2;
			}
			else
			{
				roots.X0 = -num2 * c1;
				roots.X1 = float.NaN;
				roots.RootCount = 1;
			}
			return true;
		}

		public static bool Cubic(float c0, float c1, float c2, float c3, out CubicRoots roots, float epsilon = 1E-05f)
		{
			if (Mathf.Abs(c3) <= epsilon)
			{
				QuadraticRoots quadraticRoots;
				bool flag = RootFinder.Quadratic(c0, c1, c2, out quadraticRoots, epsilon);
				if (flag)
				{
					roots.X0 = quadraticRoots.X0;
					roots.X1 = quadraticRoots.X1;
					roots.X2 = float.NaN;
					roots.RootCount = quadraticRoots.RootCount;
				}
				else
				{
					roots.X0 = float.NaN;
					roots.X1 = float.NaN;
					roots.X2 = float.NaN;
					roots.RootCount = 0;
				}
				return flag;
			}
			float num = 1f / c3;
			c2 *= num;
			c1 *= num;
			c0 *= num;
			float num2 = 0.333333343f * c2;
			float num3 = c1 - c2 * num2;
			float num4 = c0 + c2 * (2f * c2 * c2 - 9f * c1) * 0.0370370373f;
			float num5 = 0.5f * num4;
			float num6 = num5 * num5 + num3 * num3 * num3 * 0.0370370373f;
			if (Mathf.Abs(num6) <= epsilon)
			{
				num6 = 0f;
			}
			if (num6 > 0f)
			{
				num6 = Mathf.Sqrt(num6);
				float num7 = -num5 + num6;
				if (num7 >= 0f)
				{
					roots.X0 = Mathf.Pow(num7, 0.333333343f);
				}
				else
				{
					roots.X0 = -Mathf.Pow(-num7, 0.333333343f);
				}
				num7 = -num5 - num6;
				if (num7 >= 0f)
				{
					roots.X0 += Mathf.Pow(num7, 0.333333343f);
				}
				else
				{
					roots.X0 -= Mathf.Pow(-num7, 0.333333343f);
				}
				roots.X0 -= num2;
				roots.X1 = float.NaN;
				roots.X2 = float.NaN;
				roots.RootCount = 1;
			}
			else if (num6 < 0f)
			{
				float num8 = Mathf.Sqrt(-0.333333343f * num3);
				float f = 0.333333343f * Mathf.Atan2(Mathf.Sqrt(-num6), -num5);
				float num9 = Mathf.Cos(f);
				float num10 = Mathf.Sin(f);
				roots.X0 = 2f * num8 * num9 - num2;
				roots.X1 = -num8 * (num9 + RootFinder.sqrt3 * num10) - num2;
				roots.X2 = -num8 * (num9 - RootFinder.sqrt3 * num10) - num2;
				roots.RootCount = 3;
			}
			else
			{
				float num11;
				if (num5 >= 0f)
				{
					num11 = -Mathf.Pow(num5, 0.333333343f);
				}
				else
				{
					num11 = Mathf.Pow(-num5, 0.333333343f);
				}
				roots.X0 = 2f * num11 - num2;
				roots.X1 = -num11 - num2;
				roots.X2 = roots.X1;
				roots.RootCount = 3;
			}
			return true;
		}

		public static bool Quartic(float c0, float c1, float c2, float c3, float c4, out QuarticRoots roots, float epsilon = 1E-05f)
		{
			roots.X0 = float.NaN;
			roots.X1 = float.NaN;
			roots.X2 = float.NaN;
			roots.X3 = float.NaN;
			if (Mathf.Abs(c4) <= epsilon)
			{
				CubicRoots cubicRoots;
				bool flag = RootFinder.Cubic(c0, c1, c2, c3, out cubicRoots, epsilon);
				if (flag)
				{
					roots.X0 = cubicRoots.X0;
					roots.X1 = cubicRoots.X1;
					roots.X2 = cubicRoots.X2;
					roots.RootCount = cubicRoots.RootCount;
				}
				else
				{
					roots.RootCount = 0;
				}
				return flag;
			}
			float num = 1f / c4;
			c0 *= num;
			c1 *= num;
			c2 *= num;
			c3 *= num;
			float c5 = -c3 * c3 * c0 + 4f * c2 * c0 - c1 * c1;
			float c6 = c3 * c1 - 4f * c0;
			float c7 = -c2;
			CubicRoots cubicRoots2;
			RootFinder.Cubic(c5, c6, c7, 1f, out cubicRoots2, epsilon);
			float x = cubicRoots2.X0;
			roots.RootCount = 0;
			float num2 = 0.25f * c3 * c3 - c2 + x;
			if (Mathf.Abs(num2) <= epsilon)
			{
				num2 = 0f;
			}
			if (num2 > 0f)
			{
				float num3 = Mathf.Sqrt(num2);
				float num4 = 0.75f * c3 * c3 - num3 * num3 - 2f * c2;
				float num5 = (4f * c3 * c2 - 8f * c1 - c3 * c3 * c3) / (4f * num3);
				float num6 = num4 + num5;
				float num7 = num4 - num5;
				if (Mathf.Abs(num6) <= epsilon)
				{
					num6 = 0f;
				}
				if (Mathf.Abs(num7) <= epsilon)
				{
					num7 = 0f;
				}
				if (num6 >= 0f)
				{
					float num8 = Mathf.Sqrt(num6);
					roots.X0 = -0.25f * c3 + 0.5f * (num3 + num8);
					roots.X1 = -0.25f * c3 + 0.5f * (num3 - num8);
					roots.RootCount += 2;
				}
				if (num7 >= 0f)
				{
					float num9 = Mathf.Sqrt(num7);
					if (roots.RootCount == 0)
					{
						roots.X0 = -0.25f * c3 + 0.5f * (num9 - num3);
						roots.X1 = -0.25f * c3 - 0.5f * (num9 + num3);
					}
					else
					{
						roots.X2 = -0.25f * c3 + 0.5f * (num9 - num3);
						roots.X3 = -0.25f * c3 - 0.5f * (num9 + num3);
					}
					roots.RootCount += 2;
				}
			}
			else if (num2 < 0f)
			{
				roots.RootCount = 0;
			}
			else
			{
				float num10 = x * x - 4f * c0;
				if (num10 >= -epsilon)
				{
					if (num10 < 0f)
					{
						num10 = 0f;
					}
					num10 = 2f * Mathf.Sqrt(num10);
					float num11 = 0.75f * c3 * c3 - 2f * c2;
					float num12 = num11 + num10;
					if (num12 >= epsilon)
					{
						float num13 = Mathf.Sqrt(num12);
						roots.X0 = -0.25f * c3 + 0.5f * num13;
						roots.X1 = -0.25f * c3 - 0.5f * num13;
						roots.RootCount += 2;
					}
					float num14 = num11 - num10;
					if (num14 >= epsilon)
					{
						float num15 = Mathf.Sqrt(num14);
						if (roots.RootCount == 0)
						{
							roots.X0 = -0.25f * c3 + 0.5f * num15;
							roots.X1 = -0.25f * c3 - 0.5f * num15;
						}
						else
						{
							roots.X2 = -0.25f * c3 + 0.5f * num15;
							roots.X3 = -0.25f * c3 - 0.5f * num15;
						}
						roots.RootCount += 2;
					}
				}
			}
			return roots.RootCount > 0;
		}

		public static float PolynomialBound(Polynomial poly, float epsilon = 1E-05f)
		{
			Polynomial polynomial = poly.DeepCopy();
			polynomial.Compress(epsilon);
			int degree = polynomial.Degree;
			if (degree < 1)
			{
				return -1f;
			}
			float num = 1f / polynomial[degree];
			float num2 = 0f;
			for (int i = 0; i < degree; i++)
			{
				float num3 = Mathf.Abs(polynomial[i]) * num;
				if (num3 > num2)
				{
					num2 = num3;
				}
			}
			return 1f + num2;
		}

		public static bool Polynomial(Polynomial poly, float xMin, float xMax, out float[] roots, int digits = 6, float epsilon = 1E-05f)
		{
			RootFinder.PolyRootFinder polyRootFinder = new RootFinder.PolyRootFinder(epsilon);
			if (polyRootFinder.Find(poly, xMin, xMax, digits))
			{
				roots = polyRootFinder.Roots;
				return true;
			}
			roots = new float[0];
			return false;
		}

		public static bool Polynomial(Polynomial poly, out float[] roots, int digits = 6, float epsilon = 1E-05f)
		{
			float num = RootFinder.PolynomialBound(poly, 1E-05f);
			if (num == -1f)
			{
				roots = new float[0];
				return false;
			}
			return RootFinder.Polynomial(poly, -num, num, out roots, digits, epsilon);
		}
	}
}

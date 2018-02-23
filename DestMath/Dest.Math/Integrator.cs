using System;

namespace Dest.Math
{
	public static class Integrator
	{
		private const int _degree = 5;

		private static float[] root = new float[]
		{
			-0.906179845f,
			-0.5384693f,
			0f,
			0.5384693f,
			0.906179845f
		};

		private static float[] coeff = new float[]
		{
			0.236926883f,
			0.478628665f,
			0.5688889f,
			0.478628665f,
			0.236926883f
		};

		public static float TrapezoidRule(Func<float, float> function, float a, float b, int sampleCount)
		{
			if (sampleCount < 2)
			{
				return float.NaN;
			}
			float num = (b - a) / (float)(sampleCount - 1);
			float num2 = 0.5f * (function(a) + function(b));
			for (int i = 1; i <= sampleCount - 2; i++)
			{
				num2 += function(a + (float)i * num);
			}
			return num2 * num;
		}

		public static float RombergIntegral(Func<float, float> function, float a, float b, int order)
		{
			if (order <= 0)
			{
				return float.NaN;
			}
			float[,] array = new float[2, order];
			float num = b - a;
			array[0, 0] = 0.5f * num * (function(a) + function(b));
			int i = 2;
			int num2 = 1;
			while (i <= order)
			{
				float num3 = 0f;
				for (int j = 1; j <= num2; j++)
				{
					num3 += function(a + num * ((float)j - 0.5f));
				}
				array[1, 0] = 0.5f * (array[0, 0] + num * num3);
				int k = 1;
				int num4 = 4;
				while (k < i)
				{
					array[1, k] = ((float)num4 * array[1, k - 1] - array[0, k - 1]) / (float)(num4 - 1);
					k++;
					num4 *= 4;
				}
				for (int j = 0; j < i; j++)
				{
					array[0, j] = array[1, j];
				}
				i++;
				num2 *= 2;
				num *= 0.5f;
			}
			return array[0, order - 1];
		}

		public static float GaussianQuadrature(Func<float, float> function, float a, float b)
		{
			float num = 0.5f * (b - a);
			float num2 = 0.5f * (b + a);
			float num3 = 0f;
			for (int i = 0; i < 5; i++)
			{
				num3 += Integrator.coeff[i] * function(num * Integrator.root[i] + num2);
			}
			return num3 * num;
		}
	}
}

using System;
using UnityEngine;

namespace Dest.Math
{
	public static class EigenDecomposition
	{
		private static void Tridiagonal2(float[] diagonal, float[] subdiagonal, float[,] matrix, out bool isRotation)
		{
			diagonal[0] = matrix[0, 0];
			diagonal[1] = matrix[1, 1];
			subdiagonal[0] = matrix[0, 1];
			subdiagonal[1] = 0f;
			matrix[0, 0] = 1f;
			matrix[0, 1] = 0f;
			matrix[1, 0] = 0f;
			matrix[1, 1] = 1f;
			isRotation = true;
		}

		private static void Tridiagonal3(float[] diagonal, float[] subdiagonal, float[,] matrix, out bool isRotation)
		{
			float num = matrix[0, 0];
			float num2 = matrix[0, 1];
			float num3 = matrix[0, 2];
			float num4 = matrix[1, 1];
			float num5 = matrix[1, 2];
			float num6 = matrix[2, 2];
			diagonal[0] = num;
			subdiagonal[2] = 0f;
			if (Mathf.Abs(num3) > 1E-05f)
			{
				float num7 = Mathf.Sqrt(num2 * num2 + num3 * num3);
				float num8 = 1f / num7;
				num2 *= num8;
				num3 *= num8;
				float num9 = 2f * num2 * num5 + num3 * (num6 - num4);
				diagonal[1] = num4 + num3 * num9;
				diagonal[2] = num6 - num3 * num9;
				subdiagonal[0] = num7;
				subdiagonal[1] = num5 - num2 * num9;
				matrix[0, 0] = 1f;
				matrix[0, 1] = 0f;
				matrix[0, 2] = 0f;
				matrix[1, 0] = 0f;
				matrix[1, 1] = num2;
				matrix[1, 2] = num3;
				matrix[2, 0] = 0f;
				matrix[2, 1] = num3;
				matrix[2, 2] = -num2;
				isRotation = false;
				return;
			}
			diagonal[1] = num4;
			diagonal[2] = num6;
			subdiagonal[0] = num2;
			subdiagonal[1] = num5;
			matrix[0, 0] = 1f;
			matrix[0, 1] = 0f;
			matrix[0, 2] = 0f;
			matrix[1, 0] = 0f;
			matrix[1, 1] = 1f;
			matrix[1, 2] = 0f;
			matrix[2, 0] = 0f;
			matrix[2, 1] = 0f;
			matrix[2, 2] = 1f;
			isRotation = true;
		}

		private static void TridiagonalN(float[] diagonal, float[] subdiagonal, float[,] matrix, out bool isRotation)
		{
			int num = diagonal.Length;
			int i = num - 1;
			int num2 = num - 2;
			while (i >= 1)
			{
				float num3 = 0f;
				float num4 = 0f;
				if (num2 > 0)
				{
					for (int j = 0; j <= num2; j++)
					{
						num4 += Mathf.Abs(matrix[i, j]);
					}
					if (num4 == 0f)
					{
						subdiagonal[i] = matrix[i, num2];
					}
					else
					{
						float num5 = 1f / num4;
						for (int j = 0; j <= num2; j++)
						{
							matrix[i, j] *= num5;
							num3 += matrix[i, j] * matrix[i, j];
						}
						float num6 = matrix[i, num2];
						float num7 = Mathf.Sqrt(num3);
						if (num6 > 0f)
						{
							num7 = -num7;
						}
						subdiagonal[i] = num4 * num7;
						num3 -= num6 * num7;
						matrix[i, num2] = num6 - num7;
						num6 = 0f;
						float num8 = 1f / num3;
						for (int k = 0; k <= num2; k++)
						{
							matrix[k, i] = matrix[i, k] * num8;
							num7 = 0f;
							for (int j = 0; j <= k; j++)
							{
								num7 += matrix[k, j] * matrix[i, j];
							}
							for (int j = k + 1; j <= num2; j++)
							{
								num7 += matrix[j, k] * matrix[i, j];
							}
							subdiagonal[k] = num7 * num8;
							num6 += subdiagonal[k] * matrix[i, k];
						}
						float num9 = 0.5f * num6 * num8;
						for (int k = 0; k <= num2; k++)
						{
							num6 = matrix[i, k];
							num7 = subdiagonal[k] - num9 * num6;
							subdiagonal[k] = num7;
							for (int j = 0; j <= k; j++)
							{
								matrix[k, j] -= num6 * subdiagonal[j] + num7 * matrix[i, j];
							}
						}
					}
				}
				else
				{
					subdiagonal[i] = matrix[i, num2];
				}
				diagonal[i] = num3;
				i--;
				num2--;
			}
			diagonal[0] = 0f;
			subdiagonal[0] = 0f;
			i = 0;
			num2 = -1;
			while (i <= num - 1)
			{
				if (diagonal[i] != 0f)
				{
					for (int k = 0; k <= num2; k++)
					{
						float num10 = 0f;
						for (int j = 0; j <= num2; j++)
						{
							num10 += matrix[i, j] * matrix[j, k];
						}
						for (int j = 0; j <= num2; j++)
						{
							matrix[j, k] -= num10 * matrix[j, i];
						}
					}
				}
				diagonal[i] = matrix[i, i];
				matrix[i, i] = 1f;
				for (int k = 0; k <= num2; k++)
				{
					matrix[k, i] = 0f;
					matrix[i, k] = 0f;
				}
				i++;
				num2++;
			}
			i = 1;
			num2 = 0;
			while (i < num)
			{
				subdiagonal[num2] = subdiagonal[i];
				i++;
				num2++;
			}
			subdiagonal[num - 1] = 0f;
			isRotation = (num % 2 == 0);
		}

		private static bool QLAlgorithm(float[] diagonal, float[] subdiagonal, float[,] matrix)
		{
			int num = 32;
			int num2 = diagonal.Length;
			for (int i = 0; i < num2; i++)
			{
				int j;
				for (j = 0; j < num; j++)
				{
					int k;
					for (k = i; k <= num2 - 2; k++)
					{
						float num3 = Mathf.Abs(diagonal[k]) + Mathf.Abs(diagonal[k + 1]);
						if (Mathf.Abs(subdiagonal[k]) + num3 == num3)
						{
							break;
						}
					}
					if (k == i)
					{
						break;
					}
					float num4 = (diagonal[i + 1] - diagonal[i]) / (2f * subdiagonal[i]);
					float num5 = Mathf.Sqrt(num4 * num4 + 1f);
					if (num4 < 0f)
					{
						num4 = diagonal[k] - diagonal[i] + subdiagonal[i] / (num4 - num5);
					}
					else
					{
						num4 = diagonal[k] - diagonal[i] + subdiagonal[i] / (num4 + num5);
					}
					float num6 = 1f;
					float num7 = 1f;
					float num8 = 0f;
					for (int l = k - 1; l >= i; l--)
					{
						float num9 = num6 * subdiagonal[l];
						float num10 = num7 * subdiagonal[l];
						if (Mathf.Abs(num9) >= Mathf.Abs(num4))
						{
							num7 = num4 / num9;
							num5 = Mathf.Sqrt(num7 * num7 + 1f);
							subdiagonal[l + 1] = num9 * num5;
							num6 = 1f / num5;
							num7 *= num6;
						}
						else
						{
							num6 = num9 / num4;
							num5 = Mathf.Sqrt(num6 * num6 + 1f);
							subdiagonal[l + 1] = num4 * num5;
							num7 = 1f / num5;
							num6 *= num7;
						}
						num4 = diagonal[l + 1] - num8;
						num5 = (diagonal[l] - num4) * num6 + 2f * num10 * num7;
						num8 = num6 * num5;
						diagonal[l + 1] = num4 + num8;
						num4 = num7 * num5 - num10;
						for (int m = 0; m < num2; m++)
						{
							num9 = matrix[m, l + 1];
							matrix[m, l + 1] = num6 * matrix[m, l] + num7 * num9;
							matrix[m, l] = num7 * matrix[m, l] - num6 * num9;
						}
					}
					diagonal[i] -= num8;
					subdiagonal[i] = num4;
					subdiagonal[k] = 0f;
				}
				if (j == num)
				{
					return false;
				}
			}
			return true;
		}

		private static void IncreasingSort(float[] diagonal, float[] subdiagonal, float[,] matrix, ref bool isRotation)
		{
			int num = diagonal.Length;
			for (int i = 0; i <= num - 2; i++)
			{
				int num2 = i;
				float num3 = diagonal[num2];
				for (int j = i + 1; j < num; j++)
				{
					if (diagonal[j] < num3)
					{
						num2 = j;
						num3 = diagonal[num2];
					}
				}
				if (num2 != i)
				{
					diagonal[num2] = diagonal[i];
					diagonal[i] = num3;
					for (int j = 0; j < num; j++)
					{
						float num4 = matrix[j, i];
						matrix[j, i] = matrix[j, num2];
						matrix[j, num2] = num4;
						isRotation = !isRotation;
					}
				}
			}
		}

		private static void DecreasingSort(float[] diagonal, float[] subdiagonal, float[,] matrix, ref bool isRotation)
		{
			int num = diagonal.Length;
			for (int i = 0; i <= num - 2; i++)
			{
				int num2 = i;
				float num3 = diagonal[num2];
				for (int j = i + 1; j < num; j++)
				{
					if (diagonal[j] > num3)
					{
						num2 = j;
						num3 = diagonal[num2];
					}
				}
				if (num2 != i)
				{
					diagonal[num2] = diagonal[i];
					diagonal[i] = num3;
					for (int j = 0; j < num; j++)
					{
						float num4 = matrix[j, i];
						matrix[j, i] = matrix[j, num2];
						matrix[j, num2] = num4;
						isRotation = !isRotation;
					}
				}
			}
		}

		private static void GuaranteeRotation(float[,] matrix, bool isRotation)
		{
			if (!isRotation)
			{
				int length = matrix.GetLength(0);
				for (int i = 0; i < length; i++)
				{
					matrix[i, 0] = -matrix[i, 0];
				}
			}
		}

		public static EigenData Solve(float[,] symmetricSquareMatrix, bool increasingSort)
		{
			int length;
			if ((length = symmetricSquareMatrix.GetLength(0)) != symmetricSquareMatrix.GetLength(1))
			{
				return null;
			}
			if (length < 2)
			{
				return null;
			}
			float[,] array = new float[length, length];
			Buffer.BlockCopy(symmetricSquareMatrix, 0, array, 0, symmetricSquareMatrix.Length * 4);
			float[] diagonal = new float[length];
			float[] subdiagonal = new float[length];
			bool isRotation;
			if (length == 2)
			{
				EigenDecomposition.Tridiagonal2(diagonal, subdiagonal, array, out isRotation);
			}
			else if (length == 3)
			{
				EigenDecomposition.Tridiagonal3(diagonal, subdiagonal, array, out isRotation);
			}
			else
			{
				EigenDecomposition.TridiagonalN(diagonal, subdiagonal, array, out isRotation);
			}
			EigenDecomposition.QLAlgorithm(diagonal, subdiagonal, array);
			if (increasingSort)
			{
				EigenDecomposition.IncreasingSort(diagonal, subdiagonal, array, ref isRotation);
			}
			else
			{
				EigenDecomposition.DecreasingSort(diagonal, subdiagonal, array, ref isRotation);
			}
			EigenDecomposition.GuaranteeRotation(array, isRotation);
			return new EigenData(diagonal, array);
		}
	}
}

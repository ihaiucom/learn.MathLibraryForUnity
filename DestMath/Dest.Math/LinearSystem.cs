using System;
using UnityEngine;

namespace Dest.Math
{
	public static class LinearSystem
	{
		public static bool Solve2(float[,] A, float[] B, out float[] X, float zeroTolerance = 1E-05f)
		{
			float num = A[0, 0] * A[1, 1] - A[0, 1] * A[1, 0];
			if (Mathf.Abs(num) < zeroTolerance)
			{
				X = null;
				return false;
			}
			float num2 = 1f / num;
			X = new float[2];
			X[0] = (A[1, 1] * B[0] - A[0, 1] * B[1]) * num2;
			X[1] = (A[0, 0] * B[1] - A[1, 0] * B[0]) * num2;
			return true;
		}

		public static bool Solve2(float[,] A, float[] B, out Vector2 X, float zeroTolerance = 1E-05f)
		{
			float[] array;
			bool flag = LinearSystem.Solve2(A, B, out array, zeroTolerance);
			if (flag)
			{
				X.x = array[0];
				X.y = array[1];
			}
			else
			{
				X = Vector2ex.Zero;
			}
			return flag;
		}

		public static bool Solve3(float[,] A, float[] B, out float[] X, float zeroTolerance = 1E-05f)
		{
			float[,] array = new float[3, 3];
			array[0, 0] = A[1, 1] * A[2, 2] - A[1, 2] * A[2, 1];
			array[0, 1] = A[0, 2] * A[2, 1] - A[0, 1] * A[2, 2];
			array[0, 2] = A[0, 1] * A[1, 2] - A[0, 2] * A[1, 1];
			array[1, 0] = A[1, 2] * A[2, 0] - A[1, 0] * A[2, 2];
			array[1, 1] = A[0, 0] * A[2, 2] - A[0, 2] * A[2, 0];
			array[1, 2] = A[0, 2] * A[1, 0] - A[0, 0] * A[1, 2];
			array[2, 0] = A[1, 0] * A[2, 1] - A[1, 1] * A[2, 0];
			array[2, 1] = A[0, 1] * A[2, 0] - A[0, 0] * A[2, 1];
			array[2, 2] = A[0, 0] * A[1, 1] - A[0, 1] * A[1, 0];
			float[,] array2 = array;
			float num = A[0, 0] * array2[0, 0] + A[0, 1] * array2[1, 0] + A[0, 2] * array2[2, 0];
			if (Mathf.Abs(num) < zeroTolerance)
			{
				X = null;
				return false;
			}
			float num2 = 1f / num;
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					array2[i, j] *= num2;
				}
			}
			X = new float[3];
			X[0] = array2[0, 0] * B[0] + array2[0, 1] * B[1] + array2[0, 2] * B[2];
			X[1] = array2[1, 0] * B[0] + array2[1, 1] * B[1] + array2[1, 2] * B[2];
			X[2] = array2[2, 0] * B[0] + array2[2, 1] * B[1] + array2[2, 2] * B[2];
			return true;
		}

		public static bool Solve3(float[,] A, float[] B, out Vector3 X, float zeroTolerance = 1E-05f)
		{
			float[] array;
			bool flag = LinearSystem.Solve3(A, B, out array, zeroTolerance);
			if (flag)
			{
				X.x = array[0];
				X.y = array[1];
				X.z = array[2];
			}
			else
			{
				X = Vector3ex.Zero;
			}
			return flag;
		}

		private static void SwapRows(float[,] matrix, int row0, int row1, int columnCount)
		{
			if (row0 != row1)
			{
				for (int i = 0; i < columnCount; i++)
				{
					float num = matrix[row0, i];
					matrix[row0, i] = matrix[row1, i];
					matrix[row1, i] = num;
				}
			}
		}

		public static bool Solve(float[,] A, float[] B, out float[] X)
		{
			if (A.GetLength(0) != A.GetLength(1) || A.GetLength(0) != B.Length)
			{
				X = null;
				return false;
			}
			int length = A.GetLength(1);
			float[,] array = new float[A.GetLength(0), A.GetLength(1)];
			Buffer.BlockCopy(A, 0, array, 0, A.Length * 4);
			X = new float[length];
			Buffer.BlockCopy(B, 0, X, 0, length * 4);
			int[] array2 = new int[length];
			int[] array3 = new int[length];
			bool[] array4 = new bool[length];
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < length; i++)
			{
				float num3 = 0f;
				for (int j = 0; j < length; j++)
				{
					if (!array4[j])
					{
						for (int k = 0; k < length; k++)
						{
							if (!array4[k])
							{
								float num4 = Mathf.Abs(array[j, k]);
								if (num4 > num3)
								{
									num3 = num4;
									num = j;
									num2 = k;
								}
							}
						}
					}
				}
				if (num3 == 0f)
				{
					X = null;
					return false;
				}
				array4[num2] = true;
				if (num != num2)
				{
					LinearSystem.SwapRows(array, num, num2, length);
					float num5 = X[num];
					X[num] = X[num2];
					X[num2] = num5;
				}
				array3[i] = num;
				array2[i] = num2;
				float num6 = 1f / array[num2, num2];
				array[num2, num2] = 1f;
				for (int k = 0; k < length; k++)
				{
					array[num2, k] *= num6;
				}
				X[num2] *= num6;
				for (int j = 0; j < length; j++)
				{
					if (j != num2)
					{
						float num5 = array[j, num2];
						array[j, num2] = 0f;
						for (int k = 0; k < length; k++)
						{
							array[j, k] -= array[num2, k] * num5;
						}
						X[j] -= X[num2] * num5;
					}
				}
			}
			return true;
		}

		public static bool SolveTridiagonal(float[] A, float[] B, float[] C, float[] R, out float[] U)
		{
			int num = B.Length;
			if (B[0] == 0f)
			{
				U = null;
				return false;
			}
			float[] array = new float[num - 1];
			float num2 = B[0];
			float num3 = 1f / num2;
			U = new float[num];
			U[0] = R[0] * num3;
			int num4 = 0;
			for (int i = 1; i < num; i++)
			{
				array[num4] = C[num4] * num3;
				num2 = B[i] - A[num4] * array[num4];
				if (num2 == 0f)
				{
					U = null;
					return false;
				}
				num3 = 1f / num2;
				U[i] = (R[i] - A[num4] * U[num4]) * num3;
				num4++;
			}
			num4 = num - 1;
			for (int i = num - 2; i >= 0; i--)
			{
				U[i] -= array[i] * U[num4];
				num4--;
			}
			return true;
		}

		public static bool Inverse(float[,] A, out float[,] invA)
		{
			if (A.GetLength(0) != A.GetLength(1))
			{
				invA = null;
				return false;
			}
			int length = A.GetLength(0);
			invA = new float[length, length];
			Buffer.BlockCopy(A, 0, invA, 0, A.Length * 4);
			int[] array = new int[length];
			int[] array2 = new int[length];
			bool[] array3 = new bool[length];
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < length; i++)
			{
				float num3 = 0f;
				for (int j = 0; j < length; j++)
				{
					if (!array3[j])
					{
						for (int k = 0; k < length; k++)
						{
							if (!array3[k])
							{
								float num4 = Mathf.Abs(invA[j, k]);
								if (num4 > num3)
								{
									num3 = num4;
									num = j;
									num2 = k;
								}
							}
						}
					}
				}
				if (num3 == 0f)
				{
					invA = null;
					return false;
				}
				array3[num2] = true;
				if (num != num2)
				{
					LinearSystem.SwapRows(invA, num, num2, length);
				}
				array2[i] = num;
				array[i] = num2;
				float num5 = 1f / invA[num2, num2];
				invA[num2, num2] = 1f;
				for (int k = 0; k < length; k++)
				{
					invA[num2, k] *= num5;
				}
				for (int j = 0; j < length; j++)
				{
					if (j != num2)
					{
						float num6 = invA[j, num2];
						invA[j, num2] = 0f;
						for (int k = 0; k < length; k++)
						{
							invA[j, k] -= invA[num2, k] * num6;
						}
					}
				}
			}
			for (int j = length - 1; j >= 0; j--)
			{
				if (array2[j] != array[j])
				{
					for (int k = 0; k < length; k++)
					{
						float num6 = invA[k, array2[j]];
						invA[k, array2[j]] = invA[k, array[j]];
						invA[k, array[j]] = num6;
					}
				}
			}
			return true;
		}
	}
}

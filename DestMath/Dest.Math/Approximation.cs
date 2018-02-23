using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dest.Math
{
	public static class Approximation
	{
		public static Box2 GaussPointsFit2(IList<Vector2> points)
		{
			Box2 result = new Box2(Vector2ex.Zero, Vector2ex.UnitX, Vector2ex.UnitY, Vector2ex.One);
			int count = points.Count;
			result.Center = points[0];
			for (int i = 1; i < count; i++)
			{
				result.Center += points[i];
			}
			float num = 1f / (float)count;
			result.Center *= num;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			for (int j = 0; j < count; j++)
			{
				Vector2 vector = points[j] - result.Center;
				num2 += vector.x * vector.x;
				num3 += vector.x * vector.y;
				num4 += vector.y * vector.y;
			}
			num2 *= num;
			num3 *= num;
			num4 *= num;
			float[,] array = new float[2, 2];
			array[0, 0] = num2;
			array[0, 1] = num3;
			array[1, 0] = num3;
			array[1, 1] = num4;
			float[,] symmetricSquareMatrix = array;
			EigenData eigenData = EigenDecomposition.Solve(symmetricSquareMatrix, true);
			result.Extents.x = eigenData.GetEigenvalue(0);
			result.Extents.y = eigenData.GetEigenvalue(1);
			result.Axis0 = eigenData.GetEigenvector2(0);
			result.Axis1 = eigenData.GetEigenvector2(1);
			return result;
		}

		internal static bool HeightLineFit2(IList<Vector2> points, out float a, out float b)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			int count = points.Count;
			for (int i = 0; i < count; i++)
			{
				num += points[i].x;
				num2 += points[i].y;
				num3 += points[i].x * points[i].x;
				num4 += points[i].x * points[i].y;
			}
			float[,] array = new float[2, 2];
			array[0, 0] = num3;
			array[0, 1] = num;
			array[1, 0] = num;
			array[1, 1] = (float)count;
			float[,] a2 = array;
			float[] b2 = new float[]
			{
				num4,
				num2
			};
			float[] array2;
			bool flag = LinearSystem.Solve2(a2, b2, out array2, 1E-05f);
			if (flag)
			{
				a = array2[0];
				b = array2[1];
			}
			else
			{
				a = 3.40282347E+38f;
				b = 3.40282347E+38f;
			}
			return flag;
		}

		public static Line2 LeastSquaresLineFit2(IList<Vector2> points)
		{
			Line2 result = default(Line2);
			int count = points.Count;
			result.Center = points[0];
			for (int i = 1; i < count; i++)
			{
				result.Center += points[i];
			}
			float num = 1f / (float)count;
			result.Center *= num;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			for (int i = 0; i < count; i++)
			{
				Vector2 vector = points[i] - result.Center;
				num2 += vector.x * vector.x;
				num3 += vector.x * vector.y;
				num4 += vector.y * vector.y;
			}
			num2 *= num;
			num3 *= num;
			num4 *= num;
			float[,] array = new float[2, 2];
			array[0, 0] = num4;
			array[0, 1] = -num3;
			array[1, 0] = num3;
			array[1, 1] = num2;
			float[,] symmetricSquareMatrix = array;
			EigenData eigenData = EigenDecomposition.Solve(symmetricSquareMatrix, false);
			result.Direction = eigenData.GetEigenvector2(1);
			return result;
		}

		public static Box3 GaussPointsFit3(IList<Vector3> points)
		{
			Box3 result = new Box3(Vector3ex.Zero, Vector3ex.UnitX, Vector3ex.UnitY, Vector3ex.UnitZ, Vector3ex.One);
			int count = points.Count;
			result.Center = points[0];
			for (int i = 1; i < count; i++)
			{
				result.Center += points[i];
			}
			float num = 1f / (float)count;
			result.Center *= num;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 0f;
			for (int j = 0; j < count; j++)
			{
				Vector3 vector = points[j] - result.Center;
				num2 += vector.x * vector.x;
				num3 += vector.x * vector.y;
				num4 += vector.x * vector.z;
				num5 += vector.y * vector.y;
				num6 += vector.y * vector.z;
				num7 += vector.z * vector.z;
			}
			num2 *= num;
			num3 *= num;
			num4 *= num;
			num5 *= num;
			num6 *= num;
			num7 *= num;
			float[,] array = new float[3, 3];
			array[0, 0] = num2;
			array[0, 1] = num3;
			array[0, 2] = num4;
			array[1, 0] = num3;
			array[1, 1] = num5;
			array[1, 2] = num6;
			array[2, 0] = num4;
			array[2, 1] = num6;
			array[2, 2] = num7;
			float[,] symmetricSquareMatrix = array;
			EigenData eigenData = EigenDecomposition.Solve(symmetricSquareMatrix, true);
			result.Extents.x = eigenData.GetEigenvalue(0);
			result.Axis0 = eigenData.GetEigenvector3(0);
			result.Extents.y = eigenData.GetEigenvalue(1);
			result.Axis1 = eigenData.GetEigenvector3(1);
			result.Extents.z = eigenData.GetEigenvalue(2);
			result.Axis2 = eigenData.GetEigenvector3(2);
			return result;
		}

		public static Line3 LeastsSquaresLineFit3(IList<Vector3> points)
		{
			Line3 result = default(Line3);
			int count = points.Count;
			result.Center = points[0];
			for (int i = 1; i < count; i++)
			{
				result.Center += points[i];
			}
			float num = 1f / (float)count;
			result.Center *= num;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 0f;
			for (int i = 0; i < count; i++)
			{
				Vector3 vector = points[i] - result.Center;
				num2 += vector.x * vector.x;
				num3 += vector.x * vector.y;
				num4 += vector.x * vector.z;
				num5 += vector.y * vector.y;
				num6 += vector.y * vector.z;
				num7 += vector.z * vector.z;
			}
			num2 *= num;
			num3 *= num;
			num4 *= num;
			num5 *= num;
			num6 *= num;
			num7 *= num;
			float[,] array = new float[3, 3];
			array[0, 0] = num5 + num7;
			array[0, 1] = -num3;
			array[0, 2] = -num4;
			array[1, 0] = array[0, 1];
			array[1, 1] = num2 + num7;
			array[1, 2] = -num6;
			array[2, 0] = array[0, 2];
			array[2, 1] = array[1, 2];
			array[2, 2] = num2 + num5;
			EigenData eigenData = EigenDecomposition.Solve(array, false);
			result.Direction = eigenData.GetEigenvector3(2);
			return result;
		}

		internal static bool HeightPlaneFit3(IList<Vector3> points, out float a, out float b, out float c)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 0f;
			float num8 = 0f;
			int count = points.Count;
			for (int i = 0; i < count; i++)
			{
				num += points[i].x;
				num2 += points[i].y;
				num3 += points[i].z;
				num4 += points[i].x * points[i].x;
				num5 += points[i].x * points[i].y;
				num6 += points[i].x * points[i].z;
				num7 += points[i].y * points[i].y;
				num8 += points[i].y * points[i].z;
			}
			float[,] array = new float[3, 3];
			array[0, 0] = num4;
			array[0, 1] = num5;
			array[0, 2] = num;
			array[1, 0] = num5;
			array[1, 1] = num7;
			array[1, 2] = num2;
			array[2, 0] = num;
			array[2, 1] = num2;
			array[2, 2] = (float)count;
			float[,] a2 = array;
			float[] b2 = new float[]
			{
				num6,
				num8,
				num3
			};
			float[] array2;
			bool flag = LinearSystem.Solve3(a2, b2, out array2, 1E-05f);
			if (flag)
			{
				a = array2[0];
				b = array2[1];
				c = array2[2];
			}
			else
			{
				a = 3.40282347E+38f;
				b = 3.40282347E+38f;
				c = 3.40282347E+38f;
			}
			return flag;
		}

		public static Plane3 LeastSquaresPlaneFit3(IList<Vector3> points)
		{
			Vector3 vector = Vector3ex.Zero;
			int count = points.Count;
			for (int i = 0; i < count; i++)
			{
				vector += points[i];
			}
			float num = 1f / (float)count;
			vector *= num;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 0f;
			for (int i = 0; i < count; i++)
			{
				Vector3 vector2 = points[i] - vector;
				num2 += vector2.x * vector2.x;
				num3 += vector2.x * vector2.y;
				num4 += vector2.x * vector2.z;
				num5 += vector2.y * vector2.y;
				num6 += vector2.y * vector2.z;
				num7 += vector2.z * vector2.z;
			}
			num2 *= num;
			num3 *= num;
			num4 *= num;
			num5 *= num;
			num6 *= num;
			num7 *= num;
			float[,] array = new float[3, 3];
			array[0, 0] = num2;
			array[0, 1] = num3;
			array[0, 2] = num4;
			array[1, 0] = num3;
			array[1, 1] = num5;
			array[1, 2] = num6;
			array[2, 0] = num4;
			array[2, 1] = num6;
			array[2, 2] = num7;
			EigenData eigenData = EigenDecomposition.Solve(array, false);
			Vector3 eigenvector = eigenData.GetEigenvector3(2);
			return new Plane3(ref eigenvector, ref vector);
		}
	}
}

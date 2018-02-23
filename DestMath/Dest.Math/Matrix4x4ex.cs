using System;
using UnityEngine;

namespace Dest.Math
{
	public static class Matrix4x4ex
	{
		public static readonly Matrix4x4 Identity = new Matrix4x4
		{
			m00 = 1f,
			m11 = 1f,
			m22 = 1f,
			m33 = 1f
		};

		public static void RotationMatrixToQuaternion(ref Matrix4x4 matrix, out Quaternion quaternion)
		{
			quaternion = Quaternion.LookRotation(matrix.GetColumn(2), matrix.GetColumn(1));
		}

		public static void QuaternionToRotationMatrix(Quaternion quaternion, out Matrix4x4 matrix)
		{
			float x = quaternion.x;
			float y = quaternion.y;
			float z = quaternion.z;
			float w = quaternion.w;
			matrix.m00 = 1f - 2f * y * y - 2f * z * z;
			matrix.m01 = 2f * x * y - 2f * z * w;
			matrix.m02 = 2f * x * z + 2f * y * w;
			matrix.m03 = 0f;
			matrix.m10 = 2f * x * y + 2f * z * w;
			matrix.m11 = 1f - 2f * x * x - 2f * z * z;
			matrix.m12 = 2f * y * z - 2f * x * w;
			matrix.m13 = 0f;
			matrix.m20 = 2f * x * z - 2f * y * w;
			matrix.m21 = 2f * y * z + 2f * x * w;
			matrix.m22 = 1f - 2f * x * x - 2f * y * y;
			matrix.m23 = 0f;
			matrix.m30 = 0f;
			matrix.m31 = 0f;
			matrix.m32 = 0f;
			matrix.m33 = 1f;
		}

		public static void QuaternionToRotationMatrix(ref Quaternion quaternion, out Matrix4x4 matrix)
		{
			float x = quaternion.x;
			float y = quaternion.y;
			float z = quaternion.z;
			float w = quaternion.w;
			matrix.m00 = 1f - 2f * y * y - 2f * z * z;
			matrix.m01 = 2f * x * y - 2f * z * w;
			matrix.m02 = 2f * x * z + 2f * y * w;
			matrix.m03 = 0f;
			matrix.m10 = 2f * x * y + 2f * z * w;
			matrix.m11 = 1f - 2f * x * x - 2f * z * z;
			matrix.m12 = 2f * y * z - 2f * x * w;
			matrix.m13 = 0f;
			matrix.m20 = 2f * x * z - 2f * y * w;
			matrix.m21 = 2f * y * z + 2f * x * w;
			matrix.m22 = 1f - 2f * x * x - 2f * y * y;
			matrix.m23 = 0f;
			matrix.m30 = 0f;
			matrix.m31 = 0f;
			matrix.m32 = 0f;
			matrix.m33 = 1f;
		}

		public static void CreateTranslation(Vector3 position, out Matrix4x4 matrix)
		{
			matrix.m01 = 0f;
			matrix.m02 = 0f;
			matrix.m10 = 0f;
			matrix.m12 = 0f;
			matrix.m20 = 0f;
			matrix.m21 = 0f;
			matrix.m30 = 0f;
			matrix.m31 = 0f;
			matrix.m32 = 0f;
			matrix.m03 = position.x;
			matrix.m13 = position.y;
			matrix.m23 = position.z;
			matrix.m00 = 0f;
			matrix.m11 = 0f;
			matrix.m22 = 0f;
			matrix.m33 = 1f;
		}

		public static void CreateTranslation(ref Vector3 position, out Matrix4x4 matrix)
		{
			matrix.m01 = 0f;
			matrix.m02 = 0f;
			matrix.m10 = 0f;
			matrix.m12 = 0f;
			matrix.m20 = 0f;
			matrix.m21 = 0f;
			matrix.m30 = 0f;
			matrix.m31 = 0f;
			matrix.m32 = 0f;
			matrix.m03 = position.x;
			matrix.m13 = position.y;
			matrix.m23 = position.z;
			matrix.m00 = 0f;
			matrix.m11 = 0f;
			matrix.m22 = 0f;
			matrix.m33 = 1f;
		}

		public static void CreateScale(Vector3 scale, out Matrix4x4 matrix)
		{
			matrix.m01 = 0f;
			matrix.m02 = 0f;
			matrix.m03 = 0f;
			matrix.m10 = 0f;
			matrix.m12 = 0f;
			matrix.m13 = 0f;
			matrix.m20 = 0f;
			matrix.m21 = 0f;
			matrix.m23 = 0f;
			matrix.m30 = 0f;
			matrix.m31 = 0f;
			matrix.m32 = 0f;
			matrix.m00 = scale.x;
			matrix.m11 = scale.y;
			matrix.m22 = scale.z;
			matrix.m33 = 1f;
		}

		public static void CreateScale(ref Vector3 scale, out Matrix4x4 matrix)
		{
			matrix.m01 = 0f;
			matrix.m02 = 0f;
			matrix.m03 = 0f;
			matrix.m10 = 0f;
			matrix.m12 = 0f;
			matrix.m13 = 0f;
			matrix.m20 = 0f;
			matrix.m21 = 0f;
			matrix.m23 = 0f;
			matrix.m30 = 0f;
			matrix.m31 = 0f;
			matrix.m32 = 0f;
			matrix.m00 = scale.x;
			matrix.m11 = scale.y;
			matrix.m22 = scale.z;
			matrix.m33 = 1f;
		}

		public static void CreateScale(float scale, out Matrix4x4 matrix)
		{
			matrix.m01 = 0f;
			matrix.m02 = 0f;
			matrix.m03 = 0f;
			matrix.m10 = 0f;
			matrix.m12 = 0f;
			matrix.m13 = 0f;
			matrix.m20 = 0f;
			matrix.m21 = 0f;
			matrix.m23 = 0f;
			matrix.m30 = 0f;
			matrix.m31 = 0f;
			matrix.m32 = 0f;
			matrix.m00 = scale;
			matrix.m11 = scale;
			matrix.m22 = scale;
			matrix.m33 = 1f;
		}

		public static void CreateRotationEuler(float eulerX, float eulerY, float eulerZ, out Matrix4x4 matrix)
		{
			Quaternion quaternion = Quaternion.Euler(eulerX, eulerY, eulerZ);
			Matrix4x4ex.QuaternionToRotationMatrix(ref quaternion, out matrix);
		}

		public static void CreateRotationEuler(Vector3 eulerAngles, out Matrix4x4 matrix)
		{
			Quaternion quaternion = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
			Matrix4x4ex.QuaternionToRotationMatrix(ref quaternion, out matrix);
		}

		public static void CreateRotationEuler(ref Vector3 eulerAngles, out Matrix4x4 matrix)
		{
			Quaternion quaternion = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
			Matrix4x4ex.QuaternionToRotationMatrix(ref quaternion, out matrix);
		}

		public static void CreateRotationX(float angleInDegrees, out Matrix4x4 matrix)
		{
			float f = angleInDegrees * 0.0174532924f;
			float num = Mathf.Sin(f);
			float num2 = Mathf.Cos(f);
			matrix.m00 = 1f;
			matrix.m01 = 0f;
			matrix.m02 = 0f;
			matrix.m03 = 0f;
			matrix.m10 = 0f;
			matrix.m11 = num2;
			matrix.m12 = -num;
			matrix.m13 = 0f;
			matrix.m20 = 0f;
			matrix.m21 = num;
			matrix.m22 = num2;
			matrix.m23 = 0f;
			matrix.m30 = 0f;
			matrix.m31 = 0f;
			matrix.m32 = 0f;
			matrix.m33 = 1f;
		}

		public static void CreateRotationY(float angleInDegrees, out Matrix4x4 matrix)
		{
			float f = angleInDegrees * 0.0174532924f;
			float num = Mathf.Sin(f);
			float num2 = Mathf.Cos(f);
			matrix.m00 = num2;
			matrix.m01 = 0f;
			matrix.m02 = num;
			matrix.m03 = 0f;
			matrix.m10 = 0f;
			matrix.m11 = 1f;
			matrix.m12 = 0f;
			matrix.m13 = 0f;
			matrix.m20 = -num;
			matrix.m21 = 0f;
			matrix.m22 = num2;
			matrix.m23 = 0f;
			matrix.m30 = 0f;
			matrix.m31 = 0f;
			matrix.m32 = 0f;
			matrix.m33 = 1f;
		}

		public static void CreateRotationZ(float angleInDegrees, out Matrix4x4 matrix)
		{
			float f = angleInDegrees * 0.0174532924f;
			float num = Mathf.Sin(f);
			float num2 = Mathf.Cos(f);
			matrix.m00 = num2;
			matrix.m01 = -num;
			matrix.m02 = 0f;
			matrix.m03 = 0f;
			matrix.m10 = num;
			matrix.m11 = num2;
			matrix.m12 = 0f;
			matrix.m13 = 0f;
			matrix.m20 = 0f;
			matrix.m21 = 0f;
			matrix.m22 = 1f;
			matrix.m23 = 0f;
			matrix.m30 = 0f;
			matrix.m31 = 0f;
			matrix.m32 = 0f;
			matrix.m33 = 1f;
		}

		public static void CreateRotationAngleAxis(float angleInDegrees, Vector3 rotationAxis, out Matrix4x4 matrix)
		{
			Vector3 normalized = rotationAxis.normalized;
			float f = angleInDegrees * 0.0174532924f;
			float num = Mathf.Cos(f);
			float num2 = Mathf.Sin(f);
			float num3 = 1f - num;
			float num4 = normalized.x * normalized.x;
			float num5 = normalized.y * normalized.y;
			float num6 = normalized.z * normalized.z;
			float num7 = normalized.x * normalized.y * num3;
			float num8 = normalized.x * normalized.z * num3;
			float num9 = normalized.y * normalized.z * num3;
			float num10 = normalized.x * num2;
			float num11 = normalized.y * num2;
			float num12 = normalized.z * num2;
			matrix.m00 = num4 * num3 + num;
			matrix.m01 = num7 - num12;
			matrix.m02 = num8 + num11;
			matrix.m10 = num7 + num12;
			matrix.m11 = num5 * num3 + num;
			matrix.m12 = num9 - num10;
			matrix.m20 = num8 - num11;
			matrix.m21 = num9 + num10;
			matrix.m22 = num6 * num3 + num;
			matrix.m30 = 0f;
			matrix.m31 = 0f;
			matrix.m32 = 0f;
			matrix.m03 = 0f;
			matrix.m13 = 0f;
			matrix.m23 = 0f;
			matrix.m33 = 1f;
		}

		public static void CreateRotationAngleUnitAxis(float angleInDegrees, Vector3 normalizedAxis, out Matrix4x4 matrix)
		{
			float f = angleInDegrees * 0.0174532924f;
			float num = Mathf.Cos(f);
			float num2 = Mathf.Sin(f);
			float num3 = 1f - num;
			float num4 = normalizedAxis.x * normalizedAxis.x;
			float num5 = normalizedAxis.y * normalizedAxis.y;
			float num6 = normalizedAxis.z * normalizedAxis.z;
			float num7 = normalizedAxis.x * normalizedAxis.y * num3;
			float num8 = normalizedAxis.x * normalizedAxis.z * num3;
			float num9 = normalizedAxis.y * normalizedAxis.z * num3;
			float num10 = normalizedAxis.x * num2;
			float num11 = normalizedAxis.y * num2;
			float num12 = normalizedAxis.z * num2;
			matrix.m00 = num4 * num3 + num;
			matrix.m01 = num7 - num12;
			matrix.m02 = num8 + num11;
			matrix.m10 = num7 + num12;
			matrix.m11 = num5 * num3 + num;
			matrix.m12 = num9 - num10;
			matrix.m20 = num8 - num11;
			matrix.m21 = num9 + num10;
			matrix.m22 = num6 * num3 + num;
			matrix.m30 = 0f;
			matrix.m31 = 0f;
			matrix.m32 = 0f;
			matrix.m03 = 0f;
			matrix.m13 = 0f;
			matrix.m23 = 0f;
			matrix.m33 = 1f;
		}

		public static void CreateRotation(Vector3 rotationOrigin, Quaternion rotation, out Matrix4x4 result)
		{
			Matrix4x4ex.QuaternionToRotationMatrix(ref rotation, out result);
			result.m03 = -(result.m00 * rotationOrigin.x + result.m01 * rotationOrigin.y + result.m02 * rotationOrigin.z - rotationOrigin.x);
			result.m13 = -(result.m10 * rotationOrigin.x + result.m11 * rotationOrigin.y + result.m12 * rotationOrigin.z - rotationOrigin.y);
			result.m23 = -(result.m20 * rotationOrigin.x + result.m21 * rotationOrigin.y + result.m22 * rotationOrigin.z - rotationOrigin.z);
		}

		public static void CreateRotation(ref Vector3 rotationOrigin, ref Quaternion rotation, out Matrix4x4 result)
		{
			Matrix4x4ex.QuaternionToRotationMatrix(ref rotation, out result);
			result.m03 = -(result.m00 * rotationOrigin.x + result.m01 * rotationOrigin.y + result.m02 * rotationOrigin.z - rotationOrigin.x);
			result.m13 = -(result.m10 * rotationOrigin.x + result.m11 * rotationOrigin.y + result.m12 * rotationOrigin.z - rotationOrigin.y);
			result.m23 = -(result.m20 * rotationOrigin.x + result.m21 * rotationOrigin.y + result.m22 * rotationOrigin.z - rotationOrigin.z);
		}

		public static void Transpose(ref Matrix4x4 matrix)
		{
			float num = matrix.m01;
			matrix.m01 = matrix.m10;
			matrix.m10 = num;
			num = matrix.m02;
			matrix.m02 = matrix.m20;
			matrix.m20 = num;
			num = matrix.m03;
			matrix.m03 = matrix.m30;
			matrix.m30 = num;
			num = matrix.m12;
			matrix.m12 = matrix.m21;
			matrix.m21 = num;
			num = matrix.m13;
			matrix.m13 = matrix.m31;
			matrix.m31 = num;
			num = matrix.m23;
			matrix.m23 = matrix.m32;
			matrix.m32 = num;
		}

		public static void Transpose(ref Matrix4x4 matrix, out Matrix4x4 transpose)
		{
			transpose.m00 = matrix.m00;
			transpose.m01 = matrix.m10;
			transpose.m02 = matrix.m20;
			transpose.m03 = matrix.m30;
			transpose.m10 = matrix.m01;
			transpose.m11 = matrix.m11;
			transpose.m12 = matrix.m21;
			transpose.m13 = matrix.m31;
			transpose.m20 = matrix.m02;
			transpose.m21 = matrix.m12;
			transpose.m22 = matrix.m22;
			transpose.m23 = matrix.m32;
			transpose.m30 = matrix.m03;
			transpose.m31 = matrix.m13;
			transpose.m32 = matrix.m23;
			transpose.m33 = matrix.m33;
		}

		public static float CalcDeterminant(ref Matrix4x4 matrix)
		{
			float num = matrix.m00 * matrix.m11 - matrix.m10 * matrix.m01;
			float num2 = matrix.m00 * matrix.m21 - matrix.m20 * matrix.m01;
			float num3 = matrix.m00 * matrix.m31 - matrix.m30 * matrix.m01;
			float num4 = matrix.m10 * matrix.m21 - matrix.m20 * matrix.m11;
			float num5 = matrix.m10 * matrix.m31 - matrix.m30 * matrix.m11;
			float num6 = matrix.m20 * matrix.m31 - matrix.m30 * matrix.m21;
			float num7 = matrix.m02 * matrix.m13 - matrix.m12 * matrix.m03;
			float num8 = matrix.m02 * matrix.m23 - matrix.m22 * matrix.m03;
			float num9 = matrix.m02 * matrix.m33 - matrix.m32 * matrix.m03;
			float num10 = matrix.m12 * matrix.m23 - matrix.m22 * matrix.m13;
			float num11 = matrix.m12 * matrix.m33 - matrix.m32 * matrix.m13;
			float num12 = matrix.m22 * matrix.m33 - matrix.m32 * matrix.m23;
			return num * num12 - num2 * num11 + num3 * num10 + num4 * num9 - num5 * num8 + num6 * num7;
		}

		public static void Inverse(ref Matrix4x4 matrix, float epsilon = 1E-05f)
		{
			float num = matrix.m00 * matrix.m11 - matrix.m10 * matrix.m01;
			float num2 = matrix.m00 * matrix.m21 - matrix.m20 * matrix.m01;
			float num3 = matrix.m00 * matrix.m31 - matrix.m30 * matrix.m01;
			float num4 = matrix.m10 * matrix.m21 - matrix.m20 * matrix.m11;
			float num5 = matrix.m10 * matrix.m31 - matrix.m30 * matrix.m11;
			float num6 = matrix.m20 * matrix.m31 - matrix.m30 * matrix.m21;
			float num7 = matrix.m02 * matrix.m13 - matrix.m12 * matrix.m03;
			float num8 = matrix.m02 * matrix.m23 - matrix.m22 * matrix.m03;
			float num9 = matrix.m02 * matrix.m33 - matrix.m32 * matrix.m03;
			float num10 = matrix.m12 * matrix.m23 - matrix.m22 * matrix.m13;
			float num11 = matrix.m12 * matrix.m33 - matrix.m32 * matrix.m13;
			float num12 = matrix.m22 * matrix.m33 - matrix.m32 * matrix.m23;
			float num13 = num * num12 - num2 * num11 + num3 * num10 + num4 * num9 - num5 * num8 + num6 * num7;
			if (Mathf.Abs(num13) > epsilon)
			{
				Matrix4x4 matrix4x;
				matrix4x.m00 = matrix.m11 * num12 - matrix.m21 * num11 + matrix.m31 * num10;
				matrix4x.m01 = -matrix.m01 * num12 + matrix.m21 * num9 - matrix.m31 * num8;
				matrix4x.m02 = matrix.m01 * num11 - matrix.m11 * num9 + matrix.m31 * num7;
				matrix4x.m03 = -matrix.m01 * num10 + matrix.m11 * num8 - matrix.m21 * num7;
				matrix4x.m10 = -matrix.m10 * num12 + matrix.m20 * num11 - matrix.m30 * num10;
				matrix4x.m11 = matrix.m00 * num12 - matrix.m20 * num9 + matrix.m30 * num8;
				matrix4x.m12 = -matrix.m00 * num11 + matrix.m10 * num9 - matrix.m30 * num7;
				matrix4x.m13 = matrix.m00 * num10 - matrix.m10 * num8 + matrix.m20 * num7;
				matrix4x.m20 = matrix.m13 * num6 - matrix.m23 * num5 + matrix.m33 * num4;
				matrix4x.m21 = -matrix.m03 * num6 + matrix.m23 * num3 - matrix.m33 * num2;
				matrix4x.m22 = matrix.m03 * num5 - matrix.m13 * num3 + matrix.m33 * num;
				matrix4x.m23 = -matrix.m03 * num4 + matrix.m13 * num2 - matrix.m23 * num;
				matrix4x.m30 = -matrix.m12 * num6 + matrix.m22 * num5 - matrix.m32 * num4;
				matrix4x.m31 = matrix.m02 * num6 - matrix.m22 * num3 + matrix.m32 * num2;
				matrix4x.m32 = -matrix.m02 * num5 + matrix.m12 * num3 - matrix.m32 * num;
				matrix4x.m33 = matrix.m02 * num4 - matrix.m12 * num2 + matrix.m22 * num;
				float num14 = 1f / num13;
				matrix4x.m00 *= num14;
				matrix4x.m01 *= num14;
				matrix4x.m02 *= num14;
				matrix4x.m03 *= num14;
				matrix4x.m10 *= num14;
				matrix4x.m11 *= num14;
				matrix4x.m12 *= num14;
				matrix4x.m13 *= num14;
				matrix4x.m20 *= num14;
				matrix4x.m21 *= num14;
				matrix4x.m22 *= num14;
				matrix4x.m23 *= num14;
				matrix4x.m30 *= num14;
				matrix4x.m31 *= num14;
				matrix4x.m32 *= num14;
				matrix4x.m33 *= num14;
				matrix = matrix4x;
				return;
			}
			matrix = Matrix4x4.zero;
		}

		public static void Inverse(ref Matrix4x4 matrix, out Matrix4x4 inverse, float epsilon = 1E-05f)
		{
			float num = matrix.m00 * matrix.m11 - matrix.m10 * matrix.m01;
			float num2 = matrix.m00 * matrix.m21 - matrix.m20 * matrix.m01;
			float num3 = matrix.m00 * matrix.m31 - matrix.m30 * matrix.m01;
			float num4 = matrix.m10 * matrix.m21 - matrix.m20 * matrix.m11;
			float num5 = matrix.m10 * matrix.m31 - matrix.m30 * matrix.m11;
			float num6 = matrix.m20 * matrix.m31 - matrix.m30 * matrix.m21;
			float num7 = matrix.m02 * matrix.m13 - matrix.m12 * matrix.m03;
			float num8 = matrix.m02 * matrix.m23 - matrix.m22 * matrix.m03;
			float num9 = matrix.m02 * matrix.m33 - matrix.m32 * matrix.m03;
			float num10 = matrix.m12 * matrix.m23 - matrix.m22 * matrix.m13;
			float num11 = matrix.m12 * matrix.m33 - matrix.m32 * matrix.m13;
			float num12 = matrix.m22 * matrix.m33 - matrix.m32 * matrix.m23;
			float num13 = num * num12 - num2 * num11 + num3 * num10 + num4 * num9 - num5 * num8 + num6 * num7;
			if (Mathf.Abs(num13) > epsilon)
			{
				inverse.m00 = matrix.m11 * num12 - matrix.m21 * num11 + matrix.m31 * num10;
				inverse.m01 = -matrix.m01 * num12 + matrix.m21 * num9 - matrix.m31 * num8;
				inverse.m02 = matrix.m01 * num11 - matrix.m11 * num9 + matrix.m31 * num7;
				inverse.m03 = -matrix.m01 * num10 + matrix.m11 * num8 - matrix.m21 * num7;
				inverse.m10 = -matrix.m10 * num12 + matrix.m20 * num11 - matrix.m30 * num10;
				inverse.m11 = matrix.m00 * num12 - matrix.m20 * num9 + matrix.m30 * num8;
				inverse.m12 = -matrix.m00 * num11 + matrix.m10 * num9 - matrix.m30 * num7;
				inverse.m13 = matrix.m00 * num10 - matrix.m10 * num8 + matrix.m20 * num7;
				inverse.m20 = matrix.m13 * num6 - matrix.m23 * num5 + matrix.m33 * num4;
				inverse.m21 = -matrix.m03 * num6 + matrix.m23 * num3 - matrix.m33 * num2;
				inverse.m22 = matrix.m03 * num5 - matrix.m13 * num3 + matrix.m33 * num;
				inverse.m23 = -matrix.m03 * num4 + matrix.m13 * num2 - matrix.m23 * num;
				inverse.m30 = -matrix.m12 * num6 + matrix.m22 * num5 - matrix.m32 * num4;
				inverse.m31 = matrix.m02 * num6 - matrix.m22 * num3 + matrix.m32 * num2;
				inverse.m32 = -matrix.m02 * num5 + matrix.m12 * num3 - matrix.m32 * num;
				inverse.m33 = matrix.m02 * num4 - matrix.m12 * num2 + matrix.m22 * num;
				float num14 = 1f / num13;
				inverse.m00 *= num14;
				inverse.m01 *= num14;
				inverse.m02 *= num14;
				inverse.m03 *= num14;
				inverse.m10 *= num14;
				inverse.m11 *= num14;
				inverse.m12 *= num14;
				inverse.m13 *= num14;
				inverse.m20 *= num14;
				inverse.m21 *= num14;
				inverse.m22 *= num14;
				inverse.m23 *= num14;
				inverse.m30 *= num14;
				inverse.m31 *= num14;
				inverse.m32 *= num14;
				inverse.m33 *= num14;
				return;
			}
			inverse = Matrix4x4.zero;
		}

		public static void CopyMatrix(ref Matrix4x4 source, out Matrix4x4 destination)
		{
			destination.m00 = source.m00;
			destination.m01 = source.m01;
			destination.m02 = source.m02;
			destination.m03 = source.m03;
			destination.m10 = source.m10;
			destination.m11 = source.m11;
			destination.m12 = source.m12;
			destination.m13 = source.m13;
			destination.m20 = source.m20;
			destination.m21 = source.m21;
			destination.m22 = source.m22;
			destination.m23 = source.m23;
			destination.m30 = source.m30;
			destination.m31 = source.m31;
			destination.m32 = source.m32;
			destination.m33 = source.m33;
		}

		public static void Multiply(ref Matrix4x4 matrix0, ref Matrix4x4 matrix1, out Matrix4x4 result)
		{
			result.m00 = matrix0.m00 * matrix1.m00 + matrix0.m01 * matrix1.m10 + matrix0.m02 * matrix1.m20 + matrix0.m03 * matrix1.m30;
			result.m01 = matrix0.m00 * matrix1.m01 + matrix0.m01 * matrix1.m11 + matrix0.m02 * matrix1.m21 + matrix0.m03 * matrix1.m31;
			result.m02 = matrix0.m00 * matrix1.m02 + matrix0.m01 * matrix1.m12 + matrix0.m02 * matrix1.m22 + matrix0.m03 * matrix1.m32;
			result.m03 = matrix0.m00 * matrix1.m03 + matrix0.m01 * matrix1.m13 + matrix0.m02 * matrix1.m23 + matrix0.m03 * matrix1.m33;
			result.m10 = matrix0.m10 * matrix1.m00 + matrix0.m11 * matrix1.m10 + matrix0.m12 * matrix1.m20 + matrix0.m13 * matrix1.m30;
			result.m11 = matrix0.m10 * matrix1.m01 + matrix0.m11 * matrix1.m11 + matrix0.m12 * matrix1.m21 + matrix0.m13 * matrix1.m31;
			result.m12 = matrix0.m10 * matrix1.m02 + matrix0.m11 * matrix1.m12 + matrix0.m12 * matrix1.m22 + matrix0.m13 * matrix1.m32;
			result.m13 = matrix0.m10 * matrix1.m03 + matrix0.m11 * matrix1.m13 + matrix0.m12 * matrix1.m23 + matrix0.m13 * matrix1.m33;
			result.m20 = matrix0.m20 * matrix1.m00 + matrix0.m21 * matrix1.m10 + matrix0.m22 * matrix1.m20 + matrix0.m23 * matrix1.m30;
			result.m21 = matrix0.m20 * matrix1.m01 + matrix0.m21 * matrix1.m11 + matrix0.m22 * matrix1.m21 + matrix0.m23 * matrix1.m31;
			result.m22 = matrix0.m20 * matrix1.m02 + matrix0.m21 * matrix1.m12 + matrix0.m22 * matrix1.m22 + matrix0.m23 * matrix1.m32;
			result.m23 = matrix0.m20 * matrix1.m03 + matrix0.m21 * matrix1.m13 + matrix0.m22 * matrix1.m23 + matrix0.m23 * matrix1.m33;
			result.m30 = matrix0.m30 * matrix1.m00 + matrix0.m31 * matrix1.m10 + matrix0.m32 * matrix1.m20 + matrix0.m33 * matrix1.m30;
			result.m31 = matrix0.m30 * matrix1.m01 + matrix0.m31 * matrix1.m11 + matrix0.m32 * matrix1.m21 + matrix0.m33 * matrix1.m31;
			result.m32 = matrix0.m30 * matrix1.m02 + matrix0.m31 * matrix1.m12 + matrix0.m32 * matrix1.m22 + matrix0.m33 * matrix1.m32;
			result.m33 = matrix0.m30 * matrix1.m03 + matrix0.m31 * matrix1.m13 + matrix0.m32 * matrix1.m23 + matrix0.m33 * matrix1.m33;
		}

		public static void MultiplyRight(ref Matrix4x4 matrix0, ref Matrix4x4 matrix1)
		{
			Matrix4x4 matrix4x;
			matrix4x.m00 = matrix0.m00 * matrix1.m00 + matrix0.m01 * matrix1.m10 + matrix0.m02 * matrix1.m20 + matrix0.m03 * matrix1.m30;
			matrix4x.m01 = matrix0.m00 * matrix1.m01 + matrix0.m01 * matrix1.m11 + matrix0.m02 * matrix1.m21 + matrix0.m03 * matrix1.m31;
			matrix4x.m02 = matrix0.m00 * matrix1.m02 + matrix0.m01 * matrix1.m12 + matrix0.m02 * matrix1.m22 + matrix0.m03 * matrix1.m32;
			matrix4x.m03 = matrix0.m00 * matrix1.m03 + matrix0.m01 * matrix1.m13 + matrix0.m02 * matrix1.m23 + matrix0.m03 * matrix1.m33;
			matrix4x.m10 = matrix0.m10 * matrix1.m00 + matrix0.m11 * matrix1.m10 + matrix0.m12 * matrix1.m20 + matrix0.m13 * matrix1.m30;
			matrix4x.m11 = matrix0.m10 * matrix1.m01 + matrix0.m11 * matrix1.m11 + matrix0.m12 * matrix1.m21 + matrix0.m13 * matrix1.m31;
			matrix4x.m12 = matrix0.m10 * matrix1.m02 + matrix0.m11 * matrix1.m12 + matrix0.m12 * matrix1.m22 + matrix0.m13 * matrix1.m32;
			matrix4x.m13 = matrix0.m10 * matrix1.m03 + matrix0.m11 * matrix1.m13 + matrix0.m12 * matrix1.m23 + matrix0.m13 * matrix1.m33;
			matrix4x.m20 = matrix0.m20 * matrix1.m00 + matrix0.m21 * matrix1.m10 + matrix0.m22 * matrix1.m20 + matrix0.m23 * matrix1.m30;
			matrix4x.m21 = matrix0.m20 * matrix1.m01 + matrix0.m21 * matrix1.m11 + matrix0.m22 * matrix1.m21 + matrix0.m23 * matrix1.m31;
			matrix4x.m22 = matrix0.m20 * matrix1.m02 + matrix0.m21 * matrix1.m12 + matrix0.m22 * matrix1.m22 + matrix0.m23 * matrix1.m32;
			matrix4x.m23 = matrix0.m20 * matrix1.m03 + matrix0.m21 * matrix1.m13 + matrix0.m22 * matrix1.m23 + matrix0.m23 * matrix1.m33;
			matrix4x.m30 = matrix0.m30 * matrix1.m00 + matrix0.m31 * matrix1.m10 + matrix0.m32 * matrix1.m20 + matrix0.m33 * matrix1.m30;
			matrix4x.m31 = matrix0.m30 * matrix1.m01 + matrix0.m31 * matrix1.m11 + matrix0.m32 * matrix1.m21 + matrix0.m33 * matrix1.m31;
			matrix4x.m32 = matrix0.m30 * matrix1.m02 + matrix0.m31 * matrix1.m12 + matrix0.m32 * matrix1.m22 + matrix0.m33 * matrix1.m32;
			matrix4x.m33 = matrix0.m30 * matrix1.m03 + matrix0.m31 * matrix1.m13 + matrix0.m32 * matrix1.m23 + matrix0.m33 * matrix1.m33;
			Matrix4x4ex.CopyMatrix(ref matrix4x, out matrix0);
		}

		public static void MultiplyLeft(ref Matrix4x4 matrix1, ref Matrix4x4 matrix0)
		{
			Matrix4x4 matrix4x;
			matrix4x.m00 = matrix0.m00 * matrix1.m00 + matrix0.m01 * matrix1.m10 + matrix0.m02 * matrix1.m20 + matrix0.m03 * matrix1.m30;
			matrix4x.m01 = matrix0.m00 * matrix1.m01 + matrix0.m01 * matrix1.m11 + matrix0.m02 * matrix1.m21 + matrix0.m03 * matrix1.m31;
			matrix4x.m02 = matrix0.m00 * matrix1.m02 + matrix0.m01 * matrix1.m12 + matrix0.m02 * matrix1.m22 + matrix0.m03 * matrix1.m32;
			matrix4x.m03 = matrix0.m00 * matrix1.m03 + matrix0.m01 * matrix1.m13 + matrix0.m02 * matrix1.m23 + matrix0.m03 * matrix1.m33;
			matrix4x.m10 = matrix0.m10 * matrix1.m00 + matrix0.m11 * matrix1.m10 + matrix0.m12 * matrix1.m20 + matrix0.m13 * matrix1.m30;
			matrix4x.m11 = matrix0.m10 * matrix1.m01 + matrix0.m11 * matrix1.m11 + matrix0.m12 * matrix1.m21 + matrix0.m13 * matrix1.m31;
			matrix4x.m12 = matrix0.m10 * matrix1.m02 + matrix0.m11 * matrix1.m12 + matrix0.m12 * matrix1.m22 + matrix0.m13 * matrix1.m32;
			matrix4x.m13 = matrix0.m10 * matrix1.m03 + matrix0.m11 * matrix1.m13 + matrix0.m12 * matrix1.m23 + matrix0.m13 * matrix1.m33;
			matrix4x.m20 = matrix0.m20 * matrix1.m00 + matrix0.m21 * matrix1.m10 + matrix0.m22 * matrix1.m20 + matrix0.m23 * matrix1.m30;
			matrix4x.m21 = matrix0.m20 * matrix1.m01 + matrix0.m21 * matrix1.m11 + matrix0.m22 * matrix1.m21 + matrix0.m23 * matrix1.m31;
			matrix4x.m22 = matrix0.m20 * matrix1.m02 + matrix0.m21 * matrix1.m12 + matrix0.m22 * matrix1.m22 + matrix0.m23 * matrix1.m32;
			matrix4x.m23 = matrix0.m20 * matrix1.m03 + matrix0.m21 * matrix1.m13 + matrix0.m22 * matrix1.m23 + matrix0.m23 * matrix1.m33;
			matrix4x.m30 = matrix0.m30 * matrix1.m00 + matrix0.m31 * matrix1.m10 + matrix0.m32 * matrix1.m20 + matrix0.m33 * matrix1.m30;
			matrix4x.m31 = matrix0.m30 * matrix1.m01 + matrix0.m31 * matrix1.m11 + matrix0.m32 * matrix1.m21 + matrix0.m33 * matrix1.m31;
			matrix4x.m32 = matrix0.m30 * matrix1.m02 + matrix0.m31 * matrix1.m12 + matrix0.m32 * matrix1.m22 + matrix0.m33 * matrix1.m32;
			matrix4x.m33 = matrix0.m30 * matrix1.m03 + matrix0.m31 * matrix1.m13 + matrix0.m32 * matrix1.m23 + matrix0.m33 * matrix1.m33;
			Matrix4x4ex.CopyMatrix(ref matrix4x, out matrix1);
		}

		public static void Multiply(ref Matrix4x4 matrix, float scalar)
		{
			matrix.m00 *= scalar;
			matrix.m01 *= scalar;
			matrix.m02 *= scalar;
			matrix.m03 *= scalar;
			matrix.m10 *= scalar;
			matrix.m11 *= scalar;
			matrix.m12 *= scalar;
			matrix.m13 *= scalar;
			matrix.m20 *= scalar;
			matrix.m21 *= scalar;
			matrix.m22 *= scalar;
			matrix.m23 *= scalar;
			matrix.m30 *= scalar;
			matrix.m31 *= scalar;
			matrix.m32 *= scalar;
			matrix.m33 *= scalar;
		}

		public static void Multiply(ref Matrix4x4 matrix, float scalar, out Matrix4x4 result)
		{
			result.m00 = matrix.m00 * scalar;
			result.m01 = matrix.m01 * scalar;
			result.m02 = matrix.m02 * scalar;
			result.m03 = matrix.m03 * scalar;
			result.m10 = matrix.m10 * scalar;
			result.m11 = matrix.m11 * scalar;
			result.m12 = matrix.m12 * scalar;
			result.m13 = matrix.m13 * scalar;
			result.m20 = matrix.m20 * scalar;
			result.m21 = matrix.m21 * scalar;
			result.m22 = matrix.m22 * scalar;
			result.m23 = matrix.m23 * scalar;
			result.m30 = matrix.m30 * scalar;
			result.m31 = matrix.m31 * scalar;
			result.m32 = matrix.m32 * scalar;
			result.m33 = matrix.m33 * scalar;
		}

		public static Vector4 Multiply(ref Matrix4x4 matrix, Vector4 vector)
		{
			Vector4 result;
			result.x = matrix.m00 * vector.x + matrix.m01 * vector.y + matrix.m02 * vector.z + matrix.m03 * vector.w;
			result.y = matrix.m10 * vector.x + matrix.m11 * vector.y + matrix.m12 * vector.z + matrix.m13 * vector.w;
			result.z = matrix.m20 * vector.x + matrix.m21 * vector.y + matrix.m22 * vector.z + matrix.m23 * vector.w;
			result.w = matrix.m30 * vector.x + matrix.m31 * vector.y + matrix.m32 * vector.z + matrix.m33 * vector.w;
			return result;
		}

		public static Vector4 Multiply(ref Matrix4x4 matrix, ref Vector4 vector)
		{
			Vector4 result;
			result.x = matrix.m00 * vector.x + matrix.m01 * vector.y + matrix.m02 * vector.z + matrix.m03 * vector.w;
			result.y = matrix.m10 * vector.x + matrix.m11 * vector.y + matrix.m12 * vector.z + matrix.m13 * vector.w;
			result.z = matrix.m20 * vector.x + matrix.m21 * vector.y + matrix.m22 * vector.z + matrix.m23 * vector.w;
			result.w = matrix.m30 * vector.x + matrix.m31 * vector.y + matrix.m32 * vector.z + matrix.m33 * vector.w;
			return result;
		}

		public static void CreateSRT(Vector3 scaling, Quaternion rotation, Vector3 translation, out Matrix4x4 result)
		{
			Matrix4x4ex.QuaternionToRotationMatrix(ref rotation, out result);
			result.m00 *= scaling.x;
			result.m10 *= scaling.x;
			result.m20 *= scaling.x;
			result.m01 *= scaling.y;
			result.m11 *= scaling.y;
			result.m21 *= scaling.y;
			result.m02 *= scaling.z;
			result.m12 *= scaling.z;
			result.m22 *= scaling.z;
			result.m03 = translation.x;
			result.m13 = translation.y;
			result.m23 = translation.z;
		}

		public static void CreateSRT(ref Vector3 scaling, ref Quaternion rotation, ref Vector3 translation, out Matrix4x4 result)
		{
			Matrix4x4ex.QuaternionToRotationMatrix(ref rotation, out result);
			result.m00 *= scaling.x;
			result.m10 *= scaling.x;
			result.m20 *= scaling.x;
			result.m01 *= scaling.y;
			result.m11 *= scaling.y;
			result.m21 *= scaling.y;
			result.m02 *= scaling.z;
			result.m12 *= scaling.z;
			result.m22 *= scaling.z;
			result.m03 = translation.x;
			result.m13 = translation.y;
			result.m23 = translation.z;
		}

		public static void CreateSRT(float scaling, Quaternion rotation, Vector3 translation, out Matrix4x4 result)
		{
			Matrix4x4ex.QuaternionToRotationMatrix(ref rotation, out result);
			result.m00 *= scaling;
			result.m10 *= scaling;
			result.m20 *= scaling;
			result.m01 *= scaling;
			result.m11 *= scaling;
			result.m21 *= scaling;
			result.m02 *= scaling;
			result.m12 *= scaling;
			result.m22 *= scaling;
			result.m03 = translation.x;
			result.m13 = translation.y;
			result.m23 = translation.z;
		}

		public static void CreateSRT(float scaling, ref Quaternion rotation, ref Vector3 translation, out Matrix4x4 result)
		{
			Matrix4x4ex.QuaternionToRotationMatrix(ref rotation, out result);
			result.m00 *= scaling;
			result.m10 *= scaling;
			result.m20 *= scaling;
			result.m01 *= scaling;
			result.m11 *= scaling;
			result.m21 *= scaling;
			result.m02 *= scaling;
			result.m12 *= scaling;
			result.m22 *= scaling;
			result.m03 = translation.x;
			result.m13 = translation.y;
			result.m23 = translation.z;
		}

		public static void CreateSRT(Vector3 scaling, Vector3 rotationOrigin, Quaternion rotation, Vector3 translation, out Matrix4x4 result)
		{
			Matrix4x4ex.QuaternionToRotationMatrix(ref rotation, out result);
			result.m03 = -(result.m00 * rotationOrigin.x + result.m01 * rotationOrigin.y + result.m02 * rotationOrigin.z - rotationOrigin.x) + translation.x;
			result.m13 = -(result.m10 * rotationOrigin.x + result.m11 * rotationOrigin.y + result.m12 * rotationOrigin.z - rotationOrigin.y) + translation.y;
			result.m23 = -(result.m20 * rotationOrigin.x + result.m21 * rotationOrigin.y + result.m22 * rotationOrigin.z - rotationOrigin.z) + translation.z;
			result.m00 *= scaling.x;
			result.m10 *= scaling.x;
			result.m20 *= scaling.x;
			result.m01 *= scaling.y;
			result.m11 *= scaling.y;
			result.m21 *= scaling.y;
			result.m02 *= scaling.z;
			result.m12 *= scaling.z;
			result.m22 *= scaling.z;
		}

		public static void CreateSRT(ref Vector3 scaling, ref Vector3 rotationOrigin, ref Quaternion rotation, ref Vector3 translation, out Matrix4x4 result)
		{
			Matrix4x4ex.QuaternionToRotationMatrix(ref rotation, out result);
			result.m03 = -(result.m00 * rotationOrigin.x + result.m01 * rotationOrigin.y + result.m02 * rotationOrigin.z - rotationOrigin.x) + translation.x;
			result.m13 = -(result.m10 * rotationOrigin.x + result.m11 * rotationOrigin.y + result.m12 * rotationOrigin.z - rotationOrigin.y) + translation.y;
			result.m23 = -(result.m20 * rotationOrigin.x + result.m21 * rotationOrigin.y + result.m22 * rotationOrigin.z - rotationOrigin.z) + translation.z;
			result.m00 *= scaling.x;
			result.m10 *= scaling.x;
			result.m20 *= scaling.x;
			result.m01 *= scaling.y;
			result.m11 *= scaling.y;
			result.m21 *= scaling.y;
			result.m02 *= scaling.z;
			result.m12 *= scaling.z;
			result.m22 *= scaling.z;
		}

		public static void CreateSRT(float scaling, Vector3 rotationOrigin, Quaternion rotation, Vector3 translation, out Matrix4x4 result)
		{
			Matrix4x4ex.QuaternionToRotationMatrix(ref rotation, out result);
			result.m03 = -(result.m00 * rotationOrigin.x + result.m01 * rotationOrigin.y + result.m02 * rotationOrigin.z - rotationOrigin.x) + translation.x;
			result.m13 = -(result.m10 * rotationOrigin.x + result.m11 * rotationOrigin.y + result.m12 * rotationOrigin.z - rotationOrigin.y) + translation.y;
			result.m23 = -(result.m20 * rotationOrigin.x + result.m21 * rotationOrigin.y + result.m22 * rotationOrigin.z - rotationOrigin.z) + translation.z;
			result.m00 *= scaling;
			result.m10 *= scaling;
			result.m20 *= scaling;
			result.m01 *= scaling;
			result.m11 *= scaling;
			result.m21 *= scaling;
			result.m02 *= scaling;
			result.m12 *= scaling;
			result.m22 *= scaling;
		}

		public static void CreateSRT(float scaling, ref Vector3 rotationOrigin, ref Quaternion rotation, ref Vector3 translation, out Matrix4x4 result)
		{
			Matrix4x4ex.QuaternionToRotationMatrix(ref rotation, out result);
			result.m03 = -(result.m00 * rotationOrigin.x + result.m01 * rotationOrigin.y + result.m02 * rotationOrigin.z - rotationOrigin.x) + translation.x;
			result.m13 = -(result.m10 * rotationOrigin.x + result.m11 * rotationOrigin.y + result.m12 * rotationOrigin.z - rotationOrigin.y) + translation.y;
			result.m23 = -(result.m20 * rotationOrigin.x + result.m21 * rotationOrigin.y + result.m22 * rotationOrigin.z - rotationOrigin.z) + translation.z;
			result.m00 *= scaling;
			result.m10 *= scaling;
			result.m20 *= scaling;
			result.m01 *= scaling;
			result.m11 *= scaling;
			result.m21 *= scaling;
			result.m02 *= scaling;
			result.m12 *= scaling;
			result.m22 *= scaling;
		}

		public static void CreateRT(Quaternion rotation, Vector3 translation, out Matrix4x4 result)
		{
			Matrix4x4ex.QuaternionToRotationMatrix(ref rotation, out result);
			result.m03 = translation.x;
			result.m13 = translation.y;
			result.m23 = translation.z;
		}

		public static void CreateRT(ref Quaternion rotation, ref Vector3 translation, out Matrix4x4 result)
		{
			Matrix4x4ex.QuaternionToRotationMatrix(ref rotation, out result);
			result.m03 = translation.x;
			result.m13 = translation.y;
			result.m23 = translation.z;
		}

		public static void CreateRT(Vector3 rotationOrigin, Quaternion rotation, Vector3 translation, out Matrix4x4 result)
		{
			Matrix4x4ex.QuaternionToRotationMatrix(ref rotation, out result);
			result.m03 = -(result.m00 * rotationOrigin.x + result.m01 * rotationOrigin.y + result.m02 * rotationOrigin.z - rotationOrigin.x) + translation.x;
			result.m13 = -(result.m10 * rotationOrigin.x + result.m11 * rotationOrigin.y + result.m12 * rotationOrigin.z - rotationOrigin.y) + translation.y;
			result.m23 = -(result.m20 * rotationOrigin.x + result.m21 * rotationOrigin.y + result.m22 * rotationOrigin.z - rotationOrigin.z) + translation.z;
		}

		public static void CreateRT(ref Vector3 rotationOrigin, ref Quaternion rotation, ref Vector3 translation, out Matrix4x4 result)
		{
			Matrix4x4ex.QuaternionToRotationMatrix(ref rotation, out result);
			result.m03 = -(result.m00 * rotationOrigin.x + result.m01 * rotationOrigin.y + result.m02 * rotationOrigin.z - rotationOrigin.x) + translation.x;
			result.m13 = -(result.m10 * rotationOrigin.x + result.m11 * rotationOrigin.y + result.m12 * rotationOrigin.z - rotationOrigin.y) + translation.y;
			result.m23 = -(result.m20 * rotationOrigin.x + result.m21 * rotationOrigin.y + result.m22 * rotationOrigin.z - rotationOrigin.z) + translation.z;
		}

		public static void CreateST(Vector3 scaling, Vector3 translation, out Matrix4x4 result)
		{
			result.m00 = scaling.x;
			result.m11 = scaling.y;
			result.m22 = scaling.z;
			result.m03 = translation.x;
			result.m13 = translation.y;
			result.m23 = translation.z;
			result.m01 = 0f;
			result.m02 = 0f;
			result.m10 = 0f;
			result.m12 = 0f;
			result.m20 = 0f;
			result.m21 = 0f;
			result.m30 = 0f;
			result.m31 = 0f;
			result.m32 = 0f;
			result.m33 = 1f;
		}

		public static void CreateST(ref Vector3 scaling, ref Vector3 translation, out Matrix4x4 result)
		{
			result.m00 = scaling.x;
			result.m11 = scaling.y;
			result.m22 = scaling.z;
			result.m03 = translation.x;
			result.m13 = translation.y;
			result.m23 = translation.z;
			result.m01 = 0f;
			result.m02 = 0f;
			result.m10 = 0f;
			result.m12 = 0f;
			result.m20 = 0f;
			result.m21 = 0f;
			result.m30 = 0f;
			result.m31 = 0f;
			result.m32 = 0f;
			result.m33 = 1f;
		}

		public static void CreateRotationFromColumns(Vector3 column0, Vector3 column1, Vector3 column2, out Matrix4x4 matrix)
		{
			matrix = Matrix4x4.identity;
			matrix.SetColumn(0, column0);
			matrix.SetColumn(1, column1);
			matrix.SetColumn(2, column2);
		}

		public static void CreateRotationFromColumns(ref Vector3 column0, ref Vector3 column1, ref Vector3 column2, out Matrix4x4 matrix)
		{
			matrix = Matrix4x4.identity;
			matrix.SetColumn(0, column0);
			matrix.SetColumn(1, column1);
			matrix.SetColumn(2, column2);
		}

		public static void CreateShadowDirectional(Plane3 shadowPlane, Vector3 dirLightOppositeDirection, out Matrix4x4 result)
		{
			Vector3 normal = shadowPlane.Normal;
			float constant = shadowPlane.Constant;
			float num = normal.x * dirLightOppositeDirection.x + normal.y * dirLightOppositeDirection.y + normal.z * dirLightOppositeDirection.z;
			result.m00 = num - dirLightOppositeDirection.x * normal.x;
			result.m01 = -dirLightOppositeDirection.x * normal.y;
			result.m02 = -dirLightOppositeDirection.x * normal.z;
			result.m03 = dirLightOppositeDirection.x * constant;
			result.m10 = -dirLightOppositeDirection.y * normal.x;
			result.m11 = num - dirLightOppositeDirection.y * normal.y;
			result.m12 = -dirLightOppositeDirection.y * normal.z;
			result.m13 = dirLightOppositeDirection.y * constant;
			result.m20 = -dirLightOppositeDirection.z * normal.x;
			result.m21 = -dirLightOppositeDirection.z * normal.y;
			result.m22 = num - dirLightOppositeDirection.z * normal.z;
			result.m23 = dirLightOppositeDirection.z * constant;
			result.m33 = num;
			result.m30 = 0f;
			result.m31 = 0f;
			result.m32 = 0f;
		}

		public static void CreateShadowDirectional(ref Plane3 shadowPlane, ref Vector3 dirLightOppositeDirection, out Matrix4x4 result)
		{
			Vector3 normal = shadowPlane.Normal;
			float constant = shadowPlane.Constant;
			float num = normal.x * dirLightOppositeDirection.x + normal.y * dirLightOppositeDirection.y + normal.z * dirLightOppositeDirection.z;
			result.m00 = num - dirLightOppositeDirection.x * normal.x;
			result.m01 = -dirLightOppositeDirection.x * normal.y;
			result.m02 = -dirLightOppositeDirection.x * normal.z;
			result.m03 = dirLightOppositeDirection.x * constant;
			result.m10 = -dirLightOppositeDirection.y * normal.x;
			result.m11 = num - dirLightOppositeDirection.y * normal.y;
			result.m12 = -dirLightOppositeDirection.y * normal.z;
			result.m13 = dirLightOppositeDirection.y * constant;
			result.m20 = -dirLightOppositeDirection.z * normal.x;
			result.m21 = -dirLightOppositeDirection.z * normal.y;
			result.m22 = num - dirLightOppositeDirection.z * normal.z;
			result.m23 = dirLightOppositeDirection.z * constant;
			result.m33 = num;
			result.m30 = 0f;
			result.m31 = 0f;
			result.m32 = 0f;
		}

		public static void CreateShadowPoint(Plane3 shadowPlane, Vector3 pointLightPosition, out Matrix4x4 result)
		{
			Vector3 normal = shadowPlane.Normal;
			float constant = shadowPlane.Constant;
			float num = normal.x * pointLightPosition.x + normal.y * pointLightPosition.y + normal.z * pointLightPosition.z;
			result.m00 = num + pointLightPosition.x * normal.x - constant;
			result.m01 = -pointLightPosition.x * normal.y;
			result.m02 = -pointLightPosition.x * normal.z;
			result.m03 = pointLightPosition.x * constant;
			result.m10 = -pointLightPosition.y * normal.x;
			result.m11 = num - pointLightPosition.y * normal.y - constant;
			result.m12 = -pointLightPosition.y * normal.z;
			result.m13 = pointLightPosition.y * constant;
			result.m20 = -pointLightPosition.z * normal.x;
			result.m21 = -pointLightPosition.z * normal.y;
			result.m22 = num - pointLightPosition.z * normal.z - constant;
			result.m23 = pointLightPosition.z * constant;
			result.m30 = -normal.x;
			result.m31 = -normal.y;
			result.m32 = -normal.z;
			result.m33 = num;
		}

		public static void CreateShadowPoint(ref Plane3 shadowPlane, ref Vector3 pointLightPosition, out Matrix4x4 result)
		{
			Vector3 normal = shadowPlane.Normal;
			float constant = shadowPlane.Constant;
			float num = normal.x * pointLightPosition.x + normal.y * pointLightPosition.y + normal.z * pointLightPosition.z;
			result.m00 = num + pointLightPosition.x * normal.x - constant;
			result.m01 = -pointLightPosition.x * normal.y;
			result.m02 = -pointLightPosition.x * normal.z;
			result.m03 = pointLightPosition.x * constant;
			result.m10 = -pointLightPosition.y * normal.x;
			result.m11 = num - pointLightPosition.y * normal.y - constant;
			result.m12 = -pointLightPosition.y * normal.z;
			result.m13 = pointLightPosition.y * constant;
			result.m20 = -pointLightPosition.z * normal.x;
			result.m21 = -pointLightPosition.z * normal.y;
			result.m22 = num - pointLightPosition.z * normal.z - constant;
			result.m23 = pointLightPosition.z * constant;
			result.m30 = -normal.x;
			result.m31 = -normal.y;
			result.m32 = -normal.z;
			result.m33 = num;
		}

		public static void CreateShadow(Plane3 shadowPlane, Vector4 lightData, out Matrix4x4 result)
		{
			Vector3 normal = shadowPlane.Normal;
			float constant = shadowPlane.Constant;
			float num = normal.x * lightData.x + normal.y * lightData.y + normal.z * lightData.z;
			result.m00 = num + lightData.x * normal.x - constant * lightData.w;
			result.m01 = -lightData.x * normal.y;
			result.m02 = -lightData.x * normal.z;
			result.m03 = lightData.x * constant;
			result.m10 = -lightData.y * normal.x;
			result.m11 = num - lightData.y * normal.y - constant * lightData.w;
			result.m12 = -lightData.y * normal.z;
			result.m13 = lightData.y * constant;
			result.m20 = -lightData.z * normal.x;
			result.m21 = -lightData.z * normal.y;
			result.m22 = num - lightData.z * normal.z - constant * lightData.w;
			result.m23 = lightData.z * constant;
			result.m30 = -normal.x * lightData.w;
			result.m31 = -normal.y * lightData.w;
			result.m32 = -normal.z * lightData.w;
			result.m33 = num;
		}

		public static void CreateShadow(ref Plane3 shadowPlane, ref Vector4 lightData, out Matrix4x4 result)
		{
			Vector3 normal = shadowPlane.Normal;
			float constant = shadowPlane.Constant;
			float num = normal.x * lightData.x + normal.y * lightData.y + normal.z * lightData.z;
			result.m00 = num + lightData.x * normal.x - constant * lightData.w;
			result.m01 = -lightData.x * normal.y;
			result.m02 = -lightData.x * normal.z;
			result.m03 = lightData.x * constant;
			result.m10 = -lightData.y * normal.x;
			result.m11 = num - lightData.y * normal.y - constant * lightData.w;
			result.m12 = -lightData.y * normal.z;
			result.m13 = lightData.y * constant;
			result.m20 = -lightData.z * normal.x;
			result.m21 = -lightData.z * normal.y;
			result.m22 = num - lightData.z * normal.z - constant * lightData.w;
			result.m23 = lightData.z * constant;
			result.m30 = -normal.x * lightData.w;
			result.m31 = -normal.y * lightData.w;
			result.m32 = -normal.z * lightData.w;
			result.m33 = num;
		}
	}
}

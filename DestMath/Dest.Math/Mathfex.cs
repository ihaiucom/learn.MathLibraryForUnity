using System;
using UnityEngine;

namespace Dest.Math
{
	public static class Mathfex
	{
		public const float ZeroTolerance = 1E-05f;

		public const float NegativeZeroTolerance = -1E-05f;

		public const float ZeroToleranceSqr = 9.99999944E-11f;

		public const float Pi = 3.14159274f;

		public const float HalfPi = 1.57079637f;

		public const float TwoPi = 6.28318548f;

		public static float EvalSquared(float x)
		{
			return x * x;
		}

		public static float EvalInvSquared(float x)
		{
			return Mathf.Sqrt(x);
		}

		public static float EvalCubic(float x)
		{
			return x * x * x;
		}

		public static float EvalInvCubic(float x)
		{
			return Mathf.Pow(x, 0.333333343f);
		}

		public static float EvalQuadratic(float x, float a, float b, float c)
		{
			return a * x * x + b * x + c;
		}

		public static float EvalSigmoid(float x)
		{
			return x * x * (3f - 2f * x);
		}

		public static float EvalOverlappedStep(float x, float overlap, int objectIndex, int objectCount)
		{
			float num = (x - (1f - overlap) * (float)objectIndex / ((float)objectCount - 1f)) / overlap;
			if (num < 0f)
			{
				num = 0f;
			}
			else if (num > 1f)
			{
				num = 1f;
			}
			return num;
		}

		public static float EvalSmoothOverlappedStep(float x, float overlap, int objectIndex, int objectCount)
		{
			float num = (x - (1f - overlap) * (float)objectIndex / ((float)objectCount - 1f)) / overlap;
			if (num < 0f)
			{
				num = 0f;
			}
			else if (num > 1f)
			{
				num = 1f;
			}
			return num * num * (3f - 2f * num);
		}

		public static float EvalGaussian(float x, float a, float b, float c)
		{
			float num = x - b;
			return a * Mathf.Exp(num * num / (-2f * c * c));
		}

		public static float EvalGaussian2D(float x, float y, float x0, float y0, float A, float a, float b, float c)
		{
			float num = x - x0;
			float num2 = y - y0;
			return A * Mathf.Exp(-(a * num * num + 2f * b * num * num2 + c * num2 * num2));
		}

		public static float Lerp(float value0, float value1, float factor)
		{
			if (factor < 0f)
			{
				factor = 0f;
			}
			else if (factor > 1f)
			{
				factor = 1f;
			}
			return value0 + (value1 - value0) * factor;
		}

		public static float LerpUnclamped(float value0, float value1, float factor)
		{
			return value0 + (value1 - value0) * factor;
		}

		public static float SigmoidInterp(float value0, float value1, float factor)
		{
			if (factor < 0f)
			{
				factor = 0f;
			}
			else if (factor > 1f)
			{
				factor = 1f;
			}
			factor = factor * factor * (3f - 2f * factor);
			return value0 + (value1 - value0) * factor;
		}

		public static float SinInterp(float value0, float value1, float factor)
		{
			if (factor < 0f)
			{
				factor = 0f;
			}
			else if (factor > 1f)
			{
				factor = 1f;
			}
			factor = Mathf.Sin(factor * 1.57079637f);
			return value0 + (value1 - value0) * factor;
		}

		public static float CosInterp(float value0, float value1, float factor)
		{
			if (factor < 0f)
			{
				factor = 0f;
			}
			else if (factor > 1f)
			{
				factor = 1f;
			}
			factor = 1f - Mathf.Cos(factor * 1.57079637f);
			return value0 + (value1 - value0) * factor;
		}

		public static float WobbleInterp(float value0, float value1, float factor)
		{
			if (factor < 0f)
			{
				factor = 0f;
			}
			else if (factor > 1f)
			{
				factor = 1f;
			}
			factor = (Mathf.Sin(factor * 3.14159274f * (0.2f + 2.5f * factor * factor * factor)) * Mathf.Pow(1f - factor, 2.2f) + factor) * (1f + 1.2f * (1f - factor));
			return value0 + (value1 - value0) * factor;
		}

		public static float CurveInterp(float value0, float value1, float factor, AnimationCurve curve)
		{
			if (factor < 0f)
			{
				factor = 0f;
			}
			else if (factor > 1f)
			{
				factor = 1f;
			}
			factor = curve.Evaluate(factor);
			return value0 + (value1 - value0) * factor;
		}

		public static float FuncInterp(float value0, float value1, float factor, Func<float, float> func)
		{
			if (factor < 0f)
			{
				factor = 0f;
			}
			else if (factor > 1f)
			{
				factor = 1f;
			}
			float num = func(factor);
			return value0 * (1f - num) + value1 * num;
		}

		public static float InvSqrt(float value)
		{
			if (value != 0f)
			{
				return 1f / Mathf.Sqrt(value);
			}
			return 0f;
		}

		public static bool Near(float value0, float value1, float epsilon = 1E-05f)
		{
			return Mathf.Abs(value0 - value1) < epsilon;
		}

		public static bool NearZero(float value, float epsilon = 1E-05f)
		{
			return Mathf.Abs(value) < epsilon;
		}

		public static Vector2 CartesianToPolar(Vector2 cartesianCoordinates)
		{
			float x = cartesianCoordinates.x;
			float y = cartesianCoordinates.y;
			Vector2 result;
			result.x = Mathf.Sqrt(x * x + y * y);
			if (x > 0f)
			{
				if (y >= 0f)
				{
					result.y = Mathf.Atan(y / x);
				}
				else
				{
					result.y = Mathf.Atan(y / x) + 6.28318548f;
				}
			}
			else if (x < 0f)
			{
				result.y = Mathf.Atan(y / x) + 3.14159274f;
			}
			else if (y > 0f)
			{
				result.y = 1.57079637f;
			}
			else if (y < 0f)
			{
				result.y = 4.712389f;
			}
			else
			{
				result.x = 0f;
				result.y = 0f;
			}
			return result;
		}

		public static Vector2 PolarToCartesian(Vector2 polarCoordinates)
		{
			Vector2 result;
			result.x = polarCoordinates.x * Mathf.Cos(polarCoordinates.y);
			result.y = polarCoordinates.x * Mathf.Sin(polarCoordinates.y);
			return result;
		}

		public static Vector3 CartesianToSpherical(Vector3 cartesianCoordinates)
		{
			float x = cartesianCoordinates.x;
			float y = cartesianCoordinates.y;
			float z = cartesianCoordinates.z;
			float num = Mathf.Sqrt(x * x + y * y + z * z);
			float z2;
			float y2;
			if (num != 0f)
			{
				z2 = Mathf.Acos(y / num);
				if (x > 0f)
				{
					if (z >= 0f)
					{
						y2 = Mathf.Atan(z / x);
					}
					else
					{
						y2 = Mathf.Atan(z / x) + 6.28318548f;
					}
				}
				else if (x < 0f)
				{
					y2 = Mathf.Atan(z / x) + 3.14159274f;
				}
				else if (z > 0f)
				{
					y2 = 1.57079637f;
				}
				else if (z < 0f)
				{
					y2 = 4.712389f;
				}
				else
				{
					y2 = 0f;
				}
			}
			else
			{
				num = 0f;
				y2 = 0f;
				z2 = 0f;
			}
			Vector3 result;
			result.x = num;
			result.y = y2;
			result.z = z2;
			return result;
		}

		public static Vector3 SphericalToCartesian(Vector3 sphericalCoordinates)
		{
			float x = sphericalCoordinates.x;
			float y = sphericalCoordinates.y;
			float z = sphericalCoordinates.z;
			float num = Mathf.Sin(z);
			Vector3 result;
			result.x = x * Mathf.Cos(y) * num;
			result.y = x * Mathf.Cos(z);
			result.z = x * Mathf.Sin(y) * num;
			return result;
		}

		public static Vector3 CartesianToCylindrical(Vector3 cartesianCoordinates)
		{
			float x = cartesianCoordinates.x;
			float z = cartesianCoordinates.z;
			float x2 = Mathf.Sqrt(x * x + z * z);
			float y;
			if (x > 0f)
			{
				if (z >= 0f)
				{
					y = Mathf.Atan(z / x);
				}
				else
				{
					y = Mathf.Atan(z / x) + 6.28318548f;
				}
			}
			else if (x < 0f)
			{
				y = Mathf.Atan(z / x) + 3.14159274f;
			}
			else if (z > 0f)
			{
				y = 1.57079637f;
			}
			else if (z < 0f)
			{
				y = 4.712389f;
			}
			else
			{
				y = 0f;
			}
			Vector3 result;
			result.x = x2;
			result.y = y;
			result.z = cartesianCoordinates.y;
			return result;
		}

		public static Vector3 CylindricalToCartesian(Vector3 cylindricalCoordinates)
		{
			Vector3 result;
			result.x = cylindricalCoordinates.x * Mathf.Cos(cylindricalCoordinates.y);
			result.y = cylindricalCoordinates.z;
			result.z = cylindricalCoordinates.x * Mathf.Sin(cylindricalCoordinates.y);
			return result;
		}
	}
}

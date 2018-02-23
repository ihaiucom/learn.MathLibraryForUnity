using System;
using UnityEngine;

namespace Dest.Math
{
	public class Rand
	{
		private const int a = 5;

		private const int b = 14;

		private const int c = 1;

		private const uint DefaultY = 273326509u;

		private const uint DefaultZ = 3579807591u;

		private const uint DefaultW = 842502087u;

		private const uint PositiveMask = 2147483647u;

		private const uint BoolModuloMask = 1u;

		private const uint ByteModuloMask = 255u;

		private const double One_div_uintMaxValuePlusOne = 2.3283064365386963E-10;

		private const double TwoPi = 6.2831853071795862;

		private static Rand _seedGenerator;

		private uint _x;

		private uint _y;

		private uint _z;

		private uint _w;

		public static Rand Instance;

		static Rand()
		{
			Rand._seedGenerator = new Rand(Environment.TickCount);
			Rand.Instance = new Rand();
		}

		public Rand()
		{
			this.ResetSeed(Rand._seedGenerator.NextInt());
		}

		public Rand(int seed)
		{
			this.ResetSeed(seed);
		}

		public void ResetSeed(int seed)
		{
			this._x = (uint)(seed * 1183186591 + seed * 1431655781 + seed * 338294347 + seed * 622729787);
			this._y = 273326509u;
			this._z = 3579807591u;
			this._w = 842502087u;
		}

		public void GetState(out uint x, out uint y, out uint z, out uint w)
		{
			x = this._x;
			y = this._y;
			z = this._z;
			w = this._w;
		}

		public void SetState(uint x, uint y, uint z, uint w)
		{
			this._x = x;
			this._y = y;
			this._z = z;
			this._w = w;
		}

		public int NextInt()
		{
			uint num = this._x ^ this._x << 5;
			this._x = this._y;
			this._y = this._z;
			this._z = this._w;
			this._w = (this._w ^ this._w >> 1 ^ (num ^ num >> 14));
			return (int)this._w;
		}

		public int NextInt(int max)
		{
			uint num = this._x ^ this._x << 5;
			this._x = this._y;
			this._y = this._z;
			this._z = this._w;
			this._w = (this._w ^ this._w >> 1 ^ (num ^ num >> 14));
			return (int)(this._w * 2.3283064365386963E-10 * (double)max);
		}

		public int NextInt(int min, int max)
		{
			if (min > max)
			{
				Logger.LogError("max must be >= min");
				return 0;
			}
			uint num = this._x ^ this._x << 5;
			this._x = this._y;
			this._y = this._z;
			this._z = this._w;
			this._w = (this._w ^ this._w >> 1 ^ (num ^ num >> 14));
			int num2 = max - min;
			if (num2 >= 0)
			{
				return min + (int)(this._w * 2.3283064365386963E-10 * (double)num2);
			}
			long num3 = (long)min;
			return (int)(num3 + (long)(this._w * 2.3283064365386963E-10 * (double)((long)max - num3)));
		}

		public int NextIntInclusive(int min, int max)
		{
			return this.NextInt(min, max + 1);
		}

		public int NextPositiveInt()
		{
			uint num = this._x ^ this._x << 5;
			this._x = this._y;
			this._y = this._z;
			this._z = this._w;
			this._w = (this._w ^ this._w >> 1 ^ (num ^ num >> 14));
			return (int)(this._w & 2147483647u);
		}

		public uint NextUInt()
		{
			uint num = this._x ^ this._x << 5;
			this._x = this._y;
			this._y = this._z;
			this._z = this._w;
			this._w = (this._w ^ this._w >> 1 ^ (num ^ num >> 14));
			return this._w;
		}

		public double NextDouble()
		{
			uint num = this._x ^ this._x << 5;
			this._x = this._y;
			this._y = this._z;
			this._z = this._w;
			this._w = (this._w ^ this._w >> 1 ^ (num ^ num >> 14));
			return this._w * 2.3283064365386963E-10;
		}

		public double NextDouble(double min, double max)
		{
			if (min > max)
			{
				Logger.LogError("max must be >= min");
				return 0.0;
			}
			uint num = this._x ^ this._x << 5;
			this._x = this._y;
			this._y = this._z;
			this._z = this._w;
			this._w = (this._w ^ this._w >> 1 ^ (num ^ num >> 14));
			return min + (max - min) * (this._w * 2.3283064365386963E-10);
		}

		public float NextFloat()
		{
			uint num = this._x ^ this._x << 5;
			this._x = this._y;
			this._y = this._z;
			this._z = this._w;
			this._w = (this._w ^ this._w >> 1 ^ (num ^ num >> 14));
			return (float)(this._w * 2.3283064365386963E-10);
		}

		public float NextFloat(float min, float max)
		{
			if (min > max)
			{
				Logger.LogError("max must be >= min");
				return 0f;
			}
			uint num = this._x ^ this._x << 5;
			this._x = this._y;
			this._y = this._z;
			this._z = this._w;
			this._w = (this._w ^ this._w >> 1 ^ (num ^ num >> 14));
			return min + (max - min) * (float)(this._w * 2.3283064365386963E-10);
		}

		public bool NextBool()
		{
			uint num = this._x ^ this._x << 5;
			this._x = this._y;
			this._y = this._z;
			this._z = this._w;
			this._w = (this._w ^ this._w >> 1 ^ (num ^ num >> 14));
			return (this._w & 1u) == 0u;
		}

		public byte NextByte()
		{
			uint num = this._x ^ this._x << 5;
			this._x = this._y;
			this._y = this._z;
			this._z = this._w;
			this._w = (this._w ^ this._w >> 1 ^ (num ^ num >> 14));
			return (byte)(this._w & 255u);
		}

		public Color RandomColorOpaque()
		{
			return new Color(this.NextFloat(), this.NextFloat(), this.NextFloat());
		}

		public Color RandomColorTransparent()
		{
			return new Color(this.NextFloat(), this.NextFloat(), this.NextFloat(), this.NextFloat());
		}

		public Color32 RandomColor32Opaque()
		{
			return new Color32(this.NextByte(), this.NextByte(), this.NextByte(), 255);
		}

		public Color32 RandomColor32Transparent()
		{
			return new Color32(this.NextByte(), this.NextByte(), this.NextByte(), this.NextByte());
		}

		public float RandomAngleRadians()
		{
			return this.NextFloat() * 6.28318548f;
		}

		public float RandomAngleDegrees()
		{
			return this.NextFloat() * 360f;
		}

		public Vector2 InSquare(float side = 1f)
		{
			float num = side * 0.5f;
			float min = -num;
			return new Vector2(this.NextFloat(min, num), this.NextFloat(min, num));
		}

		public Vector2 OnSquare(float side = 1f)
		{
			float num = side * 0.5f;
			float num2 = -num;
			switch (this.NextInt(0, 4))
			{
			case 0:
				return new Vector2(this.NextFloat(num2, num), num);
			case 1:
				return new Vector2(this.NextFloat(num2, num), num2);
			case 2:
				return new Vector2(num, this.NextFloat(num2, num));
			case 3:
				return new Vector2(num2, this.NextFloat(num2, num));
			default:
				Logger.LogError("Should not get here!");
				return Vector2ex.Zero;
			}
		}

		public Vector3 InCube(float side = 1f)
		{
			float num = side * 0.5f;
			float min = -num;
			return new Vector3(this.NextFloat(min, num), this.NextFloat(min, num), this.NextFloat(min, num));
		}

		public Vector3 OnCube(float side = 1f)
		{
			float num = side * 0.5f;
			float num2 = -num;
			switch (this.NextInt(0, 6))
			{
			case 0:
				return new Vector3(this.NextFloat(num2, num), this.NextFloat(num2, num), num);
			case 1:
				return new Vector3(this.NextFloat(num2, num), this.NextFloat(num2, num), num2);
			case 2:
				return new Vector3(this.NextFloat(num2, num), num, this.NextFloat(num2, num));
			case 3:
				return new Vector3(this.NextFloat(num2, num), num2, this.NextFloat(num2, num));
			case 4:
				return new Vector3(num, this.NextFloat(num2, num), this.NextFloat(num2, num));
			case 5:
				return new Vector3(num2, this.NextFloat(num2, num), this.NextFloat(num2, num));
			default:
				Logger.LogError("Should not get here!");
				return Vector3ex.Zero;
			}
		}

		public Vector2 InCircle(float radius = 1f)
		{
			float num = radius * Mathf.Sqrt(this.NextFloat());
			float f = this.RandomAngleRadians();
			return new Vector2(num * Mathf.Cos(f), num * Mathf.Sin(f));
		}

		public Vector2 InCircle(float radiusMin, float radiusMax)
		{
			float num = 2f / (radiusMax * radiusMax - radiusMin * radiusMin);
			float num2 = Mathf.Sqrt(2f * this.NextFloat() / num + radiusMin * radiusMin);
			float f = this.RandomAngleRadians();
			return new Vector2(num2 * Mathf.Cos(f), num2 * Mathf.Sin(f));
		}

		public Vector2 OnCircle(float radius = 1f)
		{
			float f = this.RandomAngleRadians();
			return new Vector2(Mathf.Cos(f) * radius, Mathf.Sin(f) * radius);
		}

		public Vector3 InSphere(float radius = 1f)
		{
			float num = radius * 2f;
			float num2 = radius * radius;
			float num3;
			float num4;
			float num5;
			do
			{
				num3 = this.NextFloat() * num - radius;
				num4 = this.NextFloat() * num - radius;
				num5 = this.NextFloat() * num - radius;
			}
			while (num3 * num3 + num4 * num4 + num5 * num5 > num2);
			return new Vector3(num3, num4, num5);
		}

		public Vector3 OnSphere(float radius = 1f)
		{
			float num = this.NextFloat() * 2f - 1f;
			float f = this.NextFloat() * 6.28318548f;
			float num2 = Mathf.Sqrt(1f - num * num) * radius;
			return new Vector3(num2 * Mathf.Cos(f), num2 * Mathf.Sin(f), num * radius);
		}

		public Vector3 InTriangle(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2)
		{
			double num = Math.Sqrt(this.NextDouble());
			double num2 = this.NextDouble();
			Vector3 vector = (float)(1.0 - num) * v0;
			Vector3 vector2 = (float)(num * (1.0 - num2)) * v1;
			Vector3 vector3 = (float)(num2 * num) * v2;
			return new Vector3(vector.x + vector2.x + vector3.x, vector.y + vector2.y + vector3.y, vector.z + vector2.z + vector3.z);
		}

		public Vector3 InTriangle(Vector3 v0, Vector3 v1, Vector3 v2)
		{
			double num = Math.Sqrt(this.NextDouble());
			double num2 = this.NextDouble();
			Vector3 vector = (float)(1.0 - num) * v0;
			Vector3 vector2 = (float)(num * (1.0 - num2)) * v1;
			Vector3 vector3 = (float)(num2 * num) * v2;
			return new Vector3(vector.x + vector2.x + vector3.x, vector.y + vector2.y + vector3.y, vector.z + vector2.z + vector3.z);
		}

		public Quaternion RandomRotation()
		{
			double num = this.NextDouble();
			double num2 = this.NextDouble();
			double num3 = this.NextDouble();
			double num4 = Math.Sqrt(num);
			double num5 = Math.Sqrt(1.0 - num);
			double d = 6.2831853071795862 * num2;
			double d2 = 6.2831853071795862 * num3;
			return new Quaternion((float)(num5 * Math.Sin(d)), (float)(num5 * Math.Cos(d)), (float)(num4 * Math.Sin(d2)), (float)(num4 * Math.Cos(d2)));
		}
	}
}

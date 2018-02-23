using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Circle3
	{
		public Vector3 Center;

		public Vector3 Axis0;

		public Vector3 Axis1;

		public Vector3 Normal;

		public float Radius;

		public Circle3(ref Vector3 center, ref Vector3 axis0, ref Vector3 axis1, float radius)
		{
			this.Center = center;
			this.Axis0 = axis0;
			this.Axis1 = axis1;
			this.Normal = axis0.Cross(axis1);
			this.Radius = radius;
		}

		public Circle3(Vector3 center, Vector3 axis0, Vector3 axis1, float radius)
		{
			this.Center = center;
			this.Axis0 = axis0;
			this.Axis1 = axis1;
			this.Normal = axis0.Cross(axis1);
			this.Radius = radius;
		}

		public Circle3(ref Vector3 center, ref Vector3 normal, float radius)
		{
			this.Center = center;
			this.Normal = normal;
			Vector3ex.CreateOrthonormalBasis(out this.Axis0, out this.Axis1, ref this.Normal);
			this.Radius = radius;
		}

		public Circle3(Vector3 center, Vector3 normal, float radius)
		{
			this.Center = center;
			this.Normal = normal;
			Vector3ex.CreateOrthonormalBasis(out this.Axis0, out this.Axis1, ref this.Normal);
			this.Radius = radius;
		}

		public static bool CreateCircumscribed(Vector3 v0, Vector3 v1, Vector3 v2, out Circle3 circle)
		{
			Vector3 vector = v0 - v2;
			Vector3 vector2 = v1 - v2;
			float num = vector.Dot(vector);
			float num2 = vector.Dot(vector2);
			float num3 = vector2.Dot(vector2);
			float num4 = num * num3 - num2 * num2;
			if (Mathf.Abs(num4) < 1E-05f)
			{
				circle = default(Circle3);
				return false;
			}
			float num5 = 0.5f / num4;
			float d = num5 * num3 * (num - num2);
			float d2 = num5 * num * (num3 - num2);
			Vector3 b = d * vector + d2 * vector2;
			circle.Center = v2 + b;
			circle.Radius = b.magnitude;
			circle.Normal = vector.UnitCross(vector2);
			if (Mathf.Abs(circle.Normal.x) >= Mathf.Abs(circle.Normal.y) && Mathf.Abs(circle.Normal.x) >= Mathf.Abs(circle.Normal.z))
			{
				circle.Axis0.x = -circle.Normal.y;
				circle.Axis0.y = circle.Normal.x;
				circle.Axis0.z = 0f;
			}
			else
			{
				circle.Axis0.x = 0f;
				circle.Axis0.y = circle.Normal.z;
				circle.Axis0.z = -circle.Normal.y;
			}
			circle.Axis0.Normalize();
			circle.Axis1 = circle.Normal.Cross(circle.Axis0);
			return true;
		}

		public static bool CreateInscribed(Vector3 v0, Vector3 v1, Vector3 v2, out Circle3 circle)
		{
			Vector3 value = v1 - v0;
			Vector3 vector = v2 - v1;
			Vector3 value2 = v0 - v2;
			circle.Normal = vector.Cross(value);
			Vector3 vector2 = circle.Normal.UnitCross(value);
			Vector3 vector3 = circle.Normal.UnitCross(vector);
			Vector3 vector4 = circle.Normal.UnitCross(value2);
			float num = vector3.Dot(value);
			if (Mathf.Abs(num) < 1E-05f)
			{
				circle = default(Circle3);
				return false;
			}
			float num2 = vector4.Dot(vector);
			if (Mathf.Abs(num2) < 1E-05f)
			{
				circle = default(Circle3);
				return false;
			}
			float num3 = vector2.Dot(value2);
			if (Mathf.Abs(num3) < 1E-05f)
			{
				circle = default(Circle3);
				return false;
			}
			float num4 = 1f / num;
			float num5 = 1f / num2;
			float num6 = 1f / num3;
			circle.Radius = 1f / (num4 + num5 + num6);
			circle.Center = circle.Radius * (num4 * v0 + num5 * v1 + num6 * v2);
			circle.Normal.Normalize();
			circle.Axis0 = vector2;
			circle.Axis1 = circle.Normal.Cross(circle.Axis0);
			return true;
		}

		public float CalcPerimeter()
		{
			return 6.28318548f * this.Radius;
		}

		public float CalcArea()
		{
			return 3.14159274f * this.Radius * this.Radius;
		}

		public Vector3 Eval(float t)
		{
			return this.Center + this.Radius * (Mathf.Cos(t) * this.Axis0 + Mathf.Sin(t) * this.Axis1);
		}

		public Vector3 Eval(float t, float radius)
		{
			return this.Center + radius * (Mathf.Cos(t) * this.Axis0 + Mathf.Sin(t) * this.Axis1);
		}

		public float DistanceTo(Vector3 point, bool solid = true)
		{
			return Distance.Point3Circle3(ref point, ref this, solid);
		}

		public Vector3 Project(Vector3 point, bool solid = true)
		{
			Vector3 result;
			Distance.SqrPoint3Circle3(ref point, ref this, out result, solid);
			return result;
		}

		public override string ToString()
		{
			return string.Format("[Center: {0} Axis0: {1} Axis1: {2} Normal: {3} Radius: {4}]", new object[]
			{
				this.Center.ToStringEx(),
				this.Axis0.ToStringEx(),
				this.Axis1.ToStringEx(),
				this.Normal.ToStringEx(),
				this.Radius.ToString()
			});
		}
	}
}

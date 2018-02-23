using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dest.Math
{
	public struct Sphere3
	{
		private const float _4div3mulPi = 4.18879032f;

		public Vector3 Center;

		public float Radius;

		public Sphere3(ref Vector3 center, float radius)
		{
			this.Center = center;
			this.Radius = radius;
		}

		public Sphere3(Vector3 center, float radius)
		{
			this.Center = center;
			this.Radius = radius;
		}

		public static Sphere3 CreateFromPointsAAB(IEnumerable<Vector3> points)
		{
			IEnumerator<Vector3> enumerator = points.GetEnumerator();
			enumerator.Reset();
			if (!enumerator.MoveNext())
			{
				return default(Sphere3);
			}
			Vector3 center;
			Vector3 vector;
			AAB3.CreateFromPoints(points).CalcCenterExtents(out center, out vector);
			Sphere3 result;
			result.Center = center;
			result.Radius = vector.magnitude;
			return result;
		}

		public static Sphere3 CreateFromPointsAAB(IList<Vector3> points)
		{
			if (points.Count == 0)
			{
				return default(Sphere3);
			}
			Vector3 center;
			Vector3 vector;
			AAB3.CreateFromPoints(points).CalcCenterExtents(out center, out vector);
			Sphere3 result;
			result.Center = center;
			result.Radius = vector.magnitude;
			return result;
		}

		public static Sphere3 CreateFromPointsAverage(IEnumerable<Vector3> points)
		{
			IEnumerator<Vector3> enumerator = points.GetEnumerator();
			enumerator.Reset();
			if (!enumerator.MoveNext())
			{
				return default(Sphere3);
			}
			Vector3 vector = enumerator.Current;
			int num = 1;
			while (enumerator.MoveNext())
			{
				vector += enumerator.Current;
				num++;
			}
			vector /= (float)num;
			float num2 = 0f;
			foreach (Vector3 current in points)
			{
				float sqrMagnitude = (current - vector).sqrMagnitude;
				if (sqrMagnitude > num2)
				{
					num2 = sqrMagnitude;
				}
			}
			Sphere3 result;
			result.Center = vector;
			result.Radius = Mathf.Sqrt(num2);
			return result;
		}

		public static Sphere3 CreateFromPointsAverage(IList<Vector3> points)
		{
			int count = points.Count;
			if (count == 0)
			{
				return default(Sphere3);
			}
			Vector3 vector = points[0];
			for (int i = 1; i < count; i++)
			{
				vector += points[i];
			}
			vector /= (float)count;
			float num = 0f;
			for (int j = 0; j < count; j++)
			{
				float sqrMagnitude = (points[j] - vector).sqrMagnitude;
				if (sqrMagnitude > num)
				{
					num = sqrMagnitude;
				}
			}
			Sphere3 result;
			result.Center = vector;
			result.Radius = Mathf.Sqrt(num);
			return result;
		}

		public static bool CreateCircumscribed(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3, out Sphere3 sphere)
		{
			Vector3 vector = v1 - v0;
			Vector3 vector2 = v2 - v0;
			Vector3 vector3 = v3 - v0;
			float[,] array = new float[3, 3];
			array[0, 0] = vector.x;
			array[0, 1] = vector.y;
			array[0, 2] = vector.z;
			array[1, 0] = vector2.x;
			array[1, 1] = vector2.y;
			array[1, 2] = vector2.z;
			array[2, 0] = vector3.x;
			array[2, 1] = vector3.y;
			array[2, 2] = vector3.z;
			float[,] a = array;
			float[] b = new float[]
			{
				0.5f * vector.sqrMagnitude,
				0.5f * vector2.sqrMagnitude,
				0.5f * vector3.sqrMagnitude
			};
			Vector3 b2;
			if (LinearSystem.Solve3(a, b, out b2, 1E-05f))
			{
				sphere.Center = v0 + b2;
				sphere.Radius = b2.magnitude;
				return true;
			}
			sphere = default(Sphere3);
			return false;
		}

		public static bool CreateInscribed(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3, out Sphere3 sphere)
		{
			Vector3 vector = v1 - v0;
			Vector3 vector2 = v2 - v0;
			Vector3 vector3 = v3 - v0;
			Vector3 value = v2 - v1;
			Vector3 vector4 = v3 - v1;
			Vector3 vector5 = vector4.Cross(value);
			Vector3 vector6 = vector2.Cross(vector3);
			Vector3 vector7 = vector3.Cross(vector);
			Vector3 vector8 = vector.Cross(vector2);
			if (Mathf.Abs(Vector3ex.Normalize(ref vector5, 1E-05f)) < 1E-05f)
			{
				sphere = default(Sphere3);
				return false;
			}
			if (Mathf.Abs(Vector3ex.Normalize(ref vector6, 1E-05f)) < 1E-05f)
			{
				sphere = default(Sphere3);
				return false;
			}
			if (Mathf.Abs(Vector3ex.Normalize(ref vector7, 1E-05f)) < 1E-05f)
			{
				sphere = default(Sphere3);
				return false;
			}
			if (Mathf.Abs(Vector3ex.Normalize(ref vector8, 1E-05f)) < 1E-05f)
			{
				sphere = default(Sphere3);
				return false;
			}
			float[,] array = new float[3, 3];
			array[0, 0] = vector6.x - vector5.x;
			array[0, 1] = vector6.y - vector5.y;
			array[0, 2] = vector6.z - vector5.z;
			array[1, 0] = vector7.x - vector5.x;
			array[1, 1] = vector7.y - vector5.y;
			array[1, 2] = vector7.z - vector5.z;
			array[2, 0] = vector8.x - vector5.x;
			array[2, 1] = vector8.y - vector5.y;
			array[2, 2] = vector8.z - vector5.z;
			float[,] a = array;
			float[] b = new float[]
			{
				0f,
				0f,
				-vector8.Dot(vector3)
			};
			Vector3 vector9;
			if (LinearSystem.Solve3(a, b, out vector9, 1E-05f))
			{
				sphere.Center = v3 + vector9;
				sphere.Radius = Mathf.Abs(vector5.Dot(vector9));
				return true;
			}
			sphere = default(Sphere3);
			return false;
		}

		public float CalcArea()
		{
			return 12.566371f * this.Radius * this.Radius;
		}

		public float CalcVolume()
		{
			return 4.18879032f * this.Radius * this.Radius * this.Radius;
		}

		public Vector3 Eval(float theta, float phi)
		{
			float num = Mathf.Sin(phi);
			return new Vector3(this.Center.x + this.Radius * Mathf.Cos(theta) * num, this.Center.y + this.Radius * Mathf.Sin(theta) * num, this.Center.z + this.Radius * Mathf.Cos(phi));
		}

		public float DistanceTo(Vector3 point)
		{
			return Distance.Point3Sphere3(ref point, ref this);
		}

		public Vector3 Project(Vector3 point)
		{
			Vector3 result;
			Distance.SqrPoint3Sphere3(ref point, ref this, out result);
			return result;
		}

		public bool Contains(ref Vector3 point)
		{
			return (point - this.Center).sqrMagnitude <= this.Radius * this.Radius;
		}

		public bool Contains(Vector3 point)
		{
			return (point - this.Center).sqrMagnitude <= this.Radius * this.Radius;
		}

		public void Include(ref Sphere3 sphere)
		{
			Vector3 a = sphere.Center - this.Center;
			float sqrMagnitude = a.sqrMagnitude;
			float num = sphere.Radius - this.Radius;
			float num2 = num * num;
			if (num2 >= sqrMagnitude)
			{
				if (num >= 0f)
				{
					this = sphere;
				}
				return;
			}
			float num3 = Mathf.Sqrt(sqrMagnitude);
			if (num3 > 1E-05f)
			{
				float d = (num3 + num) / (2f * num3);
				this.Center += d * a;
			}
			this.Radius = 0.5f * (num3 + this.Radius + sphere.Radius);
		}

		public void Include(Sphere3 sphere)
		{
			this.Include(ref sphere);
		}

		public override string ToString()
		{
			return string.Format("[Center: {0} Radius: {1}]", this.Center.ToStringEx(), this.Radius.ToString());
		}
	}
}

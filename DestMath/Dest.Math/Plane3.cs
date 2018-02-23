using System;
using UnityEngine;

namespace Dest.Math
{
	public struct Plane3
	{
		public Vector3 Normal;

		public float Constant;

		public Plane3(ref Vector3 normal, float constant)
		{
			this.Normal = normal;
			this.Constant = constant;
		}

		public Plane3(Vector3 normal, float constant)
		{
			this.Normal = normal;
			this.Constant = constant;
		}

		public Plane3(ref Vector3 normal, ref Vector3 point)
		{
			this.Normal = normal;
			this.Constant = normal.Dot(point);
		}

		public Plane3(Vector3 normal, Vector3 point)
		{
			this.Normal = normal;
			this.Constant = normal.Dot(point);
		}

		public Plane3(ref Vector3 p0, ref Vector3 p1, ref Vector3 p2)
		{
			Vector3 vector = p1 - p0;
			Vector3 value = p2 - p0;
			this.Normal = vector.UnitCross(value);
			this.Constant = this.Normal.Dot(p0);
		}

		public Plane3(Vector3 p0, Vector3 p1, Vector3 p2)
		{
			Vector3 vector = p1 - p0;
			Vector3 value = p2 - p0;
			this.Normal = vector.UnitCross(value);
			this.Constant = this.Normal.Dot(p0);
		}

		public static implicit operator Plane(Plane3 value)
		{
			return new Plane(value.Normal, -value.Constant);
		}

		public static implicit operator Plane3(Plane value)
		{
			return new Plane3
			{
				Normal = value.normal,
				Constant = -value.distance
			};
		}

		public Vector3 CalcOrigin()
		{
			return this.Normal * this.Constant;
		}

		public void CreateOrthonormalBasis(out Vector3 u, out Vector3 v, out Vector3 n)
		{
			n = this.Normal;
			if (Mathf.Abs(n.x) >= Mathf.Abs(n.y))
			{
				float num = Mathfex.InvSqrt(n.x * n.x + n.z * n.z);
				u.x = n.z * num;
				u.y = 0f;
				u.z = -n.x * num;
			}
			else
			{
				float num2 = Mathfex.InvSqrt(n.y * n.y + n.z * n.z);
				u.x = 0f;
				u.y = n.z * num2;
				u.z = -n.y * num2;
			}
			v = Vector3.Cross(n, u);
		}

		internal float SignedDistanceTo(ref Vector3 point)
		{
			return this.Normal.Dot(point) - this.Constant;
		}

		public float SignedDistanceTo(Vector3 point)
		{
			return this.Normal.Dot(point) - this.Constant;
		}

		public float DistanceTo(Vector3 point)
		{
			return Mathf.Abs(this.Normal.Dot(point) - this.Constant);
		}

		public int QuerySide(Vector3 point, float epsilon = 1E-05f)
		{
			float num = this.Normal.Dot(point) - this.Constant;
			if (num < -epsilon)
			{
				return -1;
			}
			if (num > epsilon)
			{
				return 1;
			}
			return 0;
		}

		public bool QuerySideNegative(Vector3 point, float epsilon = 1E-05f)
		{
			float num = this.Normal.Dot(point) - this.Constant;
			return num <= epsilon;
		}

		public bool QuerySidePositive(Vector3 point, float epsilon = 1E-05f)
		{
			float num = this.Normal.Dot(point) - this.Constant;
			return num >= -epsilon;
		}

		public int QuerySide(ref Box3 box, float epsilon = 1E-05f)
		{
			float f = box.Extents.x * this.Normal.Dot(box.Axis0);
			float f2 = box.Extents.y * this.Normal.Dot(box.Axis1);
			float f3 = box.Extents.z * this.Normal.Dot(box.Axis2);
			float num = Mathf.Abs(f) + Mathf.Abs(f2) + Mathf.Abs(f3);
			float num2 = this.Normal.Dot(box.Center) - this.Constant;
			if (num2 < -num + epsilon)
			{
				return -1;
			}
			if (num2 <= num - epsilon)
			{
				return 0;
			}
			return 1;
		}

		public bool QuerySideNegative(ref Box3 box, float epsilon = 1E-05f)
		{
			float f = box.Extents.x * this.Normal.Dot(box.Axis0);
			float f2 = box.Extents.y * this.Normal.Dot(box.Axis1);
			float f3 = box.Extents.z * this.Normal.Dot(box.Axis2);
			float num = Mathf.Abs(f) + Mathf.Abs(f2) + Mathf.Abs(f3);
			float num2 = this.Normal.Dot(box.Center) - this.Constant;
			return num2 <= -num + epsilon;
		}

		public bool QuerySidePositive(ref Box3 box, float epsilon = 1E-05f)
		{
			float f = box.Extents.x * this.Normal.Dot(box.Axis0);
			float f2 = box.Extents.y * this.Normal.Dot(box.Axis1);
			float f3 = box.Extents.z * this.Normal.Dot(box.Axis2);
			float num = Mathf.Abs(f) + Mathf.Abs(f2) + Mathf.Abs(f3);
			float num2 = this.Normal.Dot(box.Center) - this.Constant;
			return num2 >= num - epsilon;
		}

		public int QuerySide(ref AAB3 box, float epsilon = 1E-05f)
		{
			Vector3 value;
			Vector3 value2;
			if (this.Normal.x >= 0f)
			{
				value.x = box.Min.x;
				value2.x = box.Max.x;
			}
			else
			{
				value.x = box.Max.x;
				value2.x = box.Min.x;
			}
			if (this.Normal.y >= 0f)
			{
				value.y = box.Min.y;
				value2.y = box.Max.y;
			}
			else
			{
				value.y = box.Max.y;
				value2.y = box.Min.y;
			}
			if (this.Normal.z >= 0f)
			{
				value.z = box.Min.z;
				value2.z = box.Max.z;
			}
			else
			{
				value.z = box.Max.z;
				value2.z = box.Min.z;
			}
			if (this.Normal.Dot(value) - this.Constant > -epsilon)
			{
				return 1;
			}
			if (this.Normal.Dot(value2) - this.Constant < epsilon)
			{
				return -1;
			}
			return 0;
		}

		public bool QuerySideNegative(ref AAB3 box, float epsilon = 1E-05f)
		{
			Vector3 value;
			if (this.Normal.x >= 0f)
			{
				value.x = box.Max.x;
			}
			else
			{
				value.x = box.Min.x;
			}
			if (this.Normal.y >= 0f)
			{
				value.y = box.Max.y;
			}
			else
			{
				value.y = box.Min.y;
			}
			if (this.Normal.z >= 0f)
			{
				value.z = box.Max.z;
			}
			else
			{
				value.z = box.Min.z;
			}
			return this.Normal.Dot(value) - this.Constant <= epsilon;
		}

		public bool QuerySidePositive(ref AAB3 box, float epsilon = 1E-05f)
		{
			Vector3 value;
			if (this.Normal.x >= 0f)
			{
				value.x = box.Min.x;
			}
			else
			{
				value.x = box.Max.x;
			}
			if (this.Normal.y >= 0f)
			{
				value.y = box.Min.y;
			}
			else
			{
				value.y = box.Max.y;
			}
			if (this.Normal.z >= 0f)
			{
				value.z = box.Min.z;
			}
			else
			{
				value.z = box.Max.z;
			}
			return this.Normal.Dot(value) - this.Constant >= -epsilon;
		}

		public int QuerySide(ref Sphere3 sphere, float epsilon = 1E-05f)
		{
			float num = this.Normal.Dot(sphere.Center) - this.Constant;
			if (num > sphere.Radius - epsilon)
			{
				return 1;
			}
			if (num >= -sphere.Radius + epsilon)
			{
				return 0;
			}
			return -1;
		}

		public bool QuerySideNegative(ref Sphere3 sphere, float epsilon = 1E-05f)
		{
			float num = this.Normal.Dot(sphere.Center) - this.Constant;
			return num <= -sphere.Radius + epsilon;
		}

		public bool QuerySidePositive(ref Sphere3 sphere, float epsilon = 1E-05f)
		{
			float num = this.Normal.Dot(sphere.Center) - this.Constant;
			return num >= sphere.Radius - epsilon;
		}

		public Vector3 Project(Vector3 point)
		{
			Vector3 result;
			Distance.SqrPoint3Plane3(ref point, ref this, out result);
			return result;
		}

		public Vector3 ProjectVector(Vector3 vector)
		{
			return vector - this.Normal.Dot(vector) * this.Normal;
		}

		public float AngleBetweenPlaneNormalAndLine(Line3 line)
		{
			float num = this.Normal.Dot(line.Direction);
			if (num > 1f)
			{
				num = 1f;
			}
			else if (num < -1f)
			{
				num = -1f;
			}
			return Mathf.Acos(num);
		}

		public float AngleBetweenPlaneNormalAndLine(Vector3 direction)
		{
			Vector3ex.Normalize(ref direction, 1E-05f);
			float num = this.Normal.Dot(direction);
			if (num > 1f)
			{
				num = 1f;
			}
			else if (num < -1f)
			{
				num = -1f;
			}
			return Mathf.Acos(num);
		}

		public float AngleBetweenPlaneAndLine(Line3 line)
		{
			float num = this.Normal.Dot(line.Direction);
			if (num > 1f)
			{
				num = 1f;
			}
			else if (num < -1f)
			{
				num = -1f;
			}
			return 1.57079637f - Mathf.Acos(num);
		}

		public float AngleBetweenPlaneAndLine(Vector3 direction)
		{
			Vector3ex.Normalize(ref direction, 1E-05f);
			float num = this.Normal.Dot(direction);
			if (num > 1f)
			{
				num = 1f;
			}
			else if (num < -1f)
			{
				num = -1f;
			}
			return 1.57079637f - Mathf.Acos(num);
		}

		public float AngleBetweenTwoPlanes(Plane3 anotherPlane)
		{
			float num = this.Normal.Dot(anotherPlane.Normal);
			if (num > 1f)
			{
				num = 1f;
			}
			else if (num < -1f)
			{
				num = -1f;
			}
			return Mathf.Acos(num);
		}

		public override string ToString()
		{
			return string.Format("[Normal: {0} Constant: {1}]", this.Normal.ToStringEx(), this.Constant.ToString());
		}
	}
}

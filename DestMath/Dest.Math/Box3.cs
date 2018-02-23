using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dest.Math
{
	public struct Box3
	{
		public Vector3 Center;

		public Vector3 Axis0;

		public Vector3 Axis1;

		public Vector3 Axis2;

		public Vector3 Extents;

		public Box3(ref Vector3 center, ref Vector3 axis0, ref Vector3 axis1, ref Vector3 axis2, ref Vector3 extents)
		{
			this.Center = center;
			this.Axis0 = axis0;
			this.Axis1 = axis1;
			this.Axis2 = axis2;
			this.Extents = extents;
		}

		public Box3(Vector3 center, Vector3 axis0, Vector3 axis1, Vector3 axis2, Vector3 extents)
		{
			this.Center = center;
			this.Axis0 = axis0;
			this.Axis1 = axis1;
			this.Axis2 = axis2;
			this.Extents = extents;
		}

		public Box3(ref AAB3 box)
		{
			box.CalcCenterExtents(out this.Center, out this.Extents);
			this.Axis0 = Vector3ex.UnitX;
			this.Axis1 = Vector3ex.UnitY;
			this.Axis2 = Vector3ex.UnitZ;
		}

		public Box3(AAB3 box)
		{
			box.CalcCenterExtents(out this.Center, out this.Extents);
			this.Axis0 = Vector3ex.UnitX;
			this.Axis1 = Vector3ex.UnitY;
			this.Axis2 = Vector3ex.UnitZ;
		}

		public static Box3 CreateFromPoints(IList<Vector3> points)
		{
			int count = points.Count;
			if (count == 0)
			{
				return default(Box3);
			}
			Box3 result = Approximation.GaussPointsFit3(points);
			Vector3 vector = points[0] - result.Center;
			Vector3 vector2 = new Vector3(vector.Dot(result.Axis0), vector.Dot(result.Axis1), vector.Dot(result.Axis2));
			Vector3 vector3 = vector2;
			for (int i = 1; i < count; i++)
			{
				vector = points[i] - result.Center;
				for (int j = 0; j < 3; j++)
				{
					float num = vector.Dot(result.GetAxis(j));
					if (num < vector2[j])
					{
						vector2[j] = num;
					}
					else if (num > vector3[j])
					{
						vector3[j] = num;
					}
				}
			}
			result.Center += 0.5f * (vector2[0] + vector3[0]) * result.Axis0 + 0.5f * (vector2[1] + vector3[1]) * result.Axis1 + 0.5f * (vector2[2] + vector3[2]) * result.Axis2;
			result.Extents.x = 0.5f * (vector3[0] - vector2[0]);
			result.Extents.y = 0.5f * (vector3[1] - vector2[1]);
			result.Extents.z = 0.5f * (vector3[2] - vector2[2]);
			return result;
		}

		public Vector3 GetAxis(int index)
		{
			if (index == 0)
			{
				return this.Axis0;
			}
			if (index == 1)
			{
				return this.Axis1;
			}
			if (index == 2)
			{
				return this.Axis2;
			}
			return Vector3ex.Zero;
		}

		public void CalcVertices(out Vector3 vertex0, out Vector3 vertex1, out Vector3 vertex2, out Vector3 vertex3, out Vector3 vertex4, out Vector3 vertex5, out Vector3 vertex6, out Vector3 vertex7)
		{
			Vector3 b = this.Extents.x * this.Axis0;
			Vector3 b2 = this.Extents.y * this.Axis1;
			Vector3 b3 = this.Extents.z * this.Axis2;
			vertex0 = this.Center - b - b2 - b3;
			vertex1 = this.Center + b - b2 - b3;
			vertex2 = this.Center + b + b2 - b3;
			vertex3 = this.Center - b + b2 - b3;
			vertex4 = this.Center - b - b2 + b3;
			vertex5 = this.Center + b - b2 + b3;
			vertex6 = this.Center + b + b2 + b3;
			vertex7 = this.Center - b + b2 + b3;
		}

		public Vector3[] CalcVertices()
		{
			Vector3 b = this.Extents.x * this.Axis0;
			Vector3 b2 = this.Extents.y * this.Axis1;
			Vector3 b3 = this.Extents.z * this.Axis2;
			return new Vector3[]
			{
				this.Center - b - b2 - b3,
				this.Center + b - b2 - b3,
				this.Center + b + b2 - b3,
				this.Center - b + b2 - b3,
				this.Center - b - b2 + b3,
				this.Center + b - b2 + b3,
				this.Center + b + b2 + b3,
				this.Center - b + b2 + b3
			};
		}

		public void CalcVertices(Vector3[] array)
		{
			Vector3 b = this.Extents.x * this.Axis0;
			Vector3 b2 = this.Extents.y * this.Axis1;
			Vector3 b3 = this.Extents.z * this.Axis2;
			array[0] = this.Center - b - b2 - b3;
			array[1] = this.Center + b - b2 - b3;
			array[2] = this.Center + b + b2 - b3;
			array[3] = this.Center - b + b2 - b3;
			array[4] = this.Center - b - b2 + b3;
			array[5] = this.Center + b - b2 + b3;
			array[6] = this.Center + b + b2 + b3;
			array[7] = this.Center - b + b2 + b3;
		}

		public float CalcVolume()
		{
			return 8f * this.Extents.x * this.Extents.y * this.Extents.z;
		}

		public float DistanceTo(Vector3 point)
		{
			return Distance.Point3Box3(ref point, ref this);
		}

		public Vector3 Project(Vector3 point)
		{
			Vector3 result;
			Distance.SqrPoint3Box3(ref point, ref this, out result);
			return result;
		}

		public bool Contains(ref Vector3 point)
		{
			Vector3 vector;
			vector.x = point.x - this.Center.x;
			vector.y = point.y - this.Center.y;
			vector.z = point.z - this.Center.z;
			float num = vector.Dot(this.Axis0);
			if (num < -this.Extents.x)
			{
				return false;
			}
			if (num > this.Extents.x)
			{
				return false;
			}
			num = vector.Dot(this.Axis1);
			if (num < -this.Extents.y)
			{
				return false;
			}
			if (num > this.Extents.y)
			{
				return false;
			}
			num = vector.Dot(this.Axis2);
			return num >= -this.Extents.z && num <= this.Extents.z;
		}

		public bool Contains(Vector3 point)
		{
			return this.Contains(ref point);
		}

		public void Include(ref Box3 box)
		{
			Box3 box2 = default(Box3);
			box2.Center = 0.5f * (this.Center + box.Center);
			Matrix4x4 matrix4x;
			Matrix4x4ex.CreateRotationFromColumns(ref this.Axis0, ref this.Axis1, ref this.Axis2, out matrix4x);
			Quaternion a;
			Matrix4x4ex.RotationMatrixToQuaternion(ref matrix4x, out a);
			Matrix4x4 matrix4x2;
			Matrix4x4ex.CreateRotationFromColumns(ref box.Axis0, ref box.Axis1, ref box.Axis2, out matrix4x2);
			Quaternion b;
			Matrix4x4ex.RotationMatrixToQuaternion(ref matrix4x2, out b);
			if (Quaternion.Dot(a, b) < 0f)
			{
				b.x = -b.x;
				b.y = -b.y;
				b.z = -b.z;
				b.w = -b.w;
			}
			Quaternion quaternion;
			quaternion.x = a.x + b.x;
			quaternion.y = a.x + b.y;
			quaternion.z = a.x + b.z;
			quaternion.w = a.x + b.w;
			float num = Mathfex.InvSqrt(Quaternion.Dot(quaternion, quaternion));
			quaternion.x *= num;
			quaternion.y *= num;
			quaternion.z *= num;
			quaternion.w *= num;
			Matrix4x4 matrix4x3;
			Matrix4x4ex.QuaternionToRotationMatrix(ref quaternion, out matrix4x3);
			box2.Axis0 = matrix4x3.GetColumn(0);
			box2.Axis1 = matrix4x3.GetColumn(1);
			box2.Axis2 = matrix4x3.GetColumn(2);
			Vector3 zero = Vector3ex.Zero;
			Vector3 zero2 = Vector3ex.Zero;
			Vector3[] array = this.CalcVertices();
			for (int i = 0; i < 8; i++)
			{
				Vector3 vector = array[i] - box2.Center;
				for (int j = 0; j < 3; j++)
				{
					float num2 = vector.Dot(box2.GetAxis(j));
					if (num2 > zero2[j])
					{
						zero2[j] = num2;
					}
					else if (num2 < zero[j])
					{
						zero[j] = num2;
					}
				}
			}
			box.CalcVertices(out array[0], out array[1], out array[2], out array[3], out array[4], out array[5], out array[6], out array[7]);
			for (int i = 0; i < 8; i++)
			{
				Vector3 vector = array[i] - box2.Center;
				for (int j = 0; j < 3; j++)
				{
					float num2 = vector.Dot(box2.GetAxis(j));
					if (num2 > zero2[j])
					{
						zero2[j] = num2;
					}
					else if (num2 < zero[j])
					{
						zero[j] = num2;
					}
				}
			}
			for (int j = 0; j < 3; j++)
			{
				box2.Center += 0.5f * (zero2[j] + zero[j]) * box2.GetAxis(j);
				box2.Extents[j] = 0.5f * (zero2[j] - zero[j]);
			}
			this = box2;
		}

		public void Include(Box3 box)
		{
			this.Include(ref box);
		}

		public override string ToString()
		{
			return string.Format("[Center: {0} Axis0: {1} Axis1: {2} Axis2: {3} Extents: {4}]", new object[]
			{
				this.Center.ToStringEx(),
				this.Axis0.ToStringEx(),
				this.Axis1.ToStringEx(),
				this.Axis2.ToStringEx(),
				this.Extents.ToStringEx()
			});
		}
	}
}

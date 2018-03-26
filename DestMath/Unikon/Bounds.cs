using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	public struct Bounds
	{
		private Vector3 m_Center;

		private Vector3 m_Extents;

		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		public Vector3 size
		{
			get
			{
				return this.m_Extents * 2f;
			}
			set
			{
				this.m_Extents = value * 0.5f;
			}
		}

		public Vector3 extents
		{
			get
			{
				return this.m_Extents;
			}
			set
			{
				this.m_Extents = value;
			}
		}

		public Vector3 min
		{
			get
			{
				return this.center - this.extents;
			}
			set
			{
				this.SetMinMax(value, this.max);
			}
		}

		public Vector3 max
		{
			get
			{
				return this.center + this.extents;
			}
			set
			{
				this.SetMinMax(this.min, value);
			}
		}

		public Bounds(Vector3 center, Vector3 size)
		{
			this.m_Center = center;
			this.m_Extents = size * 0.5f;
		}

		public bool Contains(Vector3 point)
		{
            Vector3 min = this.min;
            Vector3 max = this.max;
            return point.x >= min.x && point.x <= max.x && point.y >= min.y && point.y <= max.y && point.z >= min.z && point.z <= max.z;

		}


        private float GetExtent(int i)
        {
            switch(i)
            {
                case 0:
                    return extents.x;
                case 1:
                    return extents.y;
                case 2:
                    return extents.z;
            }
            return 0;
        }


		public float SqrDistance(Vector3 point)
		{

            Vector3 closest = point - center;
            float sqrDistance = 0.0f;

            for (int i = 0; i < 3; ++i)
            {
                float clos = closest[i];
                float ext = GetExtent(i);
                if (clos < -ext)
                {
                    float delta = clos + ext;
                    sqrDistance += delta * delta;
                    closest[i] = -ext;
                }
                else if (clos > ext)
                {
                    float delta = clos - ext;
                    sqrDistance += delta * delta;
                    closest[i] = ext;
                }
            }

            return sqrDistance;
		}

		public bool IntersectRay(Ray ray)
		{
            float tmin = -Mathf.Infinity;
            float tmax = Mathf.Infinity;

            float t0;
            float t1;
            float f;

            Vector3 p = center - ray.origin;
            Vector3 extent = this.extents;
            int i;
            for (i = 0; i < 3; i++)
            {
                // ray and plane are paralell so no valid intersection can be found
                {
                    f = 1.0F / ray.direction[i];
                    t0 = (p[i] + extent[i]) * f;
                    t1 = (p[i] - extent[i]) * f;
                    // Ray leaves on Right, Top, Back Side
                    if (t0 < t1)
                    {
                        if (t0 > tmin)
                            tmin = t0;

                        if (t1 < tmax)
                            tmax = t1;

                        if (tmin > tmax)
                            return false;

                        if (tmax < 0.0F)
                            return false;
                    }
                    // Ray leaves on Left, Bottom, Front Side
                    else
                    {
                        if (t1 > tmin)
                            tmin = t1;

                        if (t0 < tmax)
                            tmax = t0;

                        if (tmin > tmax)
                            return false;

                        if (tmax < 0.0F)
                            return false;
                    }
                }
            }

            //outT0 = tmin;
            //outT1 = tmax;

            return true;
		}

	
		public Vector3 ClosestPoint(Vector3 point)
		{
            Vector3 outPoint;
            float outSqrDistance;
            // compute coordinates of point in box coordinate system
            Vector3 kClosest = point - center;

            // project test point onto box
            float fSqrDistance = 0.0f;
            float fDelta;

            for (int i = 0; i < 3; i++)
            {
                if (kClosest[i] < -GetExtent(i))
                {
                    fDelta = kClosest[i] + GetExtent(i);
                    fSqrDistance += fDelta * fDelta;
                    kClosest[i] = -GetExtent(i);
                }
                else if (kClosest[i] > GetExtent(i))
                {
                    fDelta = kClosest[i] - GetExtent(i);
                    fSqrDistance += fDelta * fDelta;
                    kClosest[i] = GetExtent(i);
                }
            }

            // Inside
            if (fSqrDistance == 0.0F)
            {
                outPoint = point;
                outSqrDistance = 0.0F;
            }
            // Outside
            else
            {
                outPoint = kClosest + center;
                outSqrDistance = fSqrDistance;
            }

            return outPoint;
		}

		public override int GetHashCode()
		{
			return this.center.GetHashCode() ^ this.extents.GetHashCode() << 2;
		}

		public override bool Equals(object other)
		{
			bool result;
			if (!(other is Bounds))
			{
				result = false;
			}
			else
			{
				Bounds bounds = (Bounds)other;
				result = (this.center.Equals(bounds.center) && this.extents.Equals(bounds.extents));
			}
			return result;
		}

		public static bool operator ==(Bounds lhs, Bounds rhs)
		{
			return lhs.center == rhs.center && lhs.extents == rhs.extents;
		}

		public static bool operator !=(Bounds lhs, Bounds rhs)
		{
			return !(lhs == rhs);
		}

		public void SetMinMax(Vector3 min, Vector3 max)
		{
			this.extents = (max - min) * 0.5f;
			this.center = min + this.extents;
		}

		public void Encapsulate(Vector3 point)
		{
			this.SetMinMax(Vector3.Min(this.min, point), Vector3.Max(this.max, point));
		}

		public void Encapsulate(Bounds bounds)
		{
			this.Encapsulate(bounds.center - bounds.extents);
			this.Encapsulate(bounds.center + bounds.extents);
		}

		public void Expand(float amount)
		{
			amount *= 0.5f;
			this.extents += new Vector3(amount, amount, amount);
		}

		public void Expand(Vector3 amount)
		{
			this.extents += amount * 0.5f;
		}

		public bool Intersects(Bounds bounds)
		{
			return this.min.x <= bounds.max.x && this.max.x >= bounds.min.x && this.min.y <= bounds.max.y && this.max.y >= bounds.min.y && this.min.z <= bounds.max.z && this.max.z >= bounds.min.z;
		}

		public override string ToString()
		{
			return UnityString.Format("Center: {0}, Extents: {1}", new object[]
			{
				this.m_Center,
				this.m_Extents
			});
		}

		public string ToString(string format)
		{
			return UnityString.Format("Center: {0}, Extents: {1}", new object[]
			{
				this.m_Center.ToString(format),
				this.m_Extents.ToString(format)
			});
		}
	}
}

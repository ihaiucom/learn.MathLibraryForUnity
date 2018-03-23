using System;

namespace UnityEngine
{
    public struct Vector3
    {
        public const float kEpsilon = 1E-05f;

        /// <summary>
        ///   <para>X component of the vector.</para>
        /// </summary>
        public float x;

        /// <summary>
        ///   <para>Y component of the vector.</para>
        /// </summary>
        public float y;

        /// <summary>
        ///   <para>Z component of the vector.</para>
        /// </summary>
        public float z;

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                    case 2:
                        return this.z;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.x = value;
                        break;
                    case 1:
                        this.y = value;
                        break;
                    case 2:
                        this.z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
        }

        public Vector3 normalized
        {
            get { return Vector3.Normalize(this); }
        }

        public float magnitude
        {
            get
            {
                return Mathf.Sqrt((float) ((double) this.x * (double) this.x + (double) this.y * (double) this.y +
                                           (double) this.z * (double) this.z));
            }
        }

        public float sqrMagnitude
        {
            get
            {
                return (float) ((double) this.x * (double) this.x + (double) this.y * (double) this.y +
                                (double) this.z * (double) this.z);
            }
        }

        public static Vector3 zero
        {
            get { return new Vector3(0.0f, 0.0f, 0.0f); }
        }

        public static Vector3 one
        {
            get { return new Vector3(1f, 1f, 1f); }
        }

        public static Vector3 forward
        {
            get { return new Vector3(0.0f, 0.0f, 1f); }
        }

        public static Vector3 back
        {
            get { return new Vector3(0.0f, 0.0f, -1f); }
        }

        public static Vector3 up
        {
            get { return new Vector3(0.0f, 1f, 0.0f); }
        }

        public static Vector3 down
        {
            get { return new Vector3(0.0f, -1f, 0.0f); }
        }

        public static Vector3 left
        {
            get { return new Vector3(-1f, 0.0f, 0.0f); }
        }

        public static Vector3 right
        {
            get { return new Vector3(1f, 0.0f, 0.0f); }
        }


        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.x, -a.y, -a.z);
        }

        public static Vector3 operator *(Vector3 a, float d)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3 operator *(float d, Vector3 a)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3 operator /(Vector3 a, float d)
        {
            return new Vector3(a.x / d, a.y / d, a.z / d);
        }

        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            return (double) Vector3.SqrMagnitude(lhs - rhs) < 9.99999943962493E-11;
        }

        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return (double) Vector3.SqrMagnitude(lhs - rhs) >= 9.99999943962493E-11;
        }

        /// <summary>
        ///   <para>Linearly interpolates between two vectors.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            t = Mathf.Clamp01(t);
            return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        /// <summary>
        ///   <para>Linearly interpolates between two vectors.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
        {
            return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        public static Vector3 Slerp(Vector3 a, Vector3 b, float t)
        {
            Vector3 vector3;
            Vector3.INTERNAL_CALL_Slerp(ref a, ref b, t, out vector3);
            return vector3;
        }

        private static void INTERNAL_CALL_Slerp(ref Vector3 a, ref Vector3 b, float t, out Vector3 value)
        {
            float omega;
            float sinom;
            float scale0;
            float scale1;

            if (t <= 0)
            {
                value = a;
            }
            else if (t >= 1)
            {
                value = b;
            }
            else
            {
                Vector3 va = a;
                Vector3 vb = b;
                float lena = a.magnitude;
                float lenb = b.magnitude;
                a = va.normalized;
                b = vb.normalized;
                float len = (lena - lenb) * t + lena;
                float cosom = Vector3.Dot(va, vb);
                if (cosom > 1 - 1e-6)
                {
                    scale0 = 1 - t;
                    scale1 = t;
                }
                else if (cosom < -1 + 1e-6)
                {
                    Vector3 axis = INTERNAL_CALL_OrthoNormalVector(a);
                    Quaternion q = Quaternion.AngleAxis(180.0f * t, axis);
                    Vector3 v = Vector3.Cross(q.eulerAngles, a);
                    v *= len;
                    value = v;
                    return;
                }
                else
                {
                    omega = Mathf.Acos(cosom);
                    sinom = Mathf.Sin(omega);
                    scale0 = Mathf.Sin((1 - t) * omega) / sinom;
                    scale1 = Mathf.Sin((t * omega)) / sinom;
                }
                va *= scale0;
                vb *= scale1;
                vb += va;
                vb *= len;
                value = vb;
            }
        }

        /// <summary>
        /// get OrthoNormalVector
        /// </summary>
        private static double overSqrt2 = 0.7071067811865475244008443621048490;

        private static Vector3 INTERNAL_CALL_OrthoNormalVector(Vector3 vec)
        {
            Vector3 res;
            if (Mathf.Abs(vec.z) > overSqrt2)
            {
                float a = vec.y * vec.y + vec.z + vec.z;
                float k = 1 / Mathf.Sqrt(a);
                res.x = 0;
                res.y = -vec.z * k;
                res.z = vec.y * k;
            }
            else
            {
                float a = vec.x * vec.x + vec.y + vec.y;
                float k = 1 / Mathf.Sqrt(a);
                res.x = -vec.y * k;
                res.y = vec.x * k;
                res.z = 0;
            }
            return res;
        }

        public static Vector3 SlerpUnclamped(Vector3 a, Vector3 b, float t)
        {
            Vector3 vector3;
            Vector3.INTERNAL_CALL_SlerpUnclamped(ref a, ref b, t, out vector3);
            return vector3;
        }

        private static void INTERNAL_CALL_SlerpUnclamped(ref Vector3 a, ref Vector3 b, float t, out Vector3 value)
        {
            float cosAngle = Vector3.Dot(a, b);
            if (cosAngle < 0)
            {
                cosAngle = -cosAngle;
                b = new Vector3(-b.x, -b.y, -b.z);
            }
            float t1, t2;
            if (cosAngle < 0.95)
            {
                float angle = Mathf.Acos(cosAngle);
                float sinAgle = Mathf.Sin(angle);
                float invSinAngle = 1 / sinAgle;
                t1 = Mathf.Sin((1 - t) * angle) * invSinAngle;
                t2 = Mathf.Sin(t * angle) * invSinAngle;
                Vector3 quat = new Vector3(a.x * t1 + b.x * t2, a.y * t1 + b.y * t2, a.z * t1 + a.z * t2);
                value = quat;
            }
            else
            {
                value = Vector3.Lerp(a, b, t);
            }
        }

        public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
        {
            normal.SetNormalize();
            tangent = tangent - Dot(normal, tangent) * normal;
            tangent.SetNormalize();
        }

        private static void INTERNAL_CALL_Internal_OrthoNormalize2(ref Vector3 va, ref Vector3 vb,
            ref Vector3 vc)
        {
            va.SetNormalize();
            vb -= Project(vb, va);
            vb.SetNormalize();
            if (vc == Vector3.zero)
                return;
            vc -= Project(vc, va);
            vc -= Project(vc, vb);
            vc.SetNormalize();
        }

        private void SetNormalize()
        {
            float num = Mathf.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
            if (num > 1e-5)
            {
                this.x = this.x / num;
                this.y = this.y / num;
                this.z = this.z / num;
            }
            else
            {
                this.x = 0;
                this.y = 0;
                this.z = 0;
            }
        }

        public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal)
        {
            INTERNAL_CALL_Internal_OrthoNormalize2(ref normal, ref tangent, ref binormal);
        }


        public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
        {
            Vector3 vector3 = target - current;
            float magnitude = vector3.magnitude;
            if ((double) magnitude <= (double) maxDistanceDelta || (double) magnitude == 0.0)
                return target;
            return current + vector3 / magnitude * maxDistanceDelta;
        }

        public static Vector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta,
            float maxMagnitudeDelta)
        {
            Vector3 vector3;
            Vector3.INTERNAL_CALL_RotateTowards(ref current, ref target, maxRadiansDelta, maxMagnitudeDelta,
                out vector3);
            return vector3;
        }

        private static void INTERNAL_CALL_RotateTowards(ref Vector3 current, ref Vector3 target, float maxRadiansDelta,
            float maxMagnitudeDelta, out Vector3 value)
        {
            float len1 = current.magnitude;
            float len2 = target.magnitude;
            if (len1 < 1e-6 && len2 > 1e-6)
            {
                Vector3 from = current / len1;
                Vector3 to = target / len2;
                float cosom = Vector3.Dot(from, to);
                if (cosom > 1e-6)
                {
                    value = Vector3.MoveTowards(current, target, maxMagnitudeDelta);
                }
                else if (cosom < -1 + 1e-6)
                {
                    Vector3 axis = OrthoNormalVector(from);
                    Quaternion q = Quaternion.AngleAxis(maxMagnitudeDelta * Mathf.Rad2Deg, axis);
                    Vector3 rotate = q.eulerAngles - from;
                    float delta = ClampedMove(len1, len2, maxMagnitudeDelta);
                    rotate *= delta;
                    value = rotate;
                }
                else
                {
                    float angle = Mathf.Acos(cosom);
                    Vector3 axis = Vector3.Cross(from, to);
                    axis.SetNormalize();
                    Quaternion q = Quaternion.AngleAxis(Mathf.Min(maxMagnitudeDelta, angle) * Mathf.Rad2Deg, axis);
                    Vector3 rotate = q.eulerAngles - from;
                    float delta = ClampedMove(len1, len2, maxMagnitudeDelta);
                    rotate *= delta;
                    value = rotate;
                }
            }
            value = Vector3.MoveTowards(current, target, maxMagnitudeDelta);
        }

        private static float ClampedMove(float lhs, float rhs, float clampedDelta)
        {
            float delta = rhs - lhs;
            if (delta > 0)
            {
                return lhs + Mathf.Min(delta, clampedDelta);
            }
            else
            {
                return lhs - Mathf.Min(-delta, clampedDelta);
            }
        }

        private static Vector3 OrthoNormalVector(Vector3 vec)
        {
            Vector3 res;
            if (Mathf.Abs(vec.z) > overSqrt2)
            {
                float a = vec.y * vec.y + vec.z * vec.z;
                float k = 1 / Mathf.Sqrt(a);
                res.x = 0;
                res.y = -vec.z * k;
                res.z = vec.y * k;
            }
            else
            {
                float a = vec.x * vec.x + vec.y * vec.y;
                float k = 1 / Mathf.Sqrt(a);
                res.x = -vec.x * k;
                res.y = vec.x * k;
                res.z = 0;
            }
            return res;
        }

//        private float overSqrt2 = 0.7071067811865475244008443621048490;
        public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime,
            float maxSpeed)
        {
            float deltaTime = Time.deltaTime;
            return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime)
        {
            float deltaTime = Time.deltaTime;
            float maxSpeed = float.PositiveInfinity;
            return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime,
            float maxSpeed, float deltaTime)
        {
            smoothTime = Mathf.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * deltaTime;
            float num3 = (float) (1.0 / (1.0 + (double) num2 + 0.479999989271164 * (double) num2 * (double) num2 +
                                         0.234999999403954 * (double) num2 * (double) num2 * (double) num2));
            Vector3 vector = current - target;
            Vector3 vector3_1 = target;
            float maxLength = maxSpeed * smoothTime;
            Vector3 vector3_2 = Vector3.ClampMagnitude(vector, maxLength);
            target = current - vector3_2;
            Vector3 vector3_3 = (currentVelocity + num1 * vector3_2) * deltaTime;
            currentVelocity = (currentVelocity - num1 * vector3_3) * num3;
            Vector3 vector3_4 = target + (vector3_2 + vector3_3) * num3;
            if ((double) Vector3.Dot(vector3_1 - current, vector3_4 - vector3_1) > 0.0)
            {
                vector3_4 = vector3_1;
                currentVelocity = (vector3_4 - vector3_1) / deltaTime;
            }
            return vector3_4;
        }

        /// <summary>
        ///   <para>Set x, y and z components of an existing Vector3.</para>
        /// </summary>
        /// <param name="new_x"></param>
        /// <param name="new_y"></param>
        /// <param name="new_z"></param>
        public void Set(float new_x, float new_y, float new_z)
        {
            this.x = new_x;
            this.y = new_y;
            this.z = new_z;
        }

        /// <summary>
        ///   <para>Multiplies two vectors component-wise.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static Vector3 Scale(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        ///   <para>Multiplies every component of this vector by the same component of scale.</para>
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(Vector3 scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
            this.z *= scale.z;
        }

        /// <summary>
        ///   <para>Cross Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3((float) ((double) lhs.y * (double) rhs.z - (double) lhs.z * (double) rhs.y),
                (float) ((double) lhs.z * (double) rhs.x - (double) lhs.x * (double) rhs.z),
                (float) ((double) lhs.x * (double) rhs.y - (double) lhs.y * (double) rhs.x));
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector3))
                return false;
            Vector3 vector3 = (Vector3) other;
            if (this.x.Equals(vector3.x) && this.y.Equals(vector3.y))
                return this.z.Equals(vector3.z);
            return false;
        }

        /// <summary>
        ///   <para>Reflects a vector off the plane defined by a normal.</para>
        /// </summary>
        /// <param name="inDirection"></param>
        /// <param name="inNormal"></param>
        public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
        {
            return -2f * Vector3.Dot(inNormal, inDirection) * inNormal + inDirection;
        }

        /// <summary>
        ///   <para></para>
        /// </summary>
        /// <param name="value"></param>
        public static Vector3 Normalize(Vector3 value)
        {
            float num = Vector3.Magnitude(value);
            if ((double) num > 9.99999974737875E-06)
                return value / num;
            return Vector3.zero;
        }

        /// <summary>
        ///   <para>Makes this vector have a magnitude of 1.</para>
        /// </summary>
        public void Normalize()
        {
            float num = Vector3.Magnitude(this);
            if ((double) num > 9.99999974737875E-06)
                this = this / num;
            else
                this = Vector3.zero;
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1}, {2:F1})", (object) this.x, (object) this.y, (object) this.z);
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public string ToString(string format)
        {
            return string.Format("({0}, {1}, {2})", (object) this.x.ToString(format), (object) this.y.ToString(format),
                (object) this.z.ToString(format));
        }

        /// <summary>
        ///   <para>Dot Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return (float) ((double) lhs.x * (double) rhs.x + (double) lhs.y * (double) rhs.y +
                            (double) lhs.z * (double) rhs.z);
        }

        /// <summary>
        ///   <para>Projects a vector onto another vector.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="onNormal"></param>
        public static Vector3 Project(Vector3 vector, Vector3 onNormal)
        {
            float num = Vector3.Dot(onNormal, onNormal);
            if ((double) num < (double) Mathf.Epsilon)
                return Vector3.zero;
            return onNormal * Vector3.Dot(vector, onNormal) / num;
        }

        /// <summary>
        ///   <para>Projects a vector onto a plane defined by a normal orthogonal to the plane.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="planeNormal"></param>
        public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
        {
            return vector - Vector3.Project(vector, planeNormal);
        }

        [Obsolete("Use Vector3.ProjectOnPlane instead.")]
        public static Vector3 Exclude(Vector3 excludeThis, Vector3 fromThat)
        {
            return fromThat - Vector3.Project(fromThat, excludeThis);
        }

        /// <summary>
        ///   <para>Returns the angle in degrees between from and to.</para>
        /// </summary>
        /// <param name="from">The angle extends round from this vector.</param>
        /// <param name="to">The angle extends round to this vector.</param>
        public static float Angle(Vector3 from, Vector3 to)
        {
            return Mathf.Acos(Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
        }

        /// <summary>
        ///   <para>Returns the distance between a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Distance(Vector3 a, Vector3 b)
        {
            Vector3 vector3 = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
            return Mathf.Sqrt((float) ((double) vector3.x * (double) vector3.x +
                                       (double) vector3.y * (double) vector3.y +
                                       (double) vector3.z * (double) vector3.z));
        }

        /// <summary>
        ///   <para>Returns a copy of vector with its magnitude clamped to maxLength.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="maxLength"></param>
        public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
        {
            if ((double) vector.sqrMagnitude > (double) maxLength * (double) maxLength)
                return vector.normalized * maxLength;
            return vector;
        }

        public static float Magnitude(Vector3 a)
        {
            return Mathf.Sqrt((float) ((double) a.x * (double) a.x + (double) a.y * (double) a.y +
                                       (double) a.z * (double) a.z));
        }

        public static float SqrMagnitude(Vector3 a)
        {
            return (float) ((double) a.x * (double) a.x + (double) a.y * (double) a.y + (double) a.z * (double) a.z);
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the smallest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector3 Min(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the largest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector3 Max(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
        }

#if UNITY || FAKE_SERVER
        public static implicit operator global::UnityEngine.Vector3(Vector3 v)
        {
            return new global::UnityEngine.Vector3(v.x, v.y, v.z);
        }

        public static implicit operator Vector3(global::UnityEngine.Vector3 v)
        {
            return new Vector3(v.x, v.y, v.z);
        }
#endif
    }
}
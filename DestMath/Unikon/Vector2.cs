using System;

namespace UnityEngine
{
    public struct Vector2
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
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
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
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
            }
        }

        /// <summary>
        ///   <para>Returns this vector with a magnitude of 1 (Read Only).</para>
        /// </summary>
        public Vector2 normalized
        {
            get
            {
                Vector2 vector2 = new Vector2(this.x, this.y);
                vector2.Normalize();
                return vector2;
            }
        }

        /// <summary>
        ///   <para>Returns the length of this vector (Read Only).</para>
        /// </summary>
        public float magnitude
        {
            get
            {
                return Mathf.Sqrt((float) ((double) this.x * (double) this.x + (double) this.y * (double) this.y));
            }
        }

        /// <summary>
        ///   <para>Returns the squared length of this vector (Read Only).</para>
        /// </summary>
        public float sqrMagnitude
        {
            get
            {
                return (float) ((double) this.x * (double) this.x + (double) this.y * (double) this.y);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2(0, 0).</para>
        /// </summary>
        public static Vector2 zero
        {
            get
            {
                return new Vector2(0.0f, 0.0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2(1, 1).</para>
        /// </summary>
        public static Vector2 one
        {
            get
            {
                return new Vector2(1f, 1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2(0, 1).</para>
        /// </summary>
        public static Vector2 up
        {
            get
            {
                return new Vector2(0.0f, 1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2(0, -1).</para>
        /// </summary>
        public static Vector2 down
        {
            get
            {
                return new Vector2(0.0f, -1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2(-1, 0).</para>
        /// </summary>
        public static Vector2 left
        {
            get
            {
                return new Vector2(-1f, 0.0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2(1, 0).</para>
        /// </summary>
        public static Vector2 right
        {
            get
            {
                return new Vector2(1f, 0.0f);
            }
        }

        /// <summary>
        ///   <para>Constructs a new vector with given x, y components.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static implicit operator Vector2(Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        public static implicit operator Vector3(Vector2 v)
        {
            return new Vector3(v.x, v.y, 0.0f);
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(-a.x, -a.y);
        }

        public static Vector2 operator *(Vector2 a, float d)
        {
            return new Vector2(a.x * d, a.y * d);
        }

        public static Vector2 operator *(float d, Vector2 a)
        {
            return new Vector2(a.x * d, a.y * d);
        }

        public static Vector2 operator /(Vector2 a, float d)
        {
            return new Vector2(a.x / d, a.y / d);
        }

        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return (double) Vector2.SqrMagnitude(lhs - rhs) < 9.99999943962493E-11;
        }

        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return (double) Vector2.SqrMagnitude(lhs - rhs) >= 9.99999943962493E-11;
        }

        /// <summary>
        ///   <para>Set x and y components of an existing Vector2.</para>
        /// </summary>
        /// <param name="new_x"></param>
        /// <param name="new_y"></param>
        public void Set(float new_x, float new_y)
        {
            this.x = new_x;
            this.y = new_y;
        }

        /// <summary>
        ///   <para>Linearly interpolates between vectors a and b by t.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            t = Mathf.Clamp01(t);
            return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }

        /// <summary>
        ///   <para>Linearly interpolates between vectors a and b by t.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
        {
            return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }

        /// <summary>
        ///   <para>Moves a point current towards target.</para>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <param name="maxDistanceDelta"></param>
        public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            Vector2 vector2 = target - current;
            float magnitude = vector2.magnitude;
            if ((double) magnitude <= (double) maxDistanceDelta || (double) magnitude == 0.0)
                return target;
            return current + vector2 / magnitude * maxDistanceDelta;
        }

        /// <summary>
        ///   <para>Multiplies two vectors component-wise.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static Vector2 Scale(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        /// <summary>
        ///   <para>Multiplies every component of this vector by the same component of scale.</para>
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(Vector2 scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
        }

        /// <summary>
        ///   <para>Makes this vector have a magnitude of 1.</para>
        /// </summary>
        public void Normalize()
        {
            float magnitude = this.magnitude;
            if ((double) magnitude > 9.99999974737875E-06)
                this = this / magnitude;
            else
                this = Vector2.zero;
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1})", (object) this.x, (object) this.y);
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public string ToString(string format)
        {
            return string.Format("({0}, {1})", (object) this.x.ToString(format), (object) this.y.ToString(format));
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector2))
                return false;
            Vector2 vector2 = (Vector2) other;
            if (this.x.Equals(vector2.x))
                return this.y.Equals(vector2.y);
            return false;
        }

        /// <summary>
        ///   <para>Reflects a vector off the vector defined by a normal.</para>
        /// </summary>
        /// <param name="inDirection"></param>
        /// <param name="inNormal"></param>
        public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
        {
            return -2f * Vector2.Dot(inNormal, inDirection) * inNormal + inDirection;
        }

        /// <summary>
        ///   <para>Dot Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static float Dot(Vector2 lhs, Vector2 rhs)
        {
            return (float) ((double) lhs.x * (double) rhs.x + (double) lhs.y * (double) rhs.y);
        }

        /// <summary>
        ///   <para>Returns the angle in degrees between from and to.</para>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static float Angle(Vector2 from, Vector2 to)
        {
            return Mathf.Acos(Mathf.Clamp(Vector2.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
        }

        /// <summary>
        ///   <para>Returns the distance between a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Distance(Vector2 a, Vector2 b)
        {
            return (a - b).magnitude;
        }

        /// <summary>
        ///   <para>Returns a copy of vector with its magnitude clamped to maxLength.</para>
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="maxLength"></param>
        public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
        {
            if ((double) vector.sqrMagnitude > (double) maxLength * (double) maxLength)
                return vector.normalized * maxLength;
            return vector;
        }

        public static float SqrMagnitude(Vector2 a)
        {
            return (float) ((double) a.x * (double) a.x + (double) a.y * (double) a.y);
        }

        public float SqrMagnitude()
        {
            return (float) ((double) this.x * (double) this.x + (double) this.y * (double) this.y);
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the smallest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector2 Min(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the largest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector2 Max(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
        }

        public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed)
        {
            float deltaTime = Time.deltaTime;
            return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime)
        {
            float deltaTime = Time.deltaTime;
            float maxSpeed = float.PositiveInfinity;
            return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            smoothTime = Mathf.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * deltaTime;
            float num3 = (float) (1.0 / (1.0 + (double) num2 + 0.479999989271164 * (double) num2 * (double) num2 + 0.234999999403954 * (double) num2 * (double) num2 * (double) num2));
            Vector2 vector = current - target;
            Vector2 vector2_1 = target;
            float maxLength = maxSpeed * smoothTime;
            Vector2 vector2_2 = Vector2.ClampMagnitude(vector, maxLength);
            target = current - vector2_2;
            Vector2 vector2_3 = (currentVelocity + num1 * vector2_2) * deltaTime;
            currentVelocity = (currentVelocity - num1 * vector2_3) * num3;
            Vector2 vector2_4 = target + (vector2_2 + vector2_3) * num3;
            if ((double) Vector2.Dot(vector2_1 - current, vector2_4 - vector2_1) > 0.0)
            {
                vector2_4 = vector2_1;
                currentVelocity = (vector2_4 - vector2_1) / deltaTime;
            }
            return vector2_4;
        }
    }
}
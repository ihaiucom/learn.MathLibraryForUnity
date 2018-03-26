using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
//测试的命令空间， 测试完记得删除
//using UnityEngine;

namespace UnityEngine
{
    public struct Quaternion
    {
        public const float kEpsilon = 1E-06f;
        /// <summary>
        ///   <para>X component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
        /// </summary>
        public float x;
        /// <summary>
        ///   <para>Y component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
        /// </summary>
        public float y;
        /// <summary>
        ///   <para>Z component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
        /// </summary>
        public float z;
        /// <summary>
        ///   <para>W component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
        /// </summary>
        public float w;

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
                    case 3:
                        return this.w;
                    default:
                        throw new IndexOutOfRangeException("Invalid Quaternion index!");
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
                    case 3:
                        this.w = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Quaternion index!");
                }
            }
        }

        /// <summary>
        ///   <para>The identity rotation (Read Only).</para>
        /// </summary>
        public static Quaternion identity
        {
            get
            {
                return new Quaternion(0.0f, 0.0f, 0.0f, 1f);
            }
        }

        /// <summary>
        ///   <para>Returns the euler angle representation of the rotation.</para>
        /// </summary>
        public Vector3 eulerAngles
        {
            get
            {
                return Quaternion.Internal_MakePositive(Quaternion.Internal_ToEulerRad(this));
            }
            set
            {
                this = Quaternion.Internal_FromEulerRad(value * ((float) Math.PI / 180f));
            }
        }

        /// <summary>
        ///   <para>Constructs new Quaternion with given x,y,z,w components.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
        {
            return new Quaternion((float) ((double) lhs.w * (double) rhs.x + (double) lhs.x * (double) rhs.w + (double) lhs.y * (double) rhs.z - (double) lhs.z * (double) rhs.y), (float) ((double) lhs.w * (double) rhs.y + (double) lhs.y * (double) rhs.w + (double) lhs.z * (double) rhs.x - (double) lhs.x * (double) rhs.z), (float) ((double) lhs.w * (double) rhs.z + (double) lhs.z * (double) rhs.w + (double) lhs.x * (double) rhs.y - (double) lhs.y * (double) rhs.x), (float) ((double) lhs.w * (double) rhs.w - (double) lhs.x * (double) rhs.x - (double) lhs.y * (double) rhs.y - (double) lhs.z * (double) rhs.z));
        }

        public static Vector3 operator *(Quaternion rotation, Vector3 point)
        {
            float num1 = rotation.x * 2f;
            float num2 = rotation.y * 2f;
            float num3 = rotation.z * 2f;
            float num4 = rotation.x * num1;
            float num5 = rotation.y * num2;
            float num6 = rotation.z * num3;
            float num7 = rotation.x * num2;
            float num8 = rotation.x * num3;
            float num9 = rotation.y * num3;
            float num10 = rotation.w * num1;
            float num11 = rotation.w * num2;
            float num12 = rotation.w * num3;
            Vector3 vector3;
            vector3.x = (float) ((1.0 - ((double) num5 + (double) num6)) * (double) point.x + ((double) num7 - (double) num12) * (double) point.y + ((double) num8 + (double) num11) * (double) point.z);
            vector3.y = (float) (((double) num7 + (double) num12) * (double) point.x + (1.0 - ((double) num4 + (double) num6)) * (double) point.y + ((double) num9 - (double) num10) * (double) point.z);
            vector3.z = (float) (((double) num8 - (double) num11) * (double) point.x + ((double) num9 + (double) num10) * (double) point.y + (1.0 - ((double) num4 + (double) num5)) * (double) point.z);
            return vector3;
        }

        public static bool operator ==(Quaternion lhs, Quaternion rhs)
        {
            return (double) Quaternion.Dot(lhs, rhs) > 0.999998986721039;
        }

        public static bool operator !=(Quaternion lhs, Quaternion rhs)
        {
            return (double) Quaternion.Dot(lhs, rhs) <= 0.999998986721039;
        }

        /// <summary>
        ///   <para>Set x, y, z and w components of an existing Quaternion.</para>
        /// </summary>
        /// <param name="new_x"></param>
        /// <param name="new_y"></param>
        /// <param name="new_z"></param>
        /// <param name="new_w"></param>
        public void Set(float new_x, float new_y, float new_z, float new_w)
        {
            this.x = new_x;
            this.y = new_y;
            this.z = new_z;
            this.w = new_w;
        }

        /// <summary>
        ///   <para>The dot product between two rotations.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Dot(Quaternion a, Quaternion b)
        {
            return (float) ((double) a.x * (double) b.x + (double) a.y * (double) b.y + (double) a.z * (double) b.z + (double) a.w * (double) b.w);
        }

        /// <summary>
        ///   <para>Creates a rotation which rotates angle degrees around axis.</para>
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="axis"></param>
        public static Quaternion AngleAxis(float angle, Vector3 axis)
        {
            Quaternion quaternion;
            Quaternion.INTERNAL_CALL_AngleAxis(angle, ref axis, out quaternion);
            return quaternion;
        }

        private static float halfDegToRad = 0.5f * Mathf.Deg2Rad;
        private static void INTERNAL_CALL_AngleAxis(float angle, ref Vector3 axis, out Quaternion value)
        {
            Vector3 normAxis = axis.normalized;
            angle = angle * halfDegToRad;
            float s = Mathf.Sin(angle);
            float w = Mathf.Cos(angle);
            float x = normAxis.x * s;
            float y = normAxis.y * s;
            float z = normAxis.z * s;
            value = new Quaternion(x, y, z, w);
        }

        public void ToAngleAxis(out float angle, out Vector3 axis)
        {
            Quaternion.Internal_ToAxisAngleRad(this,out axis, out angle);
        }

        private static bool Approximately(float f0, float f1)
        {
            return Mathf.Abs(f0 - f1) < 1e-6;
        }

        private static void Internal_ToAxisAngleRad(Quaternion q ,out Vector3 axis, out float angle)
        {
            angle = 2 * Mathf.Acos(q.w);
            if (Approximately(angle, 0))
            {
                angle *= 57.29578f;
                axis = new Vector3(1, 0, 0);
                return;
            }
            float div = 1 / Mathf.Sqrt(1 - Mathf.Sqrt(q.w));
            angle *= 57.29578f;
            axis = new Vector3(q.x*div, q.y*div, q.z*div);
        }

        /// <summary>
        ///   <para>Creates a rotation which rotates from fromDirection to toDirection.</para>
        /// </summary>
        /// <param name="fromDirection"></param>
        /// <param name="toDirection"></param>
        public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
        {
            Quaternion quaternion;
            Quaternion.INTERNAL_CALL_FromToRotation(ref fromDirection, ref toDirection, out quaternion);
            return quaternion;
        }

        private static void INTERNAL_CALL_FromToRotation(ref Vector3 fromDirection, ref Vector3 toDirection, out Quaternion value)
        {
            Vector3 v0 = fromDirection.normalized;
            Vector3 v1 = toDirection.normalized;
            float d = Vector3.Dot(v0, v1);
            if (d>-1+1e-6)
            {
                float s = Mathf.Sqrt((1 + d) * 2);
                float invs = 1 / s;
                Vector3 c = Vector3.Cross(v0, v1) * invs;
                value = new Quaternion(c.x, c.y, c.z, s*0.5f);
            }else if (d>1-1e-6)
            {
                value = new Quaternion(0, 0, 0, 1);
            }
            else
            {
                Vector3 axis = Vector3.Cross(Vector3.right, v0);
                if (Vector3.SqrMagnitude(axis)<1e-6)
                {
                    axis = Vector3.Cross(Vector3.forward, v0);
                }
                value = new Quaternion(axis.x, axis.y, axis.z, 0);
            }
        }

        /// <summary>
        ///   <para>Creates a rotation which rotates from fromDirection to toDirection.</para>
        /// </summary>
        /// <param name="fromDirection"></param>
        /// <param name="toDirection"></param>
        public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
        {
            this = Quaternion.FromToRotation(fromDirection, toDirection);
        }

        /// <summary>
        ///   <para>Creates a rotation with the specified forward and upwards directions.</para>
        /// </summary>
        /// <param name="forward">The direction to look in.</param>
        /// <param name="upwards">The vector that defines in which direction up is.</param>
        public static Quaternion LookRotation(Vector3 forward, Vector3 upwards)
        {
            Quaternion quaternion;
            Quaternion.INTERNAL_CALL_LookRotation(ref forward, ref upwards, out quaternion);
            return quaternion;
        }

        /// <summary>
        ///   <para>Creates a rotation with the specified forward and upwards directions.</para>
        /// </summary>
        /// <param name="forward">The direction to look in.</param>
        /// <param name="upwards">The vector that defines in which direction up is.</param>
        public static Quaternion LookRotation(Vector3 forward)
        {
            Vector3 up = Vector3.up;
            Quaternion quaternion;
            Quaternion.INTERNAL_CALL_LookRotation(ref forward, ref up, out quaternion);
            return quaternion;
        }

        private static int[] _next = {1, 2, 0};
        private static void INTERNAL_CALL_LookRotation(ref Vector3 forward, ref Vector3 upwards, out Quaternion value)
        {
            float mag = forward.magnitude;
            if (mag<1e-6)
            {
                //Debug.LogError("error input forward to Quaternion"+forward.ToString());
                value = Quaternion.identity;
                return ;
            }
            forward = forward / mag;
            upwards = upwards != null ? upwards : Vector3.up;
            Vector3 right = Vector3.Cross(upwards, forward);
            right = right.normalized;
            upwards = Vector3.Cross(forward, right);
            right = Vector3.Cross(upwards, forward);
            float t = right.x + upwards.y + forward.z;
            if (t>0)
            {
                float x, y, z, w;
                t = t + 1;
                float s = 0.5f / Mathf.Sqrt(t);
                w = s * t;
                x = (upwards.z - forward.y) * s;
                y = (forward.x - right.z) * s;
                z = (right.y - upwards.x) * s;
                Quaternion ret = new Quaternion(x, y, z, w);
                ret = ret.SetNormalize();
                value = ret;
            }
            else
            {
                float[,] rot =
                {
                    {right.x, upwards.x, forward.x},
                    {right.y, upwards.y, forward.y},
                    {right.z, upwards.z, forward.z}
                };
                float[] q = {0, 0, 0};
                int i = 0;
                if (upwards.y>right.x)
                {
                    i = 1;
                }
                if (forward.z>rot[i,i])
                {
                    i = 2;
                }
                int j = _next[i];
                int k = _next[j];
                t = rot[i,i] - rot[j,j] - rot[k,k] + 1;
                float s = 0.5f / Mathf.Sqrt(t);
                q[i] = s * t;
                float w = (rot[k,j] - rot[j,k]) * s;
                q[j] = (rot[j,i] + rot[i,j]) * s;
                q[k] = (rot[k,i] + rot[i,k]) * s;
                Quaternion ret = new Quaternion(q[0], q[1], q[2], w);
                ret = ret.SetNormalize();
                value = ret;
            }
        }


        private Quaternion SetNormalize()
        {

            float n = this.x * this.x + this.y * this.y + this.z * this.z + this.w * this.w;
            if (n!=1 && n>0)
            {
                n = 1 / Mathf.Sqrt(n);
                this.x = this.x * n;
                this.y = this.y * n;
                this.z = this.z * n;
                this.w = this.w * n;
            }
            return this;
        }

        /// <summary>
        ///   <para>Creates a rotation with the specified forward and upwards directions.</para>
        /// </summary>
        /// <param name="view">The direction to look in.</param>
        /// <param name="up">The vector that defines in which direction up is.</param>
        public void SetLookRotation(Vector3 view)
        {
            Vector3 up = Vector3.up;
            this.SetLookRotation(view, up);
        }

        /// <summary>
        ///   <para>Creates a rotation with the specified forward and upwards directions.</para>
        /// </summary>
        /// <param name="view">The direction to look in.</param>
        /// <param name="up">The vector that defines in which direction up is.</param>
        public void SetLookRotation(Vector3 view, Vector3 up)
        {
            this = Quaternion.LookRotation(view, up);
        }

        /// <summary>
        ///   <para>Spherically interpolates between a and b by t. The parameter t is clamped to the range [0, 1].</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
        {
            Quaternion quaternion;
            Quaternion.INTERNAL_CALL_Slerp(ref a, ref b, t, out quaternion);
            return quaternion;
        }

        private static void INTERNAL_CALL_Slerp(ref Quaternion a, ref Quaternion b, float t, out Quaternion value)
        {
            t = Mathf.Clamp(t, 0, 1);
            value = SlerpUnclamped(a, b, t);
        }

        /// <summary>
        ///   <para>Spherically interpolates between a and b by t. The parameter t is not clamped.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t)
        {
            Quaternion quaternion;
            Quaternion.INTERNAL_CALL_SlerpUnclamped(ref a, ref b, t, out quaternion);
            return quaternion;
        }

        private static void INTERNAL_CALL_SlerpUnclamped(ref Quaternion a, ref Quaternion b, float t, out Quaternion value)
        {
            float cosAngle = Quaternion.Dot(a, b);
            if (cosAngle<0)
            {
                cosAngle = -cosAngle;
				b = new Quaternion(-b.x, -b.y, -b.z, -b.w);
            }
            float t1, t2;
            if (cosAngle<0.95)
            {
                float angle = Mathf.Acos(cosAngle);
                float sinAgle = Mathf.Sin(angle);
                float invSinAngle = 1 / sinAgle;
                t1 = Mathf.Sin((1 - t) * angle) * invSinAngle;
                t2 = Mathf.Sin(t * angle) * invSinAngle;
                Quaternion quat = new Quaternion(a.x * t1 + b.x * t2, a.y * t1 + b.y * t2, a.z * t1 + a.z * t2, a.w * t1 + b.w * t2);
                value = quat;
            }
            else
            {
                value = Quaternion.Lerp(a, b, t);
            }
        }

        /// <summary>
        ///   <para>Interpolates between a and b by t and normalizes the result afterwards. The parameter t is clamped to the range [0, 1].</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Quaternion Lerp(Quaternion a, Quaternion b, float t)
        {
            Quaternion quaternion;
            Quaternion.INTERNAL_CALL_Lerp(ref a, ref b, t, out quaternion);
            return quaternion;
        }

        private static void INTERNAL_CALL_Lerp(ref Quaternion a, ref Quaternion b, float t, out Quaternion value)
        {
            t = Mathf.Clamp(t, 0, 1);
            Quaternion q = new Quaternion(0, 0, 0, 0);
            if (Quaternion.Dot(a, b)<0)
            {
                q.x = a.x + t * (-b.x - a.x);
                q.y = a.y + t * (-b.y - a.y);
                q.z = a.z + t * (-b.z - a.z);
                q.w = a.w + t * (-b.w - b.w);
            }
            else
            {
                q.x = a.x + t * (b.x - a.x);
                q.y = a.y + t * (b.y - a.y);
                q.z = a.z + t * (b.z - a.z);
                q.w = a.w + t * (b.w - b.w);
            }
            q.SetNormalize();
            value = q;
        }

        /// <summary>
        ///   <para>Interpolates between a and b by t and normalizes the result afterwards. The parameter t is not clamped.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, float t)
        {
            Quaternion quaternion;
            Quaternion.INTERNAL_CALL_LerpUnclamped(ref a, ref b, t, out quaternion);
            return quaternion;
        }

        private static void INTERNAL_CALL_LerpUnclamped(ref Quaternion a, ref Quaternion b, float t, out Quaternion value)
        {
			Quaternion q = new Quaternion(0, 0, 0, 0);
			if (Quaternion.Dot(a, b)<0)
			{
				q.x = a.x + t * (-b.x - a.x);
				q.y = a.y + t * (-b.y - a.y);
				q.z = a.z + t * (-b.z - a.z);
				q.w = a.w + t * (-b.w - b.w);
			}
			else
			{
				q.x = a.x + t * (b.x - a.x);
				q.y = a.y + t * (b.y - a.y);
				q.z = a.z + t * (b.z - a.z);
				q.w = a.w + t * (b.w - b.w);
			}
			q.SetNormalize();
			value = q;
        }

        /// <summary>
        ///   <para>Rotates a rotation from towards to.</para>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="maxDegreesDelta"></param>
        public static Quaternion RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta)
        {
            float num = Quaternion.Angle(from, to);
            if ((double) num == 0.0)
                return to;
            float t = Mathf.Min(1f, maxDegreesDelta / num);
            return Quaternion.SlerpUnclamped(from, to, t);
        }

        /// <summary>
        ///   <para>Returns the Inverse of rotation.</para>
        /// </summary>
        /// <param name="rotation"></param>
        public static Quaternion Inverse(Quaternion rotation)
        {
            Quaternion quaternion;
            Quaternion.INTERNAL_CALL_Inverse(ref rotation, out quaternion);
            return quaternion;
        }

        private static void INTERNAL_CALL_Inverse(ref Quaternion rotation, out Quaternion value)
        {
            Quaternion quat = new Quaternion(0, 0, 0, 0);
            quat.x = -rotation.x;
            quat.y = -rotation.y;
            quat.z = -rotation.z;
            quat.w = rotation.w;
            value = quat;
        }


        /// <summary>
        ///   <para>Returns a nicely formatted string of the Quaternion.</para>
        /// </summary>
        /// <param name="format"></param>
        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", (object) this.x, (object) this.y, (object) this.z, (object) this.w);
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string of the Quaternion.</para>
        /// </summary>
        /// <param name="format"></param>
        public string ToString(string format)
        {
            return string.Format("({0}, {1}, {2}, {3})", (object) this.x.ToString(format), (object) this.y.ToString(format), (object) this.z.ToString(format), (object) this.w.ToString(format));
        }

        /// <summary>
        ///   <para>Returns the angle in degrees between two rotations a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Angle(Quaternion a, Quaternion b)
        {
            return (float) ((double) Mathf.Acos(Mathf.Min(Mathf.Abs(Quaternion.Dot(a, b)), 1f)) * 2.0 * 57.2957801818848);
        }

        /// <summary>
        ///   <para>Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static Quaternion Euler(float x, float y, float z)
        {
            return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
        }

        /// <summary>
        ///   <para>Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).</para>
        /// </summary>
        /// <param name="euler"></param>
        public static Quaternion Euler(Vector3 euler)
        {
            return Quaternion.Internal_FromEulerRad(euler);
        }
        //SetEuler()
        private static Quaternion Internal_FromEulerRad(Vector3 euler)
        {
            Quaternion quaternion;
            Quaternion.INTERNAL_CALL_Internal_FromEulerRad(ref euler, out quaternion);
            return quaternion;
        }

        private static void INTERNAL_CALL_Internal_FromEulerRad(ref Vector3 euler, out Quaternion value)
        {
            euler.x = euler.x * halfDegToRad;
            euler.y = euler.y * halfDegToRad;
            euler.z = euler.z * halfDegToRad;

            float sinX = Mathf.Sin(euler.x);
            float cosX = Mathf.Cos(euler.x);
            float sinY = Mathf.Sin(euler.y);
            float cosY = Mathf.Cos(euler.y);
            float sinZ = Mathf.Sin(euler.z);
            float cosZ = Mathf.Cos(euler.z);

            value.w = cosY * cosX * cosZ + sinY * sinX * sinZ;
            value.x = cosY * sinX * cosZ + sinY * cosX * sinZ;
            value.y = sinY * cosX * cosZ - cosY * sinX * sinZ;
            value.z = cosY * cosX * sinZ - sinY * sinX * cosZ;
        }


        private static Vector3 Internal_MakePositive(Vector3 euler)
        {
            float num1 = -9f / (500f * (float)Math.PI);
            float num2 = 360f + num1;
            if ((double) euler.x < (double) num1)
                euler.x += 360f;
            else if ((double) euler.x > (double) num2)
                euler.x -= 360f;
            if ((double) euler.y < (double) num1)
                euler.y += 360f;
            else if ((double) euler.y > (double) num2)
                euler.y -= 360f;
            if ((double) euler.z < (double) num1)
                euler.z += 360f;
            else if ((double) euler.z > (double) num2)
                euler.z -= 360f;
            return euler;
        }

        private static Vector3 Internal_ToEulerRad(Quaternion rotation)
        {
            Vector3 vector3;
//            Quaternion.INTERNAL_CALL_Internal_ToEulerRad(ref rotation, out vector3);
            FromQuaternion2Vector3(out vector3, rotation);
            return vector3;
        }
        private static void FromQuaternion2Vector3(out Vector3 vector3, Quaternion q1)
        {
            float sqw = q1.w * q1.w;
            float sqx = q1.x * q1.x;
            float sqy = q1.y * q1.y;
            float sqz = q1.z * q1.z;
            float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
            float test = q1.x * q1.w - q1.y * q1.z;
            Vector3 v;

            if (test>0.4995f*unit) { // singularity at north pole
                v.y = 2f * Mathf.Atan2 (q1.y, q1.x);
                v.x = Mathf.PI / 2;
                v.z = 0;
                vector3 = NormalizeAngles(v * Mathf.Rad2Deg);
            }
            if (test<-0.4995f*unit) { // singularity at south pole
                v.y = -2f * Mathf.Atan2 (q1.y, q1.x);
                v.x = -Mathf.PI / 2;
                v.z = 0;
                vector3 = NormalizeAngles(v * Mathf.Rad2Deg);
            }
            Quaternion q = new Quaternion (q1.w, q1.z, q1.x, q1.y);
            v.y = (float)Math.Atan2 (2f * q.x * q.w + 2f * q.y * q.z, 1 - 2f * (q.z * q.z + q.w * q.w));     // Yaw
            v.x = (float)Math.Asin (2f * (q.x * q.z - q.w * q.y));                             // Pitch
            v.z = (float)Math.Atan2 (2f * q.x * q.y + 2f * q.z * q.w, 1 - 2f * (q.y * q.y + q.z * q.z));      // Roll
            vector3 = NormalizeAngles (v * Mathf.Rad2Deg);
        }
        static Vector3 NormalizeAngles (Vector3 angles)
        {
            angles.x = NormalizeAngle (angles.x);
            angles.y = NormalizeAngle (angles.y);
            angles.z = NormalizeAngle (angles.z);
            return angles;
        }

        static float NormalizeAngle (float angle)
        {
            while (angle>360)
                angle -= 360;
            while (angle<0)
                angle += 360;
            return angle;
        }

        private static void INTERNAL_CALL_Internal_ToEulerRad(ref Quaternion rotation, out Vector3 value)
        {
            float x = rotation.x;
            float y = rotation.y;
            float z = rotation.z;
            float w = rotation.w;
            float check = 2 * (y * z - w * x);
            if (check<0.999f)
            {
                if (check > -0.999f)
                {
                    Vector3 v = new Vector3(-Mathf.Asin(check),
                        Mathf.Atan2(2 * (x * z + w * z), 1 - 2 * (x * x + y * y)),
                        Mathf.Atan2(2 * (x * y + w * z), 1 - 2 * (x * x + z * z)));
                    SanitizeEuler(ref v);
                    v *= Mathf.Rad2Deg;
                    value = v;
                }
                else
                {
                    Vector3 v = new Vector3(Mathf.PI/2, Mathf.Atan2(2*(x*y-w*z), 1-2*(y*y+z*z)), 0);
                    SanitizeEuler(ref v);
                    v *= Mathf.Rad2Deg;
                    value = v;

                }
            }
            else
            {
                Vector3 v = new Vector3(-Mathf.PI/2, Mathf.Atan2(-2*(x*y-w*z), 1-2*(y*y+z*z)), 0);
                SanitizeEuler(ref v);
                v *= Mathf.Rad2Deg;
                value = v;
            }
        }

        private static float two_pi = 2 * Mathf.PI;
        private static float negativeFlip = -0.001f;
        private static float positiveFlip = two_pi - 0.0001f;
        private static void SanitizeEuler(ref Vector3 euler)
        {
            if (euler.x<negativeFlip)
            {
                euler.x = euler.x + two_pi;
            }else if (euler.x>positiveFlip)
            {
                euler.x = euler.x - two_pi;
            }

            if (euler.y<negativeFlip)
            {
                euler.y = euler.y + two_pi;
            }else if (euler.y>positiveFlip)
            {
                euler.y = euler.y - two_pi;
            }

            if (euler.z<negativeFlip)
            {
                euler.z = euler.z + two_pi;
            }else if (euler.z>positiveFlip)
            {
                euler.z = euler.z + two_pi;
            }

        }



        [Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
        public static Quaternion EulerRotation(float x, float y, float z)
        {
            return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
        }

        [Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
        public static Quaternion EulerRotation(Vector3 euler)
        {
            return Quaternion.Internal_FromEulerRad(euler);
        }

        [Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
        public void SetEulerRotation(float x, float y, float z)
        {
            this = Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
        }

        [Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
        public void SetEulerRotation(Vector3 euler)
        {
            this = Quaternion.Internal_FromEulerRad(euler);
        }

        [Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
        public Vector3 ToEuler()
        {
            return Quaternion.Internal_ToEulerRad(this);
        }

        [Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
        public static Quaternion EulerAngles(float x, float y, float z)
        {
            return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
        }

        [Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
        public static Quaternion EulerAngles(Vector3 euler)
        {
            return Quaternion.Internal_FromEulerRad(euler);
        }

        [Obsolete("Use Quaternion.ToAngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		public void ToAxisAngle(out float angle, out Vector3 axis)
        {
            Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
        }

        [Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
        public void SetEulerAngles(float x, float y, float z)
        {
            this.SetEulerRotation(new Vector3(x, y, z));
        }

        [Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
        public void SetEulerAngles(Vector3 euler)
        {
            this = Quaternion.EulerRotation(euler);
        }

        [Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
        public static Vector3 ToEulerAngles(Quaternion rotation)
        {
            return Quaternion.Internal_ToEulerRad(rotation);
        }


        [Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
        public Vector3 ToEulerAngles()
        {
            return Quaternion.Internal_ToEulerRad(this);
        }

        [Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
        public static Quaternion AxisAngle(Vector3 axis, float angle)
        {
            Quaternion quaternion;
            Quaternion.INTERNAL_CALL_AxisAngle(ref axis, angle, out quaternion);
            return quaternion;
        }

        private static void INTERNAL_CALL_AxisAngle(ref Vector3 axis, float angle, out Quaternion value)
        {
            Vector3 normAxis = axis.normalized;
            angle = angle * halfDegToRad;
            float s = Mathf.Sin(angle);
            float w = Mathf.Cos(angle);
            float x = normAxis.x * s;
            float y = normAxis.y * s;
            float z = normAxis.z * s;

            value = new Quaternion(x, y, z, w);
        }

        [Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
        public void SetAxisAngle(Vector3 axis, float angle)
        {
            this = Quaternion.AxisAngle(axis, angle);
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
        }

        public override bool Equals(object other)
        {
            if (!(other is Quaternion))
                return false;
            Quaternion quaternion = (Quaternion) other;
            if (this.x.Equals(quaternion.x) && this.y.Equals(quaternion.y) && this.z.Equals(quaternion.z))
                return this.w.Equals(quaternion.w);
            return false;
        }



        //internal Quaternion NormalizeSafe(Quaternion q)
        //{
        //    float mag = Magnitude(q);
        //    if (mag < Vector3.kEpsilon)
        //        return Quaternion.identity;
        //    else
        //        return q / mag;
        //}


        internal static Vector3 RotateVectorByQuat(Transform self, ref Quaternion lhs, ref Vector3 rhs)
        {
            //	Matrix3x3f m;
            //	QuaternionToMatrix (lhs, &m);
            //	Vector3f restest = m.MultiplyVector3 (rhs);

            float x = lhs.x * 2.0F;
            float y = lhs.y * 2.0F;
            float z = lhs.z * 2.0F;
            float xx = lhs.x * x;
            float yy = lhs.y * y;
            float zz = lhs.z * z;
            float xy = lhs.x * y;
            float xz = lhs.x * z;
            float yz = lhs.y * z;
            float wx = lhs.w * x;
            float wy = lhs.w * y;
            float wz = lhs.w * z;

            Vector3 res;
            res.x = (1.0f - (yy + zz)) * rhs.x + (xy - wz) * rhs.y + (xz + wy) * rhs.z;
            res.y = (xy + wz) * rhs.x + (1.0f - (xx + zz)) * rhs.y + (yz - wx) * rhs.z;
            res.z = (xz - wy) * rhs.x + (yz + wx) * rhs.y + (1.0f - (xx + yy)) * rhs.z;

            //	AssertIf (!CompareApproximately (restest, res));
            return res;
        }

    }
}

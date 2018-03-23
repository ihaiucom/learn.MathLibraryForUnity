using System;


/// <summary>
/// https://forum.unity3d.com/threads/how-to-assign-matrix4x4-to-transform.121966/
/// </summary>
namespace UnityEngine
{
    public struct Matrix4x4
    {
        public float m00;
        public float m10;
        public float m20;
        public float m30;
        public float m01;
        public float m11;
        public float m21;
        public float m31;
        public float m02;
        public float m12;
        public float m22;
        public float m32;
        public float m03;
        public float m13;
        public float m23;
        public float m33;

        public float this[int row, int column]
        {
            get
            {
                return this[row + column * 4];
            }
            set
            {
                this[row + column * 4] = value;
            }
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.m00;
                    case 1:
                        return this.m10;
                    case 2:
                        return this.m20;
                    case 3:
                        return this.m30;
                    case 4:
                        return this.m01;
                    case 5:
                        return this.m11;
                    case 6:
                        return this.m21;
                    case 7:
                        return this.m31;
                    case 8:
                        return this.m02;
                    case 9:
                        return this.m12;
                    case 10:
                        return this.m22;
                    case 11:
                        return this.m32;
                    case 12:
                        return this.m03;
                    case 13:
                        return this.m13;
                    case 14:
                        return this.m23;
                    case 15:
                        return this.m33;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.m00 = value;
                        break;
                    case 1:
                        this.m10 = value;
                        break;
                    case 2:
                        this.m20 = value;
                        break;
                    case 3:
                        this.m30 = value;
                        break;
                    case 4:
                        this.m01 = value;
                        break;
                    case 5:
                        this.m11 = value;
                        break;
                    case 6:
                        this.m21 = value;
                        break;
                    case 7:
                        this.m31 = value;
                        break;
                    case 8:
                        this.m02 = value;
                        break;
                    case 9:
                        this.m12 = value;
                        break;
                    case 10:
                        this.m22 = value;
                        break;
                    case 11:
                        this.m32 = value;
                        break;
                    case 12:
                        this.m03 = value;
                        break;
                    case 13:
                        this.m13 = value;
                        break;
                    case 14:
                        this.m23 = value;
                        break;
                    case 15:
                        this.m33 = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
        }

        /// <summary>
        ///   <para>The inverse of this matrix (Read Only).</para>
        /// </summary>
        public Matrix4x4 inverse
        {
            get
            {
                return Matrix4x4.Inverse(this);
            }
        }

        /// <summary>
        ///   <para>Returns the transpose of this matrix (Read Only).</para>
        /// </summary>
        public Matrix4x4 transpose
        {
            get
            {
                return Matrix4x4.Transpose(this);
            }
        }

        /// <summary>
        ///   <para>Is this the identity matrix?</para>
        /// </summary>
        public bool isIdentity {get; private set; }

        /// <summary>
        ///   <para>The determinant of the matrix.</para>
        /// </summary>
        public float determinant
        {
            get
            {
                return Matrix4x4.Determinant(this);
            }
        }

        /// <summary>
        ///   <para>Returns a matrix with all elements set to zero (Read Only).</para>
        /// </summary>
        public static Matrix4x4 zero
        {
            get
            {
                return new Matrix4x4()
                {
                    m00 = 0.0f,
                    m01 = 0.0f,
                    m02 = 0.0f,
                    m03 = 0.0f,
                    m10 = 0.0f,
                    m11 = 0.0f,
                    m12 = 0.0f,
                    m13 = 0.0f,
                    m20 = 0.0f,
                    m21 = 0.0f,
                    m22 = 0.0f,
                    m23 = 0.0f,
                    m30 = 0.0f,
                    m31 = 0.0f,
                    m32 = 0.0f,
                    m33 = 0.0f
                };
            }
        }

        /// <summary>
        ///   <para>Returns the identity matrix (Read Only).</para>
        /// </summary>
        public static Matrix4x4 identity
        {
            get
            {
                return new Matrix4x4()
                {
                    m00 = 1f,
                    m01 = 0.0f,
                    m02 = 0.0f,
                    m03 = 0.0f,
                    m10 = 0.0f,
                    m11 = 1f,
                    m12 = 0.0f,
                    m13 = 0.0f,
                    m20 = 0.0f,
                    m21 = 0.0f,
                    m22 = 1f,
                    m23 = 0.0f,
                    m30 = 0.0f,
                    m31 = 0.0f,
                    m32 = 0.0f,
                    m33 = 1f
                };
            }
        }

        public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            return new Matrix4x4()
            {
                m00 = (float) ((double) lhs.m00 * (double) rhs.m00 + (double) lhs.m01 * (double) rhs.m10 + (double) lhs.m02 * (double) rhs.m20 + (double) lhs.m03 * (double) rhs.m30),
                m01 = (float) ((double) lhs.m00 * (double) rhs.m01 + (double) lhs.m01 * (double) rhs.m11 + (double) lhs.m02 * (double) rhs.m21 + (double) lhs.m03 * (double) rhs.m31),
                m02 = (float) ((double) lhs.m00 * (double) rhs.m02 + (double) lhs.m01 * (double) rhs.m12 + (double) lhs.m02 * (double) rhs.m22 + (double) lhs.m03 * (double) rhs.m32),
                m03 = (float) ((double) lhs.m00 * (double) rhs.m03 + (double) lhs.m01 * (double) rhs.m13 + (double) lhs.m02 * (double) rhs.m23 + (double) lhs.m03 * (double) rhs.m33),
                m10 = (float) ((double) lhs.m10 * (double) rhs.m00 + (double) lhs.m11 * (double) rhs.m10 + (double) lhs.m12 * (double) rhs.m20 + (double) lhs.m13 * (double) rhs.m30),
                m11 = (float) ((double) lhs.m10 * (double) rhs.m01 + (double) lhs.m11 * (double) rhs.m11 + (double) lhs.m12 * (double) rhs.m21 + (double) lhs.m13 * (double) rhs.m31),
                m12 = (float) ((double) lhs.m10 * (double) rhs.m02 + (double) lhs.m11 * (double) rhs.m12 + (double) lhs.m12 * (double) rhs.m22 + (double) lhs.m13 * (double) rhs.m32),
                m13 = (float) ((double) lhs.m10 * (double) rhs.m03 + (double) lhs.m11 * (double) rhs.m13 + (double) lhs.m12 * (double) rhs.m23 + (double) lhs.m13 * (double) rhs.m33),
                m20 = (float) ((double) lhs.m20 * (double) rhs.m00 + (double) lhs.m21 * (double) rhs.m10 + (double) lhs.m22 * (double) rhs.m20 + (double) lhs.m23 * (double) rhs.m30),
                m21 = (float) ((double) lhs.m20 * (double) rhs.m01 + (double) lhs.m21 * (double) rhs.m11 + (double) lhs.m22 * (double) rhs.m21 + (double) lhs.m23 * (double) rhs.m31),
                m22 = (float) ((double) lhs.m20 * (double) rhs.m02 + (double) lhs.m21 * (double) rhs.m12 + (double) lhs.m22 * (double) rhs.m22 + (double) lhs.m23 * (double) rhs.m32),
                m23 = (float) ((double) lhs.m20 * (double) rhs.m03 + (double) lhs.m21 * (double) rhs.m13 + (double) lhs.m22 * (double) rhs.m23 + (double) lhs.m23 * (double) rhs.m33),
                m30 = (float) ((double) lhs.m30 * (double) rhs.m00 + (double) lhs.m31 * (double) rhs.m10 + (double) lhs.m32 * (double) rhs.m20 + (double) lhs.m33 * (double) rhs.m30),
                m31 = (float) ((double) lhs.m30 * (double) rhs.m01 + (double) lhs.m31 * (double) rhs.m11 + (double) lhs.m32 * (double) rhs.m21 + (double) lhs.m33 * (double) rhs.m31),
                m32 = (float) ((double) lhs.m30 * (double) rhs.m02 + (double) lhs.m31 * (double) rhs.m12 + (double) lhs.m32 * (double) rhs.m22 + (double) lhs.m33 * (double) rhs.m32),
                m33 = (float) ((double) lhs.m30 * (double) rhs.m03 + (double) lhs.m31 * (double) rhs.m13 + (double) lhs.m32 * (double) rhs.m23 + (double) lhs.m33 * (double) rhs.m33)
            };
        }

        public static Vector4 operator *(Matrix4x4 lhs, Vector4 v)
        {
            Vector4 vector4;
            vector4.x = (float) ((double) lhs.m00 * (double) v.x + (double) lhs.m01 * (double) v.y + (double) lhs.m02 * (double) v.z + (double) lhs.m03 * (double) v.w);
            vector4.y = (float) ((double) lhs.m10 * (double) v.x + (double) lhs.m11 * (double) v.y + (double) lhs.m12 * (double) v.z + (double) lhs.m13 * (double) v.w);
            vector4.z = (float) ((double) lhs.m20 * (double) v.x + (double) lhs.m21 * (double) v.y + (double) lhs.m22 * (double) v.z + (double) lhs.m23 * (double) v.w);
            vector4.w = (float) ((double) lhs.m30 * (double) v.x + (double) lhs.m31 * (double) v.y + (double) lhs.m32 * (double) v.z + (double) lhs.m33 * (double) v.w);
            return vector4;
        }

        public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            if (lhs.GetColumn(0) == rhs.GetColumn(0) && lhs.GetColumn(1) == rhs.GetColumn(1) && lhs.GetColumn(2) == rhs.GetColumn(2))
                return lhs.GetColumn(3) == rhs.GetColumn(3);
            return false;
        }

        public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode()
        {
            return this.GetColumn(0).GetHashCode() ^ this.GetColumn(1).GetHashCode() << 2 ^ this.GetColumn(2).GetHashCode() >> 2 ^ this.GetColumn(3).GetHashCode() >> 1;
        }

        public override bool Equals(object other)
        {
            if (!(other is Matrix4x4))
                return false;
            Matrix4x4 matrix4x4 = (Matrix4x4) other;
            if (this.GetColumn(0).Equals((object) matrix4x4.GetColumn(0)) && this.GetColumn(1).Equals((object) matrix4x4.GetColumn(1)) && this.GetColumn(2).Equals((object) matrix4x4.GetColumn(2)))
                return this.GetColumn(3).Equals((object) matrix4x4.GetColumn(3));
            return false;
        }

        public static Matrix4x4 Inverse(Matrix4x4 matrix)
        { 
			float detA = Determinant(matrix);
			if (detA == 0)
				return Matrix4x4.identity;
			
			Matrix4x4 tempMatrix = new Matrix4x4()
			{
				//------0---------
				m00 = matrix.m11*matrix.m22*matrix.m33 + matrix.m12*matrix.m23*matrix.m31 + matrix.m13*matrix.m21*matrix.m32 - matrix.m11*matrix.m23*matrix.m32 - matrix.m12*matrix.m21*matrix.m33 - matrix.m13*matrix.m22*matrix.m31,						     								    
				m01 = matrix.m01*matrix.m23*matrix.m32 + matrix.m02*matrix.m21*matrix.m33 + matrix.m03*matrix.m22*matrix.m31 - matrix.m01*matrix.m22*matrix.m33 - matrix.m02*matrix.m23*matrix.m31 - matrix.m03*matrix.m21*matrix.m32,														     								    
				m02 = matrix.m01*matrix.m12*matrix.m33 + matrix.m02*matrix.m13*matrix.m32 + matrix.m03*matrix.m11*matrix.m32 - matrix.m01*matrix.m13*matrix.m32 - matrix.m02*matrix.m11*matrix.m33 - matrix.m03*matrix.m12*matrix.m31,
				m03 = matrix.m01*matrix.m13*matrix.m22 + matrix.m02*matrix.m11*matrix.m23 + matrix.m03*matrix.m12*matrix.m21 - matrix.m01*matrix.m12*matrix.m23 - matrix.m02*matrix.m13*matrix.m21 - matrix.m03*matrix.m11*matrix.m22,
				//-------1--------					     								    
				m10 = matrix.m10*matrix.m23*matrix.m32 + matrix.m12*matrix.m20*matrix.m33 + matrix.m13*matrix.m22*matrix.m30 - matrix.m10*matrix.m22*matrix.m33 - matrix.m12*matrix.m23*matrix.m30 - matrix.m13*matrix.m20*matrix.m32,
				m11 = matrix.m00*matrix.m22*matrix.m33 + matrix.m02*matrix.m23*matrix.m30 + matrix.m03*matrix.m20*matrix.m32 - matrix.m00*matrix.m23*matrix.m32 - matrix.m02*matrix.m20*matrix.m33 - matrix.m03*matrix.m22*matrix.m30,
				m12 = matrix.m00*matrix.m13*matrix.m32 + matrix.m02*matrix.m10*matrix.m33 + matrix.m03*matrix.m12*matrix.m30 - matrix.m00*matrix.m12*matrix.m33 - matrix.m02*matrix.m13*matrix.m30 - matrix.m03*matrix.m10*matrix.m32,
				m13 = matrix.m00*matrix.m12*matrix.m23 + matrix.m02*matrix.m13*matrix.m20 + matrix.m03*matrix.m10*matrix.m22 - matrix.m00*matrix.m13*matrix.m22 - matrix.m02*matrix.m10*matrix.m23 - matrix.m03*matrix.m12*matrix.m20,
				//-------2--------					     								    
				m20 = matrix.m10*matrix.m21*matrix.m33 + matrix.m11*matrix.m23*matrix.m30 + matrix.m13*matrix.m20*matrix.m31 - matrix.m10*matrix.m23*matrix.m31 - matrix.m11*matrix.m20*matrix.m33 - matrix.m13*matrix.m31*matrix.m30,
				m21 = matrix.m00*matrix.m23*matrix.m31 + matrix.m01*matrix.m20*matrix.m33 + matrix.m03*matrix.m21*matrix.m30 - matrix.m00*matrix.m21*matrix.m33 - matrix.m01*matrix.m23*matrix.m30 - matrix.m03*matrix.m20*matrix.m31,
				m22 = matrix.m00*matrix.m11*matrix.m33 + matrix.m01*matrix.m13*matrix.m31 + matrix.m03*matrix.m10*matrix.m31 - matrix.m00*matrix.m13*matrix.m31 - matrix.m01*matrix.m10*matrix.m33 - matrix.m03*matrix.m11*matrix.m30,
				m23 = matrix.m00*matrix.m13*matrix.m21 + matrix.m01*matrix.m10*matrix.m23 + matrix.m03*matrix.m11*matrix.m31 - matrix.m00*matrix.m11*matrix.m23 - matrix.m01*matrix.m13*matrix.m20 - matrix.m03*matrix.m10*matrix.m21,
				//------3---------					     								    
				m30 = matrix.m10*matrix.m22*matrix.m31 + matrix.m11*matrix.m20*matrix.m32 + matrix.m12*matrix.m21*matrix.m30 - matrix.m00*matrix.m21*matrix.m32 - matrix.m11*matrix.m22*matrix.m30 - matrix.m12*matrix.m20*matrix.m31,
				m31 = matrix.m00*matrix.m21*matrix.m32 + matrix.m01*matrix.m22*matrix.m30 + matrix.m02*matrix.m20*matrix.m31 - matrix.m00*matrix.m22*matrix.m31 - matrix.m01*matrix.m20*matrix.m32 - matrix.m02*matrix.m21*matrix.m30,
				m32 = matrix.m00*matrix.m12*matrix.m31 + matrix.m01*matrix.m10*matrix.m32 + matrix.m02*matrix.m11*matrix.m30 - matrix.m00*matrix.m11*matrix.m32 - matrix.m01*matrix.m12*matrix.m30- matrix.m02*matrix.m10*matrix.m31,
				m33 = matrix.m00*matrix.m11*matrix.m22 + matrix.m01*matrix.m12*matrix.m20 + matrix.m02*matrix.m10*matrix.m21 - matrix.m00*matrix.m12*matrix.m21 - matrix.m01*matrix.m10*matrix.m22 - matrix.m02*matrix.m11*matrix.m20
			};
			
			Matrix4x4 result = new Matrix4x4()
				{
					m00 = tempMatrix.m00 / detA,
                 	m01 = tempMatrix.m01 / detA,
                 	m02 = tempMatrix.m02 / detA,
                 	m03 = tempMatrix.m03 / detA,
                 	m10 = tempMatrix.m10 / detA,
                 	m11 = tempMatrix.m11 / detA,
                 	m12 = tempMatrix.m12 / detA,
                 	m13 = tempMatrix.m13 / detA,
                 	m20 = tempMatrix.m20 / detA,
                 	m21 = tempMatrix.m21 / detA,
                 	m22 = tempMatrix.m22 / detA,
                 	m23 = tempMatrix.m23 / detA,
                 	m30 = tempMatrix.m30 / detA,
                 	m31 = tempMatrix.m31 / detA,
                 	m32 = tempMatrix.m32 / detA,
                 	m33 = tempMatrix.m33 / detA

				};
			return result;
        }

        public static Matrix4x4 Transpose(Matrix4x4 m)
        {
			return new Matrix4x4()
			{
				m00 = m.m00,
				m01 = m.m10,
				m02 = m.m20,
				m03 = m.m30,
				m10 = m.m01,
				m11 = m.m11,
				m12 = m.m21,
				m13 = m.m31,
				m20 = m.m02,
				m21 = m.m12,
				m22 = m.m22,
				m23 = m.m32,
				m30 = m.m03,
				m31 = m.m13,
				m32 = m.m23,
				m33 = m.m33
			};
        }
		/// <summary>
		/// Invert the specified matrix.
		/// </summary>
		/// <param name="matrix">Matrix.</param>
		public static Matrix4x4 Invert(Matrix4x4 matrix)
		{
			Matrix4x4 result;
			Invert(ref matrix, out result);
			return result;
		}
		/// <summary>
		/// Invert the specified matrix and result.
		/// </summary>
		/// <param name="matrix">Matrix.</param>
		/// <param name="result">Result.</param>
		public static void Invert(ref Matrix4x4 matrix, out Matrix4x4 dest)
        {
			float num1 =  matrix.m00;
			float num2 =  matrix.m01;
			float num3 =  matrix.m02;
			float num4 =  matrix.m03;
			float num5 =  matrix.m10;
			float num6 =  matrix.m11;
			float num7 =  matrix.m12;
			float num8 =  matrix.m13;
			float num9 =  matrix.m20;
			float num10 = matrix.m21;
			float num11 = matrix.m22;
			float num12 = matrix.m23;
			float num13 = matrix.m30;
			float num14 = matrix.m31;
			float num15 = matrix.m32;
			float num16 = matrix.m33;
			float num17 = (float) ((double) num11 * (double) num16 - (double) num12 * (double) num15);
			float num18 = (float) ((double) num10 * (double) num16 - (double) num12 * (double) num14);
			float num19 = (float) ((double) num10 * (double) num15 - (double) num11 * (double) num14);
			float num20 = (float) ((double) num9 * (double) num16 - (double) num12 * (double) num13);
			float num21 = (float) ((double) num9 * (double) num15 - (double) num11 * (double) num13);
			float num22 = (float) ((double) num9 * (double) num14 - (double) num10 * (double) num13);
			float num23 = (float) ((double) num6 * (double) num17 - (double) num7 * (double) num18 + (double) num8 * (double) num19);
			float num24 = (float) -((double) num5 * (double) num17 - (double) num7 * (double) num20 + (double) num8 * (double) num21);
			float num25 = (float) ((double) num5 * (double) num18 - (double) num6 * (double) num20 + (double) num8 * (double) num22);
			float num26 = (float) -((double) num5 * (double) num19 - (double) num6 * (double) num21 + (double) num7 * (double) num22);
			float num27 = (float) (1.0 / ((double) num1 * (double) num23 + (double) num2 * (double) num24 + (double) num3 * (double) num25 + (double) num4 * (double) num26));

			Matrix4x4 result = new Matrix4x4();

			result.m00 = num23 * num27;
			result.m10 = num24 * num27;
			result.m20 = num25 * num27;
			result.m30 = num26 * num27;
			result.m01 = (float) -((double) num2 * (double) num17 - (double) num3 * (double) num18 + (double) num4 * (double) num19) * num27;
			result.m11 = (float) ((double) num1 * (double) num17 - (double) num3 * (double) num20 + (double) num4 * (double) num21) * num27;
			result.m21 = (float) -((double) num1 * (double) num18 - (double) num2 * (double) num20 + (double) num4 * (double) num22) * num27;
			result.m31 = (float) ((double) num1 * (double) num19 - (double) num2 * (double) num21 + (double) num3 * (double) num22) * num27;
			float num28 = (float) ((double) num7 * (double) num16 - (double) num8 * (double) num15);
			float num29 = (float) ((double) num6 * (double) num16 - (double) num8 * (double) num14);
			float num30 = (float) ((double) num6 * (double) num15 - (double) num7 * (double) num14);
			float num31 = (float) ((double) num5 * (double) num16 - (double) num8 * (double) num13);
			float num32 = (float) ((double) num5 * (double) num15 - (double) num7 * (double) num13);
			float num33 = (float) ((double) num5 * (double) num14 - (double) num6 * (double) num13);
			result.m02 = (float) ((double) num2 * (double) num28 - (double) num3 * (double) num29 + (double) num4 * (double) num30) * num27;
			result.m12 = (float) -((double) num1 * (double) num28 - (double) num3 * (double) num31 + (double) num4 * (double) num32) * num27;
			result.m22 = (float) ((double) num1 * (double) num29 - (double) num2 * (double) num31 + (double) num4 * (double) num33) * num27;
			result.m32 = (float) -((double) num1 * (double) num30 - (double) num2 * (double) num32 + (double) num3 * (double) num33) * num27;
			float num34 = (float) ((double) num7 * (double) num12 - (double) num8 * (double) num11);
			float num35 = (float) ((double) num6 * (double) num12 - (double) num8 * (double) num10);
			float num36 = (float) ((double) num6 * (double) num11 - (double) num7 * (double) num10);
			float num37 = (float) ((double) num5 * (double) num12 - (double) num8 * (double) num9);
			float num38 = (float) ((double) num5 * (double) num11 - (double) num7 * (double) num9);
			float num39 = (float) ((double) num5 * (double) num10 - (double) num6 * (double) num9);
			result.m03 = (float) -((double) num2 * (double) num34 - (double) num3 * (double) num35 + (double) num4 * (double) num36) * num27;
			result.m13 = (float) ((double) num1 * (double) num34 - (double) num3 * (double) num37 + (double) num4 * (double) num38) * num27;
			result.m23 = (float) -((double) num1 * (double) num35 - (double) num2 * (double) num37 + (double) num4 * (double) num39) * num27;
			result.m33 = (float) ((double) num1 * (double) num36 - (double) num2 * (double) num38 + (double) num3 * (double) num39) * num27;
        	
			dest = result;
		}


        public static float Determinant(Matrix4x4 m)
        {
			return 
				m[0,3] * m[1,2] * m[2,1] * m[3,0] - m[0,2] * m[1,3] * m[2,1] * m[3,0] -
				m[0,3] * m[1,1] * m[2,2] * m[3,0] + m[0,1] * m[1,3] * m[2,2] * m[3,0] +
				m[0,2] * m[1,1] * m[2,3] * m[3,0] - m[0,1] * m[1,2] * m[2,3] * m[3,0] -
				m[0,3] * m[1,2] * m[2,0] * m[3,1] + m[0,2] * m[1,3] * m[2,0] * m[3,1] +
				m[0,3] * m[1,0] * m[2,2] * m[3,1] - m[0,0] * m[1,3] * m[2,2] * m[3,1] -
				m[0,2] * m[1,0] * m[2,3] * m[3,1] + m[0,0] * m[1,2] * m[2,3] * m[3,1] +
				m[0,3] * m[1,1] * m[2,0] * m[3,2] - m[0,1] * m[1,3] * m[2,0] * m[3,2] -
				m[0,3] * m[1,0] * m[2,1] * m[3,2] + m[0,0] * m[1,3] * m[2,1] * m[3,2] +
				m[0,1] * m[1,0] * m[2,3] * m[3,2] - m[0,0] * m[1,1] * m[2,3] * m[3,2] -
				m[0,2] * m[1,1] * m[2,0] * m[3,3] + m[0,1] * m[1,2] * m[2,0] * m[3,3] +
				m[0,2] * m[1,0] * m[2,1] * m[3,3] - m[0,0] * m[1,2] * m[2,1] * m[3,3] -
				m[0,1] * m[1,0] * m[2,2] * m[3,3] + m[0,0] * m[1,1] * m[2,2] * m[3,3];
        }

        /// <summary>
        ///   <para>Get a column of the matrix.</para>
        /// </summary>
        /// <param name="i"></param>
        public Vector4 GetColumn(int i)
        {
            return new Vector4(this[0, i], this[1, i], this[2, i], this[3, i]);
        }

        /// <summary>
        ///   <para>Returns a row of the matrix.</para>
        /// </summary>
        /// <param name="i"></param>
        public Vector4 GetRow(int i)
        {
            return new Vector4(this[i, 0], this[i, 1], this[i, 2], this[i, 3]);
        }

        /// <summary>
        ///   <para>Sets a column of the matrix.</para>
        /// </summary>
        /// <param name="i"></param>
        /// <param name="v"></param>
        public void SetColumn(int i, Vector4 v)
        {
            this[0, i] = v.x;
            this[1, i] = v.y;
            this[2, i] = v.z;
            this[3, i] = v.w;
        }

        /// <summary>
        ///   <para>Sets a row of the matrix.</para>
        /// </summary>
        /// <param name="i"></param>
        /// <param name="v"></param>
        public void SetRow(int i, Vector4 v)
        {
            this[i, 0] = v.x;
            this[i, 1] = v.y;
            this[i, 2] = v.z;
            this[i, 3] = v.w;
        }

        /// <summary>
        ///   <para>Transforms a position by this matrix (generic).</para>
        /// </summary>
        /// <param name="v"></param>
        public Vector3 MultiplyPoint(Vector3 v)
        {
            Vector3 vector3;
            vector3.x = (float) ((double) this.m00 * (double) v.x + (double) this.m01 * (double) v.y + (double) this.m02 * (double) v.z) + this.m03;
            vector3.y = (float) ((double) this.m10 * (double) v.x + (double) this.m11 * (double) v.y + (double) this.m12 * (double) v.z) + this.m13;
            vector3.z = (float) ((double) this.m20 * (double) v.x + (double) this.m21 * (double) v.y + (double) this.m22 * (double) v.z) + this.m23;
            float num = 1f / ((float) ((double) this.m30 * (double) v.x + (double) this.m31 * (double) v.y + (double) this.m32 * (double) v.z) + this.m33);
            vector3.x *= num;
            vector3.y *= num;
            vector3.z *= num;
            return vector3;
        }

        /// <summary>
        ///   <para>Transforms a position by this matrix (fast).</para>
        /// </summary>
        /// <param name="v"></param>
        public Vector3 MultiplyPoint3x4(Vector3 v)
        {
            Vector3 vector3;
            vector3.x = (float) ((double) this.m00 * (double) v.x + (double) this.m01 * (double) v.y + (double) this.m02 * (double) v.z) + this.m03;
            vector3.y = (float) ((double) this.m10 * (double) v.x + (double) this.m11 * (double) v.y + (double) this.m12 * (double) v.z) + this.m13;
            vector3.z = (float) ((double) this.m20 * (double) v.x + (double) this.m21 * (double) v.y + (double) this.m22 * (double) v.z) + this.m23;
            return vector3;
        }

        /// <summary>
        ///   <para>Transforms a direction by this matrix.</para>
        /// </summary>
        /// <param name="v"></param>
        public Vector3 MultiplyVector(Vector3 v)
        {
            Vector3 vector3;
            vector3.x = (float) ((double) this.m00 * (double) v.x + (double) this.m01 * (double) v.y + (double) this.m02 * (double) v.z);
            vector3.y = (float) ((double) this.m10 * (double) v.x + (double) this.m11 * (double) v.y + (double) this.m12 * (double) v.z);
            vector3.z = (float) ((double) this.m20 * (double) v.x + (double) this.m21 * (double) v.y + (double) this.m22 * (double) v.z);
            return vector3;
        }

        /// <summary>
        ///   <para>Creates a scaling matrix.</para>
        /// </summary>
        /// <param name="v"></param>
        public static Matrix4x4 Scale(Vector3 v)
        {
            return new Matrix4x4()
            {
                m00 = v.x,
                m01 = 0.0f,
                m02 = 0.0f,
                m03 = 0.0f,
                m10 = 0.0f,
                m11 = v.y,
                m12 = 0.0f,
                m13 = 0.0f,
                m20 = 0.0f,
                m21 = 0.0f,
                m22 = v.z,
                m23 = 0.0f,
                m30 = 0.0f,
                m31 = 0.0f,
                m32 = 0.0f,
                m33 = 1f
            };
        }

        /// <summary>
        ///   <para>Sets this matrix to a translation, rotation and scaling matrix.</para>
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="q"></param>
        /// <param name="s"></param>
        public void SetTRS(Vector3 pos, Quaternion q, Vector3 s)
        {
            this = Matrix4x4.TRS(pos, q, s);
        }

        /// <summary>
        ///   <para>Creates a translation, rotation and scaling matrix.</para>
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="q"></param>
        /// <param name="s"></param>
        public static Matrix4x4 TRS(Vector3 pos, Quaternion q, Vector3 s)
        {
			Matrix4x4 tempMatrix4x4;
	        
	        tempMatrix4x4 = new Matrix4x4()
	        {
		        m00 = 1-2*q.y*q.y-2*q.z*q.z,
		        m01 = 2*q.x*q.y +2*q.w*q.z,
		        m02 = 2*q.x*q.z-2*q.w*q.y,
		        m03 = pos.x,
		        
		        m10 = 2*q.x*q.y-2*q.w*q.z,
		        m11 = 1-2*q.x*q.x-2*q.z*q.z,
		        m12 = 2*q.y*q.z+2*q.w*q.x,
		        m13 = pos.y,
		        
		        m20 = 2*q.x*q.z+2*q.w*q.y,
		        m21 = 2*q.y*q.z-2*q.w*q.x,
		        m22 = 1-2*q.x*q.x-2*q.y*q.y,
		        m23 = pos.z,
		        
		        m30 = s.x,
		        m31 = s.y,
		        m32 = s.z,
		        m33 = 0
	        };

	        return new Matrix4x4()
	        {
		        m00 = 1-2*q.y*q.y-2*q.z*q.z,
		        m01 = 2*q.x*q.y +2*q.w*q.z,
		        m02 = 2*q.x*q.z-2*q.w*q.y,
		        m03 = pos.x,
		        
		        m10 = 2*q.x*q.y-2*q.w*q.z,
		        m11 = 1-2*q.x*q.x-2*q.z*q.z,
		        m12 = 2*q.y*q.z+2*q.w*q.x,
		        m13 = pos.y,
		        
		        m20 = 2*q.x*q.z+2*q.w*q.y,
		        m21 = 2*q.y*q.z-2*q.w*q.x,
		        m22 = 1-2*q.x*q.x-2*q.y*q.y,
		        m23 = pos.z,
		        
		        m30 = s.x/(tempMatrix4x4.m00+tempMatrix4x4.m10+tempMatrix4x4.m20),
		        m31 = s.y/(tempMatrix4x4.m01+tempMatrix4x4.m11+tempMatrix4x4.m21),
		        m32 = s.z/(tempMatrix4x4.m02+tempMatrix4x4.m12+tempMatrix4x4.m22),
		        m33 = 0
	        };
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this matrix.</para>
        /// </summary>
        /// <param name="format"></param>
        public override string ToString()
        {
            return string.Format("{0:F5}\t{1:F5}\t{2:F5}\t{3:F5}\n{4:F5}\t{5:F5}\t{6:F5}\t{7:F5}\n{8:F5}\t{9:F5}\t{10:F5}\t{11:F5}\n{12:F5}\t{13:F5}\t{14:F5}\t{15:F5}\n", (object) this.m00, (object) this.m01, (object) this.m02, (object) this.m03, (object) this.m10, (object) this.m11, (object) this.m12, (object) this.m13, (object) this.m20, (object) this.m21, (object) this.m22, (object) this.m23, (object) this.m30, (object) this.m31, (object) this.m32, (object) this.m33);
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this matrix.</para>
        /// </summary>
        /// <param name="format"></param>
        public string ToString(string format)
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\n{4}\t{5}\t{6}\t{7}\n{8}\t{9}\t{10}\t{11}\n{12}\t{13}\t{14}\t{15}\n", (object) this.m00.ToString(format), (object) this.m01.ToString(format), (object) this.m02.ToString(format), (object) this.m03.ToString(format), (object) this.m10.ToString(format), (object) this.m11.ToString(format), (object) this.m12.ToString(format), (object) this.m13.ToString(format), (object) this.m20.ToString(format), (object) this.m21.ToString(format), (object) this.m22.ToString(format), (object) this.m23.ToString(format), (object) this.m30.ToString(format), (object) this.m31.ToString(format), (object) this.m32.ToString(format), (object) this.m33.ToString(format));
        }

        /// <summary>
        ///   <para>Creates an orthogonal projection matrix.</para>
        /// </summary>
		/// reference
		/// https://www.scratchapixel.com/lessons/3d-basic-rendering/perspective-and-orthographic-projection-matrix/orthographic-projection-matrix
		/// http://relativity.net.au/gaming/java/ProjectionMatrix.html
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="top"></param>
        /// <param name="zNear"></param>
        /// <param name="zFar"></param>
        public static Matrix4x4 Ortho(float left, float right, float bottom, float top, float zNear, float zFar)
        {
			return new Matrix4x4()
			{
				m00 = 2/(right-1),
				m01 = 0,
				m02 = 0,
				m03 = 0,

				m10 = 0,
				m11 = 2/(top-bottom),
				m12 = 0,
				m13 = 0,

				m20 = 0,
				m21 = 0,
				m22 = -2/(zFar-zNear),
				m23 = 0,

				m30 = -(right+1)/(right-1),
				m31 = -(top+bottom)/(top-bottom),
				m32 = -(zFar+zNear)/(zFar-zNear),
				m33 = 1f
			};
        }


        /// <summary>
        ///   <para>Creates a perspective projection matrix.</para>
        /// </summary>
        /// <param name="fov"></param>
        /// <param name="aspect"></param>
        /// <param name="zNear"></param>
        /// <param name="zFar"></param>
        public static Matrix4x4 Perspective(float fov, float aspect, float zNear, float zFar)
        {
			if ((fov <= 0f) || (fov >= 3.141593f))
			{
				throw new ArgumentException("zFar <= 0 or >= PI");
			}
			if (zNear <= 0f)
			{
				throw new ArgumentException("zNear <= 0");
			}
			if (zFar <= 0f)
			{
				throw new ArgumentException("zFar <= 0");
			}
			if (zNear >= zFar)
			{
				throw new ArgumentException("zNear >= zFar");
			}
			float num = 1f / ((float) Math.Tan((double) (fov * 0.5f)));
			float num9 = num / aspect;

			return new Matrix4x4()
			{ 
				m00 = num9,
				m01 = 0,
				m02 = 0,
				m03 = 0,

				m10 = 0,
				m11 = num,
				m12 = 0,
				m13 = 0,

				m20 = 0,
				m21 = 0,
				m22 = zFar/(zNear-zFar),
				m23 = -1,

				m30 = 0,
				m31 = 0,
				m32 = (zNear*zFar)/(zNear-zFar),
				m33 = 0
			};
        }
    }
}
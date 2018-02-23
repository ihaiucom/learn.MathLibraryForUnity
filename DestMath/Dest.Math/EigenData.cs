using System;
using UnityEngine;

namespace Dest.Math
{
	public class EigenData
	{
		private int _size;

		private float[] _diagonal;

		private float[,] _matrix;

		public int Size
		{
			get
			{
				return this._size;
			}
		}

		internal EigenData(float[] diagonal, float[,] matrix)
		{
			this._size = diagonal.Length;
			this._diagonal = diagonal;
			this._matrix = matrix;
		}

		public float GetEigenvalue(int index)
		{
			return this._diagonal[index];
		}

		public Vector2 GetEigenvector2(int index)
		{
			if (this._size == 2)
			{
				Vector2 result = default(Vector2);
				for (int i = 0; i < this._size; i++)
				{
					result[i] = this._matrix[i, index];
				}
				return result;
			}
			return Vector2ex.Zero;
		}

		public Vector3 GetEigenvector3(int index)
		{
			if (this._size == 3)
			{
				Vector3 result = default(Vector3);
				for (int i = 0; i < this._size; i++)
				{
					result[i] = this._matrix[i, index];
				}
				return result;
			}
			return Vector3ex.Zero;
		}

		public float[] GetEigenvector(int index)
		{
			float[] array = new float[this._size];
			for (int i = 0; i < this._size; i++)
			{
				array[i] = this._matrix[i, index];
			}
			return array;
		}

		public void GetEigenvector(int index, float[] out_eigenvector)
		{
			for (int i = 0; i < this._size; i++)
			{
				out_eigenvector[i] = this._matrix[i, index];
			}
		}
	}
}

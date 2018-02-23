using System;
using UnityEngine;

namespace Dest.Math
{
	public static class Quaternionex
	{
		public static Quaternion DeltaTo(this Quaternion quat, Quaternion target)
		{
			return target * Quaternion.Inverse(quat);
		}

		public static string ToStringEx(this Quaternion quat)
		{
			return string.Format("[{0}, {1}, {2}, {3}]", new object[]
			{
				quat.x.ToString(),
				quat.y.ToString(),
				quat.z.ToString(),
				quat.w.ToString()
			});
		}
	}
}

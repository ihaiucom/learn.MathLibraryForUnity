using System;
using UnityEngine;

namespace Dest.Math
{
	public class DefaultLogger : ILogger
	{
		public void LogInfo(object value)
		{
			Debug.Log(value);
		}

		public void LogWarning(object value)
		{
			Debug.LogWarning(value);
		}

		public void LogError(object value)
		{
			Debug.LogError(value);
		}
	}
}

using System;

namespace Dest.Math
{
	public class EmptyLogger : ILogger
	{
		public void LogInfo(object value)
		{
		}

		public void LogWarning(object value)
		{
		}

		public void LogError(object value)
		{
		}
	}
}

using System;

namespace Dest.Math
{
	public interface ILogger
	{
		void LogInfo(object value);

		void LogWarning(object value);

		void LogError(object value);
	}
}

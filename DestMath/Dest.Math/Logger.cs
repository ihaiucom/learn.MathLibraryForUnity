using System;

namespace Dest.Math
{
	public class Logger
	{
		private static ILogger _instance;

		static Logger()
		{
			Logger._instance = new DefaultLogger();
		}

		public static void LogInfo(object value)
		{
			Logger._instance.LogInfo(value);
		}

		public static void LogWarning(object value)
		{
			Logger._instance.LogWarning(value);
		}

		public static void LogError(object value)
		{
			Logger._instance.LogError(value);
		}

		public static void SetLogger(ILogger logger)
		{
			Logger._instance = ((logger != null) ? logger : new EmptyLogger());
		}
	}
}

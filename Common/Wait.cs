using System;

namespace Common
{
	public static class Wait
	{
		public static void Until(Func<bool> condition, TimeSpan timeout, string timeoutMessage)
		{
			var latestTime = DateTime.Now + timeout;
			while (DateTime.Now < latestTime)
			{
				if (condition())
					return;
			}

			throw new TimeoutException(timeoutMessage);
		}

		public static TimeSpan Seconds(this int seconds)
		{
			return TimeSpan.FromSeconds(seconds);
		}
	}
}
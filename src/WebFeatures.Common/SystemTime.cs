using System;

namespace WebFeatures.Common
{
	public static class SystemTime
	{
		internal static ISystemTime Instance
		{
			get => _instance ??= new DefaultSystemTime();
			set => _instance = value;
		}
		private static ISystemTime _instance;

		public static DateTime Now => Instance.Now;

		internal interface ISystemTime
		{
			DateTime Now { get; }
		}

		private class DefaultSystemTime : ISystemTime
		{
			public DateTime Now => DateTime.UtcNow;
		}
	}
}

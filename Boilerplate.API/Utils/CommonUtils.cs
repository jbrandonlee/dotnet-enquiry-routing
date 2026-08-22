namespace EnquiryRouting.Api.Utils
{
	public static class CommonUtils
	{
		public static DateTimeOffset SgtNow => DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(8));
		public static string ToIntString(this Enum value) => Convert.ToInt32(value).ToString();
	}
}

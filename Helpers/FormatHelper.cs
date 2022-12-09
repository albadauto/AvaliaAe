namespace AvaliaAe.Helpers
{
	public class FormatHelper
	{
		public string FormatCPF(string format)
		{
			return format.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
		}
	}
}

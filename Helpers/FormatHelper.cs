using AvaliaAe.Helpers.Interfaces;

namespace AvaliaAe.Helpers
{
	public class FormatHelper : IFormatHelper
	{
		public string FormatCPF(string format)
		{
			return format.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
		}
	}
}

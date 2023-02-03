using AvaliaAe.Helpers.Interfaces;
using AvaliaAe.Models;
using System.Xml.Linq;

namespace AvaliaAe.Helpers
{
    public class CalculationHelper : ICalculationHelper
    {
        public double CalculateAverage(List<int> notes)
        {
            if (notes.Any())
            {
                return (Math.Truncate(notes.Average() * 1000) / 1000);
            }
            else
            {
                return 0;
            }
        }
    }
}

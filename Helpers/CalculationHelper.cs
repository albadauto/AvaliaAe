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
                return notes.Average();
            }
            else
            {
                return 0;
            }
        }
    }
}

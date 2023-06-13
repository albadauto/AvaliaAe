using AvaliaAe.Helpers.Interfaces;
using AvaliaAe.Models;
using System.Xml.Linq;

namespace AvaliaAe.Helpers
{
    public class CalculationHelper : ICalculationHelper
    {
        public double CalculateAverage(List<int> notes)
        {

            return (notes.Any() ? (Math.Truncate(notes.Average() * 1000) / 1000) : 0);

        }
    }
}

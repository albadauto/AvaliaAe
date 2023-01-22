using AvaliaAe.Helpers.Interfaces;

namespace AvaliaAe.Helpers
{
    public class CalculationHelper : ICalculationHelper
    {
        public float CalculateAverage(List<float> notes)
        {
            return notes.Average();
        }
    }
}

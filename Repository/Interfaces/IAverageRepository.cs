using AvaliaAe.Models;

namespace AvaliaAe.Repository.Interfaces
{
    public interface IAverageRepository
    {
        public List<AverageModel> getAllAverage();
        public AverageModel insertAverage(AverageModel average);
        public AverageModel updateAverage(AverageModel average);
        public AverageModel getAverageByIdInstitution(int idInst);
    }
}

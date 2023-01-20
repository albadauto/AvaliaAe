using AvaliaAe.Models;

namespace AvaliaAe.Repository.Interfaces
{
    public interface IAvaliationRepository
    {
        public AvaliationModel InsertAvaliation(AvaliationModel avaliation);
        public List<AvaliationModel> GetAllComments(int idInst);

    }
}

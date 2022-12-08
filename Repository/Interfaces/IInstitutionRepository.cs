using AvaliaAe.Models;

namespace AvaliaAe.Repository.Interfaces
{
    public interface IInstitutionRepository
    {
        public InstitutionModel InsertNewInstitution(InstitutionModel institution);

        public List<InstitutionModel> GetAllInstitutions();
    }
}

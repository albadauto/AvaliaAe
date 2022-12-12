using AvaliaAe.Models;

namespace AvaliaAe.Repository.Interfaces
{
    public interface IInstitutionRepository
    {
        public InstitutionModel InsertNewInstitution(InstitutionModel institution);

        public List<InstitutionModel> GetAllInstitutions();

        public InstitutionModel GetInstitutionByEmailAndPassword(string cnpj, string password);

        public InstitutionModel GetInstitutionById(int id);
    }
}

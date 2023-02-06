using AvaliaAe.Models;

namespace AvaliaAe.Repository.Interfaces
{
    public interface IInstitutionRepository
    {
        public InstitutionModel InsertNewInstitution(InstitutionModel institution);

        public List<InstitutionModel> GetAllInstitutions();
        public List<InstitutionsViewModel> getAllInstitutionsAndAverage();
        public InstitutionModel GetInstitutionByEmailAndPassword(string cnpj, string password);

        public InstitutionModel GetInstitutionByEmailAndCnpj(string email, string cnpj);
        public InstitutionModel GetInstitutionById(int id);

        public void ResetPassword(int id, string password);
    }
}

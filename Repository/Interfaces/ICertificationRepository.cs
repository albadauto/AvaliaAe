using AvaliaAe.Models;

namespace AvaliaAe.Repository.Interfaces
{
    public interface ICertificationRepository
    {
        public CertificationModel insertNewCertification(CertificationModel model);
        public CertificationModel GetCertificationById(int idInst);
        public CertificationModel GetCertificationByCode(string code);

    }
}

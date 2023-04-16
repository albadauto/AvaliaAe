using AvaliaAe.Context;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;

namespace AvaliaAe.Repository
{
    public class CertificationRepository : ICertificationRepository
    {
        private readonly DatabaseContext _context;
        public CertificationRepository(DatabaseContext context)
        {
            _context = context; 
        }

        public CertificationModel GetCertificationById(int idInst)
        {
            var finded = _context.Certification.FirstOrDefault(x => x.InstitutionId == idInst);
            return finded;
        }

        public CertificationModel insertNewCertification(CertificationModel certification)
        {
            _context.Certification.Add(certification);
            _context.SaveChanges();
            return certification;
        }

       
    }
}

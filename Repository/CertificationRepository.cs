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

        public CertificationModel GetCertificationByCode(string code)
        {
            var finded = (from i in _context.Institution
                          join c in _context.Certification
                          on i.Id equals c.InstitutionId
                          join t in _context.InstitutionType
                          on i.InstitutionTypeId equals t.Id
                          where c.CodeCertification == code
                          select new { c.CodeCertification, i.InstitutionName, i.InstitutionType, InstitutionTypeName = t.Name }).FirstOrDefault();
            if(finded != null ) 
            {
                var certificationObj = new CertificationModel
                {
                    CodeCertification = finded.CodeCertification,
                    Institution = new InstitutionModel
                    {
                        InstitutionName = finded.InstitutionName,
                        InstitutionType = new InstitutionTypeModel
                        {
                            Name = finded.InstitutionTypeName
                        }
                    }
                };
                return certificationObj;

            }
            else
            {
                return null;
            }

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

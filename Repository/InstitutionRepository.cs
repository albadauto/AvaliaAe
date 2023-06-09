using AvaliaAe.Context;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;

namespace AvaliaAe.Repository
{
    public class InstitutionRepository : IInstitutionRepository
    {
        private readonly DatabaseContext _context;

        public InstitutionRepository(DatabaseContext context)
        {
            _context = context;
        }
        public List<InstitutionModel> GetAllInstitutions()
        {
            var result = _context.Institution.ToList();
            return result;
        }

        public List<InstitutionsViewModel> getAllInstitutionsAndAverage()
        {
            List<InstitutionsViewModel> viewModel = new List<InstitutionsViewModel>();
            var result = (from i in _context.Institution
                          join a in _context.Average on i.Id equals a.InstitutionId into leftJoin
                          from lj in leftJoin.DefaultIfEmpty()
                          join t in _context.InstitutionType
                          on i.InstitutionTypeId equals t.Id
                          select new { i.InstitutionName, Average = lj != null ? lj.Average : (double?)null, i.Address, i.Id, t.Name });
            foreach(var i in result)
            {
                viewModel.Add(new InstitutionsViewModel()
                {
                   Institution = new InstitutionModel()
                   {
                       Id = i.Id,
                       InstitutionName = i.InstitutionName,
                       Address = i.Address, 
                       InstitutionType = new InstitutionTypeModel
                       {
                           Name = i.Name
                       }
                   },
                   Average = new AverageModel()
                   {
                       Average = i.Average, 
                   },
                   
                });
            }
            return viewModel;
        }

        public CertificationInstitutionViewModel GetInstitutionAndCertification(int id)
        {
            var result = (from i in _context.Institution
                          join c in _context.Certification
                          on i.Id equals c.InstitutionId
                          where i.Id == id
                          select new { i.InstitutionName, Institution = i, c.CodeCertification }).FirstOrDefault();

            
                var certification = new CertificationInstitutionViewModel
                {
                    Institution = new InstitutionModel
                    {
                        Id = result.Institution.Id,
                        InstitutionName = result.InstitutionName,
                        Email = result.Institution.Email,
                        Address = result.Institution.Address,
                        Cep = result.Institution.Cep,
                        Number = result.Institution.Number,
                        Phone = result.Institution.Phone,   
                        District = result.Institution.District,
                        Description = result.Institution.Description,
                        OwnerCpf = result.Institution.OwnerCpf,
                        OwnerName = result.Institution.OwnerName,
                        Cnpj = result.Institution.Cnpj,

                    },

                    Certification = new CertificationModel
                    {
                        CodeCertification= result.CodeCertification,
                    }
                    
                };

            return certification;
        }

        public InstitutionModel GetInstitutionByEmailAndCnpj(string email, string cnpj)
		{
            var result = _context.Institution.FirstOrDefault(x => x.Email == email || x.Cnpj == cnpj);
            return result;
		}

		public InstitutionModel GetInstitutionByEmailAndPassword(string cnpj, string password)
		{
           var result = _context.Institution.FirstOrDefault(x => x.Cnpj == cnpj && x.Password == password);
           return result;
		}

        public InstitutionModel GetInstitutionById(int id)
        {
            var result = _context.Institution.FirstOrDefault(x => x.Id == id);
            return result;
        }

     

        public InstitutionModel InsertNewInstitution(InstitutionModel institution)
        {
            _context.Institution.Add(institution);
            _context.SaveChanges();
            return institution;
        }

        public void ResetPassword(int Id, string newPassword)
        {
            var result = GetInstitutionById(Id);
            if(result != null)
            {
                result.Password = newPassword;
                _context.Institution.Update(result);
                _context.SaveChanges();
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public InstitutionModel UpdateInstitution(InstitutionModel institution, int id)
        {

            var result = _context.Institution.FirstOrDefault(o => o.Id == id);
            if (result != null)
            {
                result.Address = institution.Address;
                result.Cnpj = institution.Cnpj;
                result.Phone = institution.Phone;   
                result.Cep = institution.Cep;
                result.Description = institution.Description;
                result.District = institution.District;
                result.Email = institution.Email;
                result.OwnerName = institution.OwnerName;
                result.OwnerCpf = institution.OwnerCpf;
                result.Number = institution.Number;
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("é nulo");
            }

            return result;
        }
    }
}

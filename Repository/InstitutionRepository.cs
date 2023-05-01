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
    }
}

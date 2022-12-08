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

        public InstitutionModel InsertNewInstitution(InstitutionModel institution)
        {
            _context.Institution.Add(institution);
            _context.SaveChanges();
            return institution;
        }
    }
}

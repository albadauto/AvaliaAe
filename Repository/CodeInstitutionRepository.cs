using AvaliaAe.Context;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;

namespace AvaliaAe.Repository
{
    public class CodeInstitutionRepository : ICodeInstitutionRepository
    {
        private readonly DatabaseContext _context;
        public CodeInstitutionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public CodeInstitutionModel FindCode(string code)
        {
            var result = _context.CodeInstitutions.FirstOrDefault(x => x.Code == code);
            return result;
        }

        public CodeInstitutionModel InsertNewCode(CodeInstitutionModel code)
        {
            _context.CodeInstitutions.Add(code);
            _context.SaveChanges();
            return code;
        }


    }
}

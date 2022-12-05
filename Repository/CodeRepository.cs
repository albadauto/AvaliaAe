using AvaliaAe.Context;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;

namespace AvaliaAe.Repository
{
    public class CodeRepository : ICodeRepository
    {
        private readonly DatabaseContext _context;

        public CodeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public CodeModel FindCode(string code)
        {
            CodeModel coded = _context.Code.FirstOrDefault(x => x.Code == code);
            return coded;
        }

        public CodeModel InsertNewCode(CodeModel code)
        {
            _context.Code.Add(code);
            _context.SaveChanges();
            return code;
        }


    }
}

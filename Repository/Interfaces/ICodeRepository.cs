using AvaliaAe.Models;

namespace AvaliaAe.Repository.Interfaces
{
    public interface ICodeRepository
    {
        public CodeModel InsertNewCode(CodeModel code);
        public CodeModel FindCode(string code);
    }
}

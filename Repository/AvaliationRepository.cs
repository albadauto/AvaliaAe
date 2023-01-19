using AvaliaAe.Context;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;

namespace AvaliaAe.Repository
{
    public class AvaliationRepository : IAvaliationRepository
    {
        private readonly DatabaseContext _context;
        public AvaliationRepository(DatabaseContext context)
        {
            _context = context;
        }
        public AvaliationModel InsertAvaliation(AvaliationModel avaliation)
        {
            _context.Avaliations.Add(avaliation);
            _context.SaveChanges();
            return avaliation;
        }
    }
}

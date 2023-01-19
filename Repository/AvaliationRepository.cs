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

        public List<AvaliationModel> GetAllComments(int idInst, int idUser)
        {
            List<AvaliationModel> avList = new List<AvaliationModel>();
            var result = (from c in _context.Avaliations
                          join l in _context.User
                          on c.UserId equals l.Id
                          join i in _context.Institution
                          on c.InstitutionId equals i.Id
                          where i.Id == idInst && l.Id == idUser
                          select new { l.Name, c.Comment, c.Note, i.InstitutionName });
           foreach(var avaliation in result)
            {
                avList.Add(new AvaliationModel()
                {
                    User = new UserModel()
                    {
                        Name = avaliation.Name
                    },
                    Institution = new InstitutionModel()
                    {
                        InstitutionName = avaliation.InstitutionName
                    },
                    Comment = avaliation.Comment,
                    Note = avaliation.Note
                });
            }

            return avList;
        }
    }
}

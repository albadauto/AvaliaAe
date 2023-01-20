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

        public List<AvaliationModel> GetAllComments(int idInst)
        {
            List<AvaliationModel> avList = new List<AvaliationModel>();
            var result = (from c in _context.Avaliations
                          join l in _context.User
                          on c.UserId equals l.Id
                          join i in _context.Institution
                          on c.InstitutionId equals i.Id
                          where i.Id == idInst
                          select new { l.Name, c.Comment, c.Note, i.InstitutionName, l.photo_uri, idAvaliation = c.Id}).OrderByDescending(x => x.idAvaliation);
           foreach(var avaliation in result)
            {
                avList.Add(new AvaliationModel()
                {
                    User = new UserModel()
                    {
                        Name = avaliation.Name,
                        photo_uri = avaliation.photo_uri
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

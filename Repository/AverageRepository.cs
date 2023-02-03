using AvaliaAe.Context;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;

namespace AvaliaAe.Repository
{
    public class AverageRepository : IAverageRepository
    {
        private readonly DatabaseContext _context;
        public AverageRepository(DatabaseContext context)
        {
            _context = context;
        }
        public List<AverageModel> getAllAverage()
        {
            List<AverageModel> averageModel = new List<AverageModel>();
            var result = (from a in _context.Average
                          join i in _context.Institution
                          on a.Institution.Id equals i.Id 
                          select new { a.Average, i.InstitutionName});
            foreach(var i in result)
            {
                averageModel.Add(new AverageModel()
                {
                    Average = i.Average,
                    Institution = new InstitutionModel()
                    {
                        InstitutionName = i.InstitutionName
                    }
                });
            }
            return averageModel;
        }

        public AverageModel getAverageByIdInstitution(int idInst)
        {
            var result = _context.Average.FirstOrDefault(x => x.Institution.Id == idInst);
            return result;
        }

        public AverageModel insertAverage(AverageModel average)
        {
            _context.Average.Add(average);
            _context.SaveChanges();
            return average;
        }

        public AverageModel updateAverage(AverageModel average)
        {
            var result = getAverageByIdInstitution(average.Institution.Id);
            if(result != null)
            {
                result.Average = average.Average;
            }
            return result;
        }
    }
}

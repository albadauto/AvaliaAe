
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
                          join t in _context.InstitutionType
                          on i.InstitutionTypeId equals t.Id
                          select new { a.Average, i.InstitutionName, t.Name, t.Id });
            foreach(var i in result)
            {
                averageModel.Add(new AverageModel()
                {
                    Average = i.Average,
                    Institution = new InstitutionModel()
                    {
                        InstitutionName = i.InstitutionName,
                        InstitutionType = new InstitutionTypeModel()
                        {
                            Id = i.Id,
                            Name = i.Name
                        }
                    }
                });
            }
            return averageModel;
        }

        public AverageModel getAverageByIdInstitution(int idInst)
        {
            var result = _context.Average.FirstOrDefault(x => x.InstitutionId == idInst);
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
            var result = getAverageByIdInstitution(average.InstitutionId);
            if(result != null)
            {
                result.Average = average.Average;
                _context.Average.Update(result);
                _context.SaveChanges();
            }
            return result;
        }
    }
}

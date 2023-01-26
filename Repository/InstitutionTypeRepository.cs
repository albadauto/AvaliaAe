using AvaliaAe.Context;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;

namespace AvaliaAe.Repository
{
	public class InstitutionTypeRepository : IInstitutionTypeRepository
	{
		private readonly DatabaseContext _context;
		public InstitutionTypeRepository(DatabaseContext context)
		{
			_context = context;
		}
		public List<InstitutionTypeModel> getAllInstitutionTypes()
		{
			var result = _context.InstitutionType.ToList();
			return result;
		}
	}
}

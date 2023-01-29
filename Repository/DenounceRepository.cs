using AvaliaAe.Context;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;

namespace AvaliaAe.Repository
{
	public class DenounceRepository : IDenounceRepository
	{
		private readonly DatabaseContext _context;
		public DenounceRepository(DatabaseContext context)
		{
			_context = context; 
		}
		public DenouncesModel InsertDenounce(DenouncesModel denounce)
		{
			_context.Denounces.Add(denounce);
			_context.SaveChanges();
			return denounce;
		}
	}
}

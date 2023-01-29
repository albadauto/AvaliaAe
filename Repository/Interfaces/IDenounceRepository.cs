using AvaliaAe.Models;

namespace AvaliaAe.Repository.Interfaces
{
	public interface IDenounceRepository
	{
		public DenouncesModel InsertDenounce(DenouncesModel denounce);
	}
}

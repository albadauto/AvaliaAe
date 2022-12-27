using AvaliaAe.Models;

namespace AvaliaAe.Repository.Interfaces
{
	public interface IAssociationRepository
	{
		public List<AssociationsModel> GetUserAndInstitution(int UserId);
	}
}

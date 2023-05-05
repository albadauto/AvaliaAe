using AvaliaAe.Models;

namespace AvaliaAe.Repository.Interfaces
{
	public interface IAssociationRepository
	{
		public List<AssociationsModel> GetUserAndInstitution(int UserId);

		public AssociationsModel InsertNewAssociation(AssociationsModel association);

		public bool RemoveInstitution(int idInst);

        public AssociationsModel GetAssociationWithP(int userId);

    }
}

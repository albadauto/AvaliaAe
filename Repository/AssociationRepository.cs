using AvaliaAe.Context;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;

namespace AvaliaAe.Repository
{
	public class AssociationRepository : IAssociationRepository
	{
		private readonly DatabaseContext _context;
		public AssociationRepository(DatabaseContext context)
		{
			_context = context;
		}
		public List<AssociationsModel> GetUserAndInstitution(int UserId)
		{
			List<AssociationsModel> associations = new List<AssociationsModel>();	
			var result = (from u in _context.User
						  join a in _context.Associations
							on u.Id equals a.UserModel.Id
						  join i in _context.Institution
							on a.InstitutionModel.Id equals i.Id
						  where a.UserModelId == UserId && a.Status != "P"
						  select new { u.Name, i.InstitutionName, a.Status }
						  );
			foreach(var a in result )
			{
				associations.Add( new AssociationsModel { 
					InstitutionModel = new InstitutionModel { InstitutionName = a.InstitutionName },
					UserModel = new UserModel { Name = a.Name },
					Status = a.Status
				} );
			}
			return associations;
		}

        public AssociationsModel InsertNewAssociation(AssociationsModel association)
        {
            _context.Associations.Add(association);
			_context.SaveChanges();
			return association;
        }
    }
}

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
                            on u.Id equals a.User.Id
                          join i in _context.Institution
                            on a.Institution.Id equals i.Id
                          where a.UserId == UserId && a.Status !=  "P"
                          select new { u.Name, i.InstitutionName, a.Status, a.Id, institutionId = i.Id }
                          );
            foreach (var a in result)
            {
                associations.Add(new AssociationsModel
                {
                    Institution = new InstitutionModel { InstitutionName = a.InstitutionName, Id = a.institutionId },
                    User = new UserModel { Name = a.Name },
                    Status = a.Status,
                    Id = a.Id
                });
            }
            return associations;
        }

        public AssociationsModel InsertNewAssociation(AssociationsModel association)
        {
            _context.Associations.Add(association);
            _context.SaveChanges();
            return association;
        }

        public bool RemoveInstitution(int idInst)
        {
            var result = _context.Associations.FirstOrDefault(x => x.Id == idInst);

            _context.Associations.Remove(result);

            _context.SaveChanges();
            return true;
        }

    }
}

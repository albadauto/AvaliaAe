using AvaliaAe.Context;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;

namespace AvaliaAe.Repository
{
    public class DocumentationRepository : IDocumentationRepository
    {
        private readonly DatabaseContext _context;
        public DocumentationRepository(DatabaseContext context) 
        { 
            _context = context;  
        }

        public List<DocumentationModel> GetDocumentation(DocumentationModel model)
        {
            List<DocumentationModel> listDocumentation = new List<DocumentationModel>();
            var result = (from d in _context.Documentations
                          join a in _context.Associations
                             on d.AssociationsId equals a.Id
                          join u in _context.User
                             on a.UserId equals u.Id    
                          join i in _context.Institution
                             on a.InstitutionId equals i.Id
                          where a.UserId == model.Associations.User.Id && model.Associations.Status == "P"
                          select new { u.Name, i.InstitutionName, d.doc_uri }
                          );
            foreach(var i in result)
            {
                listDocumentation.Add(new DocumentationModel()
                {
                    Associations = new AssociationsModel()
                    {
                        User = new UserModel()
                        {
                            Name = i.Name,
                        },
                        Institution = new InstitutionModel()
                        {
                            InstitutionName = i.InstitutionName,
                        }
                    },
                   doc_uri = i.doc_uri
                });
            }

            return listDocumentation;
        }

     

        public DocumentationModel InsertNewDocumenation(DocumentationModel model, string docUri)
        {
            model.doc_uri = docUri;
            _context.Documentations.Add(model);
            _context.SaveChanges();
            return model;

        }
    }
}

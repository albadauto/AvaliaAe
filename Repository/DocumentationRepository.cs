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
            foreach (var i in result)
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

        public DocumentationAvaliationViewModel GetInstitution(int IdInst)
        {

            List<AvaliationModel> listAvaliation = new List<AvaliationModel>();
            var result = (from a in _context.Associations
                          join d in _context.Documentations
                             on a.Id equals d.AssociationsId into assocDocs
                          from ad in assocDocs.DefaultIfEmpty()
                          join u in _context.User
                             on a.UserId equals u.Id
                          join i in _context.Institution
                             on a.InstitutionId equals i.Id
                          join j in _context.Average
                            on i.Id equals j.InstitutionId
                          where i.Id == IdInst
                          select new { u.Name, i.InstitutionName, ad.doc_uri, u.photo_uri, i.Email, i.Address, i.Cep, i.Cnpj, i.Description, i.District, i.Number, j.Average, i.OwnerName, a.UserId, a.InstitutionId }
                          ).FirstOrDefault();

            var avaliations = (from u in _context.User
                               join t in _context.Avaliations
                               on u.Id equals t.UserId
                               where t.InstitutionId == IdInst
                               select new { u.Name, u.photo_uri, t.Note, t.Comment }
                               );
            foreach (var i in avaliations)
            {
                listAvaliation.Add(new AvaliationModel
                {
                    Comment = i.Comment,
                    User = new UserModel
                    {
                        Name = i.Name,
                        photo_uri = i.photo_uri,
                    },
                    Note = i.Note
                });
            }

            DocumentationAvaliationViewModel documentations = new DocumentationAvaliationViewModel()
            {
                Avaliations = listAvaliation,
                Institution = new InstitutionModel
                {
                    Id = result.InstitutionId,
                    InstitutionName = result.InstitutionName,
                    Cep = result.Cep,
                    Description = result.Description,
                    District = result.District,
                    Number = result.Number,
                    Cnpj = result.Cnpj,
                    Email = result.Email,
                    OwnerName = result.OwnerName,
                    Address = result.Address,
                    Average = new AverageModel
                    {
                        Average = result.Average,
                    },
                },
            };

            return documentations;
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

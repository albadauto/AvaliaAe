using AvaliaAe.Context;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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
            var institution = _context.Institution.FirstOrDefault(x => x.Id == IdInst);
            var average = _context.Average.FirstOrDefault(x => x.InstitutionId == IdInst);
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
                    Note = i.Note,
                   
                });
            }

            DocumentationAvaliationViewModel documentations = new DocumentationAvaliationViewModel()
            {
                Avaliations = listAvaliation != null ? listAvaliation : new List<AvaliationModel>(),
                Institution = new InstitutionModel
                {
                    Id = institution.Id,
                    InstitutionName = institution.InstitutionName,
                    Cep = institution.Cep,
                    Description = institution.Description,
                    District = institution.District,
                    Number = institution.Number,
                    Cnpj = institution.Cnpj,
                    Email = institution.Email,
                    OwnerName = institution.OwnerName,
                    Address = institution.Address,
                    Average = new AverageModel
                    {
                        Average = average != null ? average.Average : 0
                    }
                },
           
            };

            return documentations;
        }




        public DocumentationModel InsertNewDocumenation(DocumentationModel model, string docUri)
        {
            model.doc_uri = docUri;
            Console.WriteLine(model.InstitutionId);
            _context.Documentations.Add(model);
            _context.SaveChanges();
            return model;

        }
    }
}

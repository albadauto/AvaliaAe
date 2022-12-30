using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class ToAssociateController : Controller
    {
        private readonly IDocumentationRepository _repository;
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IAssociationRepository _associationRepository;
        private readonly IUserRepository _userRepository;
        private string pathServer;
        public ToAssociateController(IDocumentationRepository repository, IInstitutionRepository institutionRepository, IWebHostEnvironment environment, IAssociationRepository associationRepository, IUserRepository userRepository)
        {
            pathServer = environment.WebRootPath;
            _repository = repository;
            _institutionRepository = institutionRepository;
        }
        public IActionResult Index(int id)
        {
            var result = _institutionRepository.GetInstitutionById(id);
            DocumentationModel doc = new DocumentationModel()
            {
                Associations = new AssociationsModel()
                {
                    InstitutionModel = result,
                },


            };
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(doc);
        }

        [HttpPost]
        public async Task<IActionResult> UploadDoc(DocumentationModel model)
        {
            string path = pathServer + "\\docs\\";
            string newName = Guid.NewGuid() + "_" + model.File.FileName;
            if (model.File != null)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var stream = System.IO.File.Create(path + newName))
                {
                    await model.File.CopyToAsync(stream);
                }

                _repository.InsertNewDocumenation(new DocumentationModel()
                {
                    Associations = new AssociationsModel()
                    {
                        UserModelId = Convert.ToInt32(HttpContext.Session.GetInt32("Id")),
                        Status = "P",
                        InstitutionModelId = model.Associations.InstitutionModel.Id
                    },
                    FileName = newName
                }, $"/wwwroot/docs/{newName}");

                TempData["successAssociation"] = "Associação enviada para analise manual. Favor, aguardar a aprovação de um administrador";
            }
            else
            {
                Console.WriteLine("é nulo");
            }

            return RedirectToAction("Index", new { id = model.Associations.InstitutionModel.Id });
        }

        public IActionResult ShowFile(string filename)
        {
            if (filename == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return File($"docs/{filename}", "application/pdf");
        }
    }

}

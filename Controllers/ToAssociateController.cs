using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class ToAssociateController : Controller
    {
        private readonly IDocumentationRepository _repository;
        private readonly IInstitutionRepository _institutionRepository;
        private string pathServer;
        public ToAssociateController(IDocumentationRepository repository, IInstitutionRepository institutionRepository, IWebHostEnvironment environment)
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

            if(model == null)
            {
                return RedirectToAction("Index", new { id = model.Associations.InstitutionModel.Id });
            }

            if (Path.GetExtension(model.File.FileName).ToLower() != ".pdf")
            {
                TempData["errorAssociation"] = "Por favor, selecione um arquivo PDF";
                return RedirectToAction("Index", new { id = model.Associations.InstitutionModel.Id });
            }

            if (model.File != null)
            {
                string newName = Guid.NewGuid() + "_" + model.File.FileName;

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
                TempData["errorAssociation"] = "Por favor, selecione um arquivo para a associação a esta instituição";
                return RedirectToAction("Index", new { id = model.Associations.InstitutionModel.Id });
            }

            return RedirectToAction("Index", new { id = model.Associations.InstitutionModel.Id });
        }

        public IActionResult ShowFile()
        {
            var file = RouteData.Values["filename"];
            return File($"/docs/{file}", "application/pdf");
        }
    }

}

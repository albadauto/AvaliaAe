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
        private readonly IAssociationRepository _associationRepository;
        private string pathServer;
        public ToAssociateController(IDocumentationRepository repository, IInstitutionRepository institutionRepository, IWebHostEnvironment environment, IAssociationRepository associationRepository)
        {
            pathServer = environment.WebRootPath;
            _repository = repository;
            _institutionRepository = institutionRepository;
            _associationRepository = associationRepository;
        }
        public IActionResult Index(int id)
        {
           
            var result = _repository.GetInstitution(id);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UploadDoc(DocumentationModel model)
        {
            string path = pathServer + "\\docs\\";
            var getAssociation = _associationRepository.GetAssociationWithP((int)HttpContext.Session.GetInt32("Id"));
            if (model == null)
            {
                return RedirectToAction("Index", new { id = model.InstitutionId });
            }

            if (getAssociation != null )
            {
                TempData["errorAssociation"] = $"Erro: Você já se associou a uma instituição, favor aguardar a resposta de um administrador.";
                return RedirectToAction("Index", new { id = model.InstitutionId });
            }

            if (Path.GetExtension(model.File.FileName).ToLower() != ".pdf")
            {
                TempData["errorAssociation"] = "Por favor, selecione um arquivo PDF";
                return RedirectToAction("Index", new { id = model.InstitutionId });
            }

           

            if (model.File != null)
            {

                string newName = Guid.NewGuid() + "_" + new DateTime().Year + Path.GetExtension(model.File.FileName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var stream = System.IO.File.Create(path + newName))
                {
                    await model.File.CopyToAsync(stream);
                }

                Console.WriteLine($"Id vindo de Institution {model.InstitutionId}");


                _repository.InsertNewDocumenation(new DocumentationModel()
                {
                    Associations = new AssociationsModel()
                    {
                        UserId = Convert.ToInt32(HttpContext.Session.GetInt32("Id")),
                        Status = "P",
                        InstitutionId = model.InstitutionId
                    },
                    FileName = newName,
                    InstitutionId = model.InstitutionId,
                    UserId = Convert.ToInt32(HttpContext.Session.GetInt32("Id")),
                }, $"/docs/{newName}");
                TempData["successAssociation"] = "Associação enviada para analise manual. Favor, aguardar a aprovação de um administrador";
            }
            else
            {
                TempData["errorAssociation"] = "Por favor, selecione um arquivo para a associação a esta instituição";
                return RedirectToAction("Index", new { id = model.InstitutionId });
            }

            return RedirectToAction("Index", new { id = model.InstitutionId });
        }

        public IActionResult ShowFile()
        {
            string filename = Request.Path.Value.Split('/').LastOrDefault();
            if (Path.GetExtension(filename) != ".pdf") return RedirectToAction("Index", "Home");
            return File($"/docs/{filename}", "application/pdf");
        }

        public IActionResult AssociationPerson(int Id)
        {
            if (HttpContext.Session.GetString("email") == null || Id == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }

}

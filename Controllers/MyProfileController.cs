using AvaliaAe.Helpers;
using AvaliaAe.Helpers.Interfaces;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace AvaliaAe.Controllers
{
    public class MyProfileController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IAssociationRepository _associationRepository;
        private readonly IEmail _mail;
        private readonly ICodeRepository _code;
        private readonly ICertificationRepository _certification;
        private readonly IAverageRepository _average;
        private string pathServer;

        public MyProfileController(IUserRepository repository,
            IInstitutionRepository institutionRepository,
            IWebHostEnvironment webHost,
            IAssociationRepository associationRepository,
            IEmail mail,
            ICodeRepository code,
            ICertificationRepository certification,
            IAverageRepository average)
        {
            _repository = repository;
            _institutionRepository = institutionRepository;
            pathServer = webHost.WebRootPath;
            _associationRepository = associationRepository;
            _mail = mail;
            _code = code;
            _certification = certification;
            _average = average;
        }

        public IActionResult Index()
        {

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult DeleteAccount(UserPhotoViewModel user)
        {
            Random rand = new Random();
            var cod = rand.Next().ToString().Substring(0, 5);
            _mail.SendMail(user.userModel.Email, "Exclusão de conta no Avalia Ae!", $"Digite o código: {cod} para excluir a conta");
            _code.InsertNewCode(new CodeModel { Code = cod, UserId = Convert.ToInt32(HttpContext.Session.GetInt32("Id")) });
            return View(user);
        }

        public IActionResult Profile(int id)
        {
            if (HttpContext.Session.GetString("type") != "user" || HttpContext.Session.GetString("type") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (HttpContext.Session.GetInt32("Id") != id)
            {
                return RedirectToAction("Index", "Login");
            }
            UserPhotoViewModel userModel = new UserPhotoViewModel()
            {
                userModel = _repository.GetUser(id),
                associations = _associationRepository.GetUserAndInstitution(id)
            };

            return View(userModel);
        }

        public IActionResult InstitutionProfile(int id)
        {
            if (HttpContext.Session.GetString("type") != "inst" || HttpContext.Session.GetString("type") == null || HttpContext.Session.GetInt32("Id") != id)
            {
                return RedirectToAction("Index", "Home");
            }
            InstitutionModel inst = _institutionRepository.GetInstitutionById(id);
            if (inst == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(inst);
        }

        private async Task<bool> FileUploadToFolder(IFormFile file, string name)
        {
            try
            {
                string pathImage = pathServer + "\\img\\profile_photos\\";
                if (!Directory.Exists(pathImage))
                {
                    Directory.CreateDirectory(pathImage);
                }
                using (var stream = System.IO.File.Create(pathImage + name))
                {
                    await file.CopyToAsync(stream);
                }
                return true;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            

        }

        private bool verifyExtension(IFormFile file)
        {
            if (Path.GetExtension(file.FileName).ToLower() != ".jpg" && Path.GetExtension(file.FileName).ToLower() != ".png" && Path.GetExtension(file.FileName).ToLower() != ".jpeg")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(UserPhotoViewModel User)
        {
            try
            {
                if (User.File != null)
                {
                    if (!verifyExtension(User.File))
                    {
                        TempData["errorProfile"] = "Erro: Arquivo invalido";
                        return RedirectToAction("Profile", new { Id = HttpContext.Session.GetInt32("Id") });
                    }
                    string newName = Guid.NewGuid().ToString() + "_" + User.File.FileName;
                    await FileUploadToFolder(User.File, newName); //Upload da foto
                    _repository.UpdateUser(User.userModel, $"/img/profile_photos/{newName}"); //Update na tabela de usuário
                }
                else
                {
                    _repository.UpdateUser(User.userModel, ""); //Arrumar isso
                }
                TempData["successProfile"] = "Informações atualizadas com sucesso!";
                return RedirectToAction("Profile", new { Id = HttpContext.Session.GetInt32("Id") });


            }
            catch (Exception e)
            {
                TempData["errorProfile"] = "Erro: Contatar um administrador. Erro original: " + e.Message;
                return RedirectToAction("Profile", new { Id = HttpContext.Session.GetInt32("Id") });
            }

        }

      
        public IActionResult RemoveInstutionFromUser(int idInst)
        {
            try
            {
                _associationRepository.RemoveInstitution(idInst);
                TempData["successProfile"] = "Instituição desassociada com sucesso";
                return RedirectToAction("Profile", new { id = HttpContext.Session.GetInt32("Id") });
            }
            catch (Exception e)
            {
                TempData["errorProfile"] = $"Erro ao tentar desassociar este usuário a instituição selecionada, erro original: {e.Message}";
                return RedirectToAction("Profile", new { id = HttpContext.Session.GetInt32("Id") });
            }
        }
        [HttpPost]
        public IActionResult DeleteAccountFromAll(UserPhotoViewModel viewModel)
        {
            int idUser = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            var findedCode = _code.FindCode(viewModel.codeModel.Code);
            if(findedCode != null)
            {
                _repository.DeleteUser(idUser);
                HttpContext.Session.Remove("Id");
                HttpContext.Session.Remove("email");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["errorDelete"] = "Código inválido";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GenerateNewCertification(int id)
        {
            try
            {
                if (_certification.GetCertificationById(id) != null)
                {
                    TempData["errorCertification"] = "Erro: esta instituição já tem um certificado";
                    return RedirectToAction("InstitutionProfile", new { Id = id });

                }
                var average = _average.getAverageByIdInstitution(id) ?? null;
                if (average == null || average.Average < 4.7)
                {
                    TempData["errorCertification"] = "A média dessa instituição não corresponde ao mínimo (4.7) para gerar um certificado";
                    return RedirectToAction("InstitutionProfile", new { Id = id });

                }
                _certification.insertNewCertification(new CertificationModel()
                {
                    InstitutionId = id,
                    CodeCertification = Guid.NewGuid().ToString()
                });
                TempData["successCertification"] = "Certificação gerada com sucesso!";
                return RedirectToAction("InstitutionProfile", new { Id = id });   
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

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
        private string pathServer;

        public MyProfileController(IUserRepository repository,
            IInstitutionRepository institutionRepository,
            IWebHostEnvironment webHost,
            IAssociationRepository associationRepository,
            IEmail mail,
            ICodeRepository code)
        {
            _repository = repository;
            _institutionRepository = institutionRepository;
            pathServer = webHost.WebRootPath;
            _associationRepository = associationRepository;
            _mail = mail;
            _code = code;
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
            _code.InsertNewCode(new CodeModel { Code = cod, UserModelId = Convert.ToInt32(HttpContext.Session.GetInt32("Id")) });
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

        public IActionResult ForTest()
        {
            var result = _associationRepository.GetUserAndInstitution(1002);
            foreach (var value in result)
            {
                Console.WriteLine(value.UserModel.Name);
                Console.WriteLine(value.InstitutionModel.InstitutionName);
                Console.WriteLine(value.Status);

            }
            Console.WriteLine("Opa");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(UserPhotoViewModel User)
        {
            try
            {
                string pathImage = pathServer + "\\img\\profile_photos\\";
                /*if (Path.GetExtension(User.File.FileName).ToLower() != ".jpg" || Path.GetExtension(User.File.FileName).ToLower() != ".png" || Path.GetExtension(User.File.FileName).ToLower() != ".jpeg")
                {
                    return RedirectToAction("Index");
                }*/
                if (User.File != null)
                {
                    string newName = Guid.NewGuid().ToString() + "_" + User.File.FileName;
                    if (!Directory.Exists(pathImage))
                    {
                        Directory.CreateDirectory(pathImage);
                    }


                    using (var stream = System.IO.File.Create(pathImage + newName))
                    {
                        await User.File.CopyToAsync(stream);
                    }
                    _repository.UpdateUser(User.userModel, $"/img/profile_photos/{newName}");
                }
                else
                {
                    _repository.UpdateUser(User.userModel, $"/img/profile_photos/CoLocarNome");
                }

                TempData["successProfile"] = "Informações atualizadas com sucesso!";
                return RedirectToAction("Profile", new { Id = HttpContext.Session.GetInt32("Id") });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //return RedirectToAction("Profile", new { Id = HttpContext.Session.GetInt32("Id") });
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
    }
}

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
        private string pathServer;

        public MyProfileController(IUserRepository repository, IInstitutionRepository institutionRepository, IWebHostEnvironment webHost, IAssociationRepository associationRepository)
        {
            _repository = repository;
            _institutionRepository = institutionRepository;
            pathServer = webHost.WebRootPath;
            _associationRepository = associationRepository;
        }

        public ActionResult Index()
        {

            return RedirectToAction("Index", "Home");
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
            foreach(var value in result)
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

                /*if(Path.GetExtension(User.File.FileName).ToLower() != "jpg")
                {
                    return RedirectToAction("Index");
                }*/
                if(User.File != null)
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

                TempData["successMessageUpdate"] = "Informações atualizadas com sucesso!";
                return RedirectToAction("Profile", new { Id = HttpContext.Session.GetInt32("Id")});
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
                //return RedirectToAction("Profile", new { Id = HttpContext.Session.GetInt32("Id") });
            }
            
        }
    }
}

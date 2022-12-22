﻿using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace AvaliaAe.Controllers
{
    public class MyProfileController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IInstitutionRepository _institutionRepository;
        private string pathServer;

        public MyProfileController(IUserRepository repository, IInstitutionRepository institutionRepository, IWebHostEnvironment webHost)
        {
            _repository = repository;
            _institutionRepository = institutionRepository;
            pathServer = webHost.WebRootPath;
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
            UserModel result = _repository.GetUser(id);
            return View(result);
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
        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            string pathImage = pathServer + "\\images\\";
            string newName = Guid.NewGuid().ToString() + "_" + file.FileName; 
            if(!Directory.Exists(pathImage))
            {
                Directory.CreateDirectory(pathImage);
            }

            using(var stream = System.IO.File.Create(pathImage + newName))
            {
                file.CopyToAsync(stream);
            }
            return RedirectToAction("Index");
        }
    }
}

﻿using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class InstitutionsController : Controller
    {
        private readonly IInstitutionRepository _repository;
        public InstitutionsController(IInstitutionRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var result = _repository.getAllInstitutionsAndAverage();
            return View(result);
        }

        public IActionResult InsertNewInstitution()
        {
            return View();
        }

       [HttpPost]
       public IActionResult UploadFile(IFormFile file)
        {
            return RedirectToAction("Index");
        }

    }
}

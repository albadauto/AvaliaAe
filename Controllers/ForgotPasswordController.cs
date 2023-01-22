using AvaliaAe.Helpers;
using AvaliaAe.Helpers.Interfaces;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace AvaliaAe.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly IEmail _mail;
        private readonly IUserRepository _repository;
        private readonly ICodeRepository _codeRepository;
        private readonly IInstitutionRepository _institutionRepository;
        private readonly ICodeInstitutionRepository _codeInstitutionRepository;
        private readonly IFormatHelper _format;
        public ForgotPasswordController(IEmail mail, IUserRepository repository, ICodeRepository codeRepository, IInstitutionRepository institutionRepository, ICodeInstitutionRepository codeInstitutionRepository, IFormatHelper format)
        {
            _mail = mail;
            _repository = repository;
            _codeRepository = codeRepository;
            _institutionRepository = institutionRepository;
            _codeInstitutionRepository = codeInstitutionRepository;
            _format = format;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InstitutionRecuperation()
        {
            return View();
        }

        public IActionResult CompleteVerification()
        {
            return View();
        }
        public IActionResult CompleteVerificationInstitution()
        {
            return View();
        }
        private string VerificationCodeGenerate()
        {
            Random rand = new Random();
            return rand.Next().ToString().Substring(0, 5);

        }

        [HttpPost]
        public IActionResult VerifyEmail(UserModel model)
        {
            var result = _repository.VerifyIfEmailIsValid(model.Email);
            ForgotPasswordViewModel viewModel = new ForgotPasswordViewModel();
            foreach (var value in result)
            {
                viewModel.Email = value.Email;
                viewModel.Id = value.Id;
            }

            if (viewModel.Email != null)
            {
                var cod = VerificationCodeGenerate();
                _mail.SendMail(model.Email, "(Não compartilhe com ninguém!) Recuperação de senha - Avalia Aê!", $"Utilize o código: {cod} para recuperar a senha");
                _codeRepository.InsertNewCode(new CodeModel { Code = cod, UserModelId = viewModel.Id });
                HttpContext.Session.SetInt32("idUser", viewModel.Id);
                return RedirectToAction("CompleteVerification");

            }
            else
            {
                TempData["errorMail"] = "Email inexistente";
                return RedirectToAction("Index");

            }

        }
        [HttpPost]
        public IActionResult CreateNewPassword(CreatePasswordViewModel newUser)
        {
            var findedCode = _codeRepository.FindCode(newUser.Code);
            CryptographyHelper crypt = new CryptographyHelper(SHA256.Create());

            if (findedCode != null)
            {

                int idUser = HttpContext.Session.GetInt32("idUser") ?? 0;
                _repository.ResetPassword(new UserModel() { Id = idUser, Password = crypt.hashPassword(newUser.User.Password) });
                TempData["successEdit"] = "Senha atualizada com sucesso!";
                HttpContext.Session.Remove("idUser");
                return RedirectToAction("CompleteVerification");
            }
            else
            {
                TempData["errorEdit"] = "Erro: Código inexistente!";
                return RedirectToAction("CompleteVerification");

            }
        }

        [HttpPost]
        public IActionResult VerifyEmailAndCnpjInstitution(InstitutionModel inst)
        {

            var result = _institutionRepository.GetInstitutionByEmailAndCnpj(inst.Email, _format.FormatCPF(inst.Cnpj));
            if (result != null)
            {
                var cod = VerificationCodeGenerate();
                _codeInstitutionRepository.InsertNewCode(new CodeInstitutionModel() { Code = VerificationCodeGenerate(), InstitutionId = result.Id });
                _mail.SendMail(inst.Email, "NÃO COMPARTILHE ESSE CÓDIGO", $"(RECUPERAÇÃO DE SENHA AVALIA AÊ!) Não compartilhe esse código: {cod}");
                return RedirectToAction("CompleteVerificationInstitution");
            }
            else
            {
                TempData["errorRecuperationInstitution"] = "Erro: CNPJ ou Email inválido";
                return RedirectToAction("CompleteVerificationInstitution");
            }


        }



    }
}

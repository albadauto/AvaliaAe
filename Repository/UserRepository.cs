using AvaliaAe.Context;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;

namespace AvaliaAe.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public UserPhotoViewModel GetUser(int id)
        {
            UserPhotoViewModel result = new UserPhotoViewModel();
            result.userModel = _context.User.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public UserModel InsertUser(UserModel user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
            return user;
        }

        public UserModel UpdateUser(UserModel user)
        {
            var result = _context.User.FirstOrDefault(o => o.Id == user.Id);
            if(result != null)
            {
                result.Name = user.Name;
                result.Address = user.Address;
                result.Phone = user.Phone;
                result.Email = user.Email;
                result.Cep = user.Cep;
                result.District = user.District;
                result.Cpf = user.Cpf;
                result.photo_uri = user.photo_uri;
                result.Number = user.Number;
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("é nulo");
            }
           
            return result;
        }

		public UserModel VerifyLogin(UserModel user)
		{
            var result = _context.User.FirstOrDefault(o => o.Email == user.Email && o.Password == user.Password);
            return result;
		}

        public List<ForgotPasswordViewModel> VerifyIfEmailIsValid(string mail)
        {
            List<ForgotPasswordViewModel> viewModel = new List<ForgotPasswordViewModel>();
            var result = (from u in _context.User
                          where u.Email == mail
                          select new { u.Email, u.Id }
                          ).ToList();

           foreach(var value in result) 
           {
                viewModel.Add(new ForgotPasswordViewModel() { Email = value.Email, Id = value.Id });
           } 
            return viewModel;
        }

        public UserModel ResetPassword(UserModel reset)
        {
            UserModel result = _context.User.FirstOrDefault(x => x.Id == reset.Id);
            if (result == null)
            {
                throw new Exception("Erro!");
            }
            result.Password = reset.Password;
            _context.User.Update(result);
            _context.SaveChanges();
            return result;
        }
    }
}

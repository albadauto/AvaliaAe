using AvaliaAe.Models;

namespace AvaliaAe.Repository.Interfaces
{
    public interface IUserRepository
    {
        public UserModel InsertUser(UserModel user);
        public UserModel VerifyLogin(UserModel user);
        public UserModel GetUser(int id);
        public List<ForgotPasswordViewModel> VerifyIfEmailIsValid(string mail);
        public UserModel ResetPassword(UserModel user);
    }
}

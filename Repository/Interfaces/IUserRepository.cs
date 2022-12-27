using AvaliaAe.Models;

namespace AvaliaAe.Repository.Interfaces
{
    public interface IUserRepository
    {
        public UserModel InsertUser(UserModel user);
        public UserModel VerifyLogin(UserModel user);
        public UserPhotoViewModel GetUser(int id);
        public List<ForgotPasswordViewModel> VerifyIfEmailIsValid(string mail);
        public UserModel ResetPassword(UserModel user);
        public UserModel UpdateUser(UserModel user, string photo_uri);
    }
}

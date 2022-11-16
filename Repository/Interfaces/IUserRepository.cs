using AvaliaAe.Models;

namespace AvaliaAe.Repository.Interfaces
{
    public interface IUserRepository
    {
        public UserModel InsertUser(UserModel user);
        public UserModel VerifyLogin(UserModel user);
    }
}

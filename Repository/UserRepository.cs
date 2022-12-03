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

        public UserModel GetUser(int id)
        {
            var result = _context.User.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public UserModel InsertUser(UserModel user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
            return user;
        }

		public UserModel VerifyLogin(UserModel user)
		{
            var result = _context.User.FirstOrDefault(o => o.Email == user.Email && o.Password == user.Password);
            return result;
		}


	}
}

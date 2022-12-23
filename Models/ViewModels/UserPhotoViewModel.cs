namespace AvaliaAe.Models
{
    public class UserPhotoViewModel
    {
        public UserModel userModel { get; set; }
        public IFormFile File { get; set; }
        public List<UserModel> Users { get; set; }
    }
}

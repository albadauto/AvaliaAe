namespace AvaliaAe.Models
{
    public class UserPhotoViewModel
    {
        public UserModel userModel { get; set; }

        public List<AssociationsModel> associations { get; set; }
        public IFormFile File { get; set; }
    }
}

namespace AvaliaAe.Models
{
    public class CodeModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public UserModel User { get; set; }
        public int UserId { get; set; }
    }
}

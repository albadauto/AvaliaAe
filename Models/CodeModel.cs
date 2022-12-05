namespace AvaliaAe.Models
{
    public class CodeModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public UserModel UserModel { get; set; }
        public int UserModelId { get; set; }
    }
}

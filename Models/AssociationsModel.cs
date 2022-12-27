namespace AvaliaAe.Models
{
    public class AssociationsModel
    {
        public int Id { get; set; }
        public UserModel UserModel { get; set; }
        public InstitutionModel InstitutionModel { get; set; }
        public string Status { get; set; } //A => Aprovado | P => Pendente para aceite
        public int UserModelId { get; set; }
        public int InstitutionModelId { get; set; }
    }
}

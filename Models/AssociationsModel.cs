using System.ComponentModel.DataAnnotations.Schema;

namespace AvaliaAe.Models
{
    public class AssociationsModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public InstitutionModel Institution { get; set; }
        public string Status { get; set; } //A => Aprovado | P => Pendente para aceite
        public int UserId { get; set; }
        public int InstitutionId { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace AvaliaAe.Models
{
    public class CertificationModel
    {
        public int Id { get; set; }
        public InstitutionModel Institution { get; set; }
        public string CodeCertification { get; set; }

        [NotMapped]
        public int InstitutionId { get; set; }
    }
}

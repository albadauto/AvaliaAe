using System.ComponentModel.DataAnnotations.Schema;

namespace AvaliaAe.Models
{
    public class CertificationModel
    {
        public int Id { get; set; }

        [ForeignKey("InstitutionId")]
        public virtual InstitutionModel Institution { get; set; }
        public string CodeCertification { get; set; }

        public int InstitutionId { get; set; }  

        
    }
}

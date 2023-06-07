using System.ComponentModel.DataAnnotations.Schema;

namespace AvaliaAe.Models
{
    public class DocumentationModel
    {
        public int Id { get; set; }
        public AssociationsModel Associations { get; set; }
        [ForeignKey("UserId")]
        public UserModel User { get; set; }
        [ForeignKey("InstitutionId")]
        public InstitutionModel Institution { get; set; }   
        public int UserId { get; set; }
        public int InstitutionId { get; set; }  
        public string FileName { get; set; }
        public string? doc_uri { get; set; }
        public int AssociationsId { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }
}

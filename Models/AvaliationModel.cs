using System.ComponentModel.DataAnnotations.Schema;

namespace AvaliaAe.Models
{
    public class AvaliationModel
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Note { get; set; }
        public AssociationsModel associationsModel { get; set; }
        [NotMapped]
        public int AssociationId { get; set; }
    }
}

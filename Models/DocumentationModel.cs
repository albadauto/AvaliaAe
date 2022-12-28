namespace AvaliaAe.Models
{
    public class DocumentationModel
    {
        public int Id { get; set; }
        public AssociationsModel Associations { get; set; }
        public string doc_uri { get; set; }
        public int AssociationsId { get; set; }
    }
}

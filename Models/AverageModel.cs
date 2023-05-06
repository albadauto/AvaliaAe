namespace AvaliaAe.Models
{
    public class AverageModel
    {
        public int Id { get; set; }
        public double? Average { get; set; }
        public InstitutionModel Institution { get; set; }

        public AvaliationModel Avaliation { get; set; }
        public int InstitutionId { get; set; }

        public int AvaliationId { get; set; }



    }
}

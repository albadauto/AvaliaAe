using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvaliaAe.Models
{
    public class AverageModel
    {
        public int Id { get; set; }
        public double? Average { get; set; }
        public InstitutionModel Institution { get; set; }

        public int InstitutionId { get; set; }

        [NotMapped]
        public AvaliationModel Avaliation { get; set; }





    }
}

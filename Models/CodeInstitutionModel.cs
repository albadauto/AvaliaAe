﻿using System.ComponentModel.DataAnnotations.Schema;

namespace AvaliaAe.Models
{
    public class CodeInstitutionModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public InstitutionModel Institution { get; set; }
        public int InstitutionId { get; set; }
    }
}

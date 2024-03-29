﻿using AvaliaAe.Models;

namespace AvaliaAe.Repository.Interfaces
{
    public interface ICodeInstitutionRepository
    {
        public CodeInstitutionModel InsertNewCode(CodeInstitutionModel code);
        public CodeInstitutionModel FindCode(string code);

    }
}

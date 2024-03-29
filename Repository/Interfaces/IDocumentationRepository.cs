﻿
using AvaliaAe.Models;

namespace AvaliaAe.Repository.Interfaces
{
    public interface IDocumentationRepository
    {
        public List<DocumentationModel> GetDocumentation(DocumentationModel model);

        public DocumentationAvaliationViewModel GetInstitution(int IdInst);


        public DocumentationModel InsertNewDocumenation(DocumentationModel model, string docUri);

      
    }
}

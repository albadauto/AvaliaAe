using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvaliaAe.Models
{
    public class InstitutionModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome da instituição obrigatório!")]
        public string InstitutionName { get; set; }
		[Required(ErrorMessage = "CNPJ obrigatório!")]
		public string Cnpj { get; set; }
		[Required(ErrorMessage = "Telefone obrigatório!")]
		public string Phone { get; set; }
		[Required(ErrorMessage = "Email obrigatório!")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Senha obrigatório!")]
		public string Password { get; set; }
		[Required(ErrorMessage = "Endereço obrigatório!")]
		public string Address { get; set; }
		[Required(ErrorMessage = "Bairro é obrigatório")]
		public string District { get; set; }
        [Required(ErrorMessage = "Número obrigatório!")]
        public string Number { get; set; }
        [Required(ErrorMessage = "Cep é obrigatório")]
        public string Cep { get; set; }
        public InstitutionTypeModel InstitutionType { get; set; }
		[Required(ErrorMessage = "Tipo é obrigatório")]
		public int InstitutionTypeId { get; set; }
        public string? OwnerName { get; set; }
        public string? OwnerCpf { get; set; }

    }
}

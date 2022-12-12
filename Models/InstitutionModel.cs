using System.ComponentModel.DataAnnotations;

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
        public string? OwnerName { get; set; }
        public string? OwnerCpf { get; set; }

    }
}

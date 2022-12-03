using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AvaliaAe.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite um nome")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Digite um CPF válido")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Digite um telefone")]
        public string Phone { get; set; }

		[Required(ErrorMessage = "Digite um email")]
		[EmailAddress(ErrorMessage = "Digite um email válido")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Digite uma senha válida")]
		public string Password { get; set; }
		public string? Photo { get; set; }
		public bool Is_allowed { get; set; }

		[Required(ErrorMessage = "Digite um Bairro")]
        public string District { get; set; }

		[Required(ErrorMessage = "Digite um número")]
        public int Number { get; set; }

		[Required(ErrorMessage = "Digite um CEP")]
		public string Cep { get; set; }

		[Required(ErrorMessage = "Digite um Endereço")]
		public string Address { get; set; }

    }
}

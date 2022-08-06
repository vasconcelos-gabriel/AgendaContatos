using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Mvc.Models
{
    public class AccountLoginModel
    {
        [EmailAddress(ErrorMessage = "Por favor,, informe um endereço de e-mail válido")]
        [Required(ErrorMessage = "Por favor, informe o e-mail de acesso.")]
        public string Email { get; set; }

        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, sua senha de acesso.")]
        public string Senha { get; set; }

    }
}

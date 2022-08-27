using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Mvc.Models
{
    public class UsuariosAlterarSenhaModel
    {
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe sua senha.")]
        public string NovaSenha { get; set; }


        [Compare("NovaSenha", ErrorMessage = "Senhas não conferem, por favor verifique.")]
        [Required(ErrorMessage = "Por favor, confirme sua senha.")]
        public string NovaSenhaConfirmacao { get; set; }


    }
}

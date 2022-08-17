﻿using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Mvc.Models
{
    public class ContatossEdicaoModel
    {
        //oculto
        public Guid IdContato { get; set; }

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do contato.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor informe um endereço de e-mail válido.")]
        [Required(ErrorMessage = "Por favor, informe o Email do contato.")]
        public string Email { get; set; }


        [RegularExpression("^[0-9]{11}$", ErrorMessage = "Por favor, informe um numero de telefone válido")]
        [Required(ErrorMessage = "Por favor, informe o Telefone do contato.")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "Por favor, informe a data de nascimento do contato.")]
        public string DataNascimento { get; set; }
    }
}

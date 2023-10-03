using ControleContatos.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class UserNullPasswordModel
    {

        [Key]public int IdUser { get; set; }

        [Required(ErrorMessage = "Digite o Nome do Contato")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Digite o Login do Usuario")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o Email do Usuario")]
        [EmailAddress(ErrorMessage = "Email INVALIDO")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Selecione um Perfil")]
        public PerfilEnum? Perfil { get; set; }

    }
}

using ControleContatos.Enum;
using ControleContatos.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class UserModel
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
        public PerfilEnum Perfil { get; set; }

        [Required(ErrorMessage = "Digite a Senha do Usuario")]
        public string Password { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public virtual List<ContatoModel> Candidatos { get; set; }

        public bool PasswordValid(string password)
        {
            return Password == password.GenerateHash();
        }

        public void setPasswordHash()
        {
            Password = Password.GenerateHash();
        }

        public string GeneratePassword()
        {
            string newSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Password = newSenha.GenerateHash();
            return newSenha;
        }

        public void setNewPassword(string newPassword)
        {
            Password = newPassword.GenerateHash();
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class AlterPasswordModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = $"Digite a Senha Atual.")]
        public string PasswordAtual { get; set; }

        [Required(ErrorMessage = $"Digite a Nova Senha.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = $"Repita a Nova Senha.")]
        [Compare("NewPassword", ErrorMessage = "Senhas Não Coicidem, Digite Novamente.!")]
        public string ConfirmPassword { get; set;}
    }
}

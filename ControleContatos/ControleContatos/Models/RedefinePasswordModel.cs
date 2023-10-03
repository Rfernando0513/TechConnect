using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class RedefinePasswordModel
    {
        [Required(ErrorMessage = "Digite o Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o Email do Usuario")]
        [EmailAddress(ErrorMessage = "Email INVALIDO")]
        public string Email { get; set; }
    }
}

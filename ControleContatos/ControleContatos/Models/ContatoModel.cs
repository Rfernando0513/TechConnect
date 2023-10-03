using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleContatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o Nome do Contato")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o Email do Contato")]
        [EmailAddress(ErrorMessage = "EMAIL INVALIDO")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite o Celular do Contato")]
        [Phone(ErrorMessage ="CELULAR INVALIDO ")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Selecione a Senioridade")]
        public string Senioridade { get; set; }

        public int? UserId { get; set; }

        public UserModel User { get; set; }
    }
}

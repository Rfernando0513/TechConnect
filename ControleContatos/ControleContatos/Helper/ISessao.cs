using ControleContatos.Models;

namespace ControleContatos.Helper
{
    public interface ISessao 
    {
        void CreateSessaoUser(UserModel user);
        void DeleteSessaoUser();
        UserModel PesquiseSessaoUser();
    }
}

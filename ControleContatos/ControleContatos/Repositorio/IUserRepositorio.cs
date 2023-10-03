using ControleContatos.Models;
using ControleContatos.Controllers;

namespace ControleContatos.Repositorio
{
    public interface IUserRepositorio
    {
        UserModel BuscarEmailLogin(string email, string login);
        UserModel BuscarLogin(string login);
        UserModel ListarId(int id);
        List<UserModel> BuscarTodos();
        UserModel Adicionar(UserModel user);
        UserModel Atualizar(UserModel user);
        UserModel AlterarSenha(AlterPasswordModel alterPasswordModel);
        bool Delete(int id);
    }
}

using ControleContatos.Models;
using ControleContatos.Controllers;

namespace ControleContatos.Repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel ListarId(int id);
        List<ContatoModel> BuscarTodos(int UserId);
        ContatoModel Adicionar(ContatoModel candidato);
        ContatoModel Atualizar(ContatoModel candidato);
        bool Delete(int id);

    }
}

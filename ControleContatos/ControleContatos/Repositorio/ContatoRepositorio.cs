using ControleContatos.Data;
using ControleContatos.Models;
using ControleContatos.Controllers;
using ControleContatos.Repositorio;

namespace Cadastro2.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public ContatoModel ListarId(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
        }

        public List<ContatoModel> BuscarTodos(int UserId)
        {
            return _bancoContext.Contatos.Where(x => x.UserId == UserId).ToList();
        }

        public ContatoModel Adicionar(ContatoModel candidato)
        {                                         
            _bancoContext.Contatos.Add(candidato);
            _bancoContext.SaveChanges();                                                                                                
            return candidato;
        }
        public ContatoModel Atualizar(ContatoModel candidato)
        {
            ContatoModel candidatoDb = ListarId(candidato.Id);
            if (candidatoDb == null) throw new Exception("Erro na atualização do contato");

            candidatoDb.Nome = candidato.Nome;
            candidatoDb.Email = candidato.Email;
            candidatoDb.Celular = candidato.Celular;
            candidatoDb.Senioridade = candidato.Senioridade;

            _bancoContext.Contatos.Update(candidatoDb);
            _bancoContext.SaveChanges();

            return candidatoDb;
        }

        public bool Delete(int id)
        {

            ContatoModel candidatoDb = ListarId(id);
            if (candidatoDb == null) throw new Exception("Erro ao apagar contato");

            _bancoContext.Contatos.Remove(candidatoDb);
            _bancoContext.SaveChanges();

            return true;
        }     
    }
}

using ControleContatos.Data;
using ControleContatos.Models;
using ControleContatos.Controllers;
using ControleContatos.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace Cadastro2.Repositorio
{
    public class UserRepositorio : IUserRepositorio
    {
        readonly private BancoContext _bancoContext;
        public UserRepositorio(BancoContext bancoContext)
        {
            this._bancoContext = bancoContext;
        }
        public UserModel BuscarLogin(string login)
        {
            return _bancoContext.Users.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UserModel BuscarEmailLogin(string email, string login)
        {
            return _bancoContext.Users.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper());
        }

        public UserModel ListarId(int idUser)
        {
            return _bancoContext.Users.FirstOrDefault(user => user.IdUser == idUser) ?? throw new Exception("Usuário não encontrado");
        }


        public List<UserModel> BuscarTodos()
        {
            return _bancoContext.Users
                .Include(x => x.Candidatos)
                .ToList();
        }

        public UserModel Adicionar(UserModel user)
        {
            user.DataCadastro = DateTime.Now;
            user.setPasswordHash();
            _bancoContext.Users.Add(user);
            _bancoContext.SaveChanges();
            return user;
        }
        public UserModel Atualizar(UserModel user)
        {
            UserModel userDb = ListarId(user.IdUser);
            if (userDb == null) throw new Exception("Erro na atualização do usuario");

            userDb.Name = user.Name;
            userDb.Login = user.Login;
            userDb.Email = user.Email;
            user.Perfil = user.Perfil;
            userDb.DataAtualizacao = DateTime.Now;

            _bancoContext.Users.Update(userDb);
            _bancoContext.SaveChanges();

            return userDb;
        }

        public UserModel AlterarSenha(AlterPasswordModel alterPasswordModel)
        {
            UserModel userDb = ListarId(alterPasswordModel.Id);

            if (userDb == null) throw new Exception("Usuario Não Encontrado, Tente Novamente");

            if (userDb.PasswordValid(alterPasswordModel.PasswordAtual)) throw new Exception("Senha Atual não confere");

            if (userDb.PasswordValid(alterPasswordModel.NewPassword)) throw new Exception("Nova Senha Deve Ser Diferente Da Senha Atual.");

            userDb.setNewPassword(alterPasswordModel.NewPassword);
            userDb.DataAtualizacao = DateTime.Now;

            _bancoContext.Users.Update(userDb);
            _bancoContext.SaveChanges();

            return userDb;
        }

        public bool Delete(int id)
        {

            UserModel userDb = ListarId(id);
            if (userDb == null) throw new Exception("Erro ao apagar usuario");

            _bancoContext.Users.Remove(userDb);
            _bancoContext.SaveChanges();

            return true;
        }
    }
}

using ControleContatos.Data;
using ControleContatos.Helper;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;


namespace ControleContatos.Controllers
{
    public class AlterPasswordController : Controller
    {
        private readonly IUserRepositorio _userRepositorio;
        private readonly ISessao _sessao;
        private readonly BancoContext _bancoContext;

        public AlterPasswordController(IUserRepositorio userRepositorio,
                                        ISessao sessao,
                                        BancoContext bancoContext)
        {
            _userRepositorio = userRepositorio;
            _sessao = sessao;
            _bancoContext = bancoContext;
        }
        public IActionResult Index()
        {
            return View();
        }


        //public IActionResult Alter(AlterPasswordModel alterPasswordModel)
        //{
        //    try
        //    {
        //        UserModel userLogado = _sessao.PesquiseSessaoUser();
        //        alterPasswordModel.Id = userLogado.IdUser;

        //        if (ModelState.IsValid)
        //        {
        //            _userRepositorio.AlterarSenha(alterPasswordModel);
        //            TempData["MessageSucess"] = "Senha Alterada Com Sucesso";
        //            return View("Index", alterPasswordModel);
        //        }

        //        return View("Index", alterPasswordModel);
        //    }
        //    catch (Exception erro)
        //    {
        //        TempData["MessageErro"] = $"Falha ao alterar a senha, Tente Novamente. {erro.Message}";
        //        return View("Index", alterPasswordModel);
        //    }

        //}
        [HttpPost]
        public IActionResult Alter(AlterPasswordModel alterPasswordModel)
        {
            try
            {
                UserModel userLogado = _sessao.PesquiseSessaoUser();
                alterPasswordModel.Id = userLogado.IdUser;

                if (ModelState.IsValid)
                {
                    UserModel userDb = _userRepositorio.ListarId(alterPasswordModel.Id);

                    if (userDb == null)
                    {
                        TempData["MessageErro"] = "Usuário Não Encontrado, Tente Novamente";
                    }
                    else if (!userDb.PasswordValid(alterPasswordModel.PasswordAtual))
                    {
                        TempData["MessageErro"] = "Senha Atual não confere";
                    }
                    else if (userDb.PasswordValid(alterPasswordModel.NewPassword))
                    {
                        TempData["MessageErro"] = "Nova Senha Deve Ser Diferente Da Senha Atual";
                    }
                    else
                    {
                        // Se chegou aqui, a senha pode ser alterada
                        userDb.setNewPassword(alterPasswordModel.NewPassword);
                        userDb.DataAtualizacao = DateTime.Now;

                        _userRepositorio.Atualizar(userDb);
                        TempData["MessageSucess"] = "Senha Alterada Com Sucesso";
                    }

                    return View("Index", alterPasswordModel);
                }

                TempData["MessageErro"] = "Dados Inválidos. Tente Novamente.";
                return View("Index", alterPasswordModel);
            }
            catch (Exception erro)
            {
                TempData["MessageErro"] = $"Falha ao alterar a senha, Tente Novamente. {erro.Message}";
                return View("Index", alterPasswordModel);
            }
        }



    }
}

using Cadastro2.Repositorio;
using ControleContatos.Helper;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ControleContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepositorio _userRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;
        public LoginController(IUserRepositorio userRepositorio,
            ISessao sessao,
            IEmail email)
        {
            _userRepositorio = userRepositorio;
            _sessao = sessao;
            _email = email;
        }
        public IActionResult Index()
        {
            // User logado redirectio the home

            if(_sessao.PesquiseSessaoUser()  != null) return RedirectToAction("Index", "Home");
          
            return View();
        }

        public IActionResult RedefinePassword()
        {
            return View();
        }

        public IActionResult Exit()
        {
            _sessao.DeleteSessaoUser();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Acessar (LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                     UserModel user = _userRepositorio.BuscarLogin(loginModel.Login);

                    if (user != null )
                    {
                        if (user.PasswordValid(loginModel.Password))
                        {
                            _sessao.CreateSessaoUser(user);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MessageErro"] = $"A Senha  está Inválida. Tente Novamente.";

                    }

                    TempData["MessageErro"] = $"Usuario invalido. Tente Novamente.";

                    return RedirectToAction("Index");
                }
                return View("Index");
            }
            catch (Exception erro)
            {


                TempData["MessageErro"] = $"Falha ao Acessar, Tente Novamente {erro.Message} ";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult LinkRedefinePassword(RedefinePasswordModel redefinePasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel user = _userRepositorio.BuscarEmailLogin(redefinePasswordModel.Email, redefinePasswordModel.Login);

                    if (user != null)
                    {
                        string newSenha = user.GeneratePassword(); 
                        string mensagem = $"Sua Nova Senha é: {newSenha}";

                        bool emailEnviado = _email.Enviar(user.Email, "TechConnect - Nova Senha", mensagem);
                        if (emailEnviado)
                        {
                            _userRepositorio.Atualizar(user);
                            TempData["MessageSucess"] = $"Enviamos para seu E-mail cadastrado uma nova Senha.";
                        }
                        else
                        {
                            TempData["MessageErro"] = $"Falha ao Enviar O Email. Tente Novamente";
                        }

                        return RedirectToAction("Index", "Login");
                    }

                    TempData["MessageErro"] = $"Falha ao Redefinir sua Senha. Verifique os Dados Informados.";

                    return RedirectToAction("Index");
                }
                return View("Index");
            }
            catch (Exception erro)
            { 
                TempData["MessageErro"] = $"Falha ao Redefinir a Senha, Tente Novamente {erro.Message} ";
                return RedirectToAction("Index");
            }
        }
    }
}

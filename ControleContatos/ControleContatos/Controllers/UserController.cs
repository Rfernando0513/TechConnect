using Cadastro2.Repositorio;
using ControleContatos.Filters;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    [UserAdmin]
    public class UserController : Controller
    {
        public readonly IUserRepositorio _userRepositorio;
        private readonly IContatoRepositorio _contatoRepositorio;
        public UserController(IUserRepositorio userRepositorio,
            IContatoRepositorio contatoRepositorio)
        {
            _userRepositorio = userRepositorio;
            _contatoRepositorio = contatoRepositorio;
        }
        public IActionResult Index()
        {
            List<UserModel> users = _userRepositorio.BuscarTodos();

            return View(users);
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            UserModel user = _userRepositorio.ListarId(id);
            return View(user);
        }

        public IActionResult DeleteConfirmation(int id)
        {
            UserModel user = _userRepositorio.ListarId(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Create(UserModel user)
        {
            
                try
                {
                    //if (ModelState.IsValid)
                    //{
                        user = _userRepositorio.Adicionar(user);

                        TempData["MessageSucess"] = "Cadastrado com sucesso";
                        return RedirectToAction("Index");
                    //}

                    return View(user);
                }
                catch (System.Exception erro)
                {
                    TempData["MessageErro"] = $"Falha no cadastro. {erro.Message}";
                    return RedirectToAction("Index");
                }            
        }

        public IActionResult Delete(int id)
        {
            try
            {
                bool deletado = _userRepositorio.Delete(id);

                if (deletado)
                {
                    TempData["MessageSucess"] = "Sucesso ao excluir";
                }
                else
                {
                    TempData["MessageErro"] = "Falha ao excluir. ";
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {

                TempData["MessageErro"] = $"Falha ao excluir. {erro.Message} ";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ListarCandidatosPorUserId(int Id)
        {
            List<ContatoModel> candidatos = _contatoRepositorio.BuscarTodos(Id);
            return PartialView("_CandidatosUser", candidatos);
        }

        [HttpPost]
        public IActionResult Edit(UserNullPasswordModel userNullPasswordModel)
        {
            try
            {
                UserModel user = null;
                if (ModelState.IsValid)
                {
                    user = new UserModel()
                    {
                        IdUser = userNullPasswordModel.IdUser,
                        Name = userNullPasswordModel.Name,
                        Login = userNullPasswordModel.Login,
                        Email = userNullPasswordModel.Email,
                        Perfil = (Enum.PerfilEnum)userNullPasswordModel.Perfil
                    };

                    user = _userRepositorio.Atualizar(user);
                    TempData["MessageSucess"] = "Alterado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(user);
            }
            catch (System.Exception erro)
            {
                TempData["MessageErro"] = $"Falha ao Editar. {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    } 
}

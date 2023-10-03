using ControleContatos.Models;
using Microsoft.AspNetCore.Mvc;
using ControleContatos.Repositorio;
using Cadastro2.Repositorio;
using System;
using ControleContatos.Filters;
using ControleContatos.Helper;
using ControleContatos.Data;

namespace ControleContatos.Controllers
{
    [UserLogadded]
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;
        private readonly BancoContext _bancoContext;
        public ContatoController(IContatoRepositorio contatoRepositorio,
                                    ISessao sessao,
                                    BancoContext bancoContext)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
            _bancoContext = bancoContext;
        }

        public IActionResult Index()
        {
            UserModel userLogado = _sessao.PesquiseSessaoUser();
            if (userLogado == null)
            {
                // Lógica para lidar com o usuário não logado ou sessão inválida
                return RedirectToAction("Login"); // Redirecionar para a página de login, por exemplo
            }

            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(userLogado.IdUser);
            if (contatos == null)
            {
                // Lógica para lidar com a lista de contatos nula (se necessário)
                return View(new List<ContatoModel>()); // Ou outra ação adequada
            }

            return View(contatos);
        }


        public IActionResult Create()
        {
            return View();
        }

        public IActionResult DeleteConfirmation(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarId(id);
            return View(contato);
        }
        
        public IActionResult Delete(int id)
        {
            try
            {
                bool deletado = _contatoRepositorio.Delete(id);

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

        public IActionResult Edit(int id)
        {
             ContatoModel contato = _contatoRepositorio.ListarId(id);
            return View(contato);
        }

        [HttpPost]
        public IActionResult Create(ContatoModel candidato)
        {
            try
            {
                //if(ModelState.IsValid){

                    UserModel userLogado = _sessao.PesquiseSessaoUser();
                    candidato.UserId = userLogado.IdUser;

                    _bancoContext.Contatos.Add(candidato);
                    _bancoContext.SaveChanges();

                //}

                TempData["MessageSucess"] = "Cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MessageErro"] = $"Falha no cadastro. {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(ContatoModel candidato)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                    UserModel userLogado = _sessao.PesquiseSessaoUser();
                    candidato.UserId = userLogado.IdUser;

                    _contatoRepositorio.Atualizar(candidato);
                    TempData["MessageSucess"] = "Alterado com sucesso";
                    return RedirectToAction("Index");
                //}

                return View("Edit", candidato);
            }
            catch (System.Exception erro)
            {
                TempData["MessageErro"] = $"Falha ao Editar. {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}

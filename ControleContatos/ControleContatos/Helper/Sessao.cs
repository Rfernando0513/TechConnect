using ControleContatos.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ControleContatos.Helper

{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;
        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
         public UserModel PesquiseSessaoUser()
        {
            string sessaoUser = _httpContext.HttpContext.Session.GetString("sessaoUserLogado");

            if (string.IsNullOrEmpty(sessaoUser)) return null;

            return JsonConvert.DeserializeObject<UserModel>(sessaoUser);
            
        }
        public void  CreateSessaoUser(UserModel user)
        {
            string value = JsonConvert.SerializeObject(user);

            _httpContext.HttpContext.Session.SetString("sessaoUserLogado", value );
        }

        public void DeleteSessaoUser()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUserLogado");
        }
    }
}

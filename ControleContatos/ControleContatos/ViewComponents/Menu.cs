using ControleContatos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ControleContatos.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUser = HttpContext.Session.GetString("sessaoUserLogado");

            if (string.IsNullOrEmpty(sessaoUser)) return null;

            UserModel user = JsonConvert.DeserializeObject<UserModel>(sessaoUser);

            return View(user);
        }
    }
}

using AgendaContatos.Data.Repositories;
using AgendaContatos.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AgendaContatos.Mvc.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        
        public IActionResult Dados()
        {
            return View();
        }
        public IActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AlterarSenha(UsuariosAlterarSenhaModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var json = User.Identity.Name;
                    var auth = JsonConvert.DeserializeObject<AuthenticationModel>(json);

                    var usuarioRepository = new UsuarioRepository();
                    usuarioRepository.Update(auth.IdUsuario, model.NovaSenha);

                    TempData["Mensagem"] = $"Senha alterada com sucesso!";

                    ModelState.Clear();
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = $"Erro ao alterar senha: {e.Message}";
                }
            }

            return View();
        }
    }
}

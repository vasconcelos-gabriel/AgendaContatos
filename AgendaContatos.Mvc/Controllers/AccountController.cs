using AgendaContatos.Data.Entities;
using AgendaContatos.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AgendaContatos.Mvc.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(string nome, string email, string senha, string confirmaSenha)
        {
            try
            {
                var usuario = new Usuario();
                usuario.IdUsuario = Guid.NewGuid();
                usuario.Nome = nome;
                usuario.Email = email;
                usuario.Senha = senha;
                usuario.DataCadastro = DateTime.Now;

                var usuarioRepository = new UsuarioRepository();
                usuarioRepository.Create(usuario);

                TempData["Mensagem"] = $"Parabéns {usuario.Nome}, sua conta foi registrada com sucesso!";
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = $"Falha ao cadastrar: {e.Message}";

            }




            return View();
        }

        public IActionResult PasswordRecover()
        {
            return View();
        }
    }
}

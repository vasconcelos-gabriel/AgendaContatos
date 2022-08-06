using AgendaContatos.Data.Entities;
using AgendaContatos.Data.Repositories;
using AgendaContatos.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaContatos.Mvc.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //consultar o usuario no banco de dados atraves do email e senha
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmailAndSenha(model.Email, model.Senha);

                    //verificar se o usuario foi encontrando
                    if (usuario != null)
                    {
                        //redirecionando para outra página
                        return RedirectToAction("Consulta", "Contatos");
                    }
                    else
                    {
                        TempData["Mensagem"] = "Acesso negado. Usuário Inválido!";
                    }

                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = $"Falha ao efetuar login: {e.Message}";
                }
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var usuarioRepository = new UsuarioRepository();

                    //verificar se ja existe um email cadastrado
                    if (usuarioRepository.GetByEmail(model.Email) == null)
                    {
                        var usuario = new Usuario();
                        usuario.IdUsuario = Guid.NewGuid();
                        usuario.Nome = model.Nome;
                        usuario.Email = model.Email;
                        usuario.Senha = model.Senha;

                        usuario.DataCadastro = DateTime.Now;

                        usuarioRepository.Create(usuario);

                        TempData["Mensagem"] = $"Parabéns {usuario.Nome}, sua conta foi registrada com sucesso!";
                    }
                    else
                    {
                        TempData["Mensagem"] = $"O e-mail {model.Email} já está cadastrado, tente outro e-mail";
                    }


                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Falha ao cadastrar: {e.Message}";

                }

            }

         return View();
        }

        public IActionResult PasswordRecover()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PasswordRecover(AccountPasswordRecoverModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //buscar usuario atraves do email
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmail(model.Email);

                    if(usuario != null)
                    {
                        TempData["Mensagem"] = $"Olá, {usuario.Nome}, você receberá um email para cadastrar uma nova senha";
                    }
                    else
                    {
                        TempData["Mensagem"] = $"Email informado não encontrado";
                    }
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = $"Falha ao cadastrar: {e.Message}";
                }

            }
            else
            {

            }





            return View();
        }
    }
}

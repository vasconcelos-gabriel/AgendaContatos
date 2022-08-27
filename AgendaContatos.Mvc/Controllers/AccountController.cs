using AgendaContatos.Data.Entities;
using AgendaContatos.Data.Repositories;
using AgendaContatos.Messages;
using AgendaContatos.Mvc.Models;
using Bogus;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

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
                        //autenticação com cookies+
                        var authenticationModel = new AuthenticationModel();
                        authenticationModel.IdUsuario = usuario.IdUsuario;
                        authenticationModel.Nome = usuario.Nome;
                        authenticationModel.Email = usuario.Email;
                        authenticationModel.DataHoraAcesso = DateTime.Now;

                        //converter para json

                        var json = JsonConvert.SerializeObject(authenticationModel);

                        GravarCookieDeAutenticacao(json);



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
                        RecuperarSenhaDoUsuario(usuario);

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

            return View();
        }

        public IActionResult Logout()
        {
            RemoverCookieDeAutenticacao();

            return RedirectToAction("Login", "Account");

        }


        public void GravarCookieDeAutenticacao(string json)
        {
            var claimsIdentity = new ClaimsIdentity
                (new[] { new Claim(ClaimTypes.Name, json)}, CookieAuthenticationDefaults.AuthenticationScheme);


            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        }

        public void RemoverCookieDeAutenticacao()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private void RecuperarSenhaDoUsuario(Usuario usuario)
        {
            var faker = new Faker();
            var novaSenha = faker.Internet.Password(10);

            var mailTo = usuario.Email;
            var subject = "Recuperação de senha de acesso- Agenda de Contatos";
             var body = $@"
             <div>
                 <p>Olá {usuario.Nome}, uma nova senha foi gerada com sucesso.</p>
                 <p>Utilize a senha <strong>{novaSenha}</strong>para acessar sua conta.</p>
                 <p>Depois de acessar, você poderá atualizar esta senha para outra de sua preferência.</p>
                 <p>Att,</p>
                 <p>Equipe Agenda de Contatos</p>
             </div>
             ";
            //enviando a senha para o email do usuário
            var emailMessage = new EmailMessage();
            emailMessage.SendMail(mailTo, subject, body);
            //atualizando a senha do usuário no banco de dados
            var usuarioRepository = new UsuarioRepository();
            usuarioRepository.Update(usuario.IdUsuario, novaSenha);

        }
    }
        
}

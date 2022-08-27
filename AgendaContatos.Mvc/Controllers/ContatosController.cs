using AgendaContatos.Data.Entities;
using AgendaContatos.Data.Repositories;
using AgendaContatos.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AgendaContatos.Mvc.Controllers
{
    [Authorize]
    public class ContatosController : Controller
    {
        public IActionResult Cadastro()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ContatosCadastroModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var authenticationModel = ObterUsuarioAutencicado();


                    var contato = new Contato();
                    contato.IdContato = Guid.NewGuid();
                    contato.Nome = model.Nome;
                    contato.Email = model.Email;
                    contato.Telefone = model.Telefone;
                    contato.DataNascimento = DateTime.Parse(model.DataNascimento);
                    contato.IdUsuario = authenticationModel.IdUsuario;

                    var contatoRepository = new ContatoRepository();
                    contatoRepository.Create(contato);

                    TempData["Mensagem"] = $"Contato {contato.Nome}, cadastrado com sucesso!";
                    ModelState.Clear();

                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Falha ao cadastrar contato: {e.Message}";

                }

            }

            return View();
        }


        public IActionResult Consulta()
        {
            var lista = new List<ContatosConsultaModel>();
            try
            {
                var authenticationModel = ObterUsuarioAutencicado();

                var contatoRepository = new ContatoRepository();
                var contatos = contatoRepository.GetByUsuario(authenticationModel.IdUsuario);

                foreach (var contato in contatos)
                {
                    var model = new ContatosConsultaModel();
                    model.IdContato = contato.IdContato;
                    model.Nome = contato.Nome;
                    model.Email = contato.Email;
                    model.Telefone = contato.Telefone;
                    model.DataNascimento = contato.DataNascimento.ToString("dd/MM/yyyy");
                    model.Idade = ObterIdade(contato.DataNascimento);

                    lista.Add(model);   

                }
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = $"Erro ao consultar: {e.Message}";
            }
            return View(lista);
        }
      
        public IActionResult Edicao(Guid id)
        {
            var model = new ContatossEdicaoModel();

            try
            {
                var authenticationModel = ObterUsuarioAutencicado();

                var contatoRepository = new ContatoRepository();

                var contato = contatoRepository.GetById(id, authenticationModel.IdUsuario);

                model.IdContato = contato.IdContato;
                model.Nome = contato.Nome;
                model.Email = contato.Email;
                model.Telefone = contato.Telefone;
                model.DataNascimento = contato.DataNascimento.ToString("yyyy-MM-dd");
            }
            catch(Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return View(model);
        }
        [HttpPost]
        public IActionResult Edicao(ContatossEdicaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var contato = new Contato();
                    contato.IdContato = model.IdContato;
                    contato.Nome = model.Nome;
                    contato.Email = model.Email;
                    contato.Telefone = model.Telefone;
                    contato.DataNascimento = DateTime.Parse(model.DataNascimento);

                    var contatoRepository = new ContatoRepository();
                    contatoRepository.Update(contato);

                    TempData["Mensagem"] = $"Contato {contato.Nome}, atualizado com sucesso";
                }
                catch(Exception e)
                {
                    TempData["Mnesagem"] = e.Message;
                }
            }
            return View(model);
        }

        public IActionResult Exclusao(Guid id)
        {
            try
            {
                var contato = new Contato();
                contato.IdContato = id;

                var contatoRepository = new ContatoRepository();
                contatoRepository.Delete(contato);

                TempData["Mensagem"] = $"Contato excluído com sucesso!";
            }
            catch(Exception e)
            {
                TempData["Mensagem"] = $"Erro ao tentar excluir: {e.Message}";
            }


            return RedirectToAction("Consulta");
        }

        public AuthenticationModel ObterUsuarioAutencicado()
        {
            var json = User.Identity.Name;
            var authenticationModel = JsonConvert.DeserializeObject<AuthenticationModel>(json);

            return authenticationModel;
        }

        public int ObterIdade(DateTime dataNascimento)
        {
            var idade = DateTime.Now.Year - dataNascimento.Year;

            if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear) idade--;
            return idade;
        }
    }
}

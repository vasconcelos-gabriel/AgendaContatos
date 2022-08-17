using AgendaContatos.Data.Repositories;
using AgendaContatos.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaContatos.Mvc.Controllers
{
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
                    var contatoRepository = new ContatoRepository();

                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = $"Falha ao cadastrar: {e.Message}";

                }

            }

            return View();
        }


        public IActionResult Consulta()
        {
            return View();
        }
        public IActionResult Edicao()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edicao(ContatossEdicaoModel model)
        {
            return View();
        }
    }
}

using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class PessoaController : Controller
    {
        // GET: Pessoa
        private PessoaRepository repository;

        public PessoaController()
        {
            repository = new PessoaRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObterTodos()
        {
            var pessoas = repository.ObterTodos();
            var resultado = new { data = pessoas };
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Inserir(Pessoa pessoa)
        {
            pessoa.RegistroAtivo = true;
            var id = repository.Inserir(pessoa);
            var resultado = new { id = id };
            return Json(resultado);
        }

        [HttpGet]
        public JsonResult Apagar(int id)
        {
            var apagou = repository.Apagar(id);
            var resultado = new { status = apagou};
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(Pessoa pessoa)
        {
            var alterou = repository.Alterar(pessoa);
            var resultado = new { status = alterou };
            return Json(resultado);
        }

        [HttpGet, Route("pessoa/")]
        public JsonResult ObterPeloId(int id)
        {
            return Json(repository.ObterPeloId(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet, Route("pessoa/obtertodosselect2")]
        public JsonResult ObterTodosSelect2(string term)
        {
            var pessoas = repository.ObterTodos();

            List<object> pessoasSelect2 = 
                new List<object>();
            foreach(Pessoa pessoa in pessoas)
            {
                pessoasSelect2.Add(new
                {
                    id = pessoa.Id,
                    text = pessoa.Nome
                });
            }
            var resultado = new {
                results = pessoasSelect2
            };
            return Json(resultado, 
                JsonRequestBehavior.AllowGet);
        }
    }

}
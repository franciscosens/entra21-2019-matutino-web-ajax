using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ProdutoController : Controller
    {
        private ProdutoRepository repository;

        public ProdutoController()
        {
            repository = new ProdutoRepository();
        }

        [HttpGet, Route("obtertodospeloidvenda")]
        public JsonResult ObterTodosPeloIdVenda(int idVenda)
        {
            var produtos = repository
        .ObterProdutosPeloIdVenda(idVenda);
            var resultado = new { data = produtos };
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, Route("obterpeloid")]
        public ActionResult ObterPeloId(int id)
        {
            var produto = repository.ObterPeloId(id);
            if (produto == null)
                return HttpNotFound();

            return Json(produto,
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost, Route("cadastro")]
        public JsonResult Cadastro(Produto produto)
        {
            var id = repository.Inserir(produto);
            var resultado = new { id = id };
            return Json(resultado,
                 JsonRequestBehavior.AllowGet);
        }

        [HttpPost, Route("alterar")]
        public JsonResult Alterar(Produto produto)
        {
            var alterou = repository.Alterar(produto);
            var resultado = new { status = alterou };
            return Json(resultado,
              JsonRequestBehavior.AllowGet);
        }

        [HttpGet, Route("apagar")]
        public JsonResult Apagar(int id)
        {
            var apagou = repository.Apagar(id);
            var resultado = new { status = apagou };
            return Json(resultado,
              JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veterinaria.DAO;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
    public class ProdutoController : Controller
    {
        private ProdutoDAO produtos;

        public ProdutoController()
        {
            this.produtos = new ProdutoDAO(new Connection());
        }

        // GET: Produto
        public ActionResult Index()
        {
            ViewBag.Message = "Produtos";
            return View(this.produtos.ListAll());
        }

        // GET: Produto/Details/5
        public ActionResult Details(int id)
        {
            return View(this.produtos.Search(new Produto { IdProduto = id }));
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            return View(new Produto());
        }

        // POST: Produto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var produto = new Produto
                {
                    Nome = collection["nome"].ToString(),
                    Descricao = collection["descricao"].ToString(),
                    Valor = int.Parse(collection["valor"]),
                    Qtd_Estoque = int.Parse(collection["qtdestoque"])
                };
                this.produtos.Insert(produto);
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int id)
        {
            return View(this.produtos.Search(new Produto { IdProduto = id }));
        }

        // POST: Produto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var produto = new Produto
                {
                    IdProduto = int.Parse(collection["idproduto"]),
                    Nome = collection["nome"].ToString(),
                    Descricao = collection["descricao"].ToString(),
                    Valor = int.Parse(collection["valor"]),
                    Qtd_Estoque = int.Parse(collection["qtdestoque"])
                };
                this.produtos.Update(produto);
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }

        // GET: Produto/Delete/5
        public ActionResult Delete(int id)
        {
            return View(this.produtos.Delete(new Produto { IdProduto = id }));
        }

        // POST: Produto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                this.produtos.Delete(new Produto { IdProduto = id });
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }
    }
}

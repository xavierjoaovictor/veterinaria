using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veterinaria.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.DAO;
using Veterinaria.Models;
using System.Web.Mvc;

namespace Veterinaria.Controllers.Tests
{
    [TestClass()]
    public class ProdutoControllerTests
    {
        private ProdutoController controller;

        private Produto produto;
        private ProdutoDAO produtos;

        private FormCollection form;

        [TestInitialize()]
        public void Startup()
        {
            this.InstantiateDependenciesDAO();
            this.InstantiateDependenciesObjects();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.ClearDatabase();
            this.DisposeDependenciesDAO();
        }

        private void DisposeDependenciesDAO()
        {
            this.produtos.Dispose();
        }

        private void ClearDatabase()
        {
            this.produtos.DeleteAll();
        }

        private void InstantiateDependenciesObjects()
        {
            this.produto = new Produto
            {
                IdProduto = 1,
                Nome = "Cachorrex",
                Descricao = "Limpa bem",
                Valor = 25,
                Qtd_Estoque = 1
            };
            this.form = new FormCollection();
            this.form.Add("idproduto", this.produto.IdProduto.ToString());
            this.form.Add("nome", this.produto.Nome);
            this.form.Add("descricao", this.produto.Descricao);
            this.form.Add("valor", this.produto.Valor.ToString());
            this.form.Add("qtdestoque", this.produto.Qtd_Estoque.ToString());
        }

        private void InstantiateDependenciesDAO()
        {
            this.controller = new ProdutoController();
            this.produtos = new ProdutoDAO(new Connection());
        }

        //[TestMethod()]
        //public void IndexTest()
        //{
        //    this.produtos.Insert(this.produto);
        //    this.produto.IdProduto = 2;
        //    this.produtos.Insert(this.produto);

        //    var result = this.controller.Index() as ViewResult;
        //    var produtosCollection = (List<Produto>)result.Model;

        //    Assert.AreEqual(2, produtosCollection.Count);
        //}

        //[TestMethod()]
        //public void DetailsTest()
        //{
        //    this.produtos.Insert(this.produto);

        //    var result = this.controller.Details(this.produto.IdProduto) as ViewResult;
        //    Produto produtoInstance = (Produto)result.Model;

        //    Assert.AreEqual("Cachorrex", produtoInstance.Nome);
        //}

        //[TestMethod()]
        //public void CreateTest()
        //{
        //    this.controller.Create(this.form);
        //    Assert.AreEqual(1, this.produtos.ListAll().Count);
        //}

        //[TestMethod()]
        //public void EditTest()
        //{
        //    this.produtos.Insert(this.produto);

        //    this.form["nome"] = "Sabbah";
        //    this.controller.Edit(this.produto.IdProduto, this.form);

        //    Assert.AreEqual("Sabbah", this.produtos.Search(this.produto).Nome);
        //}

        //[TestMethod()]
        //public void DeleteTest()
        //{
        //    this.produtos.Insert(this.produto);

        //    this.controller.Delete(this.produto.IdProduto);

        //    Assert.IsNull(this.produtos.Search(this.produto));
        //}
    }
}
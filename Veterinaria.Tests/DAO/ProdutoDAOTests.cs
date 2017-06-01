using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veterinaria.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Models;

namespace Veterinaria.DAO.Tests
{
    [TestClass()]
    public class ProdutoDAOTests
    {
        private Produto produto;
        private ProdutoDAO produtos;

        [TestInitialize()]
        public void Startup()
        {
            this.InstantiateDependenciesDAO();
            this.InstantiateDependenciesObjects();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.ClearDatabase();
            this.DisposeDependenciesDAO();
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
        }

        private void InstantiateDependenciesDAO()
        {
            this.produtos = new ProdutoDAO(new Connection());
        }

        private void DisposeDependenciesDAO()
        {
            this.produtos.Dispose();
        }

        private void ClearDatabase()
        {
            this.produtos.DeleteAll();
        }
        
        //[TestMethod()]
        //public void DeleteTest()
        //{
        //    this.produtos.Insert(produto);

        //    Assert.IsTrue(this.produtos.Delete(this.produto));
        //}
        
    }
}
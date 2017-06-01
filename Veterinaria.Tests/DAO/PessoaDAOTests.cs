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
    public class PessoaDAOTests
    {
        private Pessoa pessoa;
        private PessoaDAO pessoas;

        private ClienteDAO clientes;
        private Cliente cliente;

        [TestInitialize()]
        public void Startup()
        {
            this.InstantiateDependenciesObjects();
            this.InstantiateDependenciesDAO();
            this.InsertDependenciesInTheDatabase();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.ClearDatabase();
            this.DisposeDependenciesDAO();
        }

        private void DisposeDependenciesDAO()
        {
            this.pessoas.Dispose();
        }

        private void InstantiateDependenciesObjects()
        {
            this.cliente = new Cliente
            {
                Id = 1,
                Email = "dummy@dummy.com"
            };
            this.pessoa = new Pessoa
            {
                Id = 1,
                Nome = "Lucas",
                DataNascimento = DateTime.Now,
                Endereco = "Rua teste",
                Cidade = new Cidade() { Id = 1630 },
                Bairro = "Centro",
                Cpf = "333444566" + DateTime.Now.Minute,
                Numero = 1,
                Cliente = this.cliente
            };
        }

        private void InsertDependenciesInTheDatabase()
        {
            this.clientes.Insert(this.cliente);
        }

        private void InstantiateDependenciesDAO()
        {
            this.pessoas = new PessoaDAO(new Connection());
            this.clientes = new ClienteDAO(new Connection());
        }

        private void ClearDatabase()
        {
            this.pessoas.DeleteAll();
            this.clientes.DeleteAll();       
        }

        [TestMethod()]
        public void InsertTest()
        {
            Assert.AreEqual(this.pessoa.Id, this.pessoas.Insert(this.pessoa));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            this.pessoas.Insert(this.pessoa);
            this.pessoa.Nome = "Teste";

            Assert.IsTrue(this.pessoas.Update(this.pessoa));
        }

        [TestMethod()]
        public void DeleteTest()
        {
            this.pessoas.Insert(this.pessoa);

            Assert.IsTrue(this.pessoas.Delete(this.pessoa));
        }

        [TestMethod()]
        public void SearchTest()
        {
            this.pessoas.Insert(this.pessoa);

            Assert.AreEqual(this.pessoa.Id, this.pessoas.Search(this.pessoa).Id);
        }

        [TestMethod()]
        public void ListAllTest()
        {
            this.pessoas.Insert(this.pessoa);
            this.pessoa.Id = 2;
            this.pessoa.Cpf = "2342";
            this.pessoas.Insert(this.pessoa);

            Assert.AreEqual(2, this.pessoas.ListAll().Count);
        }

        [TestMethod()]
        public void ListAllClientesTest()
        {
            this.pessoas.Insert(this.pessoa);

            Assert.AreEqual(1, this.pessoas.ListAllClientes().Count);
        }

        [TestMethod()]
        public void ListAllClientesFailsIfItsNotAClienteTest()
        {
            this.pessoa.Cliente = null;
            this.pessoas.Insert(this.pessoa);

            Assert.AreEqual(0, this.pessoas.ListAllClientes().Count);
        }
    }
}
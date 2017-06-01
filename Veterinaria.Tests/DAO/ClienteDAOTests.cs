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
    public class ClienteDAOTests
    {
        private Cliente cliente;
        private ClienteDAO clientes;

        [TestInitialize()]
        public void Startup()
        {
            this.InstantiateDependenciesObjects();
            this.InstantiateDependenciesDAO();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.ClearDatabase();
            this.DisposeDependenciesDAO();
        }

        private void InstantiateDependenciesDAO()
        {
            this.clientes = new ClienteDAO(new Connection()); 
        }

        private void InstantiateDependenciesObjects()
        {
            this.cliente = new Cliente
            {
                Id = 1,
                Email = "dummy@dummy.com"
            };
        }

        private void DisposeDependenciesDAO()
        {
            this.clientes.Dispose();
        }

        private void ClearDatabase()
        {
            this.clientes.DeleteAll();
        }

        //[TestMethod()]
        //public void InsertTest()
        //{
        //    Assert.AreEqual(this.cliente.Id, this.clientes.Insert(cliente));
        //}

        [TestMethod()]
        public void InsertFailsTest()
        {
            this.clientes.Insert(this.cliente);
            Assert.AreEqual(-1, this.clientes.Insert(cliente));

        }

        [TestMethod()]
        public void DeleteTest()
        {
            this.clientes.Insert(this.cliente);
            Assert.IsTrue(this.clientes.Delete(this.cliente));
        }

        [TestMethod()]
        public void DeleteFailsTest()
        {
            Assert.IsFalse(this.clientes.Delete(this.cliente));
        }

        [TestMethod()]
        public void SearchTest()
        {
            this.clientes.Insert(this.cliente);
            Assert.AreEqual(this.cliente.Id, this.clientes.Search(this.cliente).Id);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            this.clientes.Insert(this.cliente);
            this.cliente.Email = "dummy@dummy2.com";

            Assert.IsTrue(this.clientes.Update(this.cliente));
        }

        [TestMethod()]
        public void ListAllTest()
        {
            this.clientes.Insert(this.cliente);
            this.cliente.Id = 2;
            this.clientes.Insert(this.cliente);

            Assert.AreEqual(2, this.clientes.ListAll().Count);
        }
    }
}
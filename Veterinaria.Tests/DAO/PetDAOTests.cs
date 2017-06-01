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
    public class PetDAOTests
    {
        private Pet pet;
        private PetDAO pets;

        private Cliente cliente;
        private ClienteDAO clientes;

        private Pessoa pessoa;
        private PessoaDAO pessoas;

        [TestInitialize()]
        public void Startup()
        {
            this.InstantiateDependenciesDAO();
            this.InstantiateDependenciesObjects();
            this.InsertDependenciesInTheDatabase();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.ClearDatabase();
            this.DisposeDependenciesDAO();
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
            this.pet = new Pet
            {
                Id = 1,
                Nome = "Doguilas",
                DataNascimento = DateTime.Today,
                Raca = "Labrador",
                Sexo = 1,
                Tipo = 1,
                Cliente = this.pessoa
            };
        }

        private void InstantiateDependenciesDAO()
        {
            this.clientes = new ClienteDAO(new Connection());
            this.pets = new PetDAO(new Connection());
            this.pessoas = new PessoaDAO(new Connection());
        }

        private void DisposeDependenciesDAO()
        {
            this.clientes.Dispose();
            this.pets.Dispose();
            this.pessoas.Dispose();
        }

        private void InsertDependenciesInTheDatabase()
        {
            this.clientes.Insert(this.cliente);
            this.pessoas.Insert(this.pessoa);
        }

        private void ClearDatabase()
        {
            this.clientes.DeleteAll();
            this.pets.DeleteAll();
            this.pessoas.DeleteAll();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            this.pets.Insert(this.pet);
            this.pet.Nome = "Dudedog";

            Assert.IsTrue(this.pets.Update(this.pet));
        }

        [TestMethod()]
        public void DeleteTest()
        {
            this.pets.Insert(pet);

            Assert.IsTrue(this.pets.Delete(this.pet));
        }

        [TestMethod()]
        public void SearchTest()
        {
            this.pets.Insert(pet);

            Assert.AreEqual(this.pet.Id, this.pets.Search(this.pet).Id);
        }

        [TestMethod()]
        public void ListAllTest()
        {
            this.pets.Insert(this.pet);
            this.pet.Id = 2;
            this.pets.Insert(this.pet);

            Assert.AreEqual(2, this.pets.ListAll().Count);
        }
    }
}
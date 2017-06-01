using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veterinaria.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Models;
using Veterinaria.DAO;
using System.Web.Mvc;
using System.Collections.ObjectModel;

namespace Veterinaria.Controllers.Tests
{
    [TestClass()]
    public class PetControllerTests
    {
        private PetController controller;

        private Pet pet;
        private PetDAO pets;

        private Cliente cliente;
        private ClienteDAO clientes;

        private PessoaDAO pessoas;
        private Pessoa pessoa;

        private FormCollection form;

        [TestInitialize()]
        public void Startup()
        {
            this.InstantiateDependenciesDAO();
            this.InstantiateDependenciesObjects();
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
            this.clientes.Dispose();
            this.pets.Dispose();
            this.pessoas.Dispose();
        }

        private void ClearDatabase()
        {
            this.clientes.DeleteAll();
            this.pets.DeleteAll();
            this.pessoas.DeleteAll();
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
            this.form = new FormCollection();
            this.form.Add("id", this.pet.Id.ToString());
            this.form.Add("nome", this.pet.Nome);
            this.form.Add("data_nascimento", this.pet.DataNascimento.ToString());
            this.form.Add("raca", this.pet.Raca);
            this.form.Add("sexo", this.pet.Sexo.ToString());
            this.form.Add("tipo", this.pet.Tipo.ToString());
            this.form.Add("idcliente", this.pet.Cliente.Id.ToString());
        }

        private void InsertDependenciesInTheDatabase()
        {
            this.clientes.Insert(this.cliente);
            this.pessoas.Insert(this.pessoa);
        }

        private void InstantiateDependenciesDAO()
        {
            this.controller = new PetController();
            this.pets = new PetDAO(new Connection());
            this.clientes = new ClienteDAO(new Connection());
            this.pessoas = new PessoaDAO(new Connection());
        }

        [TestMethod()]
        public void IndexTest()
        {
            this.pets.Insert(this.pet);
            this.pet.Id = 2;
            this.pets.Insert(this.pet);

            var result = this.controller.Index() as ViewResult;
            var petsCollection = (List<Pet>)result.Model;

            Assert.AreEqual(2, petsCollection.Count);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            this.pets.Insert(this.pet);

            var result = this.controller.Details(1) as ViewResult;
            Pet petInstance = (Pet)result.Model;

            Assert.AreEqual("Doguilas", petInstance.Nome);
        }

        [TestMethod()]
        public void CreateTest()
        {
            this.controller.Create(this.form);
            Assert.AreEqual(1, this.pets.ListAll().Count);
        }

        [TestMethod()]
        public void EditTest()
        {
            this.pets.Insert(this.pet);

            this.form["nome"] = "Sabbah";
            this.controller.Edit(this.pet.Id, this.form);

            Assert.AreEqual("Sabbah", this.pets.Search(this.pet).Nome);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            this.pets.Insert(this.pet);

            this.controller.Delete(this.pet.Id);

            Assert.IsNull(this.pets.Search(this.pet));
        }
    }
}
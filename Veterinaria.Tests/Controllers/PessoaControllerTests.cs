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
    public class PessoaControllerTests
    {
        PessoaController controller;

        Pessoa pessoa;
        PessoaDAO pessoas;

        FormCollection form;

        [TestInitialize()]
        public void Startup()
        {
            this.InstantiateDependenciesObjects();
            this.InstantiateDependenciesDAO();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.DisposeDependenciesDAO();
            this.ClearDatabase();
        }

        private void DisposeDependenciesDAO()
        {
            this.pessoas.Dispose();
        }

        private void InstantiateDependenciesObjects()
        {
            this.pessoa = new Pessoa
            {
                Id = 1,
                Nome = "Lucas",
                DataNascimento = DateTime.Now,
                Endereco = "Rua teste",
                Cidade = new Cidade() { Id = 1630 },
                Bairro = "Centro",
                Cpf = "333444566" + DateTime.Now.Minute,
                Numero = 1
            };
            this.form = new FormCollection();
            this.form.Add("idpessoa", this.pessoa.Id.ToString());
            this.form.Add("nome", this.pessoa.Nome);
            this.form.Add("endereco", this.pessoa.Endereco);
            this.form.Add("Idcidade", this.pessoa.Cidade.Id.ToString());
            this.form.Add("Cpf", this.pessoa.Cpf);
            this.form.Add("data_nascimento", this.pessoa.DataNascimento.ToString());
            this.form.Add("bairro", this.pessoa.Bairro);
            this.form.Add("telefone_fixo", "");
            this.form.Add("telefone_celular", "");
            this.form.Add("complemento", "");
            this.form.Add("numero", null);
            this.form.Add("numerocontrato", null);
            this.form.Add("salario", null);
            this.form.Add("data_admissao", null);
            this.form.Add("setor", null);
            this.form.Add("email", "teste@teste.com");
        }

        private void InstantiateDependenciesDAO()
        {
            this.controller = new PessoaController();
            this.pessoas = new PessoaDAO(new Connection());
        }

        private void ClearDatabase()
        {
            this.pessoas.DeleteAll();
        }

        [TestMethod()]
        public void IndexTest()
        { 
            this.pessoas.Insert(this.pessoa);
            this.pessoa.Id = 2;
            this.pessoa.Cpf = "12";
            this.pessoas.Insert(this.pessoa);

            var result = this.controller.Index() as ViewResult;
            var pessoasCollection = (List<Pessoa>)result.Model;

            Assert.AreEqual(2, pessoasCollection.Count);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            this.pessoas.Insert(this.pessoa);

            var result = this.controller.Details(this.pessoa.Id) as ViewResult;
            var pessoaInstance = (Pessoa)result.Model;

            Assert.AreEqual("Lucas", pessoaInstance.Nome);
        }

        [TestMethod()]
        public void CreateTest()
        {
            this.controller.Create(this.form);
            Assert.AreEqual(1, this.pessoas.ListAll().Count);
        }

        [TestMethod()]
        public void EditTest()
        {
            this.pessoas.Insert(this.pessoa);

            this.form["Nome"] = "Teste";
            this.controller.Edit(this.form);

            Assert.AreEqual("Teste", this.pessoas.Search(this.pessoa).Nome);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            this.pessoas.Insert(this.pessoa);

            this.controller.Delete(this.pessoa.Id);

            Assert.IsNull(this.pessoas.Search(this.pessoa));
        }
    }
}
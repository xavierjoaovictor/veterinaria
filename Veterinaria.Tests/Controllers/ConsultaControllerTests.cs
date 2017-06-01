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
using Veterinaria.Models.Enums;

namespace Veterinaria.Controllers.Tests
{
    [TestClass()]
    public class ConsultaControllerTests
    {
        private ConsultaController controller;

        private Consulta consulta;
        private ConsultaDAO consultas;

        private Cliente cliente;
        private ClienteDAO clientes;

        private Diagnostico diagnostico;
        private DiagnosticoDAO diagnosticos;

        private Pessoa pessoaVeterinario;
        private Pessoa pessoaAtendente;
        private Pessoa pessoaCliente;
        private PessoaDAO pessoas;

        private Funcionario veterinario;
        private Funcionario atendente;
        private FuncionarioDAO funcionarios;

        private Pet pet;
        private PetDAO pets;

        private FormCollection form;

        [TestInitialize()]
        public void Startup()
        {
            this.InstantiateDependenciesObjects();
            this.InstantitateDependenciesDAO();
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
            this.pessoaCliente = new Pessoa()
            {
                Id = 3,
                Nome = "Lucas Cliente",
                DataNascimento = DateTime.Now,
                Endereco = "Rua Teste",
                Cidade = new Cidade() { Id = 1630 },
                Bairro = "Centro",
                Cpf = "000884511" + DateTime.Now.Minute,
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
                Cliente = this.pessoaCliente
            };
            this.veterinario = new Funcionario
            {
                Id = 1,
                NumeroContrato = "",
                Salario = 0,
                DataAdmisao = DateTime.Now,
                Funcao = FuncaoFuncionario.Veterinario,
                NumeroCRMV = "1"

            };
            this.pessoaVeterinario = new Pessoa()
            {
                Id = 1,
                Nome = "Lucas",
                DataNascimento = DateTime.Now,
                Endereco = "Rua teste",
                Cidade = new Cidade() { Id = 1630 },
                Bairro = "Centro",
                Cpf = "313444566" + DateTime.Now.Minute,
                Numero = 1,
                Funcionario = this.veterinario
            };
            this.atendente = new Funcionario
            {
                Id = 2,
                NumeroContrato = "",
                Salario = 0,
                DataAdmisao = DateTime.Now,
                Funcao = FuncaoFuncionario.Atendente,
                NumeroCRMV = "2"
            };
            this.pessoaAtendente = new Pessoa()
            {
                Id = 2,
                Nome = "Lucas",
                DataNascimento = DateTime.Now,
                Endereco = "Rua teste",
                Cidade = new Cidade() { Id = 1630 },
                Bairro = "Centro",
                Cpf = "033444566" + DateTime.Now.Minute,
                Numero = 1,
                Funcionario = this.atendente
            };
            this.diagnostico = new Diagnostico
            {
                Id = 1,
                Posologia = "Some Posologia",
                Medicacao = "Cabulozex 2000",
                Descricao = "Sleep like a baby."
            };
            this.consulta = new Consulta
            {
                Id = 1,
                Data = DateTime.Today,
                Pet = this.pet,
                Veterinario = this.pessoaVeterinario,
                Atendente = this.pessoaAtendente,
                Diagnostico = this.diagnostico
            };
            this.controller = new ConsultaController();
            this.form = new FormCollection();
            this.form.Add("idconsulta", this.consulta.Id.ToString());
            this.form.Add("data", this.consulta.Data.ToString());
            this.form.Add("idpet", this.consulta.Pet.Id.ToString());
            this.form.Add("idveterinario", this.consulta.Veterinario.Id.ToString());
            this.form.Add("idatendente", this.consulta.Atendente.Id.ToString());
            this.form.Add("iddiagnostico", this.consulta.Diagnostico.Id.ToString());
            this.form.Add("posologia", this.consulta.Diagnostico.Posologia);
            this.form.Add("medicacao", this.consulta.Diagnostico.Medicacao);
            this.form.Add("descricao", this.consulta.Diagnostico.Descricao);
        }

        private void InstantitateDependenciesDAO()
        {
            this.consultas = new ConsultaDAO(new Connection());
            this.pets = new PetDAO(new Connection());
            this.clientes = new ClienteDAO(new Connection());
            this.funcionarios = new FuncionarioDAO(new Connection());
            this.pessoas = new PessoaDAO(new Connection());
            this.diagnosticos = new DiagnosticoDAO(new Connection());
        }

        private void InsertDependenciesInTheDatabase()
        {
            this.clientes.Insert(this.cliente);
            this.pessoas.Insert(this.pessoaCliente);
            this.pets.Insert(this.pet);
            this.funcionarios.Insert(this.veterinario);
            this.funcionarios.Insert(this.atendente);
            this.pessoas.Insert(this.pessoaVeterinario);
            this.pessoas.Insert(this.pessoaAtendente);
            this.diagnosticos.Insert(this.diagnostico);
        }

        private void DisposeDependenciesDAO()
        {
            this.pets.Dispose();
            this.clientes.Dispose();
            this.funcionarios.Dispose();
            this.consultas.Dispose();
            this.pessoas.Dispose();
            this.diagnosticos.Dispose();
        }

        private void ClearDatabase()
        {
            this.consultas.DeleteAll();
            this.pets.DeleteAll();
            this.clientes.DeleteAll();
            this.funcionarios.DeleteAll();
            this.pessoas.DeleteAll();
            this.diagnosticos.DeleteAll();
        }

        //[TestMethod()]
        //public void IndexTest()
        //{
        //    this.consultas.Insert(this.consulta);
        //    this.consulta.Id = 2;
        //    this.consultas.Insert(this.consulta);

        //    var result = this.controller.Index() as ViewResult;
        //    var ConsultasCollection = (List<Consulta>)result.Model;

        //    Assert.AreEqual(2, ConsultasCollection.Count());
        //}

        //[TestMethod()]
        //public void DetailsTest()
        //{
        //    this.consultas.Insert(this.consulta);

        //    var result = this.controller.Details(1) as ViewResult;
        //    Consulta consultaInstance = (Consulta)result.Model;

        //    Assert.AreEqual(DateTime.Today, consultaInstance.Data);
        //}

        //[TestMethod()]
        //public void CreateTest()
        //{
        //    this.controller.Create(this.form);
        //    Assert.AreEqual(1, this.consultas.ListAll().Count);
        //}

        //[TestMethod()]
        //public void EditTest()
        //{
        //    this.consultas.Insert(this.consulta);

        //    DateTime newDate = DateTime.Today.AddDays(+1);
        //    this.form["data"] = newDate.ToString();
        //    string stuff = this.form["data"];
        //    this.controller.Edit(this.consulta.Id, this.form);

        //    Assert.AreEqual(newDate, this.consultas.Search(this.consulta).Data);
        //}

        //[TestMethod()]
        //public void DeleteTest()
        //{
        //    this.consultas.Insert(this.consulta);

        //    this.controller.Delete(this.consulta.Id);

        //    Assert.IsNull(this.consultas.Search(this.consulta));
        //}
    }
}
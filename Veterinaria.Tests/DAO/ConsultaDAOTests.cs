using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veterinaria.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Models;
using Veterinaria.Models.Enums;

namespace Veterinaria.DAO.Tests
{
    [TestClass()]
    public class ConsultaDAOTests
    {
        private Pet pet;
        private PetDAO pets;

        private Cliente cliente;
        private ClienteDAO clientes;

        private Consulta consulta;
        private ConsultaDAO consultas;

        private Funcionario veterinario;
        private Funcionario atendente;
        private FuncionarioDAO funcionarios;

        private Pessoa pessoaAtendente;
        private Pessoa pessoaVeterinario;
        private Pessoa pessoaCliente;
        private PessoaDAO pessoas;

        private DiagnosticoDAO diagnosticos;
        private Diagnostico diagnostico;

        [TestInitialize()]
        public void Startup()
        {
            this.InstantiateDependenciesObjects();
            this.InstantiateDependenciesDAO();
            this.InsertDependenciesInTheDatabase();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            this.ClearDatabase();
            this.DisposeDependenciesDAO();
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

        private void InstantiateDependenciesDAO()
        {
            this.consultas = new ConsultaDAO(new Connection());
            this.pets = new PetDAO(new Connection());
            this.clientes = new ClienteDAO(new Connection());
            this.pessoas = new PessoaDAO(new Connection());
            this.funcionarios = new FuncionarioDAO(new Connection());
            this.diagnosticos = new DiagnosticoDAO(new Connection());
        }

        private void InstantiateDependenciesObjects()
        {
            this.cliente = new Cliente
            {
                Id = 1,
                Email = "dummy@dummy.com"
            };
            this.pessoaCliente = new Pessoa
            {
                Id = 3,
                Nome = "Lucas Boost",
                DataNascimento = DateTime.Now,
                Endereco = "Rua teste",
                Cidade = new Cidade() { Id = 1630 },
                Bairro = "Centro",
                Cpf = "123449926" + DateTime.Now.Minute,
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
                Cliente = new Pessoa() { Cliente = this.cliente } 
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
            this.pessoaVeterinario = new Pessoa
            {
                Id = 1,
                Nome = "Spice Old",
                DataNascimento = DateTime.Now,
                Endereco = "Rua teste",
                Cidade = new Cidade() { Id = 1630 },
                Bairro = "Centro",
                Cpf = "433444566" + DateTime.Now.Minute,
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
            this.pessoaAtendente = new Pessoa
            {
                Id = 2,
                Nome = "Rhank Jr.",
                DataNascimento = DateTime.Now,
                Endereco = "Rua teste",
                Cidade = new Cidade() { Id = 1630 },
                Bairro = "Centro",
                Cpf = "133444566" + DateTime.Now.Minute,
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
                Atendente = this.pessoaAtendente,
                Veterinario = this.pessoaVeterinario,
                Diagnostico = this.diagnostico
            };
        }   

        private void DisposeDependenciesDAO()
        {
            this.clientes.Dispose();
            this.pets.Dispose();
            this.pessoas.Dispose();
            this.consultas.Dispose();
            this.funcionarios.Dispose();
        }

        private void InsertDependenciesInTheDatabase()
        {
            this.clientes.Insert(this.cliente);
            this.pets.Insert(this.pet);
            this.funcionarios.Insert(this.veterinario);
            this.funcionarios.Insert(this.atendente);
            this.pessoas.Insert(this.pessoaAtendente);
            this.pessoas.Insert(this.pessoaVeterinario);
            this.pessoas.Insert(this.pessoaCliente);
            this.diagnosticos.Insert(this.diagnostico);
        }
        
        //[TestMethod()]
        //public void DeleteTest()
        //{
        //    this.consultas.Insert(this.consulta);
        //    Assert.IsTrue(this.consultas.Delete(this.consulta));
        //}
        
    }
}